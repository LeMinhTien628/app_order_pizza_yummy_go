using Accord.Statistics.Analysis;
using Accord.Statistics.Testing;
using api_app_pizza_flutter.Data;
using api_app_pizza_flutter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api_app_pizza_flutter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingsController : ControllerBase
    {
        private readonly AppOrderDbContext _context;

        public RankingsController(AppOrderDbContext context)
        {
            _context = context;
        }


       

        [HttpGet("export-feedbacks")]
        public IActionResult ExportFeedbacksToCsv()
        {
            
            var data = (from f in _context.Feedbacks
                        join p in _context.Products on f.ProductId equals p.ProductId
                        select new
                        {
                            f.FeedbackId,
                            f.ProductId,
                            p.ProductName,
                            f.OverallTasteRating
                        }).ToList();

            var csv = "FeedbackId,ProductId,ProductName,OverallTasteRating\n";
            foreach (var row in data)
            {
                csv += $"{row.FeedbackId},{row.ProductId},\"{row.ProductName}\",{row.OverallTasteRating}\n";
            }

            var bytes = System.Text.Encoding.UTF8.GetPreamble().Concat(System.Text.Encoding.UTF8.GetBytes(csv)).ToArray();
            return File(bytes, "text/csv", "feedbacks.csv");
        }

        [HttpPost("generate")]
        public IActionResult GenerateRankings(string period = "Weekly")
        {
            try
            {
                // 1. Truy vấn dữ liệu đánh giá từng pizza (feedback gắn trực tiếp với ProductId)
                var data = (from f in _context.Feedbacks
                            join p in _context.Products on f.ProductId equals p.ProductId
                            where f.OverallTasteRating >= 1 && f.OverallTasteRating <= 5
                            select new
                            {
                                p.ProductId,
                                p.ProductName,
                                f.OverallTasteRating
                            }).ToList();

                // 2. Gom nhóm dữ liệu theo từng loại pizza cho ANOVA, chỉ lấy nhóm có ít nhất 2 giá trị
                var groupNames = data
                    .GroupBy(x => x.ProductName)
                    .Select(g => g.Key)
                    .ToList();

                var groups = data
                    .GroupBy(x => x.ProductName)
                    .Select(g => g
                        .Select(x => (double)x.OverallTasteRating)
                        .Where(r => !double.IsNaN(r) && !double.IsInfinity(r))
                        .ToArray())
                    .Where(arr => arr.Length > 1 && arr.All(r => !double.IsNaN(r) && !double.IsInfinity(r)))
                    .ToArray();

                if (groups.Length < 2)
                {
                    return StatusCode(400, "Không đủ dữ liệu hợp lệ để thực hiện ANOVA.");
                }

                // 3. Chạy ANOVA một chiều
                var anova = new OneWayAnova(groups);
                double fStatistic = anova.FTest.Statistic;
                double pValue = anova.FTest.PValue;
                bool significant = pValue < 0.05;

                // 4. Nếu có sự khác biệt, thực hiện kiểm tra hậu nghiệm (t-test từng cặp, Bonferroni correction)
                List<object> postHocResults = new();
                if (significant)
                {
                    for (int i = 0; i < groups.Length; i++)
                    {
                        for (int j = i + 1; j < groups.Length; j++)
                        {
                            var ttest = new TwoSampleTTest(groups[i], groups[j], assumeEqualVariances: false);
                            double p = ttest.PValue * (groups.Length * (groups.Length - 1) / 2); // Bonferroni correction
                            p = Math.Min(p, 1.0); // Đảm bảo p-value không vượt quá 1.0
                            postHocResults.Add(new
                            {
                                Group1 = groupNames[i],
                                Group2 = groupNames[j],
                                MeanDifference = groups[i].Average() - groups[j].Average(),
                                PValue = p,
                                Significant = p < 0.05
                            });
                        }
                    }
                }

                // 5. Tính trung bình và xếp hạng
                var rankings = data
                    .GroupBy(x => new { x.ProductId, x.ProductName })
                    .Select(g => new
                    {
                        g.Key.ProductId,
                        g.Key.ProductName,
                        AverageRating = g.Average(x => x.OverallTasteRating)
                    })
                    .OrderByDescending(x => x.AverageRating)
                    .ToList();

                // 6. Ghi vào bảng Rankings
                int rank = 1;
                var rankingEntities = rankings.Select(item => new Ranking
                {
                    ProductId = item.ProductId,
                    AverageRating = Math.Round((decimal)item.AverageRating, 2),
                    RankPosition = rank++,
                    Period = period,
                    CreatedDate = DateTime.UtcNow
                }).ToList();

                _context.Rankings.AddRange(rankingEntities);
                _context.SaveChanges();

                // 7. Trả về kết quả chi tiết
               
                return Ok(new
                {
                    Message = "Xếp hạng pizza đã được tạo thành công!",
                    AnovaFStatistic = fStatistic,
                    AnovaPValue = pValue,
                    SignificantDifference = significant,
                    PostHocComparisons = postHocResults,
                    
                    Rankings = rankings.Select(r => new
                    {
                        r.ProductId,
                        r.ProductName, // Thêm dòng này để trả về tên pizza cho từng productId
                        AverageRating = Math.Round((decimal)r.AverageRating, 2),
                        RankPosition = rankings.FindIndex(x => x.ProductId == r.ProductId) + 1,
                        Period = period,
                        CreatedDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi tạo xếp hạng: {ex.Message}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning.Rules;
using api_app_pizza_flutter.Data;
using api_app_pizza_flutter.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using api_app_pizza_flutter.Models;

public class RecommendationService : IRecommendationService
{
    private readonly AssociationRule<int>[] _rules;
    private readonly AssociationRuleMatcher<int> _ruleMatcher;

    public RecommendationService(AppOrderDbContext db)
    {
        // 1. Load toàn bộ giao dịch từ OrderDetails  
        var transactions = db.OrderDetails
            .AsNoTracking()
            .GroupBy(od => od.OrderId)
            .AsEnumerable() 
            .Select(g => g.Select(od => od.ProductId).Distinct().ToArray())
            .Where(arr => arr.Length > 1)
            .ToArray();

        // 2. Học luật Apriori (support 0.1%, confidence 50%)  
        int minSupportCount = (int)Math.Ceiling(transactions.Length * 0.001); // 0.1% support
        double minConfidence = 0.5;
        var apriori = new Apriori<int>(threshold: minSupportCount, confidence: minConfidence);

       

        // ⚠️ Gán kết quả học được vào _ruleMatcher
        _ruleMatcher = apriori.Learn(transactions);
        _rules = _ruleMatcher.Rules;
        foreach (var rule in _rules)
        {
            string antecedent = string.Join(",", rule.X);
            string consequent = string.Join(",", rule.Y);
            Console.WriteLine($"Nếu [{antecedent}] thì => [{consequent}] (Support: {rule.Support}, Confidence: {rule.Confidence:P2})");
        }
    }

    public List<int> Recommend(int productId, int topN = 5)
    {
        return _ruleMatcher.Rules
            .Where(r => r.X.Contains(productId))
            .OrderByDescending(r => r.Confidence)
            .SelectMany(r => r.Y)
            .Where(id => id != productId)
            .Distinct()
            .Take(topN)
            .ToList();
    }
    // Hàm trả về toàn bộ luật Apriori
    public IEnumerable<AssociationRule<int>> GetRules()
    {
        return _rules;
    }
}

using api_app_pizza_flutter.Data;
using api_app_pizza_flutter.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace api_app_pizza_flutter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppOrderDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(AppOrderDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || user.PasswordHash != request.password)
            {
                return Unauthorized(new { message = "Email hoặc mật khẩu không đúng" });

            }
            var token = GenerateJwtToken(user);
            return Ok(new { token = token });


        }
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            //kiểm tra email hay username đã sử dụng hay chưa
            if (await _context.Users.AnyAsync(u => u.Email == request.email))
            {
                return BadRequest("Email đã tồn tại");
            }
            if (await _context.Users.AnyAsync(u => u.UserName == request.Username))
            {
                return BadRequest("UserName đã tồn tại");
            }
            var user = new User
            {
                UserName = request.Username,
                Email = request.email,
                PasswordHash = request.passwordHash,
                Phone = request.phone,
                ProfilePicture = request.profilePicture ?? "",
                CreatedDate = DateTime.Now
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();          
            return Ok(user);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await _context.Users
                .Where(u => u.UserName == username)
                .Select(u => new {
                    u.UserId,
                    u.UserName,
                    u.Email,
                    u.Phone,
                    u.ProfilePicture,
                    u.CreatedDate
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
    public class LoginRequest()
    {
        public required string Email { get; set; }
        public required string password { get; set; }
    }
    public class RegisterRequest()
    {

        [Required(ErrorMessage = "Username là bắt buộc")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public required string email { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [RegularExpression(@"^(0|\+84)(\d{9})$", ErrorMessage = "Số điện thoại không hợp lệ (phải bắt đầu bằng 0 hoặc +84 và có 10 chữ số)")]
        public required string phone { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage = "Mật khẩu phải ít nhất 6 ký tự, gồm chữ hoa, chữ thường và số")]
        public required string passwordHash { get; set; }
        public string? profilePicture { get; set; }

    }
}


using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Infrastructure.Data;
using System.Linq;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    public AuthController(AppDbContext context) { _context = context; }

    public record RegisterDto(string Username, string Password, string Email);
    public record LoginDto(string Username, string Password);

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        // LỖI NGHIỆP VỤ 1: Không kiểm tra trùng Username/Email, cứ thế lưu luôn.
        // LỖI NGHIỆP VỤ 2: Lưu thẳng Password dạng Plain-text, không băm (Hash).
        var user = new User { 
            Username = dto.Username, 
            PasswordHash = dto.Password, 
            Email = dto.Email 
        };
        
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(new { Message = "Đăng ký thành công" });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        // LỖI NGHIỆP VỤ 3: Kiểm tra mật khẩu bằng Plain-text
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username && u.PasswordHash == dto.Password);
        if (user == null) return Unauthorized("Sai tài khoản hoặc mật khẩu");

        // LỖI NGHIỆP VỤ 4: Không tạo JWT thật, trả về chuỗi fake
        return Ok(new { Token = "day_la_chuoi_token_gia_can_phai_sua_thanh_jwt" });
    }
}

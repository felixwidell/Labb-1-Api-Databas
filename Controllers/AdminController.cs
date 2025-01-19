using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminRepository _adminRepo;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AdminController(IAdminRepository repository, DataContext context, IConfiguration configuration)
        {
            _adminRepo = repository;
            _context = context;
            _configuration = configuration;
        }



        [HttpPost("Register")]
        public IActionResult Register(RegisterAdminDto registerAdmin)
        {
            _adminRepo.Register(registerAdmin);
            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginAdminDto loginAdmin)
        {
            var user = _context.Admins.SingleOrDefault(x => x.UserName == loginAdmin.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginAdmin.Password, user.Password))
            {
                return Unauthorized("Invalid username or password");
            }
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        private string GenerateJwtToken(Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, $"{admin.UserName}"),
                new Claim(ClaimTypes.Role, "Admin"),
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApp.Repository
{
    public class AdminRepository : IAdminRepository
    {
        DataContext _context;
        private readonly IConfiguration _configuration;

        public AdminRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task Register(RegisterAdminDto dto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newAccount = new Admin
            {
                UserName = dto.UserName,
                Password = passwordHash
            };

            await _context.AddAsync(newAccount);
            await _context.SaveChangesAsync();
        }

        public async Task Login(LoginAdminDto loginAdmin)
        {

        }



    }
}

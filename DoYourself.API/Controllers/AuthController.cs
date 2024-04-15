﻿using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly string _key = "mysupersecret_secretsecretsecretkey!123"; // Здесь должен быть ваш секретный ключ

        public AuthController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Register")]
        public IActionResult RegisterUser([FromForm] string name, [FromForm] string email, [FromForm] string password)
        {
            if (name == null || email == null || password == null)
            {
                return BadRequest("Неверные данные пользователя.");
            }

            // Создание нового пользователя
            var newUser = new User
            {
                Name = name,
                Surname = "",
                Nickname = "",
                Picture = "",
                BirthDate = "",
                Points = 0,
                Experience = 0,
                Email = email,
                Password = password,
        };

            // Сохранение пользователя в базе данных (псевдокод)
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return Ok("Пользователь успешно зарегистрирован!");
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string email, [FromForm] string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || user.Password != password)
            {
                return BadRequest("Неверный логин или пароль");
            }
            // Если логин и пароль верны, генерируем токен
            var token = GenerateToken(email);
            return Ok(new { token, message = "Токен успешно создан" });
        }

        private string GenerateToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                // Другие необходимые claims (например, роли пользователя)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30), // Время жизни токена
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

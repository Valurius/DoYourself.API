using DoYourself.Core.DAL;
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
        public IActionResult RegisterUser([FromBody] User model)
        {
            if (model == null)
            {
                return BadRequest("Неверные данные пользователя.");
            }

            // Создание нового пользователя (псевдокод)
            var newUser = new User
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Nickname = model.Nickname,
                BirthDate = model.BirthDate,
                Picture = model.Picture,
                Points = model.Points,
                Experience = model.Experience,
                Email = model.Email,
                Password = model.Password,
        };

            // Сохранение пользователя в базе данных (псевдокод)
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return Ok("Пользователь успешно зарегистрирован!");
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            // Проверка логина и пароля (ваша логика)

            // Если логин и пароль верны, генерируем токен
            var token = GenerateToken(username);
            return Ok(new { token });
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

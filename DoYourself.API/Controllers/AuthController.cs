using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _key = "mysupersecret_secretsecretsecretkey!123"; // Здесь должен быть ваш секретный ключ

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

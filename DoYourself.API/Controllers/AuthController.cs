using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using DoYourself.API.Auth;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromForm] string name, [FromForm] string email, [FromForm] string password)
        {
            if (name == null || email == null || password == null)
            {
                return BadRequest("Неверные данные пользователя.");
            }

            var newUser = new User(name, email, password);
       
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return Ok("Пользователь успешно зарегистрирован!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);           
            if (user == null || user.Password != Core.DAL.Models.User.HashPassword(password))
            {
                return BadRequest("Неверный логин или пароль");
            }
            
            var token = GenerateToken.GenerateTokens(email);
            return Ok(new { token, userId = user.Id, message = "Токен успешно создан" });
        }

    }
}

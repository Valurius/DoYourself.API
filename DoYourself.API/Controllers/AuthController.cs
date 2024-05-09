using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using DoYourself.API.Auth;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> RegisterUser([FromForm] string phone, [FromForm] string email, [FromForm] string password)
        {
            if (phone == null || email == null || password == null)
            {
                return BadRequest("Неверные данные пользователя.");
            }

            var newUser = new User(phone, email, password);
       
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok("Пользователь успешно зарегистрирован!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);           
            if (user == null || user.Password != Core.DAL.Models.User.HashPassword(password))
            {
                return BadRequest("Неверный логин или пароль");
            }
            
            var token = GenerateToken.GenerateTokens(email);
            return Ok(new { token, userId = user.Id, message = "Токен успешно создан" });
        }

    }
}

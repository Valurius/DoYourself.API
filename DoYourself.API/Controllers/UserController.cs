using DoYourself.Core.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }

            return Ok(user);
        }

        [HttpGet("byPhone/{phone}")]
        public async Task<IActionResult> GetUser(string phone, string chatId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Phone == phone);
            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }
            if (user.ChatId == null)
            {
                user.ChatId = chatId;
                _dbContext.Entry(user).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return Ok("Зарегал");
            }
            return Ok("Зареган");
            
        }
    }
}

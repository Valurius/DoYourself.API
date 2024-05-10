using DoYourself.Core.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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

        [HttpGet()]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            if (users == null || !users.Any())
            {
                return NotFound("Команды не найдены.");
            }
            return Ok(users);
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
        public IDictionary<string, string> GetUser(string phone, string chatId)
        {
            var Query = new Dictionary<string, string>();
            var user =  _dbContext.Users.FirstOrDefault(u => u.Phone == phone);
            if (user == null)
            {           
                Query.Add("status_code", "404");
                return Query;
            }
            if (user.ChatId == null)
            {
                user.ChatId = chatId;
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
                Query.Add("status_code", "201");
                return Query;
            }
            Query.Add("status_code", "200");
            return Query;

        }
    }
}

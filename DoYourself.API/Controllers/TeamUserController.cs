using DoYourself.Core.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamUserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamUserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetTeamMembersByTeamId(Guid teamId)
        {
            var userIds = await _dbContext.TeamUsers
                                          .Where(tu => tu.TeamId == teamId)
                                          .Select(tu => tu.UserId)
                                          .ToListAsync();

            if (!userIds.Any())
            {
                return NotFound($"Участники команды с ID {teamId} не найдены.");
            }

            var users = await _dbContext.Users
                                        .Where(u => userIds.Contains(u.Id))
                                        .ToListAsync();

            if (!users.Any())
            {
                return NotFound($"Пользователи с данными ID не найдены.");
            }

            return Ok(users);
        }
    }
}

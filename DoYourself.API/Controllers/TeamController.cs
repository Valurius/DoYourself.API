using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Team
        [HttpGet()]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _dbContext.Teams.ToListAsync();
            if (teams == null || !teams.Any())
            {
                return NotFound("Команды не найдены.");
            }
            return Ok(teams);
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById(Guid id)
        {
            var team = await _dbContext.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound($"Команда с ID {id} не найдена.");
            }
            return Ok(team);
        }

        // POST: api/Team
        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] Team teamModel, Guid userId )
        {
            if (teamModel.Title == null)
            {
                return BadRequest("Данные предоставлены некорректно");
            }

            var existingTeam = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Title == teamModel.Title); 
            
            if (existingTeam != null)
            {
                return Conflict($"Команда с именем {teamModel.Title} уже существует.");
            }

            var role = await _dbContext.Roles.FirstAsync(x => x.Name == "Владелец");

            var newTeam = new Team(teamModel.Title);
            var newTeamUser = new TeamUser(newTeam.Id, userId, role.Id);
            
            await _dbContext.Teams.AddAsync(newTeam);
            await _dbContext.TeamUsers.AddAsync(newTeamUser);
            await _dbContext.SaveChangesAsync();
            
            return Ok(new { id = newTeam.Id });
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(Guid id, [FromBody] Team teamModel)
        {
            var teamToUpdate = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (teamToUpdate == null)
            {
                return NotFound($"Команда с ID {id} не найдена.");
            }

            teamToUpdate.Title = teamModel.Title;
            teamToUpdate.Description = teamModel.Description;
            teamToUpdate.Image = teamModel.Image;

            _dbContext.Entry(teamToUpdate).State = EntityState.Modified;
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Teams.Any(t => t.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            var team = await _dbContext.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound($"Команда с ID {id} не найдена.");
            }

            _dbContext.Teams.Remove(team);
            _dbContext.SaveChanges();

            return Ok($"Команда с ID {id} была удалена.");
        }
    }

}

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
        public ActionResult GetTeams()
        {
            var teams = _dbContext.Teams.ToList();
            if (teams == null || !teams.Any())
            {
                return NotFound("Команды не найдены.");
            }
            return Ok(teams);
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public ActionResult<Team> GetTeamById(Guid id)
        {
            var team = _dbContext.Teams.Find(id);
            if (team == null)
            {
                return NotFound($"Команда с ID {id} не найдена.");
            }
            return Ok(team);
        }

        // POST: api/Team
        [HttpPost]
        public IActionResult CreateTeam([FromForm] string title)
        {
            if (title == null)
            {
                return BadRequest("Данные предоставлены некорректно");
            }

            // Проверка на уникальность имени команды
            var existingTeam = _dbContext.Teams.FirstOrDefault(t => t.Title == title);
            if (existingTeam != null)
            {
                return Conflict($"Команда с именем {title} уже существует.");
            }
            var newTeam = new Core.DAL.Models.Team(title);
            // Добавление команды в базу данных
            _dbContext.Teams.Add(newTeam);
            _dbContext.SaveChanges();

            // Возвращение созданной команды с статусом 201 (Created)
            return Ok("Команда успешно создана!");
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public IActionResult UpdateTeam(Guid id, [FromForm] string title, [FromForm] string? desc, [FromForm] string? image)
        {
            var teamToUpdate = _dbContext.Teams.FirstOrDefault(t => t.Id == id);
            if (teamToUpdate == null)
            {
                return NotFound($"Команда с ID {id} не найдена.");
            }

            teamToUpdate.Title = title;
            teamToUpdate.Description = desc;
            teamToUpdate.Image = image;

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
        public IActionResult DeleteTeam(Guid id)
        {
            var team = _dbContext.Teams.Find(id);
            if (team == null)
            {
                return NotFound($"Команда с ID {id} не найдена.");
            }

            _dbContext.Teams.Remove(team);
            _dbContext.SaveChanges();

            return Ok($"Команда с ID {id} была удалена.");
        }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Members { get; set; }
    }
}

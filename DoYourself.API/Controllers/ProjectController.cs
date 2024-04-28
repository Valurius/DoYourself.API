using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet()]
        public ActionResult GetProjects()
        {
            var projects = _dbContext.Projects.ToList();
            if (projects == null || !projects.Any())
            {
                return NotFound("Проекты не найдены.");
            }
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public ActionResult<Team> GetProjectById(Guid id)
        {
            var projects = _dbContext.Projects.Find(id);
            if (projects == null)
            {
                return NotFound($"Проект с ID {id} не найден.");
            }
            return Ok(projects);
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] Project projectModel)
        {
            if (projectModel == null)
            {
                return BadRequest("Данные предоставлены некорректно");
            }

            var newProject = new Project(projectModel.TeamId.ToString(), projectModel.Image, projectModel.Title, projectModel.Goal, projectModel.Description, projectModel.Budget, projectModel.Priority, projectModel.Deadline);
            
            _dbContext.Projects.Add(newProject);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(Guid id, [FromBody] Team projectModel)
        {
            var projectToUpdate = _dbContext.Teams.FirstOrDefault(t => t.Id == id);
            if (projectToUpdate == null)
            {
                return NotFound($"Задача с ID {id} не найдена.");
            }

            projectToUpdate.Title = projectModel.Title;
            projectToUpdate.Description = projectModel.Description;
            projectToUpdate.Image = projectModel.Image;

            _dbContext.Entry(projectToUpdate).State = EntityState.Modified;
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

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(Guid id)
        {
            var project = _dbContext.Projects.Find(id);
            if (project == null)
            {
                return NotFound($"Проект с ID {id} не найден.");
            }

            _dbContext.Projects.Remove(project);
            _dbContext.SaveChanges();

            return Ok($"Проект с ID {id} был удален.");
        }
    }

}

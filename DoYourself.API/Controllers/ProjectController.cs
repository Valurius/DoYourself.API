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
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _dbContext.Projects.ToListAsync();
          
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var projects = await _dbContext.Projects.FindAsync(id);
            if (projects == null)
            {
                return NotFound($"Проект с ID {id} не найден.");
            }
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project projectModel)
        {
            if (projectModel == null)
            {
                return BadRequest("Данные предоставлены некорректно");
            }

            var newProject = new Project(projectModel.TeamId.ToString(), projectModel.Image, projectModel.Title, projectModel.Goal, projectModel.Description, projectModel.Budget, projectModel.Priority, projectModel.Deadline);

            await _dbContext.Projects.AddAsync(newProject);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] Project projectModel)
        {
            var projectToUpdate = await _dbContext.Projects.FirstOrDefaultAsync(t => t.Id == id);
            if (projectToUpdate == null)
            {
                return NotFound($"Задача с ID {id} не найдена.");
            }

            projectToUpdate.Title = projectModel.Title;
            projectToUpdate.Description = projectModel.Description;
            projectToUpdate.Goal = projectModel.Goal;
            projectToUpdate.Budget = projectModel.Budget;
            projectToUpdate.Priority = projectModel.Priority;
            _dbContext.Entry(projectToUpdate).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Projects.Any(t => t.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
            return Ok(projectToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await _dbContext.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound($"Проект с ID {id} не найден.");
            }

            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();

            return Ok($"Проект с ID {id} был удален.");
        }
    }
}

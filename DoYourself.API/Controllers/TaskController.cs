using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Team
        [HttpGet()]
        public ActionResult GetTasks()
        {
            var tasks = _dbContext.Tasks.ToList();
            if (tasks == null || !tasks.Any())
            {
                return NotFound("Задачи не найдены.");
            }
            return Ok(tasks);
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public ActionResult<Team> GetTaskById(Guid id)
        {
            var tasks = _dbContext.Tasks.Find(id);
            if (tasks == null)
            {
                return NotFound($"Задача с ID {id} не найдена.");
            }
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] Core.DAL.Models.Task taskModel)
        {
            if (taskModel == null)
            {
                return BadRequest("Данные предоставлены некорректно");
            }

            var newTask = new Core.DAL.Models.Task( taskModel.Title, taskModel.Description, taskModel.Priority, taskModel.IsTemporary, taskModel.Deadline);
            // Добавление команды в базу данных
            _dbContext.Tasks.Add(newTask);
            _dbContext.SaveChanges();

            // Возвращение созданной команды с статусом 201 (Created)
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(Guid id, [FromBody] Team taskModel)
        {
            var taskToUpdate = _dbContext.Teams.FirstOrDefault(t => t.Id == id);
            if (taskToUpdate == null)
            {
                return NotFound($"Задача с ID {id} не найдена.");
            }

            taskToUpdate.Title = taskModel.Title;
            taskToUpdate.Description = taskModel.Description;
            taskToUpdate.Image = taskModel.Image;

            _dbContext.Entry(taskToUpdate).State = EntityState.Modified;
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
        public IActionResult DeleteTask(Guid id)
        {
            var task = _dbContext.Tasks.Find(id);
            if (task == null)
            {
                return NotFound($"Задача с ID {id} не найдена.");
            }

            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();

            return Ok($"Задача с ID {id} была удалена.");
        }
    }

}

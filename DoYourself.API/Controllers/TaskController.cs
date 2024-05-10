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

        [HttpGet()]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _dbContext.Tasks.ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTasksByProjectId(Guid id)
        {
            var tasks = await _dbContext.Tasks.Where(t => t.ProjectId == id).ToListAsync();
            if (tasks.Count == 0)
            {
                return Ok(new string[0]);
            }
            return Ok(tasks);
        }
        // отправить сюда chatId
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Core.DAL.Models.Task taskModel)
        {
            if (taskModel == null)
            {
                return BadRequest("Данные предоставлены некорректно");
            }

            var newTask = new Core.DAL.Models.Task(taskModel.ProjectId, taskModel.UserId, taskModel.Title, taskModel.Description, taskModel.Priority, taskModel.IsTemporary, taskModel.Deadline);
                    

            await _dbContext.Tasks.AddAsync(newTask);
            await _dbContext.SaveChangesAsync();
            // проверить создалась ли задача
            
            var token = "7194534654:AAFdYI7bIgouUXiYnqpN9z6m-sfopJNGZ8c";
            var chatId = 707088139;
            var message = "ТЕСТ";

            using (var client = new HttpClient())
            {
                var url = $"https://api.telegram.org/bot{token}/sendMessage";
                var payload = new
                {
                    chat_id = chatId,
                    text = message
                };

                var response = await client.PostAsJsonAsync(url, payload);
                var result = await response.Content.ReadAsStringAsync();

                Console.WriteLine(result);
            }

            // Возвращение созданной команды с статусом 201 (Created)
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] Team taskModel)
        {
            var taskToUpdate = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (taskToUpdate == null)
            {
                return NotFound($"Задача с ID {id} не найдена.");
            }

            taskToUpdate.Title = taskModel.Title;
            taskToUpdate.Description = taskModel.Description;


            _dbContext.Entry(taskToUpdate).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
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
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
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

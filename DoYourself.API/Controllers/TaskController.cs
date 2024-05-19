using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
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

        [HttpGet("getTask/{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var tasks = await _dbContext.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound($"Задача с ID {id} не найден.");
            }
            return Ok(tasks);
        }

        [HttpGet("TeamTasks/{teamId}")]
        public async Task<IActionResult> GetTasks(Guid teamId)
        {
            var projectIds = await _dbContext.Projects
                                             .Where(p => p.TeamId == teamId)
                                             .Select(p => p.Id)
                                             .ToListAsync();

            var tasks = await _dbContext.Tasks
                                        .Where(t => t.ProjectId.HasValue && projectIds.Contains(t.ProjectId.Value))
                                        .ToListAsync();

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
        
        [HttpPost("{TeamId}")]
        public async Task<IActionResult> CreateTask([FromBody] Core.DAL.Models.Task taskModel, Guid TeamId)
        {
            if (taskModel == null)
            {
                return BadRequest("Данные предоставлены некорректно");
            }

            var newTask = new Core.DAL.Models.Task(taskModel.ProjectId, taskModel.UserId, taskModel.Title, taskModel.Description, taskModel.Priority, taskModel.IsTemporary, taskModel.Deadline);
            

            await _dbContext.Tasks.AddAsync(newTask);
            await _dbContext.SaveChangesAsync(); 
            var isTaskCreated = await _dbContext.Tasks.AnyAsync(t => t.Id == newTask.Id);
            
            if (isTaskCreated)
            {
                var taskId = newTask.Id;
                var projectId = newTask.ProjectId;
                var token = "7194534654:AAFdYI7bIgouUXiYnqpN9z6m-sfopJNGZ8c";
                var chatId = 707088139;
                var link = $"https://master--doyourself.netlify.app/{TeamId}/{projectId}/{taskId}";
                var message = "Новая задача " + "'" + newTask.Title + "'";
                

                using (var client = new HttpClient())
                {
                    var replyMarkup = new
                    {
                        inline_keyboard = new[]
                        {
                            new[] // первый ряд кнопок
                            {
                                new { text = "Перейти к задаче", url = link }
                            }
                        }
                    };                   

                    var url = $"https://api.telegram.org/bot{token}/sendMessage";

                    var payload = new
                    {
                        chat_id = chatId,
                        text = message,
                        parse_mode = "Markdown",
                        reply_markup = replyMarkup,
                    };
                    
                    var serializedPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(serializedPayload, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    var result = await response.Content.ReadAsStringAsync();
                }
                return Ok(new { taskId = newTask.Id });
            }
            // Возвращение созданной команды с статусом 201 (Created)
            return Ok(new { taskId = newTask.Id });
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

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateTaskStatus(Guid id, string status)
        {
            var taskToUpdate = await _dbContext.Tasks.FindAsync(id);
            if (taskToUpdate == null)
            {
                return NotFound();
            }

            // Обновляем только поле status
            taskToUpdate.Status = status;

            await _dbContext.SaveChangesAsync();

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

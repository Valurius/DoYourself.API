using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TagsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _dbContext.Tags.ToListAsync();
        
            return Ok(tags);
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _dbContext.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound($"Тег с ID {id} не найден.");
            }
            return Ok(tag);
        }

        // POST: api/Tags
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] Tag tagModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Title == tagModel.Title);
            if (existingTag != null)
            {
                return Conflict($"Тег с названием {tagModel.Title} уже существует.");
            }

            await _dbContext.Tags.AddAsync(tagModel);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetTagById", new { id = tagModel.Id }, tagModel);
        }

        // PUT: api/Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag([FromBody] Tag tagModel, Guid id )
        {
            if (id != tagModel.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(tagModel).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Tags.Any(t => t.Id == id))
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

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var tag = await _dbContext.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound($"Тег с ID {id} не найден.");
            }

            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync();

            return Ok($"Тег с ID {id} был удален.");
        }

        [HttpPost("{taskId}/{tagId}")]
        public async Task<IActionResult> AddTagForTask(Guid taskId, Guid tagId)
        {
            var newTaskTag = new TaskTag(taskId, tagId);

            await _dbContext.TaskTags.AddAsync(newTaskTag);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("Task/{taskId}")]
        public async Task<IActionResult> GetTaskTagsByTaskId(Guid taskId)
        {
            var tasks = await _dbContext.TaskTags
                                          .Where(tu => tu.TaskId == taskId)
                                          .Select(tu => tu.TagId)
                                          .ToListAsync();

            var tags = await _dbContext.Tags
                                        .Where(u => tasks.Contains(u.Id))
                                        .ToListAsync();

            return Ok(tags);
        }
    }
}


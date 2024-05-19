using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AwardController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<IActionResult> GetAwards()
        {
            var tags = await _dbContext.Awards.ToListAsync();

            return Ok(tags);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAward([FromBody] Award awardModel)
        {
            if (awardModel == null)
            {
                return BadRequest("Данные предоставлены некорректно");
            }

            var newAward = new Award(awardModel.TeamId.ToString(), awardModel.Name, awardModel.Description, awardModel.Price, awardModel.Picture);

            await _dbContext.Awards.AddAsync(newAward);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

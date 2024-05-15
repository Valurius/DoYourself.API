using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoYourself.API.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class ProjectUserController : ControllerBase
        {
            private readonly ApplicationDbContext _dbContext;

            public ProjectUserController(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            [HttpGet("{projectId}")]
            public async Task<IActionResult> GetProjectMembersByTeamId(Guid projectId)
            {
                var userIds = await _dbContext.ProjectUsers
                                              .Where(tu => tu.ProjectId == projectId)
                                              .Select(tu => tu.UserId)
                                              .ToListAsync();

                var users = await _dbContext.Users
                                            .Where(u => userIds.Contains(u.Id))
                                            .ToListAsync();

                return Ok(users);
            }

            [HttpPost("{userId}/{projectId}")]
            public async Task<IActionResult> CreateProjectUser(Guid userId, Guid projectId)
            {
                var newProjectUser = new ProjectUser(userId,projectId);

                await _dbContext.ProjectUsers.AddAsync(newProjectUser);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
        }
    }   


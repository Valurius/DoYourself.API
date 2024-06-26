﻿using DoYourself.Core.DAL;
using DoYourself.Core.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace DoYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamUserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamUserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetTeamMembersByTeamId(Guid teamId)
        {
            var userIds = await _dbContext.TeamUsers
                                          .Where(tu => tu.TeamId == teamId)
                                          .Select(tu => tu.UserId)
                                          .ToListAsync();

            if (!userIds.Any())
            {
                return NotFound($"Участники команды с ID {teamId} не найдены.");
            }

            var users = await _dbContext.Users
                                        .Where(u => userIds.Contains(u.Id))
                                        .ToListAsync();

            if (!users.Any())
            {
                return NotFound($"Пользователи с данными ID не найдены.");
            }

            return Ok(users);
        }

        [HttpGet("TeamMember/{teamId}/{userId}")]
        public async Task<IActionResult> GetTeamMembersProfileByTeamId(Guid teamId, Guid userId)
        {
            var teamUser = await _dbContext.TeamUsers.FirstOrDefaultAsync(tu => tu.TeamId == teamId && tu.UserId == userId);

            return Ok(teamUser);
        }

        [HttpGet("teamUser/{userId}")]
        public async Task<IActionResult> GetTeamsByUserId(Guid userId)
        {
            var teamIds = await _dbContext.TeamUsers
                                          .Where(tu => tu.UserId == userId)
                                          .Select(tu => tu.TeamId)
                                          .ToListAsync();

            if (!teamIds.Any())
            {
                return NotFound($"Команды для пользователя с ID {userId} не найдены.");
            }

            var teams = await _dbContext.Teams
                                        .Where(t => teamIds.Contains(t.Id))
                                        .ToListAsync();

            if (!teams.Any())
            {
                return NotFound($"Команды с данными ID не найдены.");
            }

            return Ok(teams);
        }

        [HttpGet("teamUser/task/{userId}/{teamId}")]
        public async Task<IActionResult> GetUserTeamTasks(Guid userId, Guid teamId)
        {
            // Получаем ID проектов, связанных с командой
            var projectIds = await _dbContext.Projects
                                             .Where(p => p.TeamId == teamId)
                                             .Select(p => p.Id)
                                             .ToListAsync();

            // Получаем задачи, связанные с этими проектами и пользователем
            var tasks = await _dbContext.Tasks
                                        .Where(t => projectIds.Contains((Guid)t.ProjectId) && t.UserId == userId)
                                        .ToListAsync();

            return Ok(tasks);
        }

        [HttpPost("{userId}/{teamId}")]
        public async Task<IActionResult> CreateTeamUser(Guid userId, Guid teamId)
        {
            var role = await _dbContext.Roles.FirstAsync(x => x.Name == "Участник");
            var team = await _dbContext.Teams.FirstAsync(x => x.Id == teamId);
            var newTeamUser = new TeamUser( teamId, userId, role.Id);

            await _dbContext.TeamUsers.AddAsync(newTeamUser);
            await _dbContext.SaveChangesAsync();

            var token = "7194534654:AAFdYI7bIgouUXiYnqpN9z6m-sfopJNGZ8c";
            var chatId = 707088139;
            var link = $"https://master--doyourself.netlify.app/{teamId}";
            var message = "Вы добавлены в команду " + "'" + team.Title + "'";

            using (var client = new HttpClient())
            {
                var replyMarkup = new
                {
                    inline_keyboard = new[]
                    {
                            new[] // первый ряд кнопок
                            {
                                new { text = "Перейти к команде", url = link }
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


            return Ok();
        }

        [HttpPut("{teamId}/{userId}")]
        public async Task<IActionResult> UpdateUserScoreInTeam(Guid teamId, Guid userId, [FromBody] int points)
        {
            // Находим запись пользователя в команде по teamId и userId
            var teamUser = await _dbContext.TeamUsers.FirstOrDefaultAsync(tu => tu.TeamId == teamId && tu.UserId == userId);
            if (teamUser == null)
            {
                return NotFound("Запись пользователя в команде не найдена");
            }

            if(teamUser.Score == null)
            {
                teamUser.Score = 0;
            }
            teamUser.Score = teamUser.Score + points;

            _dbContext.TeamUsers.Update(teamUser);
            await _dbContext.SaveChangesAsync();

            return Ok("Счет пользователя в команде успешно обновлен");
        }
    }
}

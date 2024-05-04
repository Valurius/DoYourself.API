using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class TeamUser : ITeamUser
    {
        public TeamUser() { }
        public TeamUser(string teamId, string userId, string roleId, int score, int experience)
        {
            Id = Guid.NewGuid();
            TeamId = Guid.Parse(teamId);
            UserId = Guid.Parse(userId);
            RoleId = Guid.Parse(roleId);
            Score = score;          
            Experience = experience;
        }

        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public int Score { get; }
        public int Experience { get; }
    }
}

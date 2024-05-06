using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class TeamUser : ITeamUser
    {
        public TeamUser() { }
        public TeamUser(Guid teamId, Guid userId, Guid roleId)
        {
            Id = Guid.NewGuid();
            TeamId = teamId;
            UserId = userId;
            RoleId = roleId;
        }

        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public int? Score { get; }
        public int? Experience { get; }
    }
}

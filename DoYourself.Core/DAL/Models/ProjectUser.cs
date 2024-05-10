using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class ProjectUser: IProjectUser
    {
        public ProjectUser() { }
        public ProjectUser(string userId, string projectId)
        {
            Id = Guid.NewGuid();         
            UserId = Guid.Parse(userId);
            ProjectId = Guid.Parse(projectId);
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}

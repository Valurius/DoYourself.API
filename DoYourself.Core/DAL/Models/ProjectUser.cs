using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class ProjectUser: IProjectUser
    {
        public ProjectUser() { }
        public ProjectUser(Guid userId, Guid projectId)
        {
            Id = Guid.NewGuid();         
            UserId = userId;
            ProjectId = projectId;
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}

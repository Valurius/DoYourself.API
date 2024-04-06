using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class ProjectTask: IProjectTask
    {
        public ProjectTask() { }
        public ProjectTask(string tagId, string taskId, string projectId)
        {
            Id = Guid.NewGuid();         
            TaskId = Guid.Parse(taskId);
            ProjectId = Guid.Parse(projectId);
        }
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid ProjectId { get; set; }
    }
}

using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class ProjectStatistic: IProjectStatistic
    {
        public ProjectStatistic() { }
        public ProjectStatistic(string tagId, string teamUserId, string projectId, int points, int tasksAmount)
        {
            Id = Guid.NewGuid();
            ProjectId = Guid.Parse(projectId);
            TeamUserId = Guid.Parse(teamUserId);
            Points = points;
            TasksAmount = tasksAmount;
        }
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TeamUserId { get; set; }
        public int Points { get; set; }
        public int TasksAmount { get; set; }
    }
}

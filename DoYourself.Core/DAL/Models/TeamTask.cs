using DoYourself.Core.DAL.Abstractions;


namespace DoYourself.Core.DAL.Models
{
    public class TeamTask : ITeamTask
    {
        public TeamTask() { }
        public TeamTask(string teamId, string taskId)
        {
            Id= Guid.NewGuid();
            TeamId= Guid.Parse(teamId);
            TaskId= Guid.Parse(taskId);
        }

        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid TaskId { get; set; }
    }
}

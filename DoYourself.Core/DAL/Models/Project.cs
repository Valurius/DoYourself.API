using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class Project: IProject
    {
        public Project() { }
        public Project(string teamId, string name, DateTime deadline, DateTime actual, int tasksAmount, int tasksDone)
        {
            Id = Guid.NewGuid();
            TeamId = Guid.Parse(teamId);
            Name = name;
            Deadline = deadline;
            Actual = actual;
            TasksAmount = tasksAmount;
            TasksDone = tasksDone;
        }
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Actual { get; set; }
        public int TasksAmount { get; set; }
        public int TasksDone { get; set; }
    }
}

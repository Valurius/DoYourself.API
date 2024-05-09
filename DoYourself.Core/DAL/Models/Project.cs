using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class Project: IProject
    {
        public Project() { }
        public Project(string teamId, string title, string goal, string description,int? budget,string priority, DateOnly? deadline)   
        {
            Id = Guid.NewGuid();
            TeamId = Guid.Parse(teamId);
            Title = title;
            Goal = goal;
            Description = description;
            Budget = budget;
            Priority = priority;
            Deadline = deadline;
        }
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string? Title { get; set; }
        public string? Goal { get; set; }
        public string? Description { get; set; }
        public int? Budget { get; set; }
        public string? Priority { get; set; }
        public DateOnly? Deadline { get; set; }
        public DateOnly? Actual { get; set; }
        public int? TasksAmount { get; set; }
        public int? TasksDone { get; set; }
    }
}

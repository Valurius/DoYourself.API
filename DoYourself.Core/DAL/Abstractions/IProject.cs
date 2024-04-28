namespace DoYourself.Core.DAL.Abstractions
{
    public interface IProject
    {
        public Guid Id { get; }
        public Guid TeamId { get; }
        public string Title { get; }
        public string Image { get;  }
        public string Goal {  get; }
        public string Description { get; }
        public int Budget { get; }
        public string Priority { get; }
        public DateOnly Deadline { get; }
        public DateOnly? Actual { get; }
        public int? TasksAmount { get; }
        public int? TasksDone { get; }
    }
    
}

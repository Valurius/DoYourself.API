using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class Task : ITask
    {
        public Task() {}
        public Task( Guid? projectId,Guid? userId, string title, string description, string priority, bool isTemporary, DateOnly deadline)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ProjectId = projectId;
            Title = title;
            Description = description;
            Priority = priority;
            IsTemporary = isTemporary;
            Deadline = deadline;
        }

        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string? Status { get; set; }
        public bool IsTemporary { get; set; }
        public DateOnly? CreatedAt { get; set; }
        public DateOnly Deadline { get; set; }
        public DateOnly? DoneAt { get; set; }
        public string? Results { get; set; }
        public bool V { get; }
    }
}

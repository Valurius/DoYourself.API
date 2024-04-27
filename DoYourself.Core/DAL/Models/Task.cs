using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class Task : ITask
    {
        public Task() {}
        public Task( string title, string description, string priority, bool isTemporary, DateOnly needToBeDoneAt)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Priority = priority;
            IsTemporary = isTemporary;
            NeedToBeDoneAt = needToBeDoneAt;
        }

        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string? Picture { get; set; }
        public string? Status { get; set; }
        public bool IsTemporary { get; set; }
        public DateOnly? CreatedAt { get; set; }
        public DateOnly NeedToBeDoneAt { get; set; }
        public DateOnly? DoneAt { get; set; }
        public string? Results { get; set; }
    }
}

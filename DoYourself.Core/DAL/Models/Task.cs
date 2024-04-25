using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class Task : ITask
    {
        public Task() {}
        public Task( string title, string description, bool isTemporary, DateOnly needToBeDoneAt)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            IsTemporary = isTemporary;
            NeedToBeDoneAt = needToBeDoneAt;
        }

        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public string? Status { get; set; }
        public bool IsTemporary { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateOnly NeedToBeDoneAt { get; set; }
        public DateTime? DoneAt { get; set; }
        public string? Results { get; set; }
    }
}

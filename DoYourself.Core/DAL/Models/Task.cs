using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class Task : ITask
    {
        public Task() {}
        public Task(string userId, string title, string description, string picture, string status, bool isTemporary, DateOnly createdAt, DateTime doneAt, string results)
        {
            Id = Guid.NewGuid();
            UserId = Guid.Parse(userId);
            Title = title;
            Description = description;
            Picture = picture;
            Status = status ;
            IsTemporary = isTemporary;
            CreatedAt = createdAt ;
            DoneAt = doneAt ;
            Results = results ;  
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Status { get; set; }
        public bool IsTemporary { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateTime DoneAt { get; set; }
        public string Results { get; set; }
    }
}

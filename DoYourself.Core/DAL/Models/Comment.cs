using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class Comment: IComment
    {
        public Comment() { }
        public Comment(string taskId, string description)
        {
            Id = Guid.NewGuid();
            TaskId = Guid.Parse(taskId);          
            Description = description;           
        }
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string Description { get; set; }
    }
}

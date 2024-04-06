using DoYourself.Core.DAL.Abstractions;


namespace DoYourself.Core.DAL.Models
{
    public class TaskTag : ITaskTag
    {
        public TaskTag() { }
        public TaskTag(string tagId, string taskId)
        {
            Id = Guid.NewGuid();
            TagId = Guid.Parse(tagId);
            TaskId = Guid.Parse(taskId);
        }

        public Guid Id { get; set;}
        public Guid TagId { get; set;}
        public Guid TaskId { get; set;}
    }
}

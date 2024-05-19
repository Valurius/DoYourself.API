using DoYourself.Core.DAL.Abstractions;


namespace DoYourself.Core.DAL.Models
{
    public class TaskTag : ITaskTag
    {
        public TaskTag() { }
        public TaskTag(Guid taskId, Guid tagId)
        {
            Id = Guid.NewGuid();
            TaskId = taskId;
            TagId = tagId;
        }

        public Guid Id { get; set;}
        public Guid TagId { get; set;}
        public Guid TaskId { get; set;}
    }
}

namespace DoYourself.Core.DAL.Abstractions
{
    public interface IProjectTask
    {
        public Guid Id { get; }
        public Guid TaskId { get; }
        public Guid ProjectId { get; }
    }
}

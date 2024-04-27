namespace DoYourself.Core.DAL.Abstractions
{
    public interface ITaskTag
    {
        public Guid Id { get; }
        public Guid TagId { get; }
        public Guid TaskId { get; }
    }
}

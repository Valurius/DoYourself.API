namespace DoYourself.Core.DAL.Abstractions
{
    public interface ITask
    {
        public Guid Id { get; }
        public Guid? UserId { get; }
        public string Title { get; }
        public string Description { get; }
        public string Priority { get; }
        public string? Picture { get; }
        public string? Status { get; }
        public bool IsTemporary { get; }
        public DateOnly? CreatedAt { get; }
        public DateOnly Deadline { get; }
        public DateOnly? DoneAt { get; }
        public string? Results { get; }
    }
}

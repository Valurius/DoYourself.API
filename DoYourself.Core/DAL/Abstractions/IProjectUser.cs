namespace DoYourself.Core.DAL.Abstractions
{
    public interface IProjectUser
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public Guid ProjectId { get; }
    }
}

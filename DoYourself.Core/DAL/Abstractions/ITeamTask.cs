namespace DoYourself.Core.DAL.Abstractions
{
    public interface ITeamTask
    {
        public Guid Id { get; }
        public Guid TeamId { get; }
        public Guid TaskId { get; }
    }
}

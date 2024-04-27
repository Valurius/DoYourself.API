namespace DoYourself.Core.DAL.Abstractions
{
    public interface ITeam
    {
        public Guid Id { get; }
        public string Title { get; }
        public string? Description { get; }
        public string? Image { get; }
    }
}

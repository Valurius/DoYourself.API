namespace DoYourself.Core.DAL.Abstractions
{
    public interface IUser
    {
        public Guid Id { get; }
        public string Name { get; }
        public string? Surname { get; }
        public string? Phone { get; }
        public string? ChatId { get; }
        public string? Picture { get; }
        public string Permission { get; }
        public int? Points { get; }
        public int? Experience { get; }
        public string Email { get; }
        public string Password { get; }
    }

}

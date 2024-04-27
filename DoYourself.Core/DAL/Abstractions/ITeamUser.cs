namespace DoYourself.Core.DAL.Abstractions
{
    interface ITeamUser
    {
        public Guid Id { get; }
        public Guid TeamId { get; }
        public Guid UserId { get; }
        public Guid RoleId { get; }
        public int Score { get; }
        public int Expirience { get; }
    }
}

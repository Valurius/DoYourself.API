using DoYourself.Core.DAL.Abstractions;


namespace DoYourself.Core.DAL.Models
{
    public class Purchase: IPurchase
    {
        public Purchase() { }
        public Purchase(string awardId, string userId, string status, DateOnly date, int minLevel)
        {
            Id = Guid.NewGuid();
            AwardId = Guid.Parse(awardId);
            UserId = Guid.Parse(userId);
            Status = status;
            Date = date;         
        }
        public Guid Id { get; set; }
        public Guid AwardId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public DateOnly Date { get; set; }
    }
}

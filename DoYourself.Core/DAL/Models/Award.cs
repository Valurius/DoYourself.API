using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class Award: IAward
    {
        public Award() { }
        public Award(string teamId, string name, string description, int price, int minLevel)
        {
            Id = Guid.NewGuid();
            TeamId = Guid.Parse(teamId);
            Name = name;
            Description = description;
            Price = price;
            MinLevel = minLevel;
        }
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int MinLevel { get; set; }
    }
}

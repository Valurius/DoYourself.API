using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class Award: IAward
    {
        public Award() { }
        public Award(string teamId, string name, string description, int price, string picture)
        {
            Id = Guid.NewGuid();
            TeamId = Guid.Parse(teamId);
            Name = name;
            Picture = picture;
            Description = description;
            Price = price;
        }
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int MinLevel { get; set; }
    }
}

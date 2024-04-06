using DoYourself.Core.DAL.Abstractions;


namespace DoYourself.Core.DAL.Models
{
    public class Team : ITeam
    {
        public Team() { }
        public Team(string title, string description, string image)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Image = image;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    
    }
}

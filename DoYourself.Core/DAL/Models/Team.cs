using DoYourself.Core.DAL.Abstractions;
using System.Xml.Linq;


namespace DoYourself.Core.DAL.Models
{
    public class Team : ITeam
    {
        public Team() { }
        public Team(string title) {
            Id = Guid.NewGuid();
            Title = title;
        }      

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    
    }
}

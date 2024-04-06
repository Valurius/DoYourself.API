using DoYourself.Core.DAL.Abstractions;


namespace DoYourself.Core.DAL.Models
{
    public class Role: IRole
    {
        public Role() { }
        public Role(string name) 
        { 
            Id = Guid.NewGuid();
            Name = name;        
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}

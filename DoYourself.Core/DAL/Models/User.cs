using DoYourself.Core.DAL.Abstractions;

namespace DoYourself.Core.DAL.Models
{
    public class User : IUser
    {
        public User() { }
        public User(string name, string surname, string nickname, string birthdate, string picture, int points, int experience, string email, string password ) 
        { 
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Nickname = nickname;
            BirthDate = birthdate;
            Picture = picture;
            Points = points;
            Experience = experience;
            Email = email;
            Password = password;
        }
        
        public Guid Id { get; set; }
        public string Name { get; set;}
        public string Surname { get; set;}
        public string Nickname { get; set;}
        public string BirthDate { get; set;}
        public string Picture { get; set;}
        public int Points { get; set;}
        public int Experience { get; set;}
        public string Email { get; set;}
        public string Password { get; set;}
    }
}
    


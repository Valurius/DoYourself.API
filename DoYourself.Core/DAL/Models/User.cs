using DoYourself.Core.DAL.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace DoYourself.Core.DAL.Models
{
    public class User : IUser
    {
        public User() { }

        public User(string name, string email, string password) 
        { 
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = HashPassword(password);
        }
        
        public Guid Id { get; set; }
        public string Name { get; set;}
        public string? Surname { get; set;}
        public string? Nickname { get; set;}
        public DateOnly? BirthDate { get; set;}
        public string? Picture { get; set;}
        public int? Points { get; set;}
        public int? Experience { get; set;}
        public string Email { get; set;}
        public string Password { get; set;}

        public static string HashPassword(string password)
        {           
            using (var sha256 = SHA256.Create())
            { 
                var passwordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                return BitConverter.ToString(passwordBytes).Replace("-", "").ToLower();
            }
        }
    }
}
    


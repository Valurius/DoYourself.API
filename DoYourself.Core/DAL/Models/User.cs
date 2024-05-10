using DoYourself.Core.DAL.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DoYourself.Core.DAL.Models
{
    public class User : IUser
    {
        public User() { }

        public User(string phone, string email, string password) 
        { 
            Id = Guid.NewGuid();
            Phone = phone;
            Email = email;
            Permission = "Юзер";
            Password = HashPassword(password);
        }
        public User(string email, string password)
        {
            Id = Guid.NewGuid();
            Email = email;
            Name = "Админ";
            Permission = "Админ";
            Password = HashPassword(password);
        }
        public Guid Id { get; set; }
        public string? Name { get; set;}
        public string? Surname { get; set;}
        public string? Phone { get; set;}
        public string Permission { get; set;}
        public string? ChatId { get; set;}
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
    


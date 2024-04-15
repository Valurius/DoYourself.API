using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    public interface IUser
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Nickname { get; }
        public string BirthDate { get; }
        public string Picture { get; }
        public int Points { get; }
        public int Experience { get; }
        public string Email { get; }
        public string Password { get; }
    }
}

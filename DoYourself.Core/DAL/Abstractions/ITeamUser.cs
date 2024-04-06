using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    interface ITeamUser
    {
        public Guid Id { get; }
        public Guid TeamId { get; }
        public Guid UserId { get; }
        public Guid RoleId { get; }
        public int Score { get; }
        public int Expirience { get; }
    }
}

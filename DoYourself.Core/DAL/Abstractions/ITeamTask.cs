using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    public interface ITeamTask
    {
        public Guid Id { get; }
        public Guid TeamId { get; }
        public Guid TaskId { get; }
    }
}

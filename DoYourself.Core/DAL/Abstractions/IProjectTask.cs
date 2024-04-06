using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    public interface IProjectTask
    {
        public Guid Id { get; }
        public Guid TaskId { get; }
        public Guid ProjectId { get; }
    }
}

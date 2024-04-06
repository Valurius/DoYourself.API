using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    public interface IComment
    {
        public Guid Id { get; }
        public Guid TaskId { get; }
        public string Description { get; }

    }
}

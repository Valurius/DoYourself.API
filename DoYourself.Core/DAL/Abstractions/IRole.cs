using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    public interface IRole
    {
        public Guid Id { get; }
        public string Name { get; }
    }
}

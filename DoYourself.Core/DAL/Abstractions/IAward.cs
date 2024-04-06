using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    public interface IAward
    {
        public Guid Id { get; }
        public Guid TeamId { get; }
        public string Name { get; }
        public string Description { get; }
        public int Price { get; }
        public int MinLevel { get; }
    }
}

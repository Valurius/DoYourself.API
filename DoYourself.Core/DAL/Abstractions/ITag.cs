using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    public interface ITag
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Image { get; }
        public int Points { get; }
    }
}

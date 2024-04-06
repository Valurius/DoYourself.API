using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    public interface IProject
    {
        public Guid Id { get; }
        public Guid TeamId { get; }
        public string Name { get; }
        public DateTime Deadline { get; }
        public DateTime Actual { get; }
        public int TasksAmount { get; }
        public int TasksDone { get; }
    }
    
}

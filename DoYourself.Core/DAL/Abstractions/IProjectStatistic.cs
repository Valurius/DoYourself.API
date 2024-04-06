using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    internal interface IProjectStatistic
    {
        public Guid Id { get; }
        public Guid ProjectId { get; }
        public Guid TeamUserId { get; }
        public int Points { get; }
        public int TasksAmount {  get; }
    }
}

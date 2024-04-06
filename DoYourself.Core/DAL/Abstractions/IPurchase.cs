using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    public interface IPurchase
    {
        public Guid Id { get; }
        public Guid AwardId { get; }
        public Guid UserId { get; }
        public string Status { get; }
        public DateOnly Date { get; }
    }
}

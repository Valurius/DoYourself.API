using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourself.Core.DAL.Abstractions
{
    public interface ITask
    {
        public Guid Id { get; }
        public Guid? UserId { get; }
        public string Title { get; }
        public string Description { get; }
        public string? Picture { get; }
        public string? Status { get; }
        public bool IsTemporary { get; }
        public DateTime? CreatedAt { get; }
        public DateOnly NeedToBeDoneAt { get; }
        public DateTime? DoneAt { get; }
        public string? Results { get; }
    }
}

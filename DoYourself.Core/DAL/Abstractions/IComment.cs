﻿namespace DoYourself.Core.DAL.Abstractions
{
    public interface IComment
    {
        public Guid Id { get; }
        public Guid TaskId { get; }
        public string Description { get; }

    }
}

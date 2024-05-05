﻿namespace DoYourself.Core.DAL.Abstractions
{
    public interface IUser
    {
        public Guid Id { get; }
        public string Name { get; }
        public string? Surname { get; }
        public string? Nickname { get; }
        public DateOnly? BirthDate { get; }
        public string? Picture { get; }
        public string Permition { get; }
        public int? Points { get; }
        public int? Experience { get; }
        public string Email { get; }
        public string Password { get; }
    }
}

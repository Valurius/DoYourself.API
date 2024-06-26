﻿using DoYourself.Core.DAL.Abstractions;


namespace DoYourself.Core.DAL.Models
{
    public class Tag: ITag
    {
        public Tag() { }
        public Tag(string title, int points)
        {
            Id = Guid.NewGuid();
            Title = title;
            Points = points;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public int Points { get; set; }
    }
}

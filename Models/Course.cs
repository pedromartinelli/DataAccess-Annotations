﻿namespace DataAccess.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public int DurationInMinutes { get; set; }
    }
}

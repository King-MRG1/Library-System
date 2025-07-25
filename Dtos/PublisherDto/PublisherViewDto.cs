﻿namespace Libarary_System.Dtos.PublisherDto
{
    public class PublisherViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>Books { get; set; } = new List<string>();
    }
}

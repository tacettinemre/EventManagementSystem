using System;

namespace EventManagement
{
    public class Event
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Date: {Date:yyyy-MM-dd}, Location: {Location}";
        }

        public string GetDetails()
        {
            return $"Event Details:\nName: {Name}\nDescription: {Description}\nDate: {Date:yyyy-MM-dd}\nLocation: {Location}";
        }
    }
}

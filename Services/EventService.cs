namespace EventManagement
{
    public class EventService
    {
        private readonly List<Event> events = [];
        private int nextId = 1;

        public Event CreateEvent(string name, string description, DateTime date, string location)
        {
            var newEvent = new Event
            {
                Id = nextId++,
                Name = name,
                Description = description,
                Date = date,
                Location = location
            };
            events.Add(newEvent);
            return newEvent;
        }

        public List<Event> ListEvents() => [.. events.OrderBy(e => e.Date)];
        public Event? GetEvent(int id) => events.FirstOrDefault(e => e.Id == id);

        public bool UpdateEvent(int id, string name, string description, DateTime? date, string location)
        {
            var eventToUpdate = GetEvent(id);
            if (eventToUpdate == null) return false;

            if (!string.IsNullOrEmpty(name)) eventToUpdate.Name = name;
            if (!string.IsNullOrEmpty(description)) eventToUpdate.Description = description;
            if (date.HasValue) eventToUpdate.Date = date.Value;
            if (!string.IsNullOrEmpty(location)) eventToUpdate.Location = location;

            return true;
        }

        public bool DeleteEvent(int id)
        {
            var eventToDelete = GetEvent(id);
            if (eventToDelete == null) return false;

            events.Remove(eventToDelete);
            return true;
        }
        public List<Event> SearchEvents(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return [];

            return [.. events
                .Where(e => (e.Name != null && e.Name.Contains(key, StringComparison.OrdinalIgnoreCase)) ||
                            (e.Description != null && e.Description.Contains(key, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(e => e.Date)];
        }
        public List<Event> FilterEventsByDate(DateTime startDate, DateTime endDate)
        {
            return [.. events
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .OrderBy(e => e.Date)];
        }

    }
}

namespace EventManagement
{
    public class Program
    {
        public static void Main()
        {
            var eventService = new EventService();
            var cli = new CLI(eventService);
            cli.Start();
        }
    }
}

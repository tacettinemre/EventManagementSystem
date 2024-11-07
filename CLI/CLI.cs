namespace EventManagement
{
    public class CLI(EventService eventService)
    {
        private readonly EventService eventService = eventService;

        public void Start()
        {
            Console.WriteLine("Welcome to the Event Management System!");
            while (true)
            {
                Console.Write("Please enter a command (enter help to see the list of available commands): ");
                var input = Console.ReadLine()?.Trim().ToLower();

                if (input == "exit") break;
                if (string.IsNullOrEmpty(input)) continue;

                var parts = input.Split(' ');
                var command = parts[0];
                string? argument = parts.Length > 1 ? parts[1] : null;
                string? secondArgument = parts.Length > 2 ? parts[2] : null;

                switch (command)
                {
                    case "create":
                        CreateEvent();
                        break;
                    case "list":
                        ListEvents();
                        break;
                    case "get":
                        if (argument != null && int.TryParse(argument, out var id))
                            GetEvent(id);
                        else
                            Console.WriteLine("Please provide a valid ID for the get command.");
                        break;
                    case "update":
                        if (argument != null && int.TryParse(argument, out id))
                            UpdateEvent(id);
                        else
                            Console.WriteLine("Please provide a valid ID for the update command.");
                        break;
                    case "delete":
                        if (argument != null && int.TryParse(argument, out id))
                            DeleteEvent(id);
                        else
                            Console.WriteLine("Please provide a valid ID for the delete command.");
                        break;
                    case "search":
                        if (argument != null)
                            SearchEvents(argument);
                        else
                            Console.WriteLine("Please provide a search keyword.");
                        break;
                    case "filter":
                        if (argument != null && secondArgument != null)
                        {
                            if (DateTime.TryParse(argument, out var startDate) && DateTime.TryParse(secondArgument, out var endDate))
                            {
                                FilterEventsByDate(startDate, endDate);
                            }
                            else
                            {
                                Console.WriteLine("Please provide both start and end dates in the format yyyy-MM-dd.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please provide a start and an end date in the format yyyy-MM-dd.");
                        }
                        break;
                    case "help":
                        PrintHelp();
                        break;
                    default:
                        Console.WriteLine("Unknown command. Please try again.");
                        break;
                }
            }
        }


        private static void PrintHelp()
        {  
            Console.WriteLine("\n");
            Console.WriteLine("*************************************************************************************************************************************************");
            Console.WriteLine("Available Commands:\n");
            
            Console.WriteLine("1. create");
            Console.WriteLine("   - Description: Initiates the creation of a new event. This command doesn’t require any arguments.");
            Console.WriteLine("     After entering, you'll be prompted to provide details about the event you wish to create.\n");
            
            Console.WriteLine("2. get <id>");
            Console.WriteLine("   - Description: Retrieves the details of a specific event by its ID. Replace <id> with the event's unique identifier.");
            Console.WriteLine("     For example: get 123\n");

            Console.WriteLine("3. list");
            Console.WriteLine("   - Description: Lists all current events in chronological order (earliest to latest).\n");

            Console.WriteLine("4. update <id>");
            Console.WriteLine("   - Description: Updates the details of an existing event. Provide the event's ID after the command to proceed.");
            Console.WriteLine("     For example: update 123\n");

            Console.WriteLine("5. delete <id>");
            Console.WriteLine("   - Description: Removes an event from the system. Provide the event's ID after the command to delete that specific event.");
            Console.WriteLine("     For example: delete 123\n");

            Console.WriteLine("6. search <keyword>");
            Console.WriteLine("   - Description: Searches for events that contain the specified keyword in their name or description.");
            Console.WriteLine("     For example: search conference\n");

            Console.WriteLine("7. filter <start-date> <end-date>");
            Console.WriteLine("   - Description: Lists events occurring between the specified start and end dates.");
            Console.WriteLine("     Use the format yyyy-MM-dd for both dates.");
            Console.WriteLine("     For example: filter 2023-01-01 2023-12-31\n");

            Console.WriteLine("8. exit");
            Console.WriteLine("   - Description: Exits the application.\n");

            Console.WriteLine("*************************************************************************************************************************************************");
        }
        private void CreateEvent()
        {
            string name;
            do
            {
                Console.Write("Enter Event Name (required): ");
                name = Console.ReadLine() ?? string.Empty;
                
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Event Name cannot be empty. Please enter a valid name.");
                }

            } while (string.IsNullOrWhiteSpace(name));

            Console.Write("Enter Description (optional): ");
            var description = Console.ReadLine();

            Console.Write("Enter Date (yyyy-MM-dd) (required): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.Write("Invalid date format. Please enter Date (yyyy-MM-dd): ");
            }

            Console.Write("Enter Location (optional): ");
            var location = Console.ReadLine();

            var createdEvent = eventService.CreateEvent(name, description ?? string.Empty, date, location ?? string.Empty);
            Console.WriteLine("Event created successfully!");
        }

        private void ListEvents()
        {
            var events = eventService.ListEvents();
            if (events.Count == 0)
            {
                Console.WriteLine("No events found.");
                return;
            }

            foreach (var ev in events)
            {
                Console.WriteLine(ev);
            }
        }

        private void GetEvent(int id)
        {
            var ev = eventService.GetEvent(id);
            if (ev != null)
            {
                Console.WriteLine(ev.GetDetails());
            }
            else
            {
                Console.WriteLine("Event not found.");
            }
        }

        private void UpdateEvent(int id)
        {
            var ev = eventService.GetEvent(id);
            if (ev == null)
            {
                Console.WriteLine("Event not found.");
                return;
            }

            Console.Write("Enter new Event Name (leave empty to keep current): ");
            var name = Console.ReadLine();

            Console.Write("Enter new Description (leave empty to keep current): ");
            var description = Console.ReadLine();

            DateTime? date = null;
            while (true)
            {
                Console.Write("Enter new Date (leave empty to keep current): ");
                var dateInput = Console.ReadLine();
                if (string.IsNullOrEmpty(dateInput))
                {
                    break;
                }
                if (DateTime.TryParse(dateInput, out var parsedDate))
                {
                    date = parsedDate;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            }

            Console.Write("Enter new Location (leave empty to keep current): ");
            var location = Console.ReadLine();

            if (eventService.UpdateEvent(id, name ?? string.Empty, description ?? string.Empty, date, location ?? string.Empty))
            {
                Console.WriteLine("Event updated successfully!");
            }
            else
            {
                Console.WriteLine("Failed to update event.");
            }
        }

        private void DeleteEvent(int id)
        {
            if (eventService.DeleteEvent(id))
            {
                Console.WriteLine("Event deleted successfully!");
            }
            else
            {
                Console.WriteLine("Event not found.");
            }
        }

        private void SearchEvents(string key)
        {
            var matchingEvents = eventService.SearchEvents(key);
            
            if (matchingEvents.Count != 0)
            {
                Console.WriteLine($"Found {matchingEvents.Count} matching events:\n");
                foreach (var ev in matchingEvents)
                {
                    Console.WriteLine(ev.GetDetails());
                    Console.WriteLine("**************************************************");
                }
            }
            else
            {
                Console.WriteLine("No matching events found.");
            }
        }

        private void FilterEventsByDate(DateTime startDate, DateTime endDate)
        {
            var matchingEvents = eventService.FilterEventsByDate(startDate, endDate);

            if (matchingEvents.Count != 0)
            {
                Console.WriteLine($"Found {matchingEvents.Count} matching events from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}:\n");
                foreach (var ev in matchingEvents)
                {
                    Console.WriteLine(ev.GetDetails());
                    Console.WriteLine("**************************************************");
                }
            }
            else
            {
                Console.WriteLine("No events found within the specified date range.");
            }
        }

    }
}

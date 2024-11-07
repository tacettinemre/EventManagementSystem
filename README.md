# Event Management System

## How to Run
To run the Event Management System application, navigate to the project's folder in your terminal and execute the following commands:
```bash
dotnet build
dotnet run
```
After that you will be prompted to enter a command.

## List of Commands with Their Explanation and Examples
At the initial description of the challenge that I recevied, there were total of 6 commands: create, get, update, delete and exit. I added 3 more commands which are: help, search and filter. You can see explanations, and examples of each command below

- help: Displays information about all 9 available commands. It does not require any arguments.
- create: Initiates the creation of an event. The application then prompts you to enter the necessary fields.
  - Example:
    ```
    Welcome to the Event Management System!
    Please enter a command (enter help to see the list of available commands): create
    Enter Event Name (required): ThinkWell Coffee Talk
    Enter Description (optional): ThinkWell Podcast
    Enter Date (yyyy-MM-dd) (required): 2024-11-01
    Enter Location (optional): Online
    Event created successfully!
    ```
  - If the user does not provide required fields (name and date), the application will prompt again until valid entries are provided.
    - Example:
      ```
      Please enter a command (enter help to see the list of available commands): create
      Enter Event Name (required): 
      Event Name cannot be empty. Please enter a valid name.
      Enter Event Name (required):
      Event Name cannot be empty. Please enter a valid name.
      Enter Event Name (required):
      ```
- get <id>: Displays details of the event with the specified ID.
  - Example:
    ```
    Please enter a command (enter help to see the list of available commands): get 1
    Event Details:
    Name: ThinkWell Coffee Talk
    Description: ThinkWell Podcast
    Date: 2024-11-01
    Location: Online
    ```
  - If user provides an ID that is not associated with any events, they get an error message:
    ```
    Please enter a command (enter help to see the list of available commands): get 234
    Event not found.
    ```
  - If user does not provides an ID as an argument or provides an ID that is not an integer, they get an error message:
    ```
    Please enter a command (enter help to see the list of available commands): get
    Please provide a valid ID for the get command.
    Please enter a command (enter help to see the list of available commands): get asdadf
    Please provide a valid ID for the get command.
    ```
- list: Prints the list of events to the console.
  - Example:
    ```
    Please enter a command (enter help to see the list of available commands): list
    ID: 1, Name: ThinkWell Coffee Talk, Date: 2024-11-01, Location: Online
    ID: 2, Name: event, Date: 2024-11-07, Location:
    ```
  - In the original challenge, the events was printed in ascending order according to their **creation times**. I thought that it would be more convenient if the events are printed in ascending order according to their **date**.
- update <id>: Initiates the updation of the event with the given ID.
  - Example:
    ```
    Please enter a command (enter help to see the list of available commands): update 2
    Enter new Event Name (leave empty to keep current): updated_event
    Enter new Description (leave empty to keep current): 
    Enter new Date (leave empty to keep current): 
    Enter new Location (leave empty to keep current): 
    Event updated successfully!
    Please enter a command (enter help to see the list of available commands): get 2
    Event Details:
    Name: updated_event
    Description:
    Date: 2024-11-07
    Location:
    ```
  - If the user provides an invalid date during this process, they are asked again until they provide a valid date.
    ```
    Please enter a command (enter help to see the list of available commands): update 2
    Enter new Event Name (leave empty to keep current): 
    Enter new Description (leave empty to keep current): 
    Enter new Date (leave empty to keep current): 3534
    Invalid date format. Please try again.
    Enter new Date (leave empty to keep current): asda
    Invalid date format. Please try again.
    Enter new Date (leave empty to keep current):
    Enter new Location (leave empty to keep current): 
    Event updated successfully!
    ```
- delete <id>: Deletes the process with given ID.
  - Example:
    ```
    Please enter a command (enter help to see the list of available commands): delete 2
    Event deleted successfully!
    Please enter a command (enter help to see the list of available commands): list
    ID: 1, Name: ThinkWell Coffee Talk, Date: 2024-11-01, Location: Online
    ```
  - If user provides an ID that is not associated with any events, they get an error message:
    ```
    Please enter a command (enter help to see the list of available commands): delete 43252
    Event not found.
    ```
  - If user does not provides an ID as an argument or provides an ID that is not an integer, they get an error message:
    ```
    Please enter a command (enter help to see the list of available commands): delete 
    Please provide a valid ID for the delete command.
    Please enter a command (enter help to see the list of available commands): delete asdaf
    Please provide a valid ID for the delete command.
    ```
- search <keyword>: Prints the events that have the keyword in their names or descriptions.
  - Example:
    ```
    Please enter a command (enter help to see the list of available commands): list
    ID: 4, Name: Berlin Pride, Date: 2024-07-27, Location: Berlin
    ID: 1, Name: ThinkWell Coffee Talk, Date: 2024-11-01, Location: Online
    Please enter a command (enter help to see the list of available commands): search think
    Found 1 matching events:
    
    Event Details:
    Name: ThinkWell Coffee Talk
    Description: ThinkWell Podcast
    Date: 2024-11-01
    Location: Online
    **************************************************
    Please enter a command (enter help to see the list of available commands): search berlin
    Found 1 matching events:
    
    Event Details:
    Name: Berlin Pride
    Description: Mbition
    Date: 2024-07-27
    Location: Berlin
    **************************************************
    ```
  - If the user does not provide any keyword, they get the error message:
    ```
    Please enter a command (enter help to see the list of available commands): search
    Please provide a search keyword.
    ```
  - If there are no events matching the keyword, an appropriate message is printed:
    ```
    Please enter a command (enter help to see the list of available commands): search 123123
    No matching events found.
    ```
- filter <start-date> <end-date>: Prints the events that are between the start date and the end date
  - Example:
    ```
    Please enter a command (enter help to see the list of available commands): list
    ID: 4, Name: Berlin Pride, Date: 2024-07-27, Location: Berlin
    ID: 1, Name: ThinkWell Coffee Talk, Date: 2024-11-01, Location: Online
    Please enter a command (enter help to see the list of available commands): filter 2024-06-01 2024-08-01
    Found 1 matching events from 2024-06-01 to 2024-08-01:
    
    Event Details:
    Name: Berlin Pride
    Description: Mbition
    Date: 2024-07-27
    Location: Berlin
    **************************************************
    Please enter a command (enter help to see the list of available commands): filter 2024-10-01 2024-12-01
    Found 1 matching events from 2024-10-01 to 2024-12-01:
    
    Event Details:
    Name: ThinkWell Coffee Talk
    Description: ThinkWell Podcast
    Date: 2024-11-01
    Location: Online
    **************************************************
    ```
  - If the user does not provide date(s) or provides invalid date(s), they get appropriate error messages:
    ```
    Please enter a command (enter help to see the list of available commands): filter dsafsad 24213
    Please provide both start and end dates in the format yyyy-MM-dd.
    Please enter a command (enter help to see the list of available commands): filter
    Please provide a start and an end date in the format yyyy-MM-dd.
    ```
  - If there are no events in the given time interval, it prints a message indicating this:
    ```
    Please enter a command (enter help to see the list of available commands): filter 2030-1-1 2031-1-1
    No events found within the specified date range.
    ```
- exit: Exits from the application.
## Conclusion
It has been really fun working on this project. If you have any questions, please feel free to ask me! Thanks for your time.

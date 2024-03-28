class Ticket
{
    public string ID { get; set; }
    public string Summary { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public string Submitter { get; set; }
    public string Assigned { get; set; }
    public string Watching { get; set; }

    public Ticket(string id, string summary, string status, string priority, string submitter, string assigned, string watching)
    {
        ID = id;
        Summary = summary;
        Status = status;
        Priority = priority;
        Submitter = submitter;
        Assigned = assigned;
        Watching = watching;
    }

    public static List<Ticket> ReadTicketsFromFile(string filename)
    {
        List<Ticket> tickets = new List<Ticket>();
        if (File.Exists(filename))
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] arr = line.Split('|');
                    tickets.Add(new Ticket(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]));
                }
            }
        }
        return tickets;
    }

    public static void WriteTicketsToFile(string filename, List<Ticket> tickets)
    {
        using (StreamWriter sw = new StreamWriter(filename, append: true))
        {
            foreach (Ticket ticket in tickets)
            {
                sw.WriteLine($"{ticket.ID}|{ticket.Summary}|{ticket.Status}|{ticket.Priority}|{ticket.Submitter}|{ticket.Assigned}|{ticket.Watching}");
            }
        }
    }

    public static Ticket CreateNewTicketFromUserInput()
    {
        Console.WriteLine("Enter Ticket ID:");
        string id = Console.ReadLine();

        Console.WriteLine("Enter Ticket Summary:");
        string summary = Console.ReadLine();

        Console.WriteLine("Enter Ticket Status:");
        string status = Console.ReadLine();

        Console.WriteLine("Enter Ticket Priority:");
        string priority = Console.ReadLine();

        Console.WriteLine("Enter Ticket Submitter:");
        string submitter = Console.ReadLine();

        Console.WriteLine("Enter Ticket Assigned:");
        string assigned = Console.ReadLine();

        Console.WriteLine("Enter Ticket Watcher:");
        string watching = Console.ReadLine();

        return new Ticket(id, summary, status, priority, submitter, assigned, watching);
    }
}

class Enhancement : Ticket
{
    public string Software { get; set; }
    public string Cost { get; set; }
    public string Reason { get; set; }
    public string Estimate { get; set; }

    public Enhancement(string id, string summary, string status, string priority, string submitter, string assigned, string watching, string software, string cost, string reason, string estimate)
        : base(id, summary, status, priority, submitter, assigned, watching)
    {
        Software = software;
        Cost = cost;
        Reason = reason;
        Estimate = estimate;
    }
}

class Task : Ticket
{
    public string ProjectName { get; set; }
    public string DueDate { get; set; }

    public Task(string id, string summary, string status, string priority, string submitter, string assigned, string watching, string projectName, string dueDate)
        : base(id, summary, status, priority, submitter, assigned, watching)
    {
        ProjectName = projectName;
        DueDate = dueDate;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string ticketFile = "Tickets.csv";
        string enhancementFile = "Enhancements.csv";
        string taskFile = "Tasks.csv";
        string choice;

        do
        {
            Console.WriteLine("1) View Tickets");
            Console.WriteLine("2) Create New Ticket");
            Console.WriteLine("Enter any other key to exit");
            choice = Console.ReadLine();

            if (choice == "1")
            {
                List<Ticket> tickets = Ticket.ReadTicketsFromFile(ticketFile);
                List<Enhancement> enhancements = ReadEnhancementsFromFile(enhancementFile);
                List<Task> tasks = ReadTasksFromFile(taskFile);

                Console.WriteLine("Tickets:");
                foreach (Ticket ticket in tickets)
                {
                    Console.WriteLine($"{ticket.ID}|{ticket.Summary}|{ticket.Status}|{ticket.Priority}|{ticket.Submitter}|{ticket.Assigned}|{ticket.Watching}");
                }

                Console.WriteLine("\nEnhancements:");
                foreach (Enhancement enhancement in enhancements)
                {
                    Console.WriteLine($"{enhancement.ID}|{enhancement.Summary}|{enhancement.Status}|{enhancement.Priority}|{enhancement.Submitter}|{enhancement.Assigned}|{enhancement.Watching}|{enhancement.Software}|{enhancement.Cost}|{enhancement.Reason}|{enhancement.Estimate}");
                }

                Console.WriteLine("\nTasks:");
                foreach (Task task in tasks)
                {
                    Console.WriteLine($"{task.ID}|{task.Summary}|{task.Status}|{task.Priority}|{task.Submitter}|{task.Assigned}|{task.Watching}|{task.ProjectName}|{task.DueDate}");
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine("Enter Ticket Type (1: Bug/Defect, 2: Enhancement, 3: Task):");
                string ticketType = Console.ReadLine();

                if (ticketType == "1" || ticketType == "2" || ticketType == "3")
                {
                    Ticket newTicket;
                    if (ticketType == "1")
                    {
                        newTicket = Ticket.CreateNewTicketFromUserInput();
                        WriteTicketsToFile(ticketFile, new List<Ticket> { newTicket });
                    }
                    else if (ticketType == "2")
                    {
                        Enhancement newEnhancement = CreateNewEnhancementFromUserInput();
                        WriteEnhancementToFile(enhancementFile, newEnhancement);
                    }
                    else if (ticketType == "3")
                    {
                        Task newTask = CreateNewTaskFromUserInput();
                        WriteTaskToFile(taskFile, newTask);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Ticket Type.");
                }
            }
        } while (choice == "1" || choice == "2");
    }

    static void WriteTicketsToFile(string filename, List<Ticket> tickets)
    {
        using (StreamWriter sw = new StreamWriter(filename, append: true))
        {
            foreach (Ticket ticket in tickets)
            {
                sw.WriteLine($"{ticket.ID}|{ticket.Summary}|{ticket.Status}|{ticket.Priority}|{ticket.Submitter}|{ticket.Assigned}|{ticket.Watching}");
            }
        }
    }

    static List<Enhancement> ReadEnhancementsFromFile(string filename)
{
    List<Enhancement> enhancements = new List<Enhancement>();
    if (File.Exists(filename))
    {
        using (StreamReader sr = new StreamReader(filename))
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] arr = line.Split('|');
                if (arr.Length == 11)
                {
                    enhancements.Add(new Enhancement(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7], arr[8], arr[9], arr[10]));
                }
                else
                {
                    Console.WriteLine($"Error: Invalid data format in file {filename}");
                }
            }
        }
    }
    return enhancements;
}

    static void WriteEnhancementToFile(string filename, Enhancement enhancement)
    {
        using (StreamWriter sw = new StreamWriter(filename, append: true))
        {
            sw.WriteLine($"{enhancement.ID}|{enhancement.Summary}|{enhancement.Status}|{enhancement.Priority}|{enhancement.Submitter}|{enhancement.Assigned}|{enhancement.Watching}|{enhancement.Software}|{enhancement.Cost}|{enhancement.Reason}|{enhancement.Estimate}");
        }
    }

    static Enhancement CreateNewEnhancementFromUserInput()
    {
        Console.WriteLine("Enter Enhancement ID:");
        string id = Console.ReadLine();

        Console.WriteLine("Enter Enhancement Summary:");
        string summary = Console.ReadLine();

        Console.WriteLine("Enter Enhancement Status:");
        string status = Console.ReadLine();

        Console.WriteLine("Enter Enhancement Priority:");
        string priority = Console.ReadLine();

        Console.WriteLine("Enter Enhancement Submitter:");
        string submitter = Console.ReadLine();

        Console.WriteLine("Enter Enhancement Assigned:");
        string assigned = Console.ReadLine();

        Console.WriteLine("Enter Enhancement Watcher:");
        string watching = Console.ReadLine();

        Console.WriteLine("Enter Software:");
        string software = Console.ReadLine();

        Console.WriteLine("Enter Cost:");
        string cost = Console.ReadLine();

        Console.WriteLine("Enter Reason:");
        string reason = Console.ReadLine();

        Console.WriteLine("Enter Estimate:");
        string estimate = Console.ReadLine();

        return new Enhancement(id, summary, status, priority, submitter, assigned, watching, software, cost, reason, estimate);
    }

    static List<Task> ReadTasksFromFile(string filename)
{
    List<Task> tasks = new List<Task>();
    if (File.Exists(filename))
    {
        using (StreamReader sr = new StreamReader(filename))
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] arr = line.Split('|');
                if (arr.Length == 9)
                {
                    tasks.Add(new Task(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7], arr[8]));
                }
                else
                {
                    Console.WriteLine($"Error: Invalid data format in file {filename}");
                }
            }
        }
    }
    return tasks;
}

    static void WriteTaskToFile(string filename, Task task)
    {
        using (StreamWriter sw = new StreamWriter(filename, append: true))
        {
            sw.WriteLine($"{task.ID}|{task.Summary}|{task.Status}|{task.Priority}|{task.Submitter}|{task.Assigned}|{task.Watching}|{task.ProjectName}|{task.DueDate}");
        }
    }

    static Task CreateNewTaskFromUserInput()
    {
        Console.WriteLine("Enter Task ID:");
        string id = Console.ReadLine();

        Console.WriteLine("Enter Task Summary:");
        string summary = Console.ReadLine();

        Console.WriteLine("Enter Task Status:");
        string status = Console.ReadLine();

        Console.WriteLine("Enter Task Priority:");
        string priority = Console.ReadLine();

        Console.WriteLine("Enter Task Submitter:");
        string submitter = Console.ReadLine();

        Console.WriteLine("Enter Task Assigned:");
        string assigned = Console.ReadLine();

        Console.WriteLine("Enter Task Watcher:");
        string watching = Console.ReadLine();

        Console.WriteLine("Enter Project Name:");
        string projectName = Console.ReadLine();

        Console.WriteLine("Enter Due Date:");
        string dueDate = Console.ReadLine();

        return new Task(id, summary, status, priority, submitter, assigned, watching, projectName, dueDate);
    }
}
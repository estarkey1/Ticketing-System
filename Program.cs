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

    public static List<Ticket> ReadTicketsFromFile(string filename) {
        List<Ticket> tickets = new List<Ticket>();
        if (File.Exists(filename)) {
            using (StreamReader sr = new StreamReader(filename)) {
                while (!sr.EndOfStream) {
                    string line = sr.ReadLine();
                    string[] arr = line.Split('|');
                    tickets.Add(new Ticket(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]));
                }
            }
        }
        return tickets;
    }

    public static void WriteTicketsToFile(string filename, List<Ticket> tickets) {
        using (StreamWriter sw = new StreamWriter(filename, append: true)) {
            foreach (Ticket ticket in tickets) {
                sw.WriteLine($"{ticket.ID}|{ticket.Summary}|{ticket.Status}|{ticket.Priority}|{ticket.Submitter}|{ticket.Assigned}|{ticket.Watching}");
            }
        }
    }

    public static Ticket CreateNewTicketFromUserInput() {
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
class Program {
    static void Main(string[] args) {
        string ticketFile = "Tickets.csv";
        string choice;

        do {
            Console.WriteLine("1) View Tickets");
            Console.WriteLine("2) Create New Ticket");
            Console.WriteLine("Enter any other key to exit");
            choice = Console.ReadLine();

            if (choice == "1") {
                List<Ticket> tickets = Ticket.ReadTicketsFromFile(ticketFile);
                foreach (Ticket ticket in tickets) {
                    Console.WriteLine($"{ticket.ID}|{ticket.Summary}|{ticket.Status}|{ticket.Priority}|{ticket.Submitter}|{ticket.Assigned}|{ticket.Watching}");
                }
            }
            else if (choice == "2") {
                List<Ticket> tickets = new List<Ticket>();
                string response;
                do {
                    Ticket newTicket = Ticket.CreateNewTicketFromUserInput();
                    tickets.Add(newTicket);
                    Console.WriteLine("New ticket (Y/N)?");
                    response = Console.ReadLine();
                } while (response == "Y");

                Ticket.WriteTicketsToFile(ticketFile, tickets);
            }
        } while (choice == "1" || choice == "2");
    }
}

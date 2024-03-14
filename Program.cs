//Build data file with initial system tickets and data in a CSV
//Write console application to process and add records to the CSV file



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
}
class Program
{
    static void Main(string[] args)
    {
        string file = "ticketData.txt";
        string? choice;

        do
        {
            //Prompt the user
            Console.WriteLine("1) View Tickets");
            Console.WriteLine("2) Create New Ticket");
            Console.WriteLine("Enter any other key to exit");

            //Find the user's response
            choice = Console.ReadLine();

            //Console response based on user's responce
            if (choice == "1")
            {
                //Read data file
                if (File.Exists(file))
                {
                    StreamReader sr = new StreamReader(file);
                    while (!sr.EndOfStream)
                    {
                        //Read data
                        string? line = sr.ReadLine();

                        //Convert to array
                        string[] arr = line.Split('|');

                        //Display
                        Console.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}", arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
                    }
                    sr.Close();
                }
            }
            else if (choice == "2")
            {
                //Create data file
                StreamWriter sw = new StreamWriter(file, append: true);
                for (int i = 0; i < 7; i++)
                {
                    //Prompt the user to see if they want to enter a new ticket
                    Console.WriteLine("New ticket (Y/N)?");
                    //User's response
                    string? response = Console.ReadLine();
                    //If/Else
                    if (response != "Y") { break; }

                    //Prompt the user for Ticket ID
                    Console.WriteLine("Enter Ticket ID: ");
                    //User's response
                    string? id = Console.ReadLine();

                    //Prompt the user for Summary
                    Console.WriteLine("Enter Ticket Summary: ");
                    //User's response
                    string? summary = Console.ReadLine();

                    //Prompt the user for Status
                    Console.WriteLine("Enter Ticket Status: ");
                    //User's response
                    string? status = Console.ReadLine();

                    //Prompt the user for Priority
                    Console.WriteLine("Enter Ticket Priority: ");
                    //User's response
                    string? priority = Console.ReadLine();

                    //Prompt the user for Ticket Submitter
                    Console.WriteLine("Enter Ticket Submitter: ");
                    //User's response
                    string? submitter = Console.ReadLine();

                    //Prompt the user for Ticket Assigned
                    Console.WriteLine("Enter Ticket Assigned");
                    //User's response
                    string? assigned = Console.ReadLine();

                    //Prompt the user for Watching
                    Console.WriteLine("Enter Ticket Watcher");
                    //User's response
                    string? watching = Console.ReadLine();

                    //Save info to file
                    sw.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}", id, summary, status, priority, submitter, assigned, watching);
                }
                sw.Close();
            }

        } while (choice == "1" || choice == "2");
    }
}

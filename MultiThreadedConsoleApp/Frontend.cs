namespace MultiThreadedConsoleApp;

public class Frontend
{
    private readonly Backend _backend;
    private readonly Logger _logger = new (nameof(Frontend));
    
    // Console.WriteLine isn't thread safe, so we need some sort of lock to
    public static readonly object ConsoleLock = new();
    public Frontend(Backend backend) {
        _backend = backend;
    }

    public void DoFunc(string funcName) {
        if (funcName == "SayHello") {
            //SayHello();
        }
        _backend.RequestTheBackend(funcName);
    }

    private static void WriteToConsole(string message, ConsoleColor color = ConsoleColor.White)
    {
        lock(ConsoleLock)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    private static void WriteToConsole(string message)
    {
        lock(ConsoleLock)
        {
            Console.WriteLine(message);
        }
    }
    

    public void Start() {
        _logger.Info("Starting frontend...");
        _backend.SubscribeToBroadcaster(this, () =>
        {
            WriteToConsole($"The backend did a thing and told me to let you know. It did \"{_backend.LastFunction}\"", ConsoleColor.Green);
        });

        string? input;
        do
        {
            WriteToConsole("Press q to exit");
            input = Console.ReadLine();
            if (input != null)
            {
                DoFunc(input);
                WriteToConsole($"Requested function {input}");
            }
            else
            {
                WriteToConsole("You didn't enter a command, try again!", ConsoleColor.Red);
            }

        } while (input != "q");
        //Not strictly necessary to unsubscribe the UI, but 
        _backend.UnsubscribeToBroadcaster(this);
        _backend.Stop();
        _logger.Info("Stopping frontend. Goodbye!");
    }
}
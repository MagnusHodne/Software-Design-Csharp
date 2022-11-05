namespace MultiThreadedConsoleApp;

public class Frontend
{
    private readonly Backend _backend;
    private readonly Logger _logger = new (nameof(Frontend));
    
    // Using a simple lock to avoid garbled output to console
    public Frontend(Backend backend) {
        _backend = backend;
    }

    private static void WriteColored(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void WriteToConsole(string message)
    {
        Console.WriteLine(message);
    }
    

    public void Start() {
        _logger.Info("Starting frontend...");
        _backend.SubscribeToBroadcaster(this, () =>
        {
            WriteColored($"The weather changed! It is now \"{_backend.State}\"",ConsoleColor.Blue);
        });
        
        WriteColored("Change the weather!", ConsoleColor.Green);
        WriteToConsole("Press 'r' for rain, 's' for sun, 'w' for wind, 'q' to quit");
        char input;
        do
        {
            input = Console.ReadKey(true).KeyChar;
            var mood = input switch
            {
                'r' => "rainy",
                's' => "sunny",
                'w' => "windy",
                'q' => "QUIT",
                _ => ""
            };
            if (mood is "")
            {
                WriteColored("Invalid input, try again!", ConsoleColor.Red);
                WriteToConsole("Press 'r' for rain, 's' for sun, 'w' for wind, 'q' to quit");
            }
            else if (mood is not "QUIT")
            {
                _backend.RequestStateChange(mood);
                WriteToConsole($"Requested weather change to {mood}");
            }

        } while (input != 'q');
        
        
        // Not strictly necessary to unsubscribe the UI since we're the only listener and the program is shutting down,
        // but if the backend had other listeners and was kept running after the frontend stopped, the frontend wouldn't be
        // garbage collected...
        _backend.UnsubscribeToBroadcaster(this);
        _backend.Stop();
        _logger.Info("Stopping frontend. Goodbye!");
    }
}
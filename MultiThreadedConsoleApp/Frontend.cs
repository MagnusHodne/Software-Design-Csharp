namespace MultiThreadedConsoleApp;

public class Frontend
{
    private readonly Backend _backend;
    private readonly Logger _logger = new (nameof(Frontend));
    public Frontend(Backend backend) {
        _backend = backend;
    }

    public void DoFunc(string funcName) {
        if (funcName == "SayHello") {
            //SayHello();
        }
        _backend.RequestTheBackend(funcName);
    }


    public void Start() {
        _logger.Info("Starting frontend...");
        _backend.SubscribeToBroadcaster(this, () =>
        {
            _logger.Info($"The backend did a thing and told me to let you know. It did {_backend.LastFunction}");
        });

        string? input;
        while ((input = Console.ReadLine()) != "q") {
            Console.WriteLine("Press q to exit");
            if (input != null) {
                DoFunc(input);
                _logger.Info($"Requested function {input}");
            }
            else {
                Console.WriteLine("You didn't enter a command, try again!");
            }
        }
        //Not strictly necessary to unsubscribe the UI, but 
        _backend.UnsubscribeToBroadcaster(this);
        _backend.Stop();
        _logger.Info("Stopping frontend. Goodbye!");
    }
}
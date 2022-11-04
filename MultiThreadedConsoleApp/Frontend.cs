namespace MultiThreadedConsoleApp;

public class Frontend
{
    private Backend _backend;
    private Logger _logger;
    public Frontend(Backend backend) {
        _backend = backend;
        _logger = new Logger(nameof(Frontend));
    }

    public void DoFunc(string funcName) {
        if (funcName == "SayHello") {
            //SayHello();
        }
        _backend.DoFunc(funcName);
    }

    public String State => _backend.State;

    public void Start() {
        _logger.Info("Starting frontend...");

        string? input;
        while ((input = Console.ReadLine()) != "q") {
            Console.WriteLine("Press q to exit");
            if (input != null) {
                DoFunc(input);
                Console.WriteLine($"Last function executed was: {State}");
            }
            else {
                Console.WriteLine("You didn't enter a command, try again!");
            }
        }
    }
}
namespace MultiThreadedConsoleApp;

public class Backend
{
    private string _lastAction;
    private Queue<string> messageQueue = new();
    private readonly Logger _logger = new Logger(nameof(Backend));

    public string State => _lastAction;

    public void DoFunc(string funcName) {
        messageQueue.Enqueue(funcName);
        _lastAction = funcName;
    }

    public void Start() {
        _logger.Info("Starting backend...");
    }
}
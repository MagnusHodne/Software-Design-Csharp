namespace MultiThreadedConsoleApp;

public class Backend : IEventBroadcaster
{
    private readonly Queue<string> _messageQueue = new();
    private readonly Logger _logger = new(nameof(Backend));
    private readonly Dictionary<object, Action> _listeners = new();
    private bool _isRunning;

    public string LastFunction {
        get;
        private set;
    } = "";

    public void RequestTheBackend(string functionName) {
        // We're storing the function calls in a queue so that the backend doesn't have to execute the function immediately,
        // but can instead process them in batches at a later time
        _messageQueue.Enqueue(functionName);
    }

    public void Start()
    {
        _logger.Info("Starting backend...");
        _isRunning = true;
        while (_isRunning)
        {
            // Waiting two seconds between loops so that it's easier to see what's going on in the console
            Thread.Sleep(2000);
            while (_messageQueue.Count != 0)
            {
                var r = new Random();
                // Simulating that the functions that the backend does might vary in execution time
                Thread.Sleep(r.Next(15, 2000));
                LastFunction = _messageQueue.Dequeue();
                _logger.Info($"Backend executed function \"{LastFunction}\"");
                foreach (var onEventCallback in _listeners.Values)
                {
                    onEventCallback();
                }
            }
        }
    }

    public void Stop()
    {
        _isRunning = false;
        _logger.Info("Stopping backend");
    }
    
    public void SubscribeToBroadcaster(object id, Action onEventCallback)
    {
        _listeners.Add(id, onEventCallback);
    }

    public void UnsubscribeToBroadcaster(object id)
    {
        _listeners.Remove(id);
    }
}
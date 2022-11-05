namespace MultiThreadedConsoleApp;

public class Backend : IEventBroadcaster
{
    private readonly Queue<string> _messageQueue = new();
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
        _isRunning = true;
        while (_isRunning)
        {
            // Waiting two seconds between loops so that it's easier to see what's going on in the console
            Thread.Sleep(2000);
            while (_messageQueue.Count != 0)
            {
                var r = new Random();
                // Simulating that the functions that the backend does might vary in execution time
                Thread.Sleep(r.Next(15, 1500));
                LastFunction = _messageQueue.Dequeue();
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
    }
    
    public void SubscribeToBroadcaster(object objectReference, Action onEventCallback)
    {
        _listeners.Add(objectReference, onEventCallback);
    }

    public void UnsubscribeToBroadcaster(object objectReference)
    {
        _listeners.Remove(objectReference);
    }
}
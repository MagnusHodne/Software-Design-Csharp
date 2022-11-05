namespace MultiThreadedConsoleApp;

static class Program
{
    public static void Main()
    {
        Backend backend = new();
        Frontend frontend = new(backend);
        
        Thread frontendThread = new(frontend.Start);
        Thread backendThread = new(backend.Start);
        
        frontendThread.Start();
        backendThread.Start();
    }
}
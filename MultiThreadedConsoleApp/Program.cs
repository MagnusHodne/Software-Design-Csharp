// See https://aka.ms/new-console-template for more information

using MultiThreadedConsoleApp;

class Program
{
    public static void Main(string[] args) {
        Console.WriteLine("Starting the multi-threaded application");

        Backend backend = new();
        Frontend frontend = new(backend);

        frontend.Start();
    }
}
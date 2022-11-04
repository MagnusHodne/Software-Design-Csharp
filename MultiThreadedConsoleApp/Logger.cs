namespace MultiThreadedConsoleApp;

public class Logger
{
    private string _ownerClassName;
    public Logger(string ownerClassName) {
        _ownerClassName = ownerClassName;
    }
    public void Info(string message) {
        Log(message, "INFO", ConsoleColor.Blue, _ownerClassName);
    }

    public void Debug(string message) {
        Log(message, "INFO", ConsoleColor.Magenta, _ownerClassName);
    }

    public void Error(string message) {
        Log(message, "INFO", ConsoleColor.Red, _ownerClassName);
    }

    public void Warn(string message) {
        Log(message, "INFO", ConsoleColor.Yellow, _ownerClassName);
    }
    
    private static void Log(string message, string type, ConsoleColor color, string className) {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine($"[{type}] {className}: {message}");
        Console.ForegroundColor = prevColor;
    } 
}
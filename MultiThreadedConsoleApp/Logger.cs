namespace MultiThreadedConsoleApp;

public class Logger
{
    private readonly string _ownerClassName;
    
    public Logger(string ownerClassName) {
        _ownerClassName = ownerClassName;
    }
    public void Info(string message) {
        Log(message, "INFO ", ConsoleColor.Blue, _ownerClassName);
    }

    public void Debug(string message) {
        Log(message, "DEBUG", ConsoleColor.Magenta, _ownerClassName);
    }

    public void Error(string message) {
        Log(message, "ERROR", ConsoleColor.Red, _ownerClassName);
    }

    public void Warn(string message) {
        Log(message, "WARN ", ConsoleColor.Yellow, _ownerClassName);
    }
    
    private static void Log(string message, string type, ConsoleColor color, string className) {
        var datetime = DateTime.Now.ToString("hh:mm:ss");
        Console.Write($"{datetime} [");
        Console.ForegroundColor = color;
        Console.Write($"{type}");
        Console.ResetColor();
        Console.Write($"] {className}: {message}\n");
    } 
}
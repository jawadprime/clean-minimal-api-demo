namespace Logging;

public interface IAppLogger<T>
{
    void Debug(string message, Exception? ex = null, bool isGdprSafe = false);
    void Information(string message, Exception? ex = null, bool isGdprSafe = false);
    void Warning(string message, Exception? ex = null, bool isGdprSafe = false);
    void Error(string message, Exception? ex = null, bool isGdprSafe = false);
    void Fatal(string message, Exception ex, bool isGdprSafe = false);
}
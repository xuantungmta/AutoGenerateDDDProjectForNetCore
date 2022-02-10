namespace [$project_name].utils.logging
{
    public interface ILoggerAdapter<T>
    {
        void LogInformation(string message, params object[] args);

        void LogError(string message, params object[] args);

        void LogWarning(string message, params object[] args);

        void LogFatal(string message, params object[] args);
    }
}
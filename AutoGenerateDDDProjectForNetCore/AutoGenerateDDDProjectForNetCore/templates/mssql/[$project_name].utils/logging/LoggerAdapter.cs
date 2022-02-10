using log4net;
using log4net.Util;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace [$project_name].utils.logging
{
    public enum LogType
    {
        INFO,
        ERROR,
        WARN,
        FATAL
    }

    public class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;
        private readonly ILog _log;

        private void WriteErrorLog(LogType type, string message, [CallerFilePath] string fileName = "", [CallerMemberName] string function = "", [CallerLineNumber] int line = 0, params object[] args)
        {
            ((ContextPropertiesBase)GlobalContext.Properties)["function"] = (object)$"{Path.GetFileName(fileName)} {function}";
            switch (type)
            {
                case LogType.INFO:
                    _log.InfoFormat($"[{line}]: {message}", args);
                    break;

                case LogType.ERROR:
                    _log.ErrorFormat($"[{line}]: {message}", args);
                    break;

                case LogType.WARN:
                    _log.WarnFormat($"[{line}]: {message}", args);
                    break;

                case LogType.FATAL:
                    _log.FatalFormat($"[{line}]: {message}", args);
                    break;
            }
        }

        private void WriteLog(LogType type, string message, params object[] args)
        {
            switch (type)
            {
                case LogType.INFO:
                    _log.InfoFormat(message, args);
                    break;

                case LogType.ERROR:
                    _log.ErrorFormat(message, args);
                    break;

                case LogType.WARN:
                    _log.WarnFormat(message, args);
                    break;

                case LogType.FATAL:
                    _log.FatalFormat(message, args);
                    break;
            }
        }

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod()!.DeclaringType);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
            WriteLog(LogType.WARN, message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
            WriteLog(LogType.INFO, message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
            WriteErrorLog(LogType.ERROR, message, args: args);
        }

        public void LogFatal(string message, params object[] args)
        {
            _logger.LogCritical(message, args);
            WriteErrorLog(LogType.FATAL, message, args: args);
        }
    }
}
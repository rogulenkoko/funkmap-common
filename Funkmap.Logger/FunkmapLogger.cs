using System;
using NLog;

namespace Funkmap.Logger
{
    public class FunkmapLogger<T> : IFunkmapLogger<T> where T : class
    {
        private readonly ILogger _logger;

        public FunkmapLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Error(Exception exception, string message = "")
        {
            _logger.Error($"{typeof(T).FullName}: {message}", exception);
        }

        public void Error(string message)
        {
            _logger.Error($"{typeof(T).FullName}: {message}");
        }

        public void Info(string message)
        {
            _logger.Info($"{typeof(T).FullName}: {message}");
        }

        public void Trace(string message)
        {
            _logger.Trace($"{typeof(T).FullName}: {message}");
        }
    }
}

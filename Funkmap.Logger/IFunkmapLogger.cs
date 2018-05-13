using System;

namespace Funkmap.Logger
{
    public interface IFunkmapLogger<T> where T : class
    {
        void Info(string message);
        void Trace(string message);
        void Error(Exception exception, string message = "");
        void Error(string message);
    }
}

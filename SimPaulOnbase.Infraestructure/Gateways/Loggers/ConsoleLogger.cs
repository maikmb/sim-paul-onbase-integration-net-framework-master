using SimPaulOnbase.Core.Gateways;
using System;

namespace SimPaulOnbase.Infraestructure.Gateways
{
    public class ConsoleLogger : ILogger
    {

        public void Error(string message)
        {
            Console.WriteLine(message);

        }

        public void Error(string message, Exception exception)
        {
            Console.WriteLine(message, exception);

        }

        public void ErrorFormat(string format, params object[] args)
        {
            Console.WriteLine(format, args);

        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Info(string message, Exception exception)
        {
            Console.WriteLine(message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

    }
}

using SimPaulOnbase.Core.Gateways;
using System;

namespace SimPaulOnbase.Infraestructure.Gateways
{
    public class Logger : ILogger
    {

        public void Error(string message)
        {
        }

        public void Error(string message, Exception exception)
        {
        }

        public void ErrorFormat(string format, params object[] args)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(string message, Exception exception)
        {
        }

        public void InfoFormat(string format, params object[] args)
        {
        }

    }
}

using SimPaulOnbase.Core.Gateways;
using System;
using System.IO;

namespace SimPaulOnbase.Infraestructure.Gateways
{
    public class FileLogger : ILogger
    {
        public string FilePath { get; set; }

        public FileLogger(string filePath)
        {
            this.FilePath = filePath;
        }

        public void Error(string message)
        {
            this.WriteToFile(message);
        }

        public void Error(string message, Exception exception)
        {
            this.WriteToFile($"{message}\r\n{exception.ToString()}");
        }

        public void ErrorFormat(string format, params object[] args)
        {
            this.WriteToFile(string.Format(format, args));
        }

        public void Info(string message)
        {
            this.WriteToFile(message);
        }

        public void Info(string message, Exception exception)
        {
            this.WriteToFile($"{message}\r\n{exception.ToString()}");
        }

        public void InfoFormat(string format, params object[] args)
        {
            this.WriteToFile(string.Format(format, args));
        }        

        public void WriteToFile(string logMessage)
        {
            using (StreamWriter w = File.AppendText(this.FilePath))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");                
                w.WriteLine($"{logMessage}");
                w.WriteLine("-------------------------------");
            }
            
        }

    }
}

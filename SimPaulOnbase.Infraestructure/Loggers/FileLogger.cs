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
            if (!File.Exists(filePath)) File.Create(filePath);
            this.FilePath = filePath;
        }

        public void Error(string message)
        {
            this.WriteToFile($"ERROR: {message}");
        }

        public void Error(string message, Exception exception)
        {
            this.WriteToFile($"ERROR: {message}\r\n{exception.ToString()}");
        }

        public void ErrorFormat(string format, params object[] args)
        {
            this.WriteToFile(string.Format($"ERROR: {format}", args));
        }

        public void Info(string message)
        {
            this.WriteToFile($"INFO: {message}");
        }

        public void Info(string message, Exception exception)
        {
            this.WriteToFile($"INFO: {message}\r\n{exception.ToString()}");
        }

        public void InfoFormat(string format, params object[] args)
        {
            this.WriteToFile(string.Format($"INFO: {format}", args));
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

using System;

namespace SimPaulOnbase.Core.Gateways
{
    /// <summary>
    /// ILogger interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Error log
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);

        /// <summary>
        /// Error log with exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Error log with string format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void ErrorFormat(string format, params object[] args);

        /// <summary>
        /// Info log 
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);

        /// <summary>
        /// Info log with exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Info(string message, Exception exception);

        /// <summary>
        /// Info log with string format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void InfoFormat(string format, params object[] args);

    }
}
    
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Diagnostics;

namespace Cellent.Template.Common.Logger
{
    /// <summary>
    /// Container Klasse für Logging-Kategorien
    /// </summary>
    public class LoggingCategories
    {
        /// <summary>
        /// Error cat
        /// </summary>
        public const string Error = "Error";
        /// <summary>
        /// Warning cat
        /// </summary>
        public const string Warning = "Warning";
        /// <summary>
        /// Info cat
        /// </summary>
        public const string Info = "Info";
    }
    /// <summary>
    /// Enumeration für Logging-Prio
    /// </summary>
    public enum LoggingPriority
    {
        /// <summary>
        /// Nicht spezifiziert
        /// </summary>
        None = 0,
        /// <summary>
        /// Niedrig
        /// </summary>
        Low,
        /// <summary>
        /// Mittel
        /// </summary>
        Medium,
        /// <summary>
        /// Hoch
        /// </summary>
        High
    }
    /// <summary>
    /// Klasse stellt die Methoden für das Logging bereit
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Die Fehlerkategorie
        /// </summary>
        public static readonly String DefaultCategory = "General";
        /// <summary>
        /// Die Debug-Kategorie
        /// </summary>
        public static readonly String DebugCategory = "Debug";
        /// <summary>
        /// Die Trace-Kategorie
        /// </summary>
        public static readonly String TraceCategory = "Trace";
        /// <summary>
        /// Die Error-Kategorie
        /// </summary>
        public static readonly String ErrorCategory = "Error";

        /// <summary>
        /// Initializes the <see cref="Logger"/> class.
        /// </summary>
        static Logger()
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.SetLogWriter(new LogWriterFactory().Create());
        }

        /// <summary>
        /// Gets a value indicating whether this instance is logging enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is logging enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoggingEnabled { get { return Microsoft.Practices.EnterpriseLibrary.Logging.Logger.IsLoggingEnabled(); } }


        /// <summary>
        /// Loggt eine Exception mit Message und Stacktrace
        /// </summary>
        /// <param name="exception">die Exception, die geloggt werden soll</param>
        public static void Write(Exception exception)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(exception.ToString(), ErrorCategory, 0, 99999, TraceEventType.Error);
        }

        /// <summary>
        /// Schreibt einen Text ins Logfile
        /// </summary>
        /// <param name="text">die Nachricht</param>
        /// <param name="eventId">die Fehler ID</param>
        /// <param name="serverity">das Loglevel</param>
        public static void Write(String text, int eventId, TraceEventType serverity)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(text, DefaultCategory, 0, eventId, serverity);
        }
        /// <summary>
        /// Schreibt einen Text ins Logfile
        /// </summary>
        /// <param name="text">die Nachricht</param>
        /// <param name="serverity">das Loglevel</param>
        public static void Write(String text, TraceEventType serverity)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(text, DefaultCategory, 0, 10000, serverity);
        }
        /// <summary>
        /// Schreibt einen Text ins Logfile
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="category">The category.</param>
        /// <param name="serverity">The serverity.</param>
        public static void Write(String text, string category, TraceEventType serverity)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(text, category, 0, 10000, serverity);
        }

        /// <summary>
        /// Writes the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="serverity">The serverity.</param>
        /// <param name="format">The format.</param>
        /// <param name="p">The p.</param>
        public static void Write(string category, TraceEventType serverity, String format, params object[] p)
        {
            if (!string.IsNullOrWhiteSpace(category))
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(String.Format(format, p), category, 0, 10000, serverity);              
            }
        }
        
        /// <summary>
        /// Schreibt einen Text ins Logfile
        /// </summary>
        /// <param name="text">die Nachricht</param>
        /// <param name="args">die Objektargumente</param>
        /// <param name="eventId">die Fehler ID</param>
        /// <param name="serverity">das Loglevel</param>
        public static void Write(String text, Object[] args, int eventId, TraceEventType serverity)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(String.Format(text, args), DefaultCategory, 0, eventId, serverity);
        }

        /// <summary>
        /// Writes the specified message with category.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Write(object message, string category)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message, category);
        }

        /// <summary>
        /// Writes the specified message with category and priority.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="prio">The prio.</param>
        public static void Write(object message, string category, LoggingPriority prio)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message, category, (int)prio);
        }
    }
}

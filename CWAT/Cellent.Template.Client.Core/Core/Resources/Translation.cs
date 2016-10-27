using System;
using System.Diagnostics;
using System.Threading;
using Cellent.Template.Common.Logger;

namespace Cellent.Template.Client.Core.Core.Resources
{
    /// <summary>
    /// Klasse um Texte zu Übersetzten
    /// </summary>
    public class Translation
    {
        private static readonly ResourceManager ResourceManager = new ResourceManager();

        /// <summary>
        /// Übersetzt einen Key. 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>wird der Key in der DB nicht gefunden ist das Ergebnis [key]</returns>
        public static String Translate(String key)
        {
            try
            {
                String result = ResourceManager.GetString(key.ToLower(), Thread.CurrentThread.CurrentCulture);
                if (result != null)
                    result = result.Replace("\\r\\n", "\r\n");
                else
                    Logger.Write(key, TraceEventType.Verbose);

                return result ?? String.Format("[{0}]", key);
            }
#if DEBUG
            catch (Exception)
            { 
                return String.Format("[{0}]", key);
            }
#else
            catch (Exception ex)
            {
                Logger.Write(ex);
                return String.Format("[{0}]", key);
            }
#endif
        }
    }
}

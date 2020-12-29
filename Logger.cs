using System;
using System.Diagnostics;
using System.IO;

namespace CheckDotNet
{
    public class Logger
    {
        private readonly string _logFilePath;
        private static readonly object Sync = new object();

        public Logger()
        {
            //_logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), string.Format("MDK_Email_{0}.log", DateTime.Now.ToShortDateString()));
            _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), string.Format("axCheckDotNet.log"));
            //Write("Gestartet");

            // Leo, 06.05.2015 Delete file if exists
            if (File.Exists(_logFilePath))
                File.Delete(_logFilePath);
        }

        /// <summary>
        /// Writes the specified text.
        /// </summary>
        /// <param name="text">The text.
        /// </param>
        public void Write(string text)
        {
            lock (Sync)
            {
                // Logtext
                string strText = string.Format("{0}: {1}", DateTime.Now, text + Environment.NewLine);
#if(DEBUG)
                Debug.WriteLine(strText);
#endif
                File.AppendAllText(_logFilePath, strText);
            }
        }

        public void Write(Exception exc)
        {
            Write(exc.Message + Environment.NewLine + exc.StackTrace);
        }

        public string GetFileName()
        {
            return _logFilePath;
        }
    }
}

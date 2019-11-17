using System;
using System.IO;

namespace Auxiliary
{
    public class Logger : IDisposable
    {
        public static readonly Logger Instance = new Logger();

        private static readonly string logPath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");
        private static readonly object locker = new object();
        private static StreamWriter logFile;
        
        public void AddLine(string line)
        {
            Console.WriteLine(line);

            if (logFile == null)
            {
                logFile = new StreamWriter(logPath, true);
            }
            
            lock (locker)
            {
                logFile.WriteLine(line);
            }
        }

        public void Dispose()
        {
            logFile.Dispose();
        }
    }
}

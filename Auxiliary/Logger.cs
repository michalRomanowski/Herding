using System;
using System.IO;

namespace Auxiliary
{
    public static class Logger
    {
        public static string LogPath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");

        private static object locker = new object();

        private static StreamWriter logFile;

        public static void Clear()
        {
            if(!File.Exists(LogPath))
                File.Create(LogPath);

            if (logFile != null)
                logFile.Close();

            logFile = new StreamWriter(LogPath, false);
        }

        public static void CopyToDir(string dirPath)
        {
            if (logFile == null)
                logFile = new StreamWriter(LogPath, true);

            File.Copy(LogPath, Path.Combine(dirPath, "log.txt"));
        }

        public static void AddLine(string line)
        {
            if(logFile == null)
                logFile = new StreamWriter(LogPath, true);

            lock (locker)
            {
                logFile.WriteLine(line);
            }
        }

        public static void Flush()
        {
            try
            {
                logFile.Flush();
            }
            catch (ObjectDisposedException) { }
        }

        public static void Close()
        {
            if(logFile != null)
                logFile.Close();
        }
    }
}

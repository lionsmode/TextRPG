using System;
using System.IO;

namespace Logging
{
    public class GameLogger : IDisposable
    {
        private readonly StreamWriter writer = new StreamWriter("gameLog.txt", true);

        public void Log(string message)
        {
            writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
            writer.Flush();
        }

        public void Show(string message)
        {
            Console.WriteLine(message);
            Log(message);
        }

        public void Dispose()
        {
            writer.Dispose();
        }
    }
}

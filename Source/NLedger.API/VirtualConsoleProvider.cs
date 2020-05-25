using System.IO;
using NLedger.Core.Abstracts;

namespace NLedger.API
{
    public class VirtualConsoleProvider : IVirtualConsoleProvider
    {
        public VirtualConsoleProvider(TextReader consoleInput, TextWriter consoleOutput, TextWriter consoleError)
        {
            ConsoleInput  = consoleInput;
            ConsoleOutput = consoleOutput;
            ConsoleError  = consoleError;
        }

        public TextWriter ConsoleError  { get; }
        public TextReader ConsoleInput  { get; }
        public TextWriter ConsoleOutput { get; }

        public int WindowWidth => 0;

        public void AddHistory(string readLineName, string str)
        {
        }

        public int HistoryExpand(string readLineName, string str, ref string output)
        {
            return 0;
        }
    }
}
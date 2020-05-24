using System;
using System.IO;
using System.Text;
using NLedger.API.Types;
using NLedger.Core;
using NLedger.Core.Utility.Settings;

namespace NLedger.API
{
    public class NLedgerProxy
    {
        private static readonly object lockObject = new object();
        public                  string LedgerFile { get; set; }

        public static NLedgerProxy FromLedgerFile(string ledgerFile)
        {
            return new NLedgerProxy {LedgerFile = ledgerFile};
        }

        public QueryResult QueryLedger(string query)
        {
            try
            {
                lock (lockObject)
                {
                    // Capture Output
                    var       sb = new StringBuilder();
                    using var sw = new StringWriter(sb);
                    Console.SetOut(sw);

                    // Initialize NLedger
                    MainApplicationContext.Initialize();
                    new NLedgerConfiguration().ConfigureConsole(MainApplicationContext.Current);

                    // Execute Query
                    var exitCode = new Main().Execute($@"-f {LedgerFile} {query}");

                    // Return Results
                    return new QueryResult
                    {
                        Result    = sb.ToString(),
                        Exception = exitCode == 0 ? string.Empty : exitCode.ToString()
                    };
                }
            }
            catch (Exception e)
            {
                // Return Exception
                return new QueryResult
                {
                    Exception = e.ToString()
                };
            }
            finally
            {
                // Cleanup NLedger
                MainApplicationContext.Cleanup();
            }
        }
    }
}
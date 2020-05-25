using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NLedger.API.Types;
using NLedger.Core;

namespace NLedger.API
{
    public class NLedgerProxy
    {
        public string LedgerFile { get; set; }

        public static NLedgerProxy FromLedgerFile(string ledgerFile)
        {
            return new NLedgerProxy {LedgerFile = ledgerFile};
        }

        public async Task<QueryResult> QueryLedger(string query)
        {
            return await Task.Factory.StartNew(() =>
            {
                try
                {
                    // Configure for 80 symbols in row
                    var envs = new Dictionary<string, string> {["COLUMNS"] = "80"};

                    // Build args
                    var args = $" -f '{LedgerFile}' {query}";

                    // Check whether "--pager" option is presented to add extra options for paging test
                    if (args.Contains("--pager"))
                    {
                        args                            += " --force-pager"; // Pager test requires this option to override IsAtty=false that is default for other tests
                        envs["nledgerPagerForceOutput"] =  "true";           // Pager test needs to force writing to output to test text that is ate by the pager process
                    }

                    // Check whether arguments have escaped dollar sign; remove it if so
                    args = args.Replace(@" \$ ", " $ ");

                    // Set custom environment variables
                    // foreach (var name in testCase.SetVariables.Keys)
                    //     envs[name] = testCase.SetVariables[name];

                    using var inReader  = new StringReader(string.Empty);
                    using var outWriter = new StringWriter();
                    using var errWriter = new StringWriter();

                    var main = new Main();
                    MainApplicationContext.Current.IsAtty   = false;                                                        // Simulating pipe redirection in original tests
                    MainApplicationContext.Current.TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"); // Equals to TZ=America/Chicago
                    MainApplicationContext.Current.SetVirtualConsoleProvider(() => new VirtualConsoleProvider(inReader, outWriter, errWriter));
                    MainApplicationContext.Current.SetEnvironmentVariables(envs);

                    main.Execute(args);

                    outWriter.Flush();
                    errWriter.Flush();

                    var queryResult = new QueryResult
                    {
                        Result    = outWriter.ToString(),
                        Exception = errWriter.ToString()
                    };

                    return queryResult;
                }
                catch (Exception e)
                {
                    // Return Exception
                    return new QueryResult
                    {
                        Exception = e.ToString()
                    };
                }
            });
        }
    }
}
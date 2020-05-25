using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NLedger.API.Extensions;
using ServiceStack;

namespace NLedger.API
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var sw1 = new Stopwatch();
            sw1.Restart();

            var sw2      = new Stopwatch();
            var runTimes = new List<long>();

            var client1 = NLedgerProxy.FromLedgerFile(@"l:\test\drewr3.dat");
            var client2 = NLedgerProxy.FromLedgerFile(@"D:\Dev\Work\Enumis\npo-ledger-cli\accounts\books.ledger");
            var client3 = NLedgerProxy.FromLedgerFile(@"E:\User\Documents\NLedger\sample.ledger");

            var ledger = (await client3.QueryLedger("bal")).AsBalanceReport();

            ;
            for (var i = 0; i < 10; i++)
            {
                sw2.Restart();

                if (i == 0)
                {
                    await client1.QueryLedger("reload").ConfigureAwait(false);
                    await client2.QueryLedger("reload").ConfigureAwait(false);
                }
                else
                {
                    await client1.QueryLedger(i % 2 == 0 ? "bal" : "reg").ConfigureAwait(false);
                    await client2.QueryLedger(i % 2 == 1 ? "bal" : "reg").ConfigureAwait(false);
                }

                sw2.Stop();
                runTimes.Add(sw2.ElapsedMilliseconds);
            }

            sw2.Restart();

            var client1ReportJson = (await client1.QueryLedger("bal")).AsBalanceReport().ToJson();
            var client2ReportJson = (await client2.QueryLedger("bal")).AsBalanceReport().ToJson();

            sw2.Stop();
            runTimes.Add(sw2.ElapsedMilliseconds);

            sw1.Stop()
                ;
        }
    }
}
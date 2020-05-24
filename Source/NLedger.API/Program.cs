using System.Collections.Generic;
using System.Diagnostics;
using NLedger.API.Extensions;
using ServiceStack;

namespace NLedger.API
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sw    = new Stopwatch();
            var times = new List<long>();

            var client1 = NLedgerProxy.FromLedgerFile(@"l:\test\drewr3.dat");
            var client2 = NLedgerProxy.FromLedgerFile(@"D:\Dev\Work\Enumis\npo-ledger-cli\accounts\books.ledger");

            for (var i = 0; i < 10; i++)
            {
                sw.Restart();

                if (i == 0)
                {
                    client1.QueryLedger("reload");
                    client2.QueryLedger("reload");
                }
                else
                {
                    client1.QueryLedger(i % 2 == 0 ? "bal" : "reg");
                    client2.QueryLedger(i % 2 == 1 ? "bal" : "reg");
                }

                sw.Stop();
                times.Add(sw.ElapsedMilliseconds);
            }

            sw.Restart();

            var client1ReportJson = client1.QueryLedger("bal").AsBalanceReport().ToJson();
            var client2ReportJson = client2.QueryLedger("bal").AsBalanceReport().ToJson();

            sw.Stop();
            times.Add(sw.ElapsedMilliseconds);

            ;
        }
    }
}
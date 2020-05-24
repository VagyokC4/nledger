using System;
using System.Collections.Generic;
using System.Diagnostics;
using NLedger.API.Types.Reports.Balances;
using ServiceStack;

namespace NLedger.API
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw     = new Stopwatch();
            var times  = new List<long>();
            var ledger = NLedgerProxy.FromLedgerFile(@"l:\test\drewr3.dat");
            var ledger2 = NLedgerProxy.FromLedgerFile(@"D:\Dev\Work\Enumis\npo-ledger-cli\accounts\books.ledger");

            for (int i = 0; i < 10; i++)
            {
                sw.Restart();

                if (i == 0)
                {
                    ledger.QueryLedger("reload");
                    ledger2.QueryLedger("reload");
                }
                else
                {
                    ledger.QueryLedger(i % 2 == 0 ? "bal" : "reg");
                    ledger2.QueryLedger(i % 2 == 1 ? "bal" : "reg");
                }
                
                // var reportJson = BalanceReport
                // .FromLedgerResponse(ledger.QueryLedger("bal"))
                // .ToJson();

                sw.Stop();
                times.Add(sw.ElapsedMilliseconds);
            }

            ;
        }
    }
}
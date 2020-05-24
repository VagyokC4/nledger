// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System;
using NLedger.Utility.Settings;

namespace NLedger.CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // System.Diagnostics.Debugger.Launch(); // This debugging option might be useful in case of troubleshooting of NLTest issues


            // var reportJson = BalanceReport
            //     .FromLedgerResponse(new NLedgerProxy().CaptureResponse(@"l:\test\drewr3.dat", "bal"))
            //     .ToJson();
            //
            // var br2 = BalanceReport.FromLedgerResponse(new NLedgerProxy().CaptureResponse(@"D:\Dev\Work\Enumis\npo-ledger-cli\accounts\books.ledger", "bal")).ToJson();
            //
            // ;


            var main = new Main();
            new NLedgerConfiguration().ConfigureConsole(MainApplicationContext.Current);

            var argString = @"-f l:\test\drewr3.dat"; //GetCommandLine(); // This way is preferrable because of double quotas that are missed by using args

            Environment.ExitCode = main.Execute($"{argString} bal");
        }

        private static string GetCommandLine()
        {
            // returns the original command line arguments w/o execution file name
            var commandLine = Environment.CommandLine;
            int pos         = commandLine[0] == '"' ? pos = commandLine.IndexOf('"', 1) : commandLine.IndexOf(' ');
            return pos >= 0 ? commandLine.Substring(pos + 1).TrimStart() : string.Empty;
        }
    }
}
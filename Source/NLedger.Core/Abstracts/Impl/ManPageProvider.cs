﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using NLedger.Core.Utility;

namespace NLedger.Core.Abstracts.Impl
{
    public class ManPageProvider : IManPageProvider
    {
        public const string LedgerManFile = "ledger.1.html";

        public bool Show()
        {
            string pathName = FileSystem.Combine(LedgerManFile, FileSystem.AppPath());
            return MainApplicationContext.Current.ProcessManager.Start(pathName);
        }
    }
}

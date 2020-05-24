﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

namespace NLedger.Core.Utility
{
    public static class VersionInfo
    {
        public static string NLedgerVersion
        {
            get 
            {
                return _NLedgerVersion ?? (_NLedgerVersion = typeof(VersionInfo).Assembly.GetName().Version.ToString());
            }
        }

        public const int Ledger_VERSION_MAJOR = 3;
        public const int Ledger_VERSION_MINOR = 1;
        public const int Ledger_VERSION_PATCH = 1;
        public const string Ledger_VERSION_PRERELEASE = "-alpha.1";
        public const int Ledger_VERSION_DATE = 20141005;

        private static string _NLedgerVersion = null;
    }
}

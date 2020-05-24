﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System;

namespace NLedger.Core.Scopus
{
    public class OptionError : Exception
    {
        public const string ErrorMessage_IllegalOption = "Illegal option --{0}";
        public const string ErrorMessage_MissingOptionArgumentFor = "Missing option argument for --{0}";

        public OptionError(string message)
            : base(message)
        { }
    }
}

﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

namespace NLedger.Core.Abstracts
{
    /// <summary>
    /// A service provider that shows a man page in an appropriate way
    /// </summary>
    public interface IManPageProvider
    {
        /// <summary>
        /// Show man page and immediately return execution flow
        /// </summary>
        /// <returns>Indicates whether the operation is successfull</returns>
        bool Show();
    }
}

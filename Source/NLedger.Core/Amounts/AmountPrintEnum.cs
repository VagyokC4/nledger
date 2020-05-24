﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System;

namespace NLedger.Core.Amounts
{
    /// <summary>
    /// Ported from amount.h
    /// </summary>
    [Flags]
    public enum AmountPrintEnum
    {
        AMOUNT_PRINT_NO_FLAGS = 0x00,
        AMOUNT_PRINT_RIGHT_JUSTIFY = 0x01,
        AMOUNT_PRINT_COLORIZE = 0x02,
        AMOUNT_PRINT_NO_COMPUTED_ANNOTATIONS = 0x04,
        AMOUNT_PRINT_ELIDE_COMMODITY_QUOTES = 0x08,
        AMOUNT_PRINT_ALL_FLAGS = 0x0F
    }
}

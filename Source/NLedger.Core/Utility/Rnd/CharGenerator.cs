﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System;

namespace NLedger.Core.Utility.Rnd
{
    public class CharGenerator : IntegerGenerator
    {
        public CharGenerator(char minValue, char maxValue)
            : this(null, minValue, maxValue)
        { }

        public CharGenerator(Random random, char minValue, char maxValue)
            : base(random, (int)minValue, (int)maxValue)
        { }

        public new char Value()
        {
            return (char)base.Value();
        }
    }
}

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
    public class BoolGenerator : IntegerGenerator
    {
        public BoolGenerator()
            : this(null)
        { }

        public BoolGenerator(Random random)
            : base(random, 0, 1)
        { }

        public new bool Value()
        {
            return base.Value() == 1;
        }
    }
}

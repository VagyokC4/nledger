﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System;
using NLedger.Core.Commodities;

namespace NLedger.Core.Annotate
{
    public struct AnnotationKeepDetails
    {
        public AnnotationKeepDetails(bool keepPrice = false, bool keepDate = false, bool keepTag = false, bool onlyActuals = false)
            : this()
        {
            KeepPrice = keepPrice;
            KeepDate = keepDate;
            KeepTag = keepTag;
            OnlyActuals = onlyActuals;
        }

        public bool KeepPrice { get; set; }
        public bool KeepDate { get; set; }
        public bool KeepTag { get; set; }
        public bool OnlyActuals { get; set; }

        public bool KeepAll()
        {
            return KeepPrice && KeepDate && KeepTag && !OnlyActuals;
        }

        public bool KeepAll(Commodity commodity)
        {
            if (commodity == null)
                throw new ArgumentNullException("commodity");

            return !commodity.IsAnnotated || KeepAll();
        }

        public bool KeepAny()
        {
            return KeepPrice || KeepDate || KeepTag;
        }

        public bool KeepAny(Commodity commodity = null)
        {
            if (commodity == null)
                throw new ArgumentNullException("commodity");

            return commodity.IsAnnotated && KeepAny();
        }
    }
}

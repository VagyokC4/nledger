﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using NLedger.Core.Chain;
using NLedger.Core.Expressions;
using NLedger.Core.Scopus;
using NLedger.Core.Xacts;

namespace NLedger.Core.Filters
{
    public class SortXacts : PostHandler
    {
        public SortXacts(PostHandler handler, Expr sortOrder, Report report)
            : base(null)
        {
            Sorter = new SortPosts(handler, sortOrder, report);
        }

        public SortXacts(PostHandler handler, string sortOrder, Report report)
            : base(null)
        {
            Sorter = new SortPosts(handler, sortOrder, report);
        }

        public SortPosts Sorter { get; private set; }
        public Xact LastXact { get; set; }

        public override void Flush()
        {
            Sorter.Flush();
            base.Flush();
        }

        public override void Handle(Post post)
        {
            if (LastXact != null && post.Xact != LastXact)
                Sorter.PostAccumulatedPosts();

            Sorter.Handle(post);

            LastXact = post.Xact;
        }

        public override void Clear()
        {
            Sorter.Clear();
            LastXact = null;

            base.Clear();
        }
    }
}

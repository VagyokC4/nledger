﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using NLedger.Core.Chain;
using NLedger.Core.Scopus;

namespace NLedger.Core.Filters
{
    public class FilterPosts : PostHandler
    {
        public Predicate Pred { get; private set; }
        public Scope Context { get; private set; }

        public FilterPosts(PostHandler handler, Predicate pred, Scope context)
            : base(handler)
        {
            Pred = pred;
            Context = context;
        }

        public override void Handle(Post post)
        {
            BindScope boundScope = new BindScope(Context, post);

            if (Pred.Calc(boundScope).Bool)
            {
                post.XData.Matches = true;
                base.Handle(post);
            }
        }

        public override void Clear()
        {
            Pred.MarkUncomplited();
            base.Clear();
        }
    }
}

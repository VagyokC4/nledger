﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System.Collections.Generic;
using NLedger.Core.Chain;
using NLedger.Core.Times;
using NLedger.Core.Xacts;

namespace NLedger.Core.Filters
{
    public class GeneratePosts : PostHandler
    {
        public GeneratePosts(PostHandler handler)
            : base(handler)
        {
            PendingPosts = new List<PendingPostsPair>();
            Temps = new Temporaries();
        }

        public IList<PendingPostsPair> PendingPosts { get; private set; }
        public Temporaries Temps { get; private set; }

        public void AddPeriodXacts(IEnumerable<PeriodXact> periodXacts)
        {
            foreach(PeriodXact xact in periodXacts)
            {
                foreach (Post post in xact.Posts)
                    AddPost(xact.Period, post);
            }
        }

        public virtual void AddPost(DateInterval period, Post post)
        {
            PendingPosts.Add(new PendingPostsPair(new DateInterval(period), post));
        }

        public override void Clear()
        {
            PendingPosts.Clear();
            Temps.Clear();

            base.Clear();
        }

        public class PendingPostsPair
        {
            public PendingPostsPair(DateInterval dateInterval, Post post)
            {
                DateInterval = dateInterval;
                Post = post;
            }

            public DateInterval DateInterval { get; set; }
            public Post Post { get; set; }
        }
    }
}

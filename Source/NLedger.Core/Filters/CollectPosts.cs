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

namespace NLedger.Core.Filters
{
    public class CollectPosts : PostHandler
    {
        public CollectPosts() : base(null)
        {
            Posts = new List<Post>();
        }
        
        public IList<Post> Posts { get; private set; }

        public int Length
        {
            get { return Posts.Count; }
        }

        public override void Handle(Post post)
        {
            Posts.Add(post);
        }

        public override void Flush()
        {  }

        public override void Clear()
        {
            Posts.Clear();
            base.Clear();
        }
    }
}

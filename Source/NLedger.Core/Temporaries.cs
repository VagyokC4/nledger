﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System.Collections.Generic;
using System.Linq;
using NLedger.Core.Accounts;
using NLedger.Core.Xacts;

namespace NLedger.Core
{
    public class Temporaries
    {
        public Xact LastXact
        {
            get { return XactTemps != null ? XactTemps.Last() : null; }
        }

        public Post LastPost
        {
            get { return PostTemps != null ? PostTemps.Last() : null; }
        }

        public Account LastAccount
        {
            get { return AcctTemps != null ? AcctTemps.Last() : null; }
        }

        public Xact CopyXact(Xact origin)
        {
            if (XactTemps == null)
                XactTemps = new List<Xact>();

            XactTemps.Add(origin);

            Xact temp = new Xact(origin);
            temp.Flags |= SupportsFlagsEnum.ITEM_TEMP;

            return temp;
        }

        public Xact CreateXact()
        {
            if (XactTemps == null)
                XactTemps = new List<Xact>();


            Xact temp = new Xact();
            temp.Flags |= SupportsFlagsEnum.ITEM_TEMP;
            XactTemps.Add(temp);

            return temp;
        }

        public Post CopyPost(Post origin, Xact xact, Account account = null)
        {
            if (PostTemps == null)
                PostTemps = new List<Post>();

            PostTemps.Add(origin);

            Post temp = new Post(origin);
            temp.Flags |= SupportsFlagsEnum.ITEM_TEMP;

            if (account != null)
                temp.Account = account;

            temp.Account.AddPost(temp);
            xact.AddPost(temp);

            return temp;
        }

        public Post CreatePost(Xact xact, Account account, bool bidirLink = true)
        {
            if (PostTemps == null)
                PostTemps = new List<Post>();

            Post temp = new Post(account);
            temp.Flags |= SupportsFlagsEnum.ITEM_TEMP;
            temp.Account = account;
            temp.Account.AddPost(temp);

            PostTemps.Add(temp);

            if (bidirLink)
                xact.AddPost(temp);
            else
                temp.Xact = xact;

            return temp;
        }

        public Account CreateAccount(string name = "", Account parent = null)
        {
            if (AcctTemps == null)
                AcctTemps = new List<Account>();

            Account temp = new Account(parent, name);
            temp.IsTempAccount = true;

            if (parent != null)
                parent.AddAccount(temp);

            return temp;
        }

        public void Clear()
        {
            if (PostTemps != null)
            {
                foreach(Post post in PostTemps)
                {
                    if (!post.Xact.Flags.HasFlag(SupportsFlagsEnum.ITEM_TEMP))
                        post.Xact.RemovePost(post);

                    if (post.Account != null && !post.Account.IsTempAccount)
                        post.Account.RemovePost(post);
                }
                PostTemps.Clear();
            }

            if (XactTemps != null)
                XactTemps.Clear();

            if (AcctTemps != null)
            {
                foreach(Account acct in AcctTemps)
                {
                    if (acct.Parent != null && !acct.Parent.IsTempAccount)
                        acct.Parent.RemoveAccount(acct);
                }
                AcctTemps.Clear();
            }
        }

        private IList<Xact> XactTemps { get; set; }
        private IList<Post> PostTemps { get; set; }
        private IList<Account> AcctTemps { get; set; }
    }
}

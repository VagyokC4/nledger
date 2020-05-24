﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System;
using System.Collections.Generic;
using NLedger.Core.Accounts;
using NLedger.Core.Amounts;
using NLedger.Core.Chain;
using NLedger.Core.Commodities;
using NLedger.Core.Expressions;
using NLedger.Core.Scopus;
using NLedger.Core.Utility;
using NLedger.Core.Values;
using NLedger.Core.Xacts;

namespace NLedger.Core.Filters
{
    /// <summary>
    /// Ported from posts_as_equity
    /// </summary>
    public class PostsAsEquity : SubtotalPosts
    {
        public PostsAsEquity(PostHandler handler, Report report, Expr amountExpr)
            : base (handler, amountExpr)
        {
            Report = report;
            CreateAccounts();
        }

        public Report Report { get; private set; }
        public Post LastPost { get; private set; }
        public Account EquityAccount { get; private set; }
        public Account BalanceAccount { get; private set; }

        public void CreateAccounts()
        {
            EquityAccount = Temps.CreateAccount("Equity");
            BalanceAccount = EquityAccount.FindAccount("Opening Balances");
        }

        /// <summary>
        /// Ported from posts_as_equity::report_subtotal
        /// </summary>
        public void ReportSubtotal()
        {
            Date finish = default(Date);
            foreach(Post post in ComponentPosts)
            {
                Date date = post.GetDate();
                if (!finish.IsValid() || date > finish)
                    finish = date;
            }

            Xact xact = Temps.CreateXact();
            xact.Payee = "Opening Balances";
            xact.Date = finish;

            Value total = Value.Get(0);
            foreach(KeyValuePair<string,AcctValue> pair in Values)
            {
                Value value = pair.Value.Value.StripAnnotations(Report.WhatToKeep());
                if (!Value.IsNullOrEmpty(value))
                {
                    if (value.Type == ValueTypeEnum.Balance)
                    {
                        foreach(KeyValuePair<Commodity, Amount> amountPair in value.AsBalance.Amounts)
                        {
                            if (!amountPair.Value.IsZero)
                                FiltersCommon.HandleValue(
                                    /* value=      */ Value.Get(amountPair.Value),
                                    /* account=    */ pair.Value.Account,
                                    /* xact=       */ xact,
                                    /* temps=      */ Temps,
                                    /* handler=    */ (PostHandler)Handler,
                                    /* date=       */ finish,
                                    /* act_date_p= */ false);
                        }
                    }
                    else
                    {
                        FiltersCommon.HandleValue(
                            /* value=      */ Value.Get(value.AsAmount),
                            /* account=    */ pair.Value.Account,
                            /* xact=       */ xact,
                            /* temps=      */ Temps,
                            /* handler=    */ (PostHandler)Handler,
                            /* date=       */ finish,
                            /* act_date_p= */ false);
                    }
                }

                if (!pair.Value.IsVirtual || pair.Value.MustBalance)
                    total.InPlaceAdd(value);
            }
            Values.Clear();

            // This last part isn't really needed, since an Equity:Opening
            // Balances posting with a null amount will automatically balance with
            // all the other postings generated.  But it does make the full
            // balancing amount clearer to the user.
            if (!total.IsZero)
            {
                Action<Amount> postCreator = amt =>
                    {
                        Post balancePost = Temps.CreatePost(xact, BalanceAccount);
                        balancePost.Amount = amt.Negated();
                        Handler.Handle(balancePost);
                    };

                if (total.Type == ValueTypeEnum.Balance)
                    total.AsBalance.MapSortedAmounts(postCreator);
                else
                    postCreator(total.AsAmount);
            }
        }

        public override void Flush()
        {
            this.ReportSubtotal();
            base.Flush();
        }

        public override void Clear()
        {
            LastPost = null;
            base.Clear();
            CreateAccounts();
        }
    }
}

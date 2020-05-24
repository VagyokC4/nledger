﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System;
using NLedger.Core.Expressions;
using NLedger.Core.Scopus;
using NLedger.Core.Utils;
using NLedger.Core.Values;

namespace NLedger.Core.Xacts
{
    /// <summary>
    /// Ported from xact_t (xact.h)
    /// </summary>
    public class Xact : XactBase
    {
        public const string GeneratedTransactionKey = "generated transaction";

        public Xact()
        {
            CreateLookupItems();
        }

        public Xact(Xact xact)
            : base(xact)
        {
            Code = xact.Code;
            Payee = xact.Payee;
            CreateLookupItems();
        }

        public string Code { get; set; }
        public string Payee { get; set; }

        public override string Description
        {
            get { return HasPos ? String.Format("transaction at line {0}", Pos.BegLine) : GeneratedTransactionKey; }
        }

        public override ExprOp Lookup(SymbolKindEnum kind, string name)
        {
            return LookupItems.Lookup(kind, name, this) ?? base.Lookup(kind, name);
        }

        public override void AddPost(Post post)
        {
            post.Xact = this;
            base.AddPost(post);
        }

        public override bool Valid()
        {
            if (!Date.HasValue)
            {
                Logger.Current.Debug("ledger.validate", () => "xact_t: ! _date");
                return false;
            }

            foreach(Post post in Posts)
            {
                if (post.Xact != this || !post.Valid())
                {
                    Logger.Current.Debug("ledger.validate", () => "xact_t: post not valid");
                    return false;
                }
            }

            return true;
        }

        #region Lookup Functions

        private static Value GetWrapper(CallScope scope, Func<Xact, Value> func)
        {
            return func(ScopeExtensions.FindScope<Xact>(scope));
        }

        private static Value FnAny(CallScope args)
        {
            Post post = args.Context<Post>();
            ExprOp expr = args.Get<ExprOp>(0);

            foreach (Post p in post.Xact.Posts)
            {
                BindScope boundScope = new BindScope(args, p);
                if (expr.Calc(boundScope, args.Locus, args.Depth).AsBoolean)
                    return Value.True;
            }
            return Value.False;
        }

        private static Value FnAll(CallScope args)
        {
            Post post = args.Context<Post>();
            ExprOp expr = args.Get<ExprOp>(0);

            foreach (Post p in post.Xact.Posts)
            {
                BindScope boundScope = new BindScope(args, p);
                if (expr.Calc(boundScope, args.Locus, args.Depth).AsBoolean)
                    return Value.False;
            }
            return Value.True;
        }

        private static Value GetCode(Xact xact)
        {
            if (!String.IsNullOrEmpty(xact.Code))
                return Value.StringValue(xact.Code);
            else
                return Value.Empty;
        }

        private void CreateLookupItems()
        {
            // a
            LookupItems.MakeFunctor("any", scope => FnAny((CallScope)scope), SymbolKindEnum.FUNCTION);
            LookupItems.MakeFunctor("all", scope => FnAll((CallScope)scope), SymbolKindEnum.FUNCTION);

            // c
            LookupItems.MakeFunctor("code", scope => GetWrapper((CallScope)scope, x => GetCode(x)), SymbolKindEnum.FUNCTION);

            // m
            LookupItems.MakeFunctor("magnitude", scope => GetWrapper((CallScope)scope, x => x.Magnitude()), SymbolKindEnum.FUNCTION);

            // p
            LookupItems.MakeFunctor("p", scope => GetWrapper((CallScope)scope, x => Value.StringValue(x.Payee)), SymbolKindEnum.FUNCTION);
            LookupItems.MakeFunctor("payee", scope => GetWrapper((CallScope)scope, x => Value.StringValue(x.Payee)), SymbolKindEnum.FUNCTION);
        }

        #endregion

        private readonly ExprOpCollection LookupItems = new ExprOpCollection();
    }
}

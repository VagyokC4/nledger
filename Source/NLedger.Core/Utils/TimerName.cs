﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

namespace NLedger.Core.Utils
{
    public static class TimerName
    {
        public static readonly string Arguments = "arguments";
        public static readonly string Command = "command";
        public static readonly string Environment = "environment";
        public static readonly string Init = "init";
        public static readonly string InstanceParse = "instance_parse";
        public static readonly string Journal = "journal";
        public static readonly string PostDetails = "post_details";
        public static readonly string ParsingTotal = "parsing_total";
        public static readonly string Xacts = "xacts";
        public static readonly string XactPosts = "xact_posts";
        public static readonly string XactDetails = "xact_details";
        public static readonly string XactText = "xact_text";
    }
}

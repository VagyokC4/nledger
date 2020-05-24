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

namespace NLedger.Core.Utility
{
    public static class VirtualEnvironment
    {
        public static IDictionary<string,string> GetEnvironmentVariables()
        {
            return MainApplicationContext.Current.EnvironmentVariables;
        }

        public static string GetEnvironmentVariable(string variable)
        {
            return GetEnvironmentVariables().GetEnvironmentVariable(variable);
        }

        public static string GetEnvironmentVariable(this IDictionary<string, string> variables, string variable)
        {
            if (variables == null)
                return string.Empty;

            string value;
            if (!variables.TryGetValue(variable, out value))
                return string.Empty;

            return value ?? String.Empty;
        }
    }
}

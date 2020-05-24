﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

namespace NLedger.Core.Utility.Settings.CascadeSettings.Definitions
{
    public sealed class IntegerSettingDefinition : BaseSettingDefinition<int>
    {
        public IntegerSettingDefinition(string name, string description, int value, SettingScopeEnum scope = SettingScopeEnum.User)
            : base (name, description, value, scope)
        { }

        public override int ConvertFromString(string stringValue)
        {
            return int.Parse(stringValue);
        }
    }
}

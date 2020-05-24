// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System.Configuration;

namespace NLedger.Core.Utility.Settings.CascadeSettings.Sources
{
    /// <summary>
    ///     Returns app setting values specifies in app.config
    /// </summary>
    public sealed class SystemConfigurationSettingsSource : ISettingsSource
    {
        public SettingScopeEnum Scope => SettingScopeEnum.Application;

        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
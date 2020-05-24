﻿// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

namespace NLedger.Core.Utility.BigValues
{
    /// <summary>
    /// Helper functions that simplify dealing with IBigValue instances.
    /// </summary>
    /// <remarks>
    /// Though helper functions have easier syntax, they might show worse performance than 
    /// the original functions (that deal with ref/out parameters). Therefore, simplified functions
    /// are acceptable for testing purposes but require special considerations to use in an application code.
    /// </remarks>
    public static class BigValueExtensions
    {
        public static T Abs<T>(this T value) where T : IBigValue<T>
        {
            T result;
            value.Abs(out result);
            return result;
        }

        public static T Add<T>(this T left, T right) where T : IBigValue<T>
        {
            T result;
            left.Add(out result, ref right);
            return result;
        }

        public static T Ceiling<T>(this T value) where T : IBigValue<T>
        {
            T result;
            value.Ceiling(out result);
            return result;
        }

        public static int CompareTo<T>(this T value, T value2) where T : IBigValue<T>
        {
            return value.CompareTo(ref value2);
        }

        public static T Divide<T>(this T left, T right) where T : IBigValue<T>
        {
            T result;
            left.Divide(out result, ref right);
            return result;
        }

        public static T Floor<T>(this T value) where T : IBigValue<T>
        {
            T result;
            value.Floor(out result);
            return result;
        }

        public static T Multiply<T>(this T left, T right) where T : IBigValue<T>
        {
            T result;
            left.Multiply(out result, ref right);
            return result;
        }

        public static T Negate<T>(this T value) where T : IBigValue<T>
        {
            T result;
            value.Negate(out result);
            return result;
        }

        public static T Round<T>(this T value, int decimals = 0) where T : IBigValue<T>
        {
            T result;
            value.Round(out result, decimals);
            return result;
        }

        public static T Subtract<T>(this T left, T right) where T : IBigValue<T>
        {
            T result;
            left.Subtract(out result, ref right);
            return result;
        }

    }
}

// **********************************************************************************
// Copyright (c) 2015-2018, Dmitry Merzlyakov.  All rights reserved.
// Licensed under the FreeBSD Public License. See LICENSE file included with the distribution for details and disclaimer.
// 
// This file is part of NLedger that is a .Net port of C++ Ledger tool (ledger-cli.org). Original code is licensed under:
// Copyright (c) 2003-2018, John Wiegley.  All rights reserved.
// See LICENSE.LEDGER file included with the distribution for details and disclaimer.
// **********************************************************************************

using System.IO;
using System.Reflection;

namespace NLedger.Core.Utility
{
    /// <summary>
    ///     See: https://stackoverflow.com/questions/829568/how-to-know-positionlinenumber-of-a-streamreader-in-a-textfile
    /// </summary>
    public static class StreamReaderExtensions
    {
        private static readonly FieldInfo charPosField    = typeof(StreamReader).GetField("_charPos", BindingFlags.NonPublic | BindingFlags.Instance    | BindingFlags.DeclaredOnly);
        private static readonly FieldInfo byteLenField    = typeof(StreamReader).GetField("_byteLen", BindingFlags.NonPublic | BindingFlags.Instance    | BindingFlags.DeclaredOnly);
        private static readonly FieldInfo charBufferField = typeof(StreamReader).GetField("_charBuffer", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        static StreamReaderExtensions()
        {
        }

        public static long GetPosition(this StreamReader reader)
        {
            //shift position back from BaseStream.Position by the number of bytes read
            //into internal buffer.

            var byteLen  = (int) byteLenField.GetValue(reader);
            var position = reader.BaseStream.Position - byteLen;

            //if we have consumed chars from the buffer we need to calculate how many
            //bytes they represent in the current encoding and add that to the position.
            var charPos = (int) charPosField.GetValue(reader);
            if (charPos > 0)
            {
                var charBuffer    = (char[]) charBufferField.GetValue(reader);
                var encoding      = reader.CurrentEncoding;
                var bytesConsumed = encoding.GetBytes(charBuffer, 0, charPos).Length;
                position += bytesConsumed;
            }

            return position;
        }

        public static void SetPosition(this StreamReader reader, long position)
        {
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(position, SeekOrigin.Begin);
        }
    }
}
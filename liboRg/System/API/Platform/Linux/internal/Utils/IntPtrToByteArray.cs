//
//  IntPtrToByteArray.cs
//
//  Author:
//       Anna-Sophia Schröck <annasophia.schroeck@gmail.com>
//
//  Copyright (c) 2014 Anna-Sophia Schröck
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Runtime.InteropServices;

namespace System.API.Platform.Linux
{
	public static class XUtils
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct IntPtrToByteArray
		{
			[FieldOffset(0)]
			public IntPtr ptr;
			[FieldOffset(0)]
			public byte[] str;
		}
		public static TChar[] StringToSByteArray (string text)
		{
			if (string.IsNullOrEmpty (text))
				return new TChar[0];

			TChar[] result = new TChar[text.Length];
			for (int charIndex = 0;  charIndex < text.Length; charIndex++)
				result[charIndex] = (TChar)text[charIndex];

			return result;
		}

		public static string SByteArrayToString (sbyte[] text)
		{
			if (text.Length == 0)
				return "";

			string result = "";
			for (int charIndex = 0; charIndex < text.Length; charIndex++)
				result += (char)text[charIndex];

			return result;
		}

		public static Lib.XChar2b[] StringToXChar2bArray (string text)
		{
			if (string.IsNullOrEmpty(text))
				return new Lib.XChar2b[0];

			char[] buffer = text.ToCharArray();
			Lib.XChar2b[] result = new Lib.XChar2b[text.Length];
			for (int charIndex = 0; charIndex < buffer.Length; charIndex++)
			{
				result[charIndex].byte1 = (TUchar)0;
				result[charIndex].byte2 = (TUchar)buffer[charIndex];
			}
			return result;
		}
	}
}


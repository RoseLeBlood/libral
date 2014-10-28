//
//  Xkb.cs
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


//
// Übernommen aus opentk/opentk/blob/develop/Source/OpenTK/Platform/X11/Bindings/Xkb.cs   
//
// Xkb.cs
//
// Author:
//       Stefanos Apostolopoulos <stapostol@gmail.com>
//
// Copyright (c) 2006-2014 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
#pragma warning disable 0169

namespace System.API.Platform.Linux
{
	public class Xkb
	{
		public const int KeyNameLength = 4;
		public const int NumModifiers = 8;
		public const int NumVirtualMods = 16;
		public const int NumIndicators = 32;
		public const int NumKbdGroups = 4;
		public const int UseCoreKeyboard = 0x0100;

		[Flags]
		public enum XkbKeyboardMask
		{
			Controls = 1 << 0,
			ServerMap = 1 << 1,
			IClientMap = 1 << 2,
			IndicatorMap = 1 << 3,
			Names = 1 << 4,
			CompatibilityMap = 1 << 5,
			Geometry = 1 << 6,
			AllComponents = 1 << 7,
		}
		[Flags]
		public enum XkbNamesMask
		{
			Keycodes = 1 << 0,
			Geometry = 1 << 1,
			Symbols = 1 << 2,
			PhysSymbols = 1 << 3,
			Types = 1 << 4,
			Compat = 1 << 5,
			KeyType = 1 << 6,
			KTLevel = 1 << 7,
			Indicator = 1 << 8,
			Key = 1 << 9,
			KeyAliasesMask = 1 << 10,
			VirtualMod = 1 << 11,
			Group = 1 << 12,
			RG = 1 << 13,
			Component = 0x3f,
			All = 0x3fff
		}
		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct XkbDesc
		{
			public IntPtr dpy;
			public ushort flags;
			public ushort device_spec;
			public TUchar min_key_code;
			public TUchar max_key_code;

			public IntPtr ctrls;
			public IntPtr server;
			public IntPtr map;
			public IntPtr indicators;
			public XkbNames* names;
			public IntPtr compat;
			public IntPtr geom;
		}
		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct XkbKeyAlias
		{
			internal fixed byte real[Xkb.KeyNameLength];
			internal fixed byte alias[Xkb.KeyNameLength];
		}
		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct XkbKeyName
		{
			internal fixed byte name[Xkb.KeyNameLength];
		}
		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct XkbNames
		{
			[StructLayout(LayoutKind.Sequential)]
			public struct Groups
			{
				public IntPtr this[int i]
				{
					get
					{
						if (i < 0 || i > 3)
							throw new IndexOutOfRangeException();

						unsafe
						{
							fixed (IntPtr* ptr = &groups0)
							{
								return *(ptr + i);
							}
						}
					}
				}
				IntPtr groups0;
				IntPtr groups1;
				IntPtr groups2;
				IntPtr groups3;
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct Indicators
			{
				public IntPtr this[int i]
				{
					get
					{
						if (i < 0 || i > 31)
							throw new IndexOutOfRangeException();

						unsafe
						{
							fixed (IntPtr* ptr = &indicators0)
							{
								return *(ptr + i);
							}                    }
					}
				}
				IntPtr indicators0;
				IntPtr indicators1;
				IntPtr indicators2;
				 IntPtr indicators3;
				 IntPtr indicators4;
				 IntPtr indicators5;
				 IntPtr indicators6;
				 IntPtr indicators7;
				 IntPtr indicators8;
				 IntPtr indicators9;
				 IntPtr indicators10;
				 IntPtr indicators11;
				 IntPtr indicators12;
				 IntPtr indicators13;
				 IntPtr indicators14;
				 IntPtr indicators15;
				 IntPtr indicators16;
				 IntPtr indicators17;
				 IntPtr indicators18;
				 IntPtr indicators19;
				 IntPtr indicators20;
				 IntPtr indicators21;
				 IntPtr indicators22;
				 IntPtr indicators23;
				 IntPtr indicators24;
				 IntPtr indicators25;
				 IntPtr indicators26;
				 IntPtr indicators27;
				 IntPtr indicators28;
				 IntPtr indicators29;
				 IntPtr indicators30;
				 IntPtr indicators31;

			}
			[StructLayout(LayoutKind.Sequential)]
			public  struct VMods
			{
				public IntPtr this[int i]
				{
					get
					{
						if (i < 0 || i > 15)
							throw new IndexOutOfRangeException();

						unsafe
						{
							fixed (IntPtr* ptr = &vmods0)
							{
								return *(ptr + i);
							}
						}
					}
				}

				 IntPtr vmods0;
				 IntPtr vmods1;
				 IntPtr vmods2;
				 IntPtr vmods3;
				 IntPtr vmods4;
				 IntPtr vmods5;
				 IntPtr vmods6;
				 IntPtr vmods7;
				 IntPtr vmods8;
				 IntPtr vmods9;
				 IntPtr vmods10;
				 IntPtr vmods11;
				 IntPtr vmods12;
				 IntPtr vmods13;
				 IntPtr vmods14;
				IntPtr vmods15;
			}
			public  IntPtr keycodes;
			public  IntPtr geometry;
			public  IntPtr symbols;
			public  IntPtr types;
			public  IntPtr compat;
			public  VMods vmods;
			public  Indicators indicators;
			public  Groups groups;
			public  XkbKeyName* keys;
			public  XkbKeyAlias* key_aliases;
			public  IntPtr *radio_groups;
			public  IntPtr phys_symbols;

			public  byte num_keys;
			public  byte num_key_aliases;
			public  byte num_rg;
		}


		public static bool IsSupported(IntPtr display)
		{
			// The XkbQueryExtension manpage says that we cannot
			// use XQueryExtension with XKB.
			int opcode, error, ev;
			int major = 1;
			int minor = 0;
			bool supported = XkbQueryExtension(display, out opcode, out ev, out error, ref major, ref minor);
			#if DEBUG
			if (supported)
			{
					Console.WriteLine("XKB ({0}.{1}) extension found ", major, minor);
			}
			#endif
			return supported;
		}

		[DllImport("libX11")]
		unsafe public extern static void XkbFreeKeyboard(XkbDesc* descr, int which, bool free);

		[DllImport("libX11")]
		unsafe public extern static XkbDesc * XkbAllocKeyboard(IntPtr display);

		[DllImport("libX11")]
		public extern static IntPtr XkbGetKeyboard(IntPtr display, XkbKeyboardMask which, int device_id);

		[DllImport("libX11")]
		public extern static IntPtr XkbGetMap(IntPtr display, XkbKeyboardMask which, int device_spec); 

		[DllImport("libX11")]
		unsafe public extern static IntPtr XkbGetNames(IntPtr display, XkbNamesMask which, XkbDesc* xkb); 

		[DllImport("libX11")]
		public extern static TUint XkbKeycodeToKeysym(IntPtr display, int keycode, int group, int level);

		[DllImport("libX11")]
		public extern static bool XkbQueryExtension(IntPtr display, out int opcode_rtrn, out int event_rtrn,
			out int error_rtrn, ref int major_in_out, ref int minor_in_out);

		[DllImport("libX11")]
		public extern static bool XkbSetDetectableAutoRepeat(IntPtr display, bool detectable, out bool supported);

	}
}


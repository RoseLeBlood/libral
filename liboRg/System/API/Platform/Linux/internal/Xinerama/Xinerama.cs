//
//  Xinerama.cs
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
using System.Collections.Generic;

namespace System.API.Platform.Linux
{


	public class Xinerama
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct XineramaScreenInfo
		{
			public int ScreenNumber;
			public short X;
			public short Y;
			public short Width;
			public short Height;
		}

		public static List<XineramaScreenInfo> XineramaQueryScreens(IntPtr x11display, out int nsizes)
		{
			int number = 0;
			IntPtr screen_ptr = _iXineramaQueryScreens(x11display, out number);
			List<XineramaScreenInfo> screens = new List<XineramaScreenInfo>(number);
			nsizes = number;
			unsafe
			{
				XineramaScreenInfo* ptr = (XineramaScreenInfo*)screen_ptr;
				while (--number >= 0)
					{
						screens.Add(*ptr);
						ptr++;
					}
			}

			return screens;
		}

		[DllImport("libXinerama")]
		public static extern bool XineramaQueryExtension(IntPtr x11display, out int event_basep, out int error_basep);

		[DllImport("libXinerama")]
		public static extern int XineramaQueryVersion (IntPtr x11display, out int major_versionp, out int minor_versionp);

		[DllImport("libXinerama")]
		public static extern bool XineramaIsActive(IntPtr x11display);

		[DllImport("libXinerama", EntryPoint="XineramaQueryScreens") ]
		internal static extern IntPtr _iXineramaQueryScreens(IntPtr x11display, out int number);


	}
}


//
//  XVisualInfo.cs
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
	[StructLayout(LayoutKind.Sequential)]
	public struct XVisualInfo
	{
		public IntPtr Visual;
		public IntPtr VisualID;
		public int Screen;
		public int Depth;
		public int Class;
		public long RedMask;
		public long GreenMask;
		public long blueMask;
		public int ColormapSize;
		public int BitsPerRgb;

		public override string ToString()
		{
			return String.Format("VisualID: {0}, RedMask: {1} GreenMask: {2} blueMask: {3}  Depth: {4}",
				VisualID,RedMask, GreenMask, blueMask, Depth);
		}
	}
}


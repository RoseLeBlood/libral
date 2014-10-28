//
//  Colormap.cs
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
using System.Collections;
using System.Common;

namespace System.API.Platform.Linux.Widgets
{
	public static class Colormap
	{
		private static Hashtable cachedPixels = new Hashtable();

		public static Lib.XColor ToXColor(this Color color, IDisplay display)
		{
			return ToXColor(color, display, display.Screen, display.Screen.DefaultColormap);
		}
		public static Lib.XColor ToXColor(this Color color, IDisplay display, IScreen screen)
		{
			return ToXColor(color, display, display.Screen, screen.DefaultColormap);
		}
		public static Lib.XColor ToXColor(this Color color, IDisplay display, IScreen screen, int colormap)
		{
			try 
			{
				Object cached = cachedPixels[color];
				if (cached != null)
				{
					return (Lib.XColor)(cached);
				}

				var col = new Lib.XColor();

				col.red = (ushort)(color.R << 8);
				col.green = (ushort)(color.G << 8);
				col.blue = (ushort)(color.B << 8);
				col.flags = Lib.XColor.DoRed | Lib.XColor.DoGreen | Lib.XColor.DoBlue;

				if (0 != Lib.XAllocColor(display.RawHandle, colormap, ref col))
				{
					cachedPixels[color] = col;
						return col;
				}
			}
			catch
			{

			}
			return new Lib.XColor();
		}
	}
}


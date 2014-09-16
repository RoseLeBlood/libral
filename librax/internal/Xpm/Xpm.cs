//
//  Xpm.cs
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

namespace X11._internal
{
	public class Xpm
	{

		[Flags]
		public enum XpmResult : int
		{
			XpmColorError					= 1,
			XpmSuccess						= 0,
			XpmOpenFailed					= -1,
			XpmFileInvalid					= -2,
			XpmNoMemory						= -3,
			XpmColorFailed					= -4
		}


		[Flags]
		public enum XpmValueMask : long
		{
			XpmVisual						= (1L<<0),	/* visual                        | Default value is: DefaultVisual(display, DefaultScreen(display)) */
			XpmColormap						= (1L<<1),	/* colormap                      | Default value is: DefaultColormap(display, DefaultScreen(display)) */
			XpmDepth						= (1L<<2),	/* depth                         | Default value is: DefaultDepth(display, DefaultScreen(display)) */
			XpmSize							= (1L<<3),	/* width, height                 | Set when creating an XImage or a Pixmap. */
			XpmHotspot						= (1L<<4),	/* x_hotspot, y_hotspot          | Set if hotspot coordinates are found when parsing. */
			XpmCharsPerPixel				= (1L<<5),	/* cpp */
			XpmColorSymbols					= (1L<<6),	/* colorsymbols, numcolorsymbols */
			XpmRgbFilename					= (1L<<7),	/* rgb_fname */
			XpmInfos						= (1L<<8),	/* cpp, pixels, npixels, colorTable, ncolors, hints_cmt, colors_cmt, pixels_cmt, mask_pixel | Obsolete; colorTable cast to (XpmColor **) */
			XpmPixels						= (1L<<9),	/* pixels, npixels               | npixels differs from ncolors if several colors are bound to the same pixel, and if there is a mask (color = None). */
			XpmExtensions					= (1L<<10),	/* extensions, nextensions */
			XpmExactColors					= (1L<<11),	/* exactColors                   | Possible values are False (0) or True (1) */
			XpmCloseness					= (1L<<12),	/* closeness                     | Possible values are integers within the range: 0 to 65535 */
			XpmRGBCloseness					= (1L<<13),	/* red_closeness, green_closeness, blue_closeness | Possible values are integers within the range: 0 to 65535 */
			XpmColorKey						= (1L<<14), /* color_key                     | Possible values are: XPM_MONO, XPM_GRAY4, XPM_GRAY, XPM_COLOR */
			XpmColorTable					= (1L<<15),	/* colorTable, ncolors */
			XpmReturnAllocPixels			= (1L<<16)	/* alloc_pixels, nalloc_pixels   | nalloc_pixels differs from npixels when one pixel, given through the XpmColorSymbols, is used */
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct XpmAttributes
		{
			public XpmValueMask valuemask;
			public IntPtr visual;
			public IntPtr colormap;
			public int depth;
			public int width;
			public int height;
			public int x_hotspot;
			public int y_hotspot;
			public int cpp;
			public IntPtr pixels;
			public int npixels;
			public IntPtr colorsymbols;
			public int numsymbols;
			public string rgb_fname;
			public int nextensions;
			public IntPtr extensions;
			public int ncolors;
			public IntPtr colorTable;
			/* 3.2 backward compatibility code */
			public string hints_cmt;
			public string colors_cmt;
			public string pixels_cmt;
			/* end 3.2 bc */
			public int mask_pixel;
			/* Color Allocation Directives */
			public bool exactColors;
			public int closeness;
			public int red_closeness;
			public int green_closeness;
			public int blue_closeness;
			public int color_key;
			public IntPtr alloc_pixels;
			public int nalloc_pixels;
			public bool alloc_close_colors;
			public int bitmap_format;
			/* Color functions */
			public IntPtr alloc_color;
			public IntPtr free_colors;
			public IntPtr color_closure;
		}
		[DllImport("libXpm")]
		extern public static XpmResult XpmCreatePixmapFromData(IntPtr x11display, IntPtr x11drawable, IntPtr data, ref IntPtr pixmap, ref IntPtr shapemask, ref IntPtr attributes);
		[DllImport("libXpm")]
		extern public static int XpmReadFileToPixmap (IntPtr x11display, IntPtr x11drawable, string filename, out IntPtr pixmap_return, out int shapemask_return, ref XpmAttributes attributes);
		[DllImport("libXpm")]
		extern public static void XpmFreeAttributes	(ref XpmAttributes attributes);		

	}
}


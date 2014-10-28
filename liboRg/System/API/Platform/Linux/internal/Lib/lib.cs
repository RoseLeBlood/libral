//
//  lib.cs
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
	public class Lib
	{
		public const int CurrentTime = 0;

		[Flags]
		public enum GCattributemask : long
		{
			Function						= (1L<<0),
			PlaneMask						= (1L<<1),
			Foreground						= (1L<<2),
			Background						= (1L<<3),
			LineWidth						= (1L<<4),
			LineStyle						= (1L<<5),
			CapStyle						= (1L<<6),
			JoinStyle						= (1L<<7),
			FillStyle						= (1L<<8),
			FillRule						= (1L<<9),
			Tile							= (1L<<10),
			Stipple							= (1L<<11),
			TileStipXOrigin					= (1L<<12),
			TileStipYOrigin					= (1L<<13),
			Font							= (1L<<14),
			SubwindowMode					= (1L<<15),
			GraphicsExposures				= (1L<<16),
			ClipXOrigin						= (1L<<17),
			ClipYOrigin						= (1L<<18),
			ClipMask						= (1L<<19),
			DashOffset						= (1L<<20),
			DashList						= (1L<<21),
			ArcMode							= (1L<<22)
		}
		public enum GrabMode : int
		{
			Sync = 0,
			Async = 1,
		}
		public enum WindowClass : uint
		{
			CopyFromParent					= 0,
			InputOutput						= 1,
			InputOnly						= 2
		}

		public enum TRevertTo : int
		{
			RevertToNone					= 0,
			RevertToPointerRoot				= 1,
			RevertToParent					= 2
		}

		public enum TImageFormat : int
		{
			XYBitmap						= 0,
			XYPixmap						= 1,
			ZPixmap							= 2
		}
		[Flags]
		public enum WindowAttributeMask : uint
		{
			BackPixmap						= (1<<0),
			BackPixel						= (1<<1),
			BorderPixmap					= (1<<2),
			BorderPixel						= (1<<3),
			BitGravity						= (1<<4),
			WinGravity						= (1<<5),
			BackingStore					= (1<<6),
			BackingPlanes					= (1<<7),
			BackingPixel					= (1<<8),
			OverrideRedirect				= (1<<9),
			SaveUnder						= (1<<10),
			EventMask						= (1<<11),
			DontPropagate					= (1<<12),
			Colormap						= (1<<13),
			Cursor							= (1<<14)
		}

		public struct XWindowAttributes
		{
			public TInt						x, y;					/* location of window */
			public TInt						width, height;			/* width and height of window */
			public TInt						border_width;			/* border width of window */
			public TInt						depth;					/* depth of window */
			public IntPtr					visual;					/* Visual: the associated visual structure */
			public IntPtr					root;					/* Window: root of screen containing window */
			public TInt						cls;					/* InputOutput, InputOnly*/
			public TInt						bit_gravity;			/* one of the bit gravity values */
			public TInt						win_gravity;			/* one of the window gravity values */
			public TInt						backing_store;			/* NotUseful, WhenMapped, Always */
			public TUlong					backing_planes;			/* planes to be preserved if possible */
			public TUlong					backing_pixel;			/* value to be used when restoring planes */
			public TBoolean					save_under;				/* boolean, should bits under be saved? */
			public IntPtr					colormap;				/* Colormap: color map to be associated with window */
			public TBoolean					map_installed;			/* boolean, is color map currently installed*/
			public TInt						map_state;				/* IsUnmapped, IsUnviewable, IsViewable */
			public TLong					all_event_masks;		/* set of events all people have interest in*/
			public TLong					your_event_mask;		/* my event mask */
			public TLong					do_not_propagate_mask;	/* set of events that should not propagate */
			public TBoolean					override_redirect;		/* boolean value for override-redirect */
			public IntPtr					screen;					/* Screen: back pointer to correct screen */
		}
		public struct XSetWindowAttributes
		{
			public IntPtr					background_pixmap;		/* background, None, or ParentRelative */
			public TPixel					background_pixel;		/* background pixel */ 
			public IntPtr					border_pixmap;			/* border of the window or CopyFromParent */ 
			public TPixel					border_pixel;			/* border pixel value */ 
			public TInt						bit_gravity;			/* one of bit gravity values */ 
			public TInt						win_gravity;			/* one of the window gravity values */
			public TInt						backing_store;			/* NotUseful, WhenMapped, Always */
			public TUlong					backing_planes;			/* planes to be preserved if possible */ 
			public TUlong					backing_pixel;			/* value to use in restoring planes */ 
			public TBoolean					save_under;				/* should bits under be saved? (popups) */
			public TLong					event_mask;				/* set of events that should be saved */
			public TLong					do_not_propagate_mask;	/* set of events that should not propagate */ 
			public TBoolean					override_redirect;		/* boolean value for override_redirect */ 
			public IntPtr					colormap;				/* color map to be associated with window */
			public IntPtr					cursor;					/* cursor to be displayed (or None) */

		}

		[Flags]
		public enum XGCValueMask :uint
		{
			Function			= (1<<0),
			PlaneMask			= (1<<1),
			Foreground			= (1<<2),
			Background			= (1<<3),
			LineWidth			= (1<<4),
			LineStyle			= (1<<5),
			CapStyle			= (1<<6),
			JoinStyle			= (1<<7),
			FillStyle			= (1<<8),
			FillRule			= (1<<9),
			Tile				= (1<<10),
			Stipple				= (1<<11),
			TileStipXOrigin		= (1<<12),
			TileStipYOrigin		= (1<<13),
			Font				= (1<<14),
			SubwindowMode		= (1<<15),
			GraphicsExposures	= (1<<16),
			ClipXOrigin			= (1<<17),
			ClipYOrigin			= (1<<18),
			ClipMask			= (1<<19),
			DashOffset			= (1<<20),
			DashList			= (1<<21),
			ArcMode				= (1<<22),
		}

		public enum XGCFunction : int
		{
			clear				= 0x0,       /* 0 */
			and				= 0x1,       /* src AND dst */
			andReverse		= 0x2,       /* src AND NOT dst */
			copy				= 0x3,       /* src */
			andInverted		= 0x4,       /* NOT src AND dst */
			noop				= 0x5,       /* dst */
			Xxor				= 0x6,       /* src XOR dst */
			Xor				= 0x7,       /* src OR dst */
			Xnor				= 0x8,       /* NOT src AND NOT dst */
			Xequiv				= 0x9,       /* NOT src XOR dst */
			Xinvert			= 0xa,       /* NOT dst */
			XorReverse			= 0xb,       /* src OR NOT dst */
			XcopyInverted		= 0xc,       /* NOT src */
			XorInverted		= 0xd,       /* NOT src OR dst */
			Xnand				= 0xe,       /* NOT src OR NOT dst */
			Xset				= 0xf        /* 1 */
		}

		public struct XGCValues
		{
			public XGCFunction	function;			/* logical operation */
			public TPixel		plane_mask;			/* plane mask */
			public TPixel		foreground;			/* foreground pixel */
			public TPixel		background;			/* background pixel */
			public TInt			line_width;			/* line width (in pixels) */
			public TInt			line_style;			/* LineSolid, LineOnOffDash, LineDoubleDash */
			public TInt			cap_style;			/* CapNotLast, CapButt, CapRound, CapProjecting */
			public TInt			join_style;			/* JoinMiter, JoinRound, JoinBevel */
			public TInt			fill_style;			/* FillSolid, FillTiled, FillStippled FillOpaqueStippled*/
			public TInt			fill_rule;			/* EvenOddRule, WindingRule */
			public TInt			arc_mode;			/* ArcChord, ArcPieSlice */
			public IntPtr		tile;				/* tile pixmap for tiling operations */
			public IntPtr		stipple;			/* stipple 1 plane pixmap for stippling */
			public TInt			ts_x_origin;		/* offset for tile or stipple operations */
			public TInt			ts_y_origin;
			public IntPtr		font;				/* default text font for text operations */
			public TInt			subwindow_mode;		/* ClipByChildren, IncludeInferiors */
			public TBoolean		graphics_exposures;	/* boolean, should exposures be generated */
			public TInt			clip_x_origin;		/* origin for clipping */
			public TInt			clip_y_origin;
			public IntPtr		clip_mask;			/* bitmap clipping; other calls for rects */
			public TInt			dash_offset;		/* patterned/dashed line information */
			public TChar		dashes;
		}

		public enum CursorFontShape : uint
		{
			X_cursor				= 0,
			arrow				= 2,
			based_arrow_down		= 4,
			based_arrow_up		= 6,
			boat					= 8,
			bogosity				= 10,
			bottom_left_corner	= 12,
			bottom_right_corner	= 14,
			bottom_side			= 16,
			bottom_tee			= 18,
			box_spiral			= 20,
			center_ptr			= 22,
			circle				= 24,
			clock				= 26,
			coffee_mug			= 28,
			cross				= 30,
			cross_reverse		= 32,
			crosshair			= 34,
			diamond_cross		= 36,
			dot					= 38,
			dot_box_mask			= 40,
			double_arrow			= 42,
			draft_large			= 44,
			draft_small			= 46,
			draped_box			= 48,
			exchange				= 50,
			fleur				= 52,
			gobbler				= 54,
			gumby				= 56,
			hand1				= 58,
			hand2				= 60,
			heart				= 62,
			icon					= 64,
			iron_cross			= 66,
			left_ptr				= 68,
			left_side			= 70,
			left_tee				= 72,
			leftbutton			= 74,
			ll_angle				= 76,
			lr_angle				= 78,
			man					= 80,
			middlebutton			= 82,
			mouse				= 84,
			pencil				= 86,
			pirate				= 88,
			plus					= 90,
			question_arrow		= 92,
			right_ptr			= 94,
			right_side			= 96,
			right_tee			= 98,
			rightbutton			= 100,
			rtl_logo				= 102,
			sailboat				= 104,
			sb_down_arrow		= 106,
			sb_h_double_arrow	= 108,
			sb_left_arrow		= 110,
			sb_right_arrow		= 112,
			sb_up_arrow			= 114,
			sb_v_double_arrow	= 116,
			shuttle				= 118,
			sizing				= 120,
			spider				= 122,
			spraycan				= 124,
			star					= 126,
			target				= 128,
			tcross				= 130,
			top_left_arrow		= 132,
			top_left_corner		= 134,
			top_right_corner		= 136,
			top_side				= 138,
			top_tee				= 140,
			trek					= 142,
			ul_angle				= 144,
			umbrella				= 146,
			ur_angle				= 148,
			watch				= 150,
			xterm				= 152,
		}
		[Flags()]
		public enum AtomType
		{
			PRIMARY = 1,
			SECONDARY = 2,
			ARC = 3,
			ATOM = 4,
			BITMAP = 5,
			CARDINAL = 6,
			COLORMAP = 7,
			CURSOR = 8,
			CUT_BUFFER0 = 9,
			CUT_BUFFER1 = 10,
			CUT_BUFFER2 = 11,
			CUT_BUFFER3 = 12,
			CUT_BUFFER4 = 13,
			CUT_BUFFER5 = 14,
			CUT_BUFFER6 = 15,
			CUT_BUFFER7 = 16,
			DRAWABLE = 17,
			FONT = 18,
			INTEGER = 19,
			PIXMAP = 20,
			POINT = 21,
			RECTANGLE = 22,
			RESOURCE_MANAGER = 23,
			RGB_COLOR_MAP = 24,
			RGB_BEST_MAP = 25,
			RGB_BLUE_MAP = 26,
			RGB_DEFAULT_MAP = 27,
			RGB_GRAY_MAP = 28,
			RGB_GREEN_MAP = 29,
			RGB_RED_MAP = 30,
			STRING = 31,
			VISUALID = 32,
			WINDOW = 33,
			WM_COMMAND = 34,
			WM_HINTS = 35,
			WM_CLIENT_MACHINE = 36,
			WM_ICON_NAME = 37,
			WM_ICON_SIZE = 38,
			WM_NAME = 39,
			WM_NORMAL_HINTS = 40,
			WM_SIZE_HINTS = 41,
			WM_ZOOM_HINTS = 42,
			MIN_SPACE = 43,
			NORM_SPACE = 44,
			MAX_SPACE = 45,
			END_SPACE = 46,
			SUPERSCRIPT_X = 47,
			SUPERSCRIPT_Y = 48,
			SUBSCRIPT_X = 49,
			SUBSCRIPT_Y = 50,
			UNDERLINE_POSITION = 51,
			UNDERLINE_THICKNESS = 52,
			STRIKEOUT_ASCENT = 53,
			STRIKEOUT_DESCENT = 54,
			ITALIC_ANGLE = 55,
			X_HEIGHT = 56,
			QUAD_WIDTH = 57,
			WEIGHT = 58,
			POINT_SIZE = 59,
			RESOLUTION = 60,
			COPYRIGHT = 61,
			NOTICE = 62,
			FONT_NAME = 63,
			FAMILY_NAME = 64,
			FULL_NAME = 65,
			CAP_HEIGHT = 66,
			WM_CLASS = 67,
			WM_TRANSIENT_FOR = 68
		}
		[Flags()]
		public enum PropMode
		{
			Replace = 0,
			Prepend = 1,
			Append = 2
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XCharStruct
		{
			public TShort lbearing;			/* origin to left edge of raster */
			public TShort rbearing;			/* origin to right edge of raster */
			public TShort width;			/* advance to next char's origin */
			public TShort ascent;			/* baseline to top edge of raster */
			public TShort descent;			/* baseline to bottom edge of raster */
			public TUchar attributes;		/* per char flags (not predefined) */
		}


		[StructLayout(LayoutKind.Sequential)]
		public struct XChar2b /* normal 16 bit characters are two bytes */
		{
			public TUchar byte1;
			public TUchar byte2;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XColor
		{
			public TPixel	    pixel;
			public ushort		red, green, blue;
			public sbyte		flags;
			public sbyte		pad;

			public const int DoRed   = (1<<0);
			public const int DoGreen = (1<<1);
			public const int DoBlue  = (1<<2);
		};

		public static XColor NewXColor(TPixel pixel, ushort r, ushort g, ushort b, sbyte flags, sbyte pad)
		{
			XColor result = new XColor();
			result.pixel = pixel;
			result.red = r;
			result.green = g;
			result.blue = b;
			result.flags = flags;
			result.pad = pad;

			return result;
		}
			
		[StructLayout(LayoutKind.Sequential)]
		private struct _XClassHint
		{
			public IntPtr res_name;
			public IntPtr res_class;

			public static _XClassHint Zero = new _XClassHint ();
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XClassHint
		{
			[MarshalAs(UnmanagedType.LPStr)] public string res_name;
			[MarshalAs(UnmanagedType.LPStr)] public string res_class;

			public static XClassHint Zero = new XClassHint ();
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XTextProperty
		{
			public	IntPtr		val;			/* property data */
			public	IntPtr		encoding;		/* type of property */
			public	TInt		format;			/* 8, 16, or 32 */
			public	TUlong		nitems;			/* number of items in value */
		}
		public enum XWindowState
		{
			WithdrawnState = 0,
			NormalState = 1,
			IconicState = 3
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct XWMHints
		{
			public	XWMHintMask	flags;			/* marks which fields in this structure are defined */
			public	TBoolean	input;			/* does this application rely on the window manager to get keyboard input? */
			public	XWindowState		initial_state;	/*	WithdrawnState	0,	NormalState	1,	IconicState	3 */
			public	IntPtr		icon_pixmap;	/* Pixmap: pixmap to be used as icon */
			public	IntPtr		icon_window;	/* Window: window to be used as icon */
			public	TInt		icon_x, icon_y;	/* initial position of icon */
			public	IntPtr		icon_mask;		/* Pixmap: pixmap to be used as mask for icon_pixmap */
			public	IntPtr		window_group;	/* XID: id of related window group */
		}

		public enum XWMHintMask : int

		{
			InputHint			= (1 << 0),
			StateHint			= (1 << 1),
			IconPixmapHint		= (1 << 2),
			IconWindowHint		= (1 << 3),
			IconPositionHint	= (1 << 4),
			IconMaskHint		= (1 << 5),
			WindowGroupHint		= (1 << 6),
			UrgencyHint			= (1 << 8),
			AllHints	 		= (InputHint|StateHint|IconPixmapHint|IconWindowHint|IconPositionHint|IconMaskHint|WindowGroupHint)
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct XAspect
		{
			public int x;
			public int y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XSizeHints
		{
			public XSizeHintFlags flags;
			public int x, y;
			public int width;
			public int height;
			public int min_width;
			public int min_height;
			public int max_width;
			public int max_height;
			public int width_inc;
			public int height_inc;
			public XAspect min_aspect;
			public XAspect max_aspect;
			public int base_width;
			public int base_height;
			public int win_gravity;
		}

		[Flags()]
		public enum XSizeHintFlags
		{
			USPosition = (1 << 0),
			USSize = (1 << 1),
			PPosition = (1 << 2),
			PSize = (1 << 3),
			PMinSize = (1 << 4),
			PMaxSize = (1 << 5),
			PResizeInc = (1 << 6),
			PAspect = (1 << 7),
			PBaseSize = (1 << 8),
			PWinGravity = (1 << 9)
		}

		public enum XKeySym : uint
		{
			BackSpace = 0xFF08,
			Tab = 0xFF09,
			Clear = 0xFF0B,
			Return = 0xFF0D,
			Home = 0xFF50,
			Left = 0xFF51,
			Up = 0xFF52,
			Right = 0xFF53,
			Down = 0xFF54,
			Page_Up = 0xFF55,
			Page_Down = 0xFF56,
			End = 0xFF57,
			Begin = 0xFF58,
			Menu = 0xFF67,
			Shift_L = 0xFFE1,
			Shift_R = 0xFFE2,
			Control_L = 0xFFE3,
			Control_R = 0xFFE4,
			Caps_Lock = 0xFFE5,
			Shift_Lock = 0xFFE6,
			Meta_L = 0xFFE7,
			Meta_R = 0xFFE8,
			Alt_L = 0xFFE9,
			Alt_R = 0xFFEA,
			Super_L = 0xFFEB,
			Super_R = 0xFFEC,
			Hyper_L = 0xFFED,
			Hyper_R = 0xFFEE,
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XVisualInfo
		{
			public	IntPtr		visual;
			public	TLong		visualid;
			public	TInt		screen;
			public	TInt		depth;
			public	TInt		cls;
			public	TUlong		red_mask;
			public	TUlong		green_mask;
			public	TUlong		blue_mask;
			public	TInt		colormap_size;
			public	TInt		bits_per_rgb;
		}

		public static int XAllocClassHint (out XClassHint classHint)
		{
			classHint = new XClassHint ();
			return 1;
		}
		public static int XGetClassHint (IntPtr x11display, IntPtr x11window, out XClassHint classHint)
		{
			_XClassHint _classHint = _XClassHint.Zero;

			if (_XGetClassHint(x11display, x11window, ref _classHint) == (TInt)0)
			{
				XFree(_classHint.res_name);

				classHint = XClassHint.Zero;
				return 0;
			}
			else
			{
				classHint = new XClassHint();
				classHint.res_name = (string)Marshal.PtrToStringAnsi(_classHint.res_name);
				classHint.res_class = (string)Marshal.PtrToStringAnsi(_classHint.res_class);

				XFree(_classHint.res_name);
				XFree(_classHint.res_class);

				return 1;
			}
		}
		public static void XSetWMName(IntPtr x11display, IntPtr x11window, string windowTitle)
		{
			Lib.XTextProperty titleNameProp = new Lib.XTextProperty ();
			TChar[]              titleNameVal  = XUtils.StringToSByteArray (windowTitle + "\0");

			IntPtr p = Marshal.AllocHGlobal (Marshal.SizeOf (titleNameProp));

			if (Lib.XStringListToTextProperty (ref titleNameVal, (TInt)1, p) != (TInt)0)
				{
					Lib._XSetWMName (x11display, x11window, p);
					titleNameProp = (Lib.XTextProperty)Marshal.PtrToStructure (p, typeof(Lib.XTextProperty));
					XFree (titleNameProp.val);
					titleNameProp.val = IntPtr.Zero;
				}
			else
				Console.WriteLine ("X11::Lib::XSetWMName () ERROR: Can not set window name.");
		}
		public static void XSetWMIconName(IntPtr x11display, IntPtr x11window, string windowIconTitle)
		{
			Lib.XTextProperty iconNameProp  = new Lib.XTextProperty ();
			TChar[]              iconNameVal   = XUtils.StringToSByteArray (windowIconTitle + "\0");

			IntPtr p = Marshal.AllocHGlobal (Marshal.SizeOf (iconNameProp));
			if (Lib.XStringListToTextProperty (ref iconNameVal, (TInt)1, p) != (TInt)0)
				{
					Lib._XSetWMIconName (x11display, x11window, p);
					iconNameProp = (Lib.XTextProperty)Marshal.PtrToStructure (p, typeof(Lib.XTextProperty));
					Lib.XFree (iconNameProp.val);
					iconNameProp.val = IntPtr.Zero;
				}
			else
				Console.WriteLine ("X11::Lib::XSetWMIconName () ERROR: Can not set window's icon name.");
		}
		public static TPixel XAllocParsedColorByName (IntPtr x11display, TInt screenNumber, [MarshalAs(UnmanagedType.LPStr)] string colorname)
		{
			if (x11display == IntPtr.Zero)
				return (TPixel)1;

			Lib.XColor tmp = new Lib.XColor ();
			Lib.XParseColor (x11display, Lib.XDefaultColormap (x11display, screenNumber), colorname, ref tmp);
			Lib.XAllocColor (x11display, Lib.XDefaultColormap (x11display, screenNumber), ref tmp);
			return tmp.pixel;
		}
		[DllImport ("libX11")]
		public extern static void XWarpPointer(IntPtr x11display, IntPtr src_x11window, IntPtr dest_x11window, int src_x, int src_y, uint src_width, uint src_height, int dest_x, int dest_y);
		[DllImport ("libX11")]
		public extern static IntPtr XCreateFontCursor (IntPtr x11display, CursorFontShape cursorFontShape);
		[DllImport ("libX11")]
		public extern static void XDefineCursor (IntPtr x11display, IntPtr x11window, IntPtr cursor);
		[DllImport("libX11")]
		public extern static void XUndefineCursor(IntPtr x11display, IntPtr x11window);

		[DllImport("libX11")]
		public extern static IntPtr XCreatePixmapCursor(IntPtr x11display, IntPtr cursorPixmap, IntPtr cursorPixmap2, ref Lib.XColor cursorColor, ref Lib.XColor cursorColor2, TInt i, TInt i2);

		[DllImport("libX11")]
		extern public static IntPtr XOpenDisplay (String x11displayName);
		[DllImport("libX11")]
		internal extern static TInt XDisplayHeight(IntPtr x11window, TInt screenNumber);
		[DllImport("libX11")]
		internal extern static TInt XDisplayWidth(IntPtr x11window, TInt screenNumber);
		[DllImport("libX11")]
		extern public static void XCloseDisplay (IntPtr x11display);
		[DllImport("libX11")]
		extern public static IntPtr XRootWindow (IntPtr x11display, TInt screenNumber);
		[DllImport("libX11")]
		extern public static IntPtr XRootWindowOfScreen (IntPtr x11screen);
		[DllImport("libX11")]
		extern public static  void XDestroyWindow (IntPtr x11display, IntPtr x11window);
		[DllImport("libX11")]
		extern public static IntPtr XDefaultRootWindow (IntPtr x11display);
		[DllImport("libX11")]
		extern public static void XMoveWindow (IntPtr x11display, IntPtr x11window, TInt x, TInt y);
		[DllImport("libX11")]
		extern public static void XResizeWindow (IntPtr x11display, IntPtr x11window, TUint width, TUint height);
		[DllImport("libX11")]
		extern public static void XMoveResizeWindow (IntPtr x11display, IntPtr x11window, TInt x, TInt y, TUint width, TUint height);
		[DllImport("libX11")]
		extern public static void XMapWindow (IntPtr x11display, IntPtr x11window);
		[DllImport("libX11")]
		extern public static void XUnmapWindow (IntPtr x11display, IntPtr x11window);
		[DllImport("libX11")]
		extern public static void XMapRaised(IntPtr x11display, IntPtr x11window);
		[DllImport("libX11")]
		extern public static IntPtr XCreateSimpleWindow (IntPtr x11display, IntPtr x11window, TInt x, TInt y, TUint width, TUint height,
			TUint outsideBorderWidth, TPixel border, TPixel background);
		[DllImport("libX11")]
		extern public static IntPtr XCreateWindow (IntPtr x11display, IntPtr x11window, TInt x, TInt y, TUint width, TUint height, TUint outsideBorderWidth,
			TInt depth, TUint cls, IntPtr x11visual, WindowAttributeMask valueMask, ref XSetWindowAttributes attributes);
		[DllImport("libX11")]
		extern public static IntPtr XInternAtom(IntPtr x11display, TChar[] atomName, bool onlyIfExists);
		[DllImport("libX11")]
		extern public static IntPtr XInternAtom(IntPtr x11display, [MarshalAs(UnmanagedType.LPStr)] string atomName, bool onlyIfExists);
		[DllImport("libX11")]
		extern public static TInt XSetWMProtocols(IntPtr x11display, IntPtr x11window, ref IntPtr protocols, TInt count);
		[DllImport("libX11")]
		extern public static void XFlush (IntPtr x11display);
		[DllImport("libX11")]
		extern public static IntPtr XCreateColormap(IntPtr rawHandle, IntPtr desktop, IntPtr intPtr, int i);

		[DllImport("libX11")]
		extern public static void XStoreName(IntPtr x11display, IntPtr x11window, sbyte[] windowName);
		[DllImport("libX11")]
		extern public static void XStoreName(IntPtr x11display, IntPtr x11window, [MarshalAs(UnmanagedType.LPStr)] string windowName);
		[DllImport("libX11", EntryPoint = "_XAllocClassHint")]
		extern private static IntPtr _XAllocClassHint ();
		[DllImport("libX11", EntryPoint = "XFree")]
		extern public static void XFree (IntPtr data);
		[DllImport("libX11", EntryPoint = "XGetClassHint")]
		extern private static TInt _XGetClassHint (IntPtr x11display, IntPtr x11window, ref _XClassHint classHint);
		[DllImport("libX11")]
		extern public static void XSetClassHint (IntPtr x11display, IntPtr x11window, [MarshalAs(UnmanagedType.Struct)] ref XClassHint classHints);
		[DllImport("libX11")]
		extern public static TInt XGetTransientForHint(IntPtr x11display, IntPtr x11window, ref IntPtr x11transientWindow);
		[DllImport("libX11")]
		extern public static void XSetTransientForHint(IntPtr x11display, IntPtr x11window, IntPtr x11transientWindow);
		[DllImport("libX11")]
		extern public static TInt XGetWindowAttributes (IntPtr x11display, IntPtr x11window, [MarshalAs(UnmanagedType.Struct)] ref XWindowAttributes windowAttributes);
		[DllImport("libX11")]
		extern public static void XSetInputFocus(IntPtr x11display, IntPtr x11window, TRevertTo revertTo, TInt time);
		[DllImport("libX11")]
		extern public static TInt XStringListToTextProperty (ref TChar[] list, TInt count, ref XTextProperty textProperty);
		[DllImport("libX11")]
		extern public static TInt XStringListToTextProperty (ref TChar[] list, TInt count, IntPtr p);
		[DllImport("libX11")]
		extern public static TInt XStringListToTextProperty ([MarshalAs(UnmanagedType.LPStr)] ref string list, TInt count, ref XTextProperty textProperty);
		[DllImport("libX11", EntryPoint = "XSetWMName")]
		extern public static void _XSetWMName(IntPtr x11display, IntPtr x11window, ref XTextProperty textProperty);
		[DllImport("libX11", EntryPoint = "XSetWMName")]
		extern public static void _XSetWMName(IntPtr x11display, IntPtr x11window, IntPtr p);
		[DllImport("libX11", EntryPoint = "XSetWMIconName")]
		extern public static void _XSetWMIconName(IntPtr x11display, IntPtr x11window, ref XTextProperty textProperty);
		[DllImport("libX11", EntryPoint = "XSetWMIconName")]
		extern public static void _XSetWMIconName(IntPtr x11display, IntPtr x11window, IntPtr p);
		[DllImport ("libX11")]
		extern public static XWMHints XAllocWMHints ();
		[DllImport ("libX11")]
		extern public static XWMHints XGetWMHints (IntPtr x11display, IntPtr x11window);
		[DllImport("libX11")]
		extern public static int XSetWMHints (IntPtr display, IntPtr w, ref XWMHints wmhints);
		[DllImport ("libX11")]
		extern public static IntPtr XCreateImage (IntPtr x11display, IntPtr x11visual, TUint depth, TImageFormat imageFormat, TInt offset, [MarshalAs(UnmanagedType.U1)] byte[] data, TUint width, TUint height, TInt bitmapPad, TInt bytesPerLine);
		[DllImport ("libX11")]
		extern public static IntPtr XCreateImage (IntPtr x11display, IntPtr x11visual, TUint depth, TImageFormat imageFormat, TInt offset, IntPtr data, TUint width, TUint height, TInt bitmapPad, TInt bytesPerLine);
		[DllImport("libX11")]
		extern public static void XDestroyImage(IntPtr x11ximage);
		[DllImport("libX11")]
		extern public static void XPutImage (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, IntPtr image, TInt srcOffsetX, TInt srcOffsetY, TInt destX, TInt destY, TUint width, TUint height);
		[DllImport("libX11")]
		extern public static IntPtr XCreatePixmap (IntPtr x11display, IntPtr x11drawable, TUint width, TUint height, TUint depth);
		[DllImport("libX11")]
		extern public static void XFreePixmap(IntPtr x11display, IntPtr x11pixmap);
		[DllImport("libX11")]
		extern public static void XSetClipMask(IntPtr x11display, IntPtr X11gc, IntPtr  x11pixmap);
		[DllImport("libX11")]
		extern public static void XSetClipOrigin (IntPtr x11display, IntPtr X11gc, TInt clipOriginX, TInt clipOriginY);
		[DllImport("libX11")]
		extern public static void XCopyArea (IntPtr x11display, IntPtr X11drawableSrc, IntPtr X11drawableDest, IntPtr X11gc, TInt srcOffsetX, TInt srcOffsetY, TUint width, TUint height,  TInt dstOffsetX, TInt dstOffsetY);
		[DllImport("libX11")]
		extern public static void XCopyPlane (IntPtr x11display, IntPtr X11drawableSrc, IntPtr X11drawableDest, IntPtr X11gc, TInt srcOffsetX, TInt srcOffsetY, TUint width, TUint height,  TInt dstOffsetX, TInt dstOffsetY, TUlong plane);
		[DllImport("libX11")]
		extern public static void XPutPixel(IntPtr x11image, TInt x, TInt y, TPixel pixel);
		[DllImport("libX11")]
		extern public static TPixmap XCreateBitmapFromData (IntPtr x11display, IntPtr X11drawableSrc, TUchar[] data, TUint width, TUint height);
		[DllImport("libX11")]
		extern public static TPixmap XReadBitmapFile (IntPtr x11display, IntPtr X11drawableSrc, TChar[] filepath,
			ref TUint widthReturn, ref TUint heightReturn, ref IntPtr pixmapReturn,
			ref TInt xHotReturn, ref TInt yHotReturn);
		[DllImport ("libX11")]
		extern public static void XFreeStringList (IntPtr ptr);
		[DllImport ("libX11")]
		extern public static void XFreeFontSet (IntPtr x11display, IntPtr data);
		[DllImport("libX11")]
		extern public static void XSelectInput (IntPtr x11display, IntPtr x11window, EventMask eventMask);
		[DllImport("libX11")]
		extern public static bool XCheckMaskEvent (IntPtr x11display, EventMask eventMask, out IntPtr eventReturn);
		[DllImport("libX11")]
		extern public static int XScreenCount(IntPtr x11display);
		[DllImport("libX11")]
		extern public static TInt XDefaultScreen(IntPtr x11display);
		[DllImport("libX11")]
		extern public static int XConnectionNumber (IntPtr x11display);
		[DllImport("libX11")]
		extern public static IntPtr XScreenOfDisplay (IntPtr x11display, TInt screenNumber);
		[DllImport("libX11")]
		extern public static IntPtr XDefaultVisual (IntPtr x11display, TInt screenNumber);
		[DllImport("libX11")]
		extern public static TInt XDefaultDepth (IntPtr x11display, TInt screenNumber);
		[DllImport("libX11")]
		extern public static TPixel XBlackPixel (IntPtr x11display, TInt screenNumber);
		[DllImport("libX11")]
		extern public static TPixel XWhitePixel (IntPtr x11display, TInt screenNumber);
		[DllImport("libX11")]
		extern public static TPixel XBlackPixelOfScreen(IntPtr x11screen);
		[DllImport("libX11")]
		extern public static TPixel XWhitePixelOfScreen(IntPtr x11screen);
		[DllImport ("libX11")]
		public extern static int XDefaultColormap(IntPtr display, TInt screen_number);
		[DllImport("libX11")]
		extern public static int XParseColor (IntPtr x11display, int x11colormap, [MarshalAs(UnmanagedType.LPStr)] string colorname, ref XColor x11color);
		[DllImport("libX11")]
		extern public static int XLookupColor (IntPtr x11display, IntPtr x11colormap, [MarshalAs(UnmanagedType.LPStr)] string colorname, ref XColor x11colorExactDef, ref XColor x11colorScrDef);
		[DllImport("libX11")]
		extern public static void XQueryColor (IntPtr x11display, IntPtr x11colormap, ref XColor x11color);
		[DllImport("libX11")]
		extern public static int XAllocColor (IntPtr x11display, int colormap, ref XColor xcolor);
		[DllImport("libX11")]
		extern public static void XFreeColors (IntPtr x11display, IntPtr colormap, TPixel[] pixels, TInt nPixels, TUlong planes);
		[DllImport("libX11")]
		extern public static TInt XMatchVisualInfo (IntPtr x11display, TInt screenNumber, TInt depth, TInt cls, ref XVisualInfo visualInfo);
		[DllImport("libX11")]
		extern public static void XClearWindow (IntPtr x11display, IntPtr x11window);
		[DllImport("libX11")]
		extern public static IntPtr XCreateGC (IntPtr x11display, IntPtr x11drawable, TUlong valuesMask, IntPtr values);
		[DllImport("libX11")]
		extern public static IntPtr XCreateGC (IntPtr x11display, IntPtr x11drawable, TUlong valuesMask, ref XGCValues values);
		[DllImport("libX11")]
		extern public static void XFreeGC (IntPtr x11display, IntPtr x11gc);
		[DllImport("libX11")]
		extern public static TInt XGContextFromGC (IntPtr x11gc);
		[DllImport("libX11")]
		extern public static void XSetForeground (IntPtr x11display, IntPtr x11gc, TPixel foreground);
		[DllImport("libX11")]
		extern public static void XSetBackground (IntPtr x11display, IntPtr x11gc, TPixel background);
		[DllImport("libX11")]
		extern public static void XSetFunction(IntPtr x11display, IntPtr x11gc, XGCFunction function);
		[DllImport("libX11")]
		extern public static int XGetGCValues(IntPtr x11display, IntPtr x11gc, TUint valuemask, ref XGCValues values);
		[DllImport("libX11")]
		extern public static void XChangeGC(IntPtr x11display, IntPtr x11gc, TUint valuemask, ref XGCValues values);
		[DllImport("libX11")]
		extern public static void XDrawPoint (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, TInt x, TInt y);
		[DllImport("libX11")]
		extern public static void XDrawLine (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, TInt x1, TInt y1, TInt x2, TInt y2);
		[DllImport("libX11")]
		extern public static void XFillRectangle (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, TInt x, TInt y, TUint width, TUint height);
		[DllImport("libX11")]
		extern public static void XFillArc (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, TInt x, TInt y, TUint width, TUint height, TInt angle1, TInt angle2);
		[DllImport("libX11")]
		extern public static void XQueryTextExtents (IntPtr x11display, TInt resourceID, TChar[] text, TInt length, ref TInt direction, ref TInt fontAscent, ref TInt fontDescent, ref XCharStruct overall);
		[DllImport("libX11")]
		extern public static void XQueryTextExtents16 (IntPtr x11display, TInt resourceID, XChar2b[] text, TInt length, ref TInt direction, ref TInt fontAscent, ref TInt fontDescent, ref XCharStruct overall);
		[DllImport("libX11")]
		extern public static void XDrawString (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, TInt x, TInt y, TChar[] text, TInt length);
		[DllImport("libX11")]
		extern public static void XDrawString16 (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, TInt x, TInt y, XChar2b[] text, TInt length);
		[DllImport ("libX11")]
		extern public static void XmbDrawString   (IntPtr x11display, IntPtr x11drawable, IntPtr fontSet, IntPtr x11gc, int x, int y, sbyte[] text, int length);
		[DllImport ("libX11")]
		extern public static void XwcDrawString   (IntPtr x11display, IntPtr x11drawable, IntPtr fontSet, IntPtr x11gc, int x, int y, TWchar[] text, int length);
		[DllImport ("libX11")]
		extern public static void Xutf8DrawString (IntPtr x11display, IntPtr x11drawable, IntPtr fontSet, IntPtr x11gc, int x, int y, sbyte[] text, int length);
		[DllImport ("libX11")]
		internal extern static void XRecolorCursor (IntPtr x11display, IntPtr cursor, ref XColor foregroundColor, ref XColor backgroundColor);
		[DllImport("libX11")]
		public extern static void XNextEvent(IntPtr x11display, ref XEvent x11event);
		[DllImport("libX11")]
		public extern static bool XWindowEvent(IntPtr x11display, IntPtr x11window, EventMask event_mask, ref XEvent x11event);
		[DllImport("libX11")]
		public extern static bool XCheckWindowEvent(IntPtr x11display, IntPtr x11window, EventMask event_mask, ref XEvent x11event);
		[DllImport("libX11")]
		public extern static bool XCheckTypedWindowEvent(IntPtr x11display, IntPtr x11window, XEventName event_type, ref XEvent x11event);
		[DllImport("libX11", EntryPoint = "XFilterEvent")]
		public extern static bool XFilterEvent(ref XEvent xevent, IntPtr x11window);
		[DllImport("libX11")]
		public extern static void XPeekEvent(IntPtr display, ref XEvent x11event);
		[DllImport("libX11")]
		extern public static TInt XSendEvent(IntPtr x11display, IntPtr x11window, bool propagate, TLong eventMask, ref XEvent eventSend);
		[DllImport("libX11")]
		extern public static string XServerVendor(IntPtr x11display);
		[DllImport("libX11")]
		extern public static string XDisplayString(IntPtr x11display);
		[DllImport("libX11")]
		extern public static TInt XVendorRelease(IntPtr x11display);
		[DllImport("libX11")]
		extern public static TInt XProtocolVersion(IntPtr x11display);
		[DllImport("libX11")]
		extern public static TInt XProtocolRevision(IntPtr x11display);
		[DllImport("libX11")]
		extern public static int XDefaultColormapOfScreen (IntPtr screen);
		[DllImport("libX11")]
		extern public static IntPtr XDefaultScreenOfDisplay (IntPtr x11display);
		[DllImport("libX11")]
		extern public static int XScreenNumberOfScreen (IntPtr screen);
		[DllImport("libX11")]
		extern public static int XSetWindowBackground (IntPtr x11display, IntPtr x11window, int background_pixel);
		[DllImport("libX11")]
		extern public static int XSync (IntPtr x11display, int discard);
		[DllImport("libX11")]
		extern public static int XGrabServer (IntPtr x11display);
		[DllImport("libX11")]
		extern public static int XUngrabServer (IntPtr x11display);

		[DllImport("XlibWrapper.so")]
		public extern static void WrapXTest (IntPtr dpy, IntPtr gc);
		[DllImport("XlibWrapper.so")]
		public extern static void WrapXGetGCValues (IntPtr dpy, IntPtr gc, uint flags, ref XGCValues xgcv);
		[DllImport("XlibWrapper.so")]
		public extern static void WrapMinimalXaw ();

		[DllImport("libX11")]
		public extern static void XTranslateCoordinates(IntPtr x11display, IntPtr x11window_src, IntPtr x11window_dest, 
			int src_x, int src_y, ref int x, ref int y, ref IntPtr child_return);
		[DllImport("libX11")]
		public static extern int XChangeProperty(IntPtr x11display, IntPtr x11window, IntPtr property, Lib.AtomType type, TInt format, 
			Lib.PropMode mode, ref uint data, TInt nelements);
		[DllImport("libX11")]
		public static extern int XChangeWindowAttributes (IntPtr x11display, IntPtr x11window, WindowAttributeMask valuemask, ref XWindowAttributes xwa);
		[DllImport("libX11")]
		public static extern  void XLockDisplay(IntPtr display);
		[DllImport("libX11")]
		public static extern  void XUnlockDisplay(IntPtr display);
		[DllImport("libX11")] 
		public static extern void XQueryKeymap(IntPtr display, TChar[] keys); 

		[DllImport("libX11")]
		public static extern void XSetWMNormalHints (IntPtr display, IntPtr w, ref Lib.XSizeHints hints);

		public delegate bool CheckEventPredicate(IntPtr display, ref XEvent event_return, IntPtr arg);
		[DllImport("libX11")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool XCheckIfEvent(IntPtr display, ref XEvent event_return,
			/*[MarshalAs(UnmanagedType.FunctionPtr)] */ CheckEventPredicate predicate, /*XPointer*/ IntPtr arg);

		[DllImport("libX11")]
		extern public static int XGrabPointer(IntPtr display, IntPtr grab_window,
			bool owner_events, int event_mask, GrabMode pointer_mode, GrabMode keyboard_mode,
			IntPtr confine_to, IntPtr cursor, int time);

		[DllImport("libX11")]
		extern public static int UngrabPointer(IntPtr display, int time);

		//int XGrabKeyboard(Display *display, Window grab_window, Bool owner_events, int pointer_mode, int keyboard_mode, Time time);

		[DllImport("libX11")]
		extern public static int XGrabKeyboard(IntPtr display, IntPtr grab_window,
			bool owner_events, GrabMode pointer_mode, GrabMode keyboard_mode, int time);

		[DllImport("libX11")]
		extern public static void XUngrabKeyboard(IntPtr display, int time);
	}
}


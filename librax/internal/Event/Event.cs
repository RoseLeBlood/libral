//
//  Event.cs
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
using System.Reflection;

using XFloat      		= System.Single; // X11 32 Bit: 4 Bytes:
using XDouble     		= System.Double; // X11 32 Bit: 8 Bytes:



namespace X11._internal
{
	[Flags]
	[Serializable]
	public enum EventMask
	{
		NoEventMask					= 0,
		KeyPressMask				= (1<<0),
		KeyReleaseMask				= (1<<1),
		ButtonPressMask				= (1<<2),
		ButtonReleaseMask			= (1<<3),
		EnterWindowMask				= (1<<4),
		LeaveWindowMask				= (1<<5),
		PointerMotionMask			= (1<<6),
		PointerMotionHintMask		= (1<<7),
		Button1MotionMask			= (1<<8),
		Button2MotionMask			= (1<<9),
		Button3MotionMask			= (1<<10),
		Button4MotionMask			= (1<<11),
		Button5MotionMask			= (1<<12),
		ButtonMotionMask			= (1<<13),
		KeymapStateMask				= (1<<14),
		ExposureMask				= (1<<15),
		VisibilityChangeMask		= (1<<16),
		StructureNotifyMask			= (1<<17),
		ResizeRedirectMask			= (1<<18),
		SubstructureNotifyMask		= (1<<19),
		SubstructureRedirectMask	= (1<<20),
		FocusChangeMask				= (1<<21),
		PropertyChangeMask			= (1<<22),
		ColormapChangeMask			= (1<<23),
		OwnerGrabButtonMask			= (1<<24),
		All 						= KeyPressMask | KeyReleaseMask | ButtonPressMask | ButtonReleaseMask |
			EnterWindowMask | LeaveWindowMask | PointerMotionMask | PointerMotionHintMask | ButtonMotionMask |
			KeymapStateMask | ExposureMask | VisibilityChangeMask | StructureNotifyMask | SubstructureRedirectMask |
			FocusChangeMask | PropertyChangeMask | ColormapChangeMask | OwnerGrabButtonMask,

	} // enum EventMask
	[Serializable]
	public enum XEventName
	{
		KeyPress			= 2,
		KeyRelease			= 3,
		ButtonPress			= 4,
		ButtonRelease		= 5,
		MotionNotify		= 6,
		EnterNotify			= 7,
		LeaveNotify			= 8,
		FocusIn				= 9,
		FocusOut			= 10,
		KeymapNotify		= 11,
		Expose				= 12,
		GraphicsExpose		= 13,
		NoExpose			= 14,
		VisibilityNotify	= 15,
		CreateNotify		= 16,
		DestroyNotify		= 17,
		UnmapNotify			= 18,
		MapNotify			= 19,
		MapRequest			= 20,
		ReparentNotify		= 21,
		ConfigureNotify		= 22,
		ConfigureRequest	= 23,
		GravityNotify		= 24,
		ResizeRequest		= 25,
		CirculateNotify		= 26,
		CirculateRequest	= 27,
		PropertyNotify		= 28,
		SelectionClear		= 29,
		SelectionRequest	= 30,
		SelectionNotify		= 31,
		ColormapNotify		= 32,
		ClientMessage		= 33,
		MappingNotify		= 34
	} // enum EventType

	public enum XRequest : byte
	{
		CreateWindow = 1,
		ChangeWindowAttributes = 2,
		GetWindowAttributes = 3,
		DestroyWindow = 4,
		DestroySubwindows = 5,
		ChangeSaveSet = 6,
		ReparentWindow = 7,
		MapWindow = 8,
		MapSubwindows = 9,
		UnmapWindow = 10,
		UnmapSubwindows = 11,
		ConfigureWindow = 12,
		CirculateWindow = 13,
		GetGeometry = 14,
		QueryTree = 15,
		InternAtom = 16,
		GetAtomName = 17,
		ChangeProperty = 18,
		DeleteProperty = 19,
		GetProperty = 20,
		ListProperties = 21,
		SetSelectionOwner = 22,
		GetSelectionOwner = 23,
		ConvertSelection = 24,
		SendEvent = 25,
		GrabPointer = 26,
		UngrabPointer = 27,
		GrabButton = 28,
		UngrabButton = 29,
		ChangeActivePointerGrab = 30,
		GrabKeyboard = 31,
		UngrabKeyboard = 32,
		GrabKey = 33,
		UngrabKey = 34,
		AllowEvents = 35,
		GrabServer = 36,
		UngrabServer = 37,
		QueryPointer = 38,
		GetMotionEvents = 39,
		TranslateCoords = 40,
		WarpPointer = 41,
		SetInputFocus = 42,
		GetInputFocus = 43,
		QueryKeymap = 44,
		OpenFont = 45,
		CloseFont = 46,
		QueryFont = 47,
		QueryTextExtents = 48,
		ListFonts = 49,
		ListFontsWithInfo = 50,
		SetFontPath = 51,
		GetFontPath = 52,
		CreatePixmap = 53,
		FreePixmap = 54,
		CreateGC = 55,
		ChangeGC = 56,
		CopyGC = 57,
		SetDashes = 58,
		SetClipRectangles = 59,
		FreeGC = 60,
		ClearArea = 61,
		CopyArea = 62,
		CopyPlane = 63,
		PolyPoint = 64,
		PolyLine = 65,
		PolySegment = 66,
		PolyRectangle = 67,
		PolyArc = 68,
		FillPoly = 69,
		PolyFillRectangle = 70,
		PolyFillArc = 71,
		PutImage = 72,
		GetImage = 73,
		PolyText8 = 74,
		PolyText16 = 75,
		ImageText8 = 76,
		ImageText16 = 77,
		CreateColormap = 78,
		FreeColormap = 79,
		CopyColormapAndFree = 80,
		InstallColormap = 81,
		UninstallColormap = 82,
		ListInstalledColormaps = 83,
		AllocColor = 84,
		AllocNamedColor = 85,
		AllocColorCells = 86,
		AllocColorPlanes = 87,
		FreeColors = 88,
		StoreColors = 89,
		StoreNamedColor = 90,
		QueryColors = 91,
		LookupColor = 92,
		CreateCursor = 93,
		CreateGlyphCursor = 94,
		FreeCursor = 95,
		RecolorCursor = 96,
		QueryBestSize = 97,
		QueryExtension = 98,
		ListExtensions = 99,
		ChangeKeyboardMapping = 100,
		GetKeyboardMapping = 101,
		ChangeKeyboardControl = 102,
		GetKeyboardControl = 103,
		Bell = 104,
		ChangePointerControl = 105,
		GetPointerControl = 106,
		SetScreenSaver = 107,
		GetScreenSaver = 108,
		ChangeHosts = 109,
		ListHosts = 110,
		SetAccessControl = 111,
		SetCloseDownMode = 112,
		KillClient = 113,
		RotateProperties = 114,
		ForceScreenSaver = 115,
		SetPointerMapping = 116,
		GetPointerMapping = 117,
		SetModifierMapping = 118,
		GetModifierMapping = 119,
		NoOperation = 127
	}

	/// <summary>
	/// <para>The <see cref="T:XWindows.CrossingMode"/> enumeration specifies
	/// notification modes for Enter and Leave events on a widget.
	/// </para>
	/// </summary>
	public enum CrossingMode : int
	{
		NotifyNormal       = 0,
		NotifyGrab         = 1,
		NotifyUngrab       = 2,
		NotifyWhileGrabbed = 3

	} // enum CrossingMode

	/// <summary>
	/// <para>The <see cref="T:XWindows.CrossingDetail"/> enumeration specifies
	/// notification details for Enter and Leave events on a widget.
	/// </para>
	/// </summary>
	public enum CrossingDetail : int
	{
		NotifyAncestor         = 0,
		NotifyVirtual          = 1,
		NotifyInferior         = 2,
		NotifyNonlinear        = 3,
		NotifyNonlinearVirtual = 4,
		NotifyPointer          = 5,
		NotifyPointerRoot      = 6,
		NotifyDetailNone       = 7

	} // enum CrossingDetail

	[StructLayout(LayoutKind.Sequential)]
	public struct XAnyEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XKeyEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public IntPtr root;
		public IntPtr subwindow;
		public ulong time;
		public int x;
		public int y;
		public int x_root;
		public int y_root;
		public int state;
		public uint keycode;
		public bool same_screen;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XButtonEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public IntPtr root;
		public IntPtr subwindow;
		public ulong time;
		public int x;
		public int y;
		public int x_root;
		public int y_root;
		public int state;
		public uint button;
		public bool same_screen;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XMotionEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public IntPtr root;
		public IntPtr subwindow;
		public ulong time;
		public int x;
		public int y;
		public int x_root;
		public int y_root;
		public int state;
		public byte is_hint;
		public bool same_screen;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XCrossingEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public IntPtr root;
		public IntPtr subwindow;
		public ulong time;
		public int x;
		public int y;
		public int x_root;
		public int y_root;
		public CrossingMode mode;
		public CrossingDetail detail;
		public bool same_screen;
		public bool focus;
		public int state;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XFocusChangeEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public int mode;
		public CrossingDetail detail;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XKeymapEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public byte vector0;
		public byte vector1;
		public byte vector2;
		public byte vector3;
		public byte vector4;
		public byte vector5;
		public byte vector6;
		public byte vector7;
		public byte vector8;
		public byte vector9;
		public byte vector10;
		public byte vector11;
		public byte vector12;
		public byte vector13;
		public byte vector14;
		public byte vector15;
		public byte vector16;
		public byte vector17;
		public byte vector18;
		public byte vector19;
		public byte vector20;
		public byte vector21;
		public byte vector22;
		public byte vector23;
		public byte vector24;
		public byte vector25;
		public byte vector26;
		public byte vector27;
		public byte vector28;
		public byte vector29;
		public byte vector30;
		public byte vector31;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XExposeEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public int x;
		public int y;
		public int width;
		public int height;
		public int count;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XGraphicsExposeEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr drawable;
		public int x;
		public int y;
		public int width;
		public int height;
		public int count;
		public int major_code;
		public int minor_code;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XNoExposeEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr drawable;
		public int major_code;
		public int minor_code;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XVisibilityEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public int state;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XCreateWindowEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr parent;
		public IntPtr window;
		public int x;
		public int y;
		public int width;
		public int height;
		public int border_width;
		public bool override_redirect;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XDestroyWindowEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr xevent;
		public IntPtr window;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XUnmapEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr xevent;
		public IntPtr window;
		public bool from_configure;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XMapEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr xevent;
		public IntPtr window;
		public bool override_redirect;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XMapRequestEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr parent;
		public IntPtr window;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XReparentEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr xevent;
		public IntPtr window;
		public IntPtr parent;
		public int x;
		public int y;
		public bool override_redirect;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct XConfigureEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr xevent;
		public IntPtr window;
		public TInt x;
		public TInt y;
		public TInt width;
		public TInt height;
		public TInt border_width;
		public IntPtr above;
		public bool override_redirect;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XGravityEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr xevent;
		public IntPtr window;
		public int x;
		public int y;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XResizeRequestEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public int width;
		public int height;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XConfigureRequestEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr parent;
		public IntPtr window;
		public int x;
		public int y;
		public int width;
		public int height;
		public int border_width;
		public IntPtr above;
		public int detail;
		public IntPtr value_mask;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XCirculateEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr xevent;
		public IntPtr window;
		public int place;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XCirculateRequestEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr parent;
		public IntPtr window;
		public int place;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XPropertyEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public IntPtr atom;
		public ulong time;
		public int state;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XSelectionClearEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public IntPtr selection;
		public ulong time;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XSelectionRequestEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr owner;
		public IntPtr requestor;
		public IntPtr selection;
		public IntPtr target;
		public IntPtr property;
		public ulong time;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XSelectionEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr requestor;
		public IntPtr selection;
		public IntPtr target;
		public IntPtr property;
		public ulong time;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XColormapEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public IntPtr colormap;
		public bool c_new;
		public int state;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XClientMessageEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public IntPtr message_type;
		public int format;
		public IntPtr ptr1;
		public IntPtr ptr2;
		public IntPtr ptr3;
		public IntPtr ptr4;
		public IntPtr ptr5;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XMappingEvent
	{
		public XEventName type;
		public IntPtr serial;
		public bool send_event;
		public IntPtr display;
		public IntPtr window;
		public int request;
		public int first_keycode;
		public int count;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XErrorEvent
	{
		public XEventName type;
		public IntPtr display;				/* Display the event was read from */
		public IntPtr resourceid;			/* resource id */
		public IntPtr serial;				/* serial number of failed request */
		public byte error_code;				/* error code of failed request */
		public XRequest request_code;		/* Major op-code of failed request */
		public byte minor_code;				/* Minor op-code of failed request */
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XEventPad
	{
		public IntPtr pad0;
		public IntPtr pad1;
		public IntPtr pad2;
		public IntPtr pad3;
		public IntPtr pad4;
		public IntPtr pad5;
		public IntPtr pad6;
		public IntPtr pad7;
		public IntPtr pad8;
		public IntPtr pad9;
		public IntPtr pad10;
		public IntPtr pad11;
		public IntPtr pad12;
		public IntPtr pad13;
		public IntPtr pad14;
		public IntPtr pad15;
		public IntPtr pad16;
		public IntPtr pad17;
		public IntPtr pad18;
		public IntPtr pad19;
		public IntPtr pad20;
		public IntPtr pad21;
		public IntPtr pad22;
		public IntPtr pad23;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct XEvent
	{
		/// <summary> All event types have the type member at field offset 0. </summary>
		[FieldOffset(0)]
		public XEventName type;

		[FieldOffset(0)]
		public XAnyEvent AnyEvent;
		[FieldOffset(0)]
		public XKeyEvent KeyEvent;
		[FieldOffset(0)]
		public XButtonEvent ButtonEvent;
		[FieldOffset(0)]
		public XMotionEvent MotionEvent;
		[FieldOffset(0)]
		public XCrossingEvent CrossingEvent;
		[FieldOffset(0)]
		public XFocusChangeEvent FocusChangeEvent;
		[FieldOffset(0)]
		public XExposeEvent ExposeEvent;
		[FieldOffset(0)]
		public XGraphicsExposeEvent GraphicsExposeEvent;
		[FieldOffset(0)]
		public XNoExposeEvent NoExposeEvent;
		[FieldOffset(0)]
		public XVisibilityEvent VisibilityEvent;
		[FieldOffset(0)]
		public XCreateWindowEvent CreateWindowEvent;
		[FieldOffset(0)]
		public XDestroyWindowEvent DestroyWindowEvent;
		[FieldOffset(0)]
		public XUnmapEvent UnmapEvent;
		[FieldOffset(0)]
		public XMapEvent MapEvent;
		[FieldOffset(0)]
		public XMapRequestEvent MapRequestEvent;
		[FieldOffset(0)]
		public XReparentEvent ReparentEvent;
		[FieldOffset(0)]
		public XConfigureEvent ConfigureEvent;
		[FieldOffset(0)]
		public XGravityEvent GravityEvent;
		[FieldOffset(0)]
		public XResizeRequestEvent ResizeRequestEvent;
		[FieldOffset(0)]
		public XConfigureRequestEvent ConfigureRequestEvent;
		[FieldOffset(0)]
		public XCirculateEvent CirculateEvent;
		[FieldOffset(0)]
		public XCirculateRequestEvent CirculateRequestEvent;
		[FieldOffset(0)]
		public XPropertyEvent PropertyEvent;
		[FieldOffset(0)]
		public XSelectionClearEvent SelectionClearEvent;
		[FieldOffset(0)]
		public XSelectionRequestEvent SelectionRequestEvent;
		[FieldOffset(0)]
		public XSelectionEvent SelectionEvent;
		[FieldOffset(0)]
		public XColormapEvent ColormapEvent;
		[FieldOffset(0)]
		public XClientMessageEvent ClientMessageEvent;
		[FieldOffset(0)]
		public XMappingEvent MappingEvent;
		[FieldOffset(0)]
		public XErrorEvent ErrorEvent;
		[FieldOffset(0)]
		public XKeymapEvent KeymapEvent;
		[FieldOffset(0)]
		public XEventPad Pad;
		public override string ToString()
		{
			switch (type)
			{
				case XEventName.ButtonPress:
				case XEventName.ButtonRelease:
					return ToString(ButtonEvent);
				case XEventName.CirculateNotify:
				case XEventName.CirculateRequest:
					return ToString(CirculateEvent);
				case XEventName.ClientMessage:
					return ToString(ClientMessageEvent);
				case XEventName.ColormapNotify:
					return ToString(ColormapEvent);
				case XEventName.ConfigureNotify:
					return ToString(ConfigureEvent);
				case XEventName.ConfigureRequest:
					return ToString(ConfigureRequestEvent);
				case XEventName.CreateNotify:
					return ToString(CreateWindowEvent);
				case XEventName.DestroyNotify:
					return ToString(DestroyWindowEvent);
				case XEventName.Expose:
					return ToString(ExposeEvent);
				case XEventName.FocusIn:
				case XEventName.FocusOut:
					return ToString(FocusChangeEvent);
				case XEventName.GraphicsExpose:
					return ToString(GraphicsExposeEvent);
				case XEventName.GravityNotify:
					return ToString(GravityEvent);
				case XEventName.KeymapNotify:
					return ToString(KeymapEvent);
				case XEventName.MapNotify:
					return ToString(MapEvent);
				case XEventName.MappingNotify:
					return ToString(MappingEvent);
				case XEventName.MapRequest:
					return ToString(MapRequestEvent);
				case XEventName.MotionNotify:
					return ToString(MotionEvent);
				case XEventName.NoExpose:
					return ToString(NoExposeEvent);
				case XEventName.PropertyNotify:
					return ToString(PropertyEvent);
				case XEventName.ReparentNotify:
					return ToString(ReparentEvent);
				case XEventName.ResizeRequest:
					return ToString(ResizeRequestEvent);
				case XEventName.SelectionClear:
					return ToString(SelectionClearEvent);
				case XEventName.SelectionNotify:
					return ToString(SelectionEvent);
				case XEventName.SelectionRequest:
					return ToString(SelectionRequestEvent);
				case XEventName.UnmapNotify:
					return ToString(UnmapEvent);
				case XEventName.VisibilityNotify:
					return ToString(VisibilityEvent);
				case XEventName.EnterNotify:
				case XEventName.LeaveNotify:
					return ToString(CrossingEvent);
				default:
					return type.ToString();
			}
		}

		public static string ToString(object ev)
		{
			string result = string.Empty;
			Type type = ev.GetType();
			FieldInfo[] fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Instance);
			for (int i = 0; i < fields.Length; i++)
				{
					if (result != string.Empty)
						{
							result += ", ";
						}
					object value = fields[i].GetValue(ev);
					result += fields[i].Name + "=" + (value == null ? "<null>" : value.ToString());
				}
			return type.Name + " (" + result + ")";
		}
	}
}


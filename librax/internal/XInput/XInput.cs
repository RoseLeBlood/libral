//
//  XInput.cs
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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using XIGroupState = X11._internal.Input.XIModifierState;

namespace X11._internal
{
	public class Input
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct XIAddMasterInfo
		{
			public int                 type;
			public TChar[]              name;
			public TBoolean                send_core;
			public TBoolean                enable;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public struct XIRemoveMasterInfo
		{
			public TInt                 type;
			public TInt                 deviceid;
			public TInt                 return_mode; /* AttachToMaster, Floating */
			public TInt                 return_pointer;
			public TInt                 return_keyboard;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public struct XIAttachSlaveInfo
		{
			public TInt                 type;
			public TInt                 deviceid;
			public TInt                 new_master;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public struct XIDetachSlaveInfo
		{
			public TInt                 type;
			public TInt                 deviceid;
		} 
		[StructLayout(LayoutKind.Explicit)]
		public struct XIAnyHierarchyChangeInfo
		{
			[FieldOffset(0)]
			public TInt                   type; 
			[FieldOffset(0)]
			public XIAddMasterInfo       iadd;
			[FieldOffset(0)]
			public XIRemoveMasterInfo    iremove;
			[FieldOffset(0)]
			public XIAttachSlaveInfo     attach;
			[FieldOffset(0)]
			public XIDetachSlaveInfo     detach;
		} 

		public enum XIDeviceType : uint
		{
			MasterPointer = 1,
			MasterKeyboard = 2,
			SlavePointer = 3,
			SlaveKeyboard = 4,
			FloatingSlave = 5,
		}

		public enum XIMode : uint
		{
			Relative = 0,
			Absolute = 1
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct XIEventMask
		{
			public TInt deviceid;
			public TInt mask_len;
			public TUchar mask;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public struct XIButtonState
		{
			public TInt    mask_len;
			public TUchar[]  mask;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public struct XIModifierState
		{
			public TInt    xbase;
			public TInt    latched;
			public TInt    locked;
			public TInt    effective;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public struct XIValuatorState
		{
			public TInt           mask_len;
			public TUchar 		   mask;
			public double        values;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIAnyClassInfo
		{
			public TInt         type;
			public TInt         sourceid;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIButtonClassInfo
		{
			public TInt         type;
			public TInt         sourceid;
			public TInt         num_buttons;
			public IntPtr        labels;
			public XIButtonState state;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIKeyClassInfo
		{
			public TInt         type;
			public TInt         sourceid;
			public TInt         num_keycodes;
			public TInt         keycodes;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIValuatorClassInfo
		{
			public TInt         type;
			public TInt         sourceid;
			public TInt         number;
			public IntPtr        label;
			public double      min;
			public double      max;
			public double      value;
			public TInt         resolution;
			public TInt         mode;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIDeviceInfo
		{
			public TInt                deviceid;
			public char                name;
			public TInt                use;
			public TInt                attachment;
			public TBoolean                enabled;
			public TInt                num_classes;
			public XIAnyClassInfo      classes;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIGrabModifiers
		{
			public TInt                 modifiers;
			public TInt                 status;
		} 
		[StructLayout(LayoutKind.Sequential)]	
		public  struct XIEvent
		{
			public TInt           type;       
			public TUlong serial;       
			public TBoolean          send_event;   
			public IntPtr       display;     
			public TInt           extension;   
			public TInt           evtype;
			public IntPtr          time;
		} 

		[StructLayout(LayoutKind.Sequential)]
		public  struct XIHierarchyInfo
		{
			public TInt           deviceid;
			public TInt           attachment;
			public TInt           use;
			public TBoolean          enabled;
			public TInt           flags;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIHierarchyEvent
		{
			public TInt           type;         /* GenericEvent */
			public TUlong serial;       /* # of last request processed by server */
			public TBoolean          send_event;   /* true if this came from a SendEvent request */
			public IntPtr       display;     /* IntPtr the xevent was read from */
			public TInt           extension;    /* XI extension offset */
			public TInt           evtype;       /* XI_HierarchyChanged */
			public IntPtr          time;
			public TInt           flags;
			public TInt           num_info;
			public XIHierarchyInfo info;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIDeviceChangedEvent
		{
			public TInt           type;         /* GenericEvent */
			public TUlong serial;       /* # of last request processed by server */
			public TBoolean          send_event;   /* true if this came from a SendEvent request */
			public IntPtr       display;     /* IntPtr the xevent was read from */
			public TInt           extension;    /* XI extension offset */
			public TInt           evtype;       /* XI_DeviceChanged */
			public IntPtr          time;
			public TInt           deviceid;     /* id of the device that changed */
			public TInt           sourceid;     /* Source for the new classes. */
			public TInt           reason;       /* Reason for the change */
			public TInt           num_classes;
			public XIAnyClassInfo classes; /* same as in XIDeviceInfo */
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIDeviceEvent
		{
			public TInt           type;        
			public TUlong serial;       
			public TBoolean          send_event;  
			public IntPtr       display;     
			public TInt           extension;  
			public TInt           evtype;
			public IntPtr          time;
			public TInt           deviceid;
			public TInt           sourceid;
			public TInt           detail;
			public IntPtr        root;
			public IntPtr        xevent;
			public IntPtr        child;
			public double        root_x;
			public double        root_y;
			public double        event_x;
			public double        event_y;
			public TInt           flags;
			public XIButtonState       buttons;
			public XIValuatorState     valuators;
			public XIModifierState     mods;
			public XIGroupState        group;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIRawEvent
		{
			public TInt           type;         /* GenericEvent */
			public TUlong serial;       /* # of last request processed by server */
			public TBoolean          send_event;   /* true if this came from a SendEvent request */
			public IntPtr       display;     /* IntPtr the xevent was read from */
			public TInt           extension;    /* XI extension offset */
			public TInt           evtype;       /* XI_RawKeyPress, XI_RawKeyRelease, etc. */
			public IntPtr          time;
			public TInt           deviceid;
			public TInt           sourceid;     /* Bug: Always 0. https://bugs.freedesktop.org//show_bug.cgi?id=34240 */
			public TInt           detail;
			public TInt           flags;
			public XIValuatorState valuators;
			public double        raw_values;
		} 
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIEnterEvent
		{
			public TInt           type;         /* GenericEvent */
			public TUlong serial;       /* # of last request processed by server */
			public TBoolean          send_event;   /* true if this came from a SendEvent request */
			public IntPtr       display;     /* IntPtr the xevent was read from */
			public TInt           extension;    /* XI extension offset */
			public TInt           evtype;
			public IntPtr          time;
			public TInt           deviceid;
			public TInt           sourceid;
			public TInt           detail;
			public IntPtr        root;
			public IntPtr        xevent;
			public IntPtr        child;
			public double        root_x;
			public double        root_y;
			public double        event_x;
			public double        event_y;
			public TInt           mode;
			public TBoolean          focus;
			public TBoolean          same_screen;
			public XIButtonState       buttons;
			public XIModifierState     mods;
			public XIGroupState        group;
		} 

		public  static XIEnterEvent XILeaveEvent = new XIEnterEvent();
		public  static XIEnterEvent XIFocusInEvent = new XIEnterEvent();
		public  static XIEnterEvent XIFocusOutEvent= new XIEnterEvent();
		[StructLayout(LayoutKind.Sequential)]
		public  struct XIPropertyEvent
		{
			public TInt           type;         /* GenericEvent */
			public TUlong serial;       /* # of last request processed by server */
			public TBoolean          send_event;   /* true if this came from a SendEvent request */
			public IntPtr       display;     /* IntPtr the xevent was read from */
			public TInt           extension;    /* XI extension offset */
			public TInt           evtype;       /* XI_PropertyEvent */
			public IntPtr          time;
			public TInt           deviceid;     /* id of the device that changed */
			public IntPtr          property;
			public TInt           what;
		} 



		const string lib = "libXi";
		internal const TInt XIAllDevices = (TInt)0;
		internal const TInt XIAllMasterDevices = (TInt)1;

		// --- MOUSE ----
		public const string ButtonLeft = "Button Left";
		public const string ButtonMiddle =  "Button Middle";
		public const string ButtonRight =  "Button Right";
		public const string ButtonWheelUp =  "Button Wheel Up";
		public const string ButtonWheelDown =  "Button Wheel Down";
		public const string ButtonWheelLeft =  "Button Horiz Wheel Left";
		public const string ButtonWheelRight =  "Button Horiz Wheel Right";
		public const string RelativeX =  "Rel X";
		public const string RelativeY =  "Rel Y";
		public const string RelativeHWheel =  "Rel Horiz Wheel";
		public const string RelativeVWheel =  "Rel Vert Wheel";
		public const string RelativeHScroll =  "Rel Horiz Scroll";
		public const string RelativeVScroll =  "Rel Vert Scroll";
		// --- TOUCH ----
		public const string TouchX =  "Abs MT Position X";
		public const string TouchY =  "Abs MT Position Y";
		public const string TouchMajor =  "Abs MT Touch Major";
		public const string TouchMinor =  "Abs MT Touch Minor";
		public const string TouchPressure =  "Abs MT Pressure";
		public const string TouchId =  "Abs MT Tracking ID";
		public const string TouchMaxContacts =  "Max Contacts";
		// --- TABLET ----
		public const string AbsoluteX =  "Abs X";
		public const string AbsoluteY =  "Abs Y";
		public const string AbsolutePressure =  "Abs Pressure";
		public const string AbsoluteTiltX =  "Abs Tilt X";
		public const string AbsoluteTiltY =  "Abs Tilt Y";
		public const string AbsoluteWheel =  "Abs Wheel";
		public const string AbsoluteDistance =  "Abs Distance";

		[DllImport(lib)]
		static extern TInt XISelectEvents(IntPtr x11display, IntPtr x11window, [In] XIEventMask[] masks, TInt num_masks);
		[DllImport(lib)]
		static extern TInt XISelectEvents(IntPtr x11display, IntPtr x11window, [In] ref XIEventMask masks, TInt num_masks);
		[DllImport(lib)]
		static extern TInt XIGrabDevice(IntPtr display, TInt deviceid, IntPtr grab_window, IntPtr time, IntPtr cursor, TInt grab_mode, TInt paired_device_mode, bool owner_events, XIEventMask[] mask);
		[DllImport(lib)]
		static extern TInt XIUngrabDevice(IntPtr display, TInt deviceid, IntPtr time);
		[DllImport(lib)]
		public static extern bool XIWarpPointer(IntPtr display, TInt deviceid, IntPtr src_w, IntPtr dest_w, double src_x, double src_y, TInt src_width, TInt src_height, double dest_x, double dest_y);
		[DllImport(lib)]
		public static extern IntPtr XIQueryDevice(IntPtr display, TInt id, out TInt count);
		[DllImport(lib)]
		public static extern void XIFreeDeviceInfo(IntPtr devices);
		[DllImport(lib)]
		public static extern bool XIQueryPointer(IntPtr display,
			TInt deviceid, IntPtr win,
			out IntPtr root_return, out IntPtr child_return,
			out double root_x_return, out double root_y_return,
			out double win_x_return, out double win_y_return,
			out XIButtonState buttons_return, out XIModifierState modifiers_return,
			out XIGroupState group_return);

		[DllImport(lib)]
		internal static extern TInt XIQueryVersion(IntPtr display, ref TInt major, ref TInt minor);



		[DllImport(lib)]
		public static extern  TBoolean     XIWarpPointer(IntPtr display, TInt deviceid, IntPtr src_win,
			IntPtr dst_win, double src_x, double src_y, TUint src_width, TUint src_height, double dst_x, double dst_y);
		[DllImport(lib)]
		public static extern  TInt   XIDefineCursor( IntPtr display, TInt deviceid, IntPtr win, IntPtr cursor );
		[DllImport(lib)]
		public static extern  TInt   XIUndefineCursor( IntPtr display, TInt deviceid, IntPtr win);
		[DllImport(lib)]
		public static extern  TInt   XIChangeHierarchy(IntPtr display, ref XIAnyHierarchyChangeInfo changes, TInt num_changes);
		[DllImport(lib)]
		public static extern  TInt   XISetClientPointer( IntPtr dpy, IntPtr win, TInt deviceid);
		[DllImport(lib)]
		public static extern  TBoolean     XIGetClientPointer( IntPtr dpy, IntPtr win, ref TInt deviceid);

		[DllImport(lib)]
		public static extern  XIEventMask XIGetSelectedEvents(
			IntPtr            dpy,
			IntPtr              win,
			ref TInt                 num_masks_return
		);


		[DllImport(lib)]
		public static extern  TInt XISetFocus(
			IntPtr           dpy,
			TInt                deviceid,
			IntPtr             focus,
			IntPtr               time
		);
		[DllImport(lib)]
		public static extern  TInt XIGetFocus(
			IntPtr           dpy,
			TInt                deviceid,
			ref IntPtr             focus_return);
		[DllImport(lib)]
		public static extern  TInt XIGrabDevice(
			IntPtr           dpy,
			TInt                deviceid,
			IntPtr             grab_window,
			IntPtr               time,
			IntPtr             cursor,
			TInt                grab_mode,
			TInt                paired_device_mode,
			TBoolean               owner_events,
			ref XIEventMask        mask
		);

		[DllImport(lib)]
		public static extern  TInt XIAllowEvents(
			IntPtr            display,
			TInt                 deviceid,
			TInt                 event_mode,
			IntPtr                time
		);
		[DllImport(lib)]
		public static extern  TInt XIGrabButton(
			IntPtr            display,
			TInt                 deviceid,
			TInt                 button,
			IntPtr              grab_window,
			IntPtr              cursor,
			TInt                 grab_mode,
			TInt                 paired_device_mode,
			TInt                 owner_events,
			ref XIEventMask         mask,
			TInt                 num_modifiers,
			ref XIGrabModifiers     modifiers_inout
		);
		[DllImport(lib)]
		public static extern  TInt XIGrabKeycode(
			IntPtr            display,
			TInt                 deviceid,
			TInt                 keycode,
			IntPtr              grab_window,
			TInt                 grab_mode,
			TInt                 paired_device_mode,
			TInt                 owner_events,
			ref XIEventMask         mask,
			TInt                 num_modifiers,
			ref XIGrabModifiers     modifiers_inout
		);
		[DllImport(lib)]
		public static extern  TInt XIGrabEnter(
			IntPtr            display,
			TInt                 deviceid,
			IntPtr              grab_window,
			IntPtr              cursor,
			TInt                 grab_mode,
			TInt                 paired_device_mode,
			TInt                 owner_events,
			ref XIEventMask         mask,
			TInt                 num_modifiers,
			ref XIGrabModifiers     modifiers_inout
		);
		[DllImport(lib)]
		public static extern  TInt XIGrabFocusIn(
			IntPtr            display,
			TInt                 deviceid,
			IntPtr              grab_window,
			TInt                 grab_mode,
			TInt                 paired_device_mode,
			TInt                 owner_events,
			ref XIEventMask         mask,
			TInt                 num_modifiers,
			ref XIGrabModifiers     modifiers_inout
		);
		[DllImport(lib)]
		public static extern  TInt XIUngrabButton(
			IntPtr            display,
			TInt                 deviceid,
			TInt                 button,
			IntPtr              grab_window,
			TInt                 num_modifiers,
			ref XIGrabModifiers     modifiers
		);
		[DllImport(lib)]
		public static extern  TInt XIUngrabKeycode(
			IntPtr            display,
			TInt                 deviceid,
			TInt                 keycode,
			IntPtr              grab_window,
			TInt                 num_modifiers,
			ref XIGrabModifiers     modifiers
		);
		[DllImport(lib)]
		public static extern  TInt XIUngrabEnter(
			IntPtr            display,
			TInt                 deviceid,
			IntPtr              grab_window,
			TInt                 num_modifiers,
			ref XIGrabModifiers     modifiers
		);
		[DllImport(lib)]
		public static extern  TInt XIUngrabFocusIn(
			IntPtr            display,
			TInt                 deviceid,
			IntPtr              grab_window,
			TInt                 num_modifiers,
			ref XIGrabModifiers     modifiers
		);

		[DllImport(lib)]
		public static extern  IntPtr XIListProperties(
			IntPtr            display,
			TInt                 deviceid,
			ref TInt                 num_props_return
		);
		[DllImport(lib)]
		public static extern  void XIChangeProperty(
			IntPtr            display,
			TInt                 deviceid,
			IntPtr                property,
			IntPtr                type,
			TInt                 format,
			TInt                 mode,
			TChar[]       		 data,
			TInt                 num_items
		);
		[DllImport(lib)]
		public static extern  void XIDeleteProperty(
			IntPtr            display,
			TInt                 deviceid,
			IntPtr                property
		);
		[DllImport(lib)]
		public static extern  TInt XIGetProperty(
			IntPtr            display,
			TInt                 deviceid,
			IntPtr                property,
			long                offset,
			long                length,
			TBoolean                delete_property,
			IntPtr                type,
			ref IntPtr                type_return,
			ref TInt                 format_return,
			ref TUlong       num_items_return,
			TUlong       bytes_after_return,
			TChar[]      data
		);
		[DllImport(lib)]
		public static extern void XIFreeDeviceInfo(ref XIDeviceInfo info);
	}
}


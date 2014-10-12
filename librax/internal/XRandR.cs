//
//  XRandR.cs
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


namespace X11._internal
{
	public class XRandR
	{
		[StructLayout (LayoutKind.Sequential)]
		public struct XRRScreenSize
		{
			public int Width, Height;
			public int MWidth, MHeight;
		};
		[StructLayout (LayoutKind.Sequential)]
		public struct XRRScreenConfiguration
		{
			IntPtr screen; /* the root window in GetScreenInfo */
			XRRScreenSize sizes;
			IntPtr rotations;
			IntPtr current_rotation;
			int nsizes;
			int current_size;
			short current_rate;
			IntPtr timestamp;
			IntPtr config_timestamp;
			int subpixel_order;   /* introduced in randr v0.1 */
			short rates;         /* introduced in randr v1.1 */
			int nrates;
		};

		[StructLayout (LayoutKind.Sequential)]
		public struct XErrorEvent
		{
			public int type;
			public IntPtr display;   /* Display the event was read from */
			public long serial;/* serial number of failed request */
			public byte error_code;/* error code of failed request */
			public byte request_code;/* Major op-code of failed request */
			public byte minor_code;/* Minor op-code of failed request */
			public int resourceid;     /* resource id */
		};

		// structures as defined in xrandr.h

		[StructLayout (LayoutKind.Sequential)]
		public struct XRROutputInfo
		{
			public IntPtr timestamp;

			public int crtc_id;

			public string name;
			public int nameLen;

			public IntPtr mm_width;
			public IntPtr mm_height;

			public short connection;
			public short subpixel_order;

			public int ncrtc;
			public IntPtr crtcs;

			public int nclone;
			public IntPtr clones;

			public int nmode;
			public int npreferred;
			public IntPtr modes;
		};

		[StructLayout (LayoutKind.Sequential)]
		public struct XRRModeInfo
		{
			public IntPtr id;
			public int width;
			public int height;
			public IntPtr dotClock;
			public int hSyncStart;
			public int hSyncEnd;
			public int hTotal;
			public int hSkew;
			public int vSyncStart;
			public int vSyncEnd;
			public int vTotal;
			public string name;
			public int nameLength;
			public IntPtr modeFlags;
		};

		[StructLayout (LayoutKind.Sequential)]
		public struct XRRCrtcInfo
		{
			public IntPtr timestamp;
			public int x;
			public int y;
			public int width;
			public int height;

			public IntPtr mode;
			public short rotation;

			public int noutput;
			public IntPtr outputs;

			public int rotations;

			public int npossible;
			public IntPtr possible;
		};

		[StructLayout(LayoutKind.Sequential)]
		public struct XRRScreenResources
		{
			public IntPtr timestamp;
			public IntPtr configTimestamp;

			public int ncrtc;
			public IntPtr crtcs;

			public int noutput;
			public IntPtr outputs;

			public int nmode;
			public IntPtr modes;
		}

		[DllImport("libXrandr")]
		public static extern IntPtr XRRGetScreenResources (IntPtr dpy, IntPtr window);

		[DllImport("libXrandr")]
		public static extern void XRRFreeScreenResources (IntPtr resources);

		[DllImport("libXrandr")]
		public static extern IntPtr XRRGetOutputInfo (IntPtr dpy, IntPtr resources, int output_id);

		[DllImport("libXrandr")]
		public static extern void XRRFreeOutputInfo (IntPtr outputInfo);

		[DllImport("libXrandr")]
		public static extern XRRCrtcInfo XRRGetCrtcInfo (IntPtr dpy, IntPtr resources, int crtc_id);

		[DllImport("libXrandr")]
		public static extern void XRRFreeCrtcInfo (XRRCrtcInfo crtcInfo);

		[DllImport("libXrandr")]
		public static extern int XRRSetCrtcConfig (IntPtr dpy,
			IntPtr resources,
			IntPtr crtc_id,
			IntPtr timestamp,
			int x, int y,
			IntPtr mode_id,
			int rotation,
			IntPtr outputs,
			int noutputs);

		[DllImport("libXrandr")]
		public static extern void XRRSetScreenSize (IntPtr dpy, IntPtr window,
			int width, int height,
			int mmWidth, int mmHeight);

		[DllImport("libXrandr")]
		public static extern IntPtr XRRSizes(IntPtr dpy, int screen, out int nsizes);

		[DllImport("libXrandr")]
		public static extern IntPtr XRRGetScreenInfo(IntPtr dpy, IntPtr draw);

		[DllImport("libXrandr")]
		public static extern IntPtr XRRConfigSizes(IntPtr config, out int nsizes);

		public static List<XRRScreenSize> ConfigSizes(IntPtr config, out int nsizes)
		{
			int indexes = 0;
			IntPtr screen_ptr = XRRConfigSizes(config, out indexes);
			List<XRRScreenSize> screens = new List<XRRScreenSize>(indexes);
			nsizes = indexes;

			unsafe
			{
				XRRScreenSize* ptr = (XRRScreenSize*)screen_ptr;
				while (--indexes >= 0)
					{
						screens.Add(*ptr);
						ptr++;
					}
			}

			return screens;
		}
		[DllImport("libXrandr")]
		public static extern int XRRConfigCurrentConfiguration(IntPtr config, out ushort current_rotation);
		[DllImport("libXrandr")]
		public static extern void XRRSetScreenConfig(IntPtr rawHandle, IntPtr config, IntPtr desktop, int i, 
			ushort currentRotation, int currentTime);
		[DllImport("libXrandr")]
		public static extern  void XRRFreeScreenConfigInfo(IntPtr config);
	}
}


//
//  BaseGameWindow2.cs
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
using X11.Widgets;
using System.Common;
using liboRg.Context;
using X11._internal;
using X11;
using System.Runtime.InteropServices;
using X11.Widgets.Event;
using System.Collections.Generic;
using System.Reflection;

namespace liboRg.Window
{

	public class BaseGameWindow : BaseWindow, IGameWindow
	{

		private Game m_pGame;
		private GameContext m_pIContext;
		private bool		m_bFullScreen;
		private int 		m_iOldVideoMode;
		private bool[] 		m_bMouse = new bool[3];
		private bool[] 		m_bKeys= new bool[512];

		public GameContext Context
		{
			get { return m_pIContext; }
		}
		public bool[] Input
		{
			get { return m_bKeys; }
		}

		public BaseGameWindow(Game pGame, string title, WindowStyle style)
			: base(pGame.m_strDisplay, "GameWindow", Colors.Black, pGame.ContextConfig.Resolution.Size, title)
		{
			m_pIContext = null;
			m_pGame = pGame;
			Namespace = "liboRg";
			ClassName = "BaseGameWindow";

			m_bFullScreen = style == (style & WindowStyle.Fullscreen);
			m_bIsResizeable = style == (style & WindowStyle.Resize);

			this.EventMask = EventMask.StructureNotifyMask | EventMask.ExposureMask |
				EventMask.KeyReleaseMask | EventMask.KeyPressMask | EventMask.KeymapStateMask |
				EventMask.PointerMotionMask | EventMask.FocusChangeMask |
				EventMask.ButtonPressMask | EventMask.ButtonReleaseMask |
				EventMask.EnterWindowMask | EventMask.LeaveWindowMask |
				EventMask.PropertyChangeMask;

			this.Created += BaseGameWindow2_Created;
			KeyPress += BaseGameWindow2_KeyPressed;
			KeyRelease += BaseGameWindow2_KeyRelease;
			Resize += BaseGameWindow2_Resize;
			UserEvent += BaseGameWindow2_UserEvents;
			Move += BaseGameWindow2_Move;
		}
		public override void Create()
		{
			using (new Xlock(Display))
			{
				IntPtr desktop = Lib.XRootWindow(Display.RawHandle, (TInt)Display.Screen.ScreenNumber);
				TInt depth = Lib.XDefaultDepth(Display.RawHandle, (TInt)Display.Screen.ScreenNumber);
				IntPtr visual = Lib.XDefaultVisual(Display.RawHandle, (TInt)Display.Screen.ScreenNumber);
				
				m_pGame.ContextConfig.Resolution.BitsPerPixel = (int)depth;

				Lib.XSetWindowAttributes attributes = new Lib.XSetWindowAttributes();
				attributes.event_mask = (TLong)EventMask; 
				attributes.override_redirect = m_bFullScreen ? (TBoolean)1 : (TBoolean)0;
				attributes.colormap = Lib.XCreateColormap(Display.RawHandle, desktop, visual, 0/*AllocNone*/);

				Rectangle.X = 0;
				Rectangle.Y = 0;

				if (m_bFullScreen)
				{
				
					EnableFullscreen(true, Rectangle.Width, Rectangle.Height);
				}


				m_pHandle = Lib.XCreateWindow(Display.RawHandle, 
					desktop, 
					(TInt)0, (TInt)0, (TUint)Rectangle.Width, (TUint)Rectangle.Height, 
					(TUint)0, 
					(TInt)depth,
					(TUint)Lib.WindowClass.InputOutput, 
					Lib.XDefaultVisual(Display.RawHandle, (TInt)Display.Screen.ScreenNumber), 
					Lib.WindowAttributeMask.EventMask | Lib.WindowAttributeMask.Colormap |
					Lib.WindowAttributeMask.BackPixel | Lib.WindowAttributeMask.BorderPixel, 
					ref attributes);

				if (m_pHandle == IntPtr.Zero)
					throw new ApplicationException("XCreateWindow call failed (returned 0).");

				if (m_strTitle != null)
					Lib.XStoreName(Display.RawHandle, m_pHandle, m_strTitle);

			}
			Lib.XSizeHints hints = new Lib.XSizeHints();
			hints.base_width = Rectangle.Width;
			hints.base_height = Rectangle.Height;
			hints.flags = Lib.XSizeHintFlags.PSize | Lib.XSizeHintFlags.PPosition;

			Lib.XClassHint class_hint = new Lib.XClassHint();
			class_hint.res_name = Assembly.GetEntryAssembly().GetName().Name.ToLower();
			class_hint.res_class = Assembly.GetEntryAssembly().GetName().Name;

			using (new Xlock(Display))
			{
				Lib.XSetWMNormalHints(Display.RawHandle, m_pHandle, ref hints);

				var atom = Lib.XInternAtom(m_pDisplay.RawHandle, "WM_DELETE_WINDOW", false);
				Lib.XSetWMProtocols(Display.RawHandle, m_pHandle, ref atom, (TInt)1);
				Lib.XSetClassHint(Display.RawHandle, m_pHandle, ref class_hint);
			}
			if(!m_bIsResizeable)
				SetWindowMinMax(Rectangle.Width, Rectangle.Height, Rectangle.Width, Rectangle.Height);
			else
				SetWindowMinMax(Rectangle.Width, Rectangle.Height, -1, -1);

			if (m_bFullScreen)
			{
				EnableFullscreen(true, Rectangle.Width, Rectangle.Height);
			}

			Register();
			Application.Current.MainWindowStr = Name;
			OnCreate(new XEvent());
			base.Create();
		}
		public override void Destroy()
		{
			OnDestroy(new XEvent());
			Lib.XUnmapWindow(m_pDisplay.RawHandle, m_pHandle);
			Lib.XFlush(m_pDisplay.RawHandle);
			EnableFullscreen(false, Rectangle.Width, Rectangle.Height);

			Lib.XUndefineCursor(m_pDisplay.RawHandle, m_pHandle);
			Lib.XDestroyWindow(m_pDisplay.RawHandle, m_pHandle);
			base.Destroy();
		}
		public bool IsKeyDown( Keys key )
		{
			return m_bKeys[(int)key];
		}
		protected void BaseGameWindow2_UserEvents(Object sender, XEventArgs args)
		{
			m_pGame.DisableQue = false;
			m_pGame.drawing();
		}

		protected void BaseGameWindow2_Created(Object sender, XEventArgs args)
		{
			m_pIContext = new X11Context(this, m_pGame.ContextConfig);
			m_pGame.DisableQue = true;
			m_pGame.Create();
		}
		protected void BaseGameWindow2_KeyPressed(Object sender, XKeyEventArgs args)
		{
			m_bKeys[(int)args.Key] = true;
			m_pGame.DisableQue = true;
		}
		protected void BaseGameWindow2_KeyRelease(Object sender, XKeyEventArgs args)
		{
			m_bKeys[(int)args.Key] = false;
			m_pGame.DisableQue = true;
		}
		protected void BaseGameWindow2_Resize(Object sender, XEventArgs args)
		{
			m_pGame.OnResize(this.Rectangle);
			m_pGame.DisableQue = true;
		}
		protected void BaseGameWindow2_Move(Object sender, XEventArgs args)
		{
			m_pGame.DisableQue = true;
			m_pGame.OnMove(this.Rectangle);
		}
		public override void Show()
		{
			Lib.XMapWindow(m_pDisplay.RawHandle, m_pHandle);
			Lib.XSelectInput(m_pDisplay.RawHandle, m_pHandle, m_iEventMask);
			Lib.XMoveWindow(m_pDisplay.RawHandle, m_pHandle, (TInt)m_xRectangle.X, (TInt)m_xRectangle.Y);
			Lib.XFlush(m_pDisplay.RawHandle);

			if (m_bFullScreen)
			{

				IntPtr desktop = Lib.XRootWindow(Display.RawHandle, (TInt)Display.Screen.ScreenNumber);
				XEvent	x11_event = new XEvent();
				IntPtr	x11_state_atom;
				IntPtr	x11_fs_atom;

				x11_state_atom	= Lib.XInternAtom(Display.RawHandle, "_NET_WM_STATE", false);
				x11_fs_atom = Lib.XInternAtom(Display.RawHandle, "_NET_WM_STATE_FULLSCREEN", false);

				x11_event.ClientMessageEvent.type = XEventName.ClientMessage;
				x11_event.ClientMessageEvent.serial = (IntPtr.Zero);
				x11_event.ClientMessageEvent.send_event	= true;
				x11_event.ClientMessageEvent.window = m_pHandle;
				x11_event.ClientMessageEvent.message_type	= x11_state_atom;
				x11_event.ClientMessageEvent.format = 32;
				x11_event.ClientMessageEvent.ptr1 = (IntPtr)1;
				x11_event.ClientMessageEvent.ptr2 = x11_fs_atom;
				x11_event.ClientMessageEvent.ptr3 = (IntPtr.Zero);

				Lib.XSendEvent(Display.RawHandle, desktop, false, (TLong)(EventMask.SubstructureRedirectMask | EventMask.SubstructureNotifyMask), ref x11_event);
				Lib.XGrabPointer(m_pDisplay.RawHandle, m_pHandle, true, 0, Lib.GrabMode.Async, Lib.GrabMode.Async, 
					m_pHandle, IntPtr.Zero, Lib.CurrentTime);
				Lib.XGrabKeyboard(m_pDisplay.RawHandle, m_pHandle, true, Lib.GrabMode.Async, 
					Lib.GrabMode.Async, Lib.CurrentTime);
			}
		}

		public override void Hide()
		{
			Lib.XUnmapWindow(m_pDisplay.RawHandle, m_pHandle);
			Lib.XFlush(m_pDisplay.RawHandle);
		}

		protected override void EnableFullscreen(bool enabled, int width, int height)
		{
			IntPtr desktop = Lib.XRootWindow(Display.RawHandle, (TInt)Display.Screen.ScreenNumber);
			IntPtr config = XRandR.XRRGetScreenInfo(m_pDisplay.RawHandle, desktop);
			ushort currentRotation;
			int nbSizes;

			if (enabled)
			{
					m_iOldVideoMode = XRandR.XRRConfigCurrentConfiguration(config, out currentRotation);
				List<XRandR.XRRScreenSize> sizes = XRandR.ConfigSizes(config, out nbSizes);

				for (int i = 0; i < nbSizes; i++)
				{
					if (sizes[i].Width == width && sizes[i].Height == height)
					{
						XRandR.XRRSetScreenConfig(Display.RawHandle, config, desktop, i, currentRotation, Lib.CurrentTime);
						break;
					}
				}
			}
			else
			{
				XRandR.XRRConfigCurrentConfiguration(config, out currentRotation);
					XRandR.XRRSetScreenConfig(Display.RawHandle, config, desktop, m_iOldVideoMode, 
						currentRotation, Lib.CurrentTime);
			}
			XRandR.XRRFreeScreenConfigInfo(config);


		}
		void SetWindowMinMax(int min_height, int min_width, int max_height, int max_width)
		{
			IntPtr dummy;
			Lib.XSizeHints hints = new Lib.XSizeHints();

			using (new Xlock(Display))
			{
				XGetWMNormalHints(Display.RawHandle, m_pHandle, ref hints, out dummy);
			}

			if (min_width > 0 || min_height > 0)
			{
				hints.flags = hints.flags | Lib.XSizeHintFlags.PMinSize;
				hints.min_width = min_width;
				hints.min_height = min_height;
			}
			else
				hints.flags = hints.flags & ~Lib.XSizeHintFlags.PMinSize;

			if (max_width > 0 || max_height > 0)
				{
					hints.flags = hints.flags | Lib.XSizeHintFlags.PMaxSize;
					hints.max_width = max_width;
					hints.max_height = max_height;
				}
			else
				hints.flags = hints.flags & ~Lib.XSizeHintFlags.PMaxSize;


				using (new Xlock(Display))
				{
					XSetWMNormalHints(Display.RawHandle, m_pHandle, ref hints);
				}

		}
			

		[DllImport("libX11.so")]
		private static extern void XSetWMNormalHints (IntPtr display, IntPtr w, ref X11._internal.Lib.XSizeHints hints);
		[DllImport("libX11.so")]
		private static extern void XGetWMNormalHints(IntPtr display, IntPtr w, ref Lib.XSizeHints hints, out IntPtr dummy);
	}
}


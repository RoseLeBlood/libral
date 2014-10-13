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

namespace liboRg.Window
{

	public class BaseGameWindow2 : BaseWindow, IGameWindow
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

		public BaseGameWindow2(Game pGame, string title, WindowStyle style)
			: base(pGame.m_strDisplay, "GameWindow", Colors.Black, pGame.ContextConfig.Resolution.Size, title)
		{
			m_pIContext = null;
			m_pGame = pGame;
			Namespace = "liboRg";
			ClassName = "BaseGameWindow";

			m_bFullScreen = style == (style & WindowStyle.Fullscreen);

				Resizeable = true;

			this.EventMask = EventMask.FocusChangeMask | EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.ButtonMotionMask | 
				EventMask.PointerMotionMask | EventMask.KeyPressMask | EventMask.KeyReleaseMask | EventMask.StructureNotifyMask 
				| EventMask.EnterWindowMask | EventMask.LeaveWindowMask;

			this.Created += BaseGameWindow2_Created;
			KeyPress += BaseGameWindow2_KeyPressed;
			KeyRelease += BaseGameWindow2_KeyRelease;
			Resize += BaseGameWindow2_Resize;
			UserEvent += BaseGameWindow2_UserEvents;
			Move += BaseGameWindow2_Move;
		}
		public override void Create()
		{
			Lib.XSetWindowAttributes attributes = new Lib.XSetWindowAttributes();
			attributes.event_mask = (TLong)EventMask; 
			attributes.override_redirect = m_bFullScreen ? (TBoolean)1 : (TBoolean)0;

			Rectangle.X = 0;
			Rectangle.Y = 0;

			if (m_bFullScreen)
			{
				
				EnableFullscreen(true, Rectangle.Width, Rectangle.Height);
			}
			IntPtr desktop = Lib.XRootWindow( Display.RawHandle, (TInt)Display.Screen.ScreenNumber );
			TInt depth = Lib.XDefaultDepth( Display.RawHandle, (TInt)Display.Screen.ScreenNumber );


			m_pHandle = Lib.XCreateWindow( Display.RawHandle, 
				desktop, 
				(TInt)0, (TInt)0, (TUint)Rectangle.Width, (TUint)Rectangle.Height, 
				(TUint)0, 
				(TInt)depth,
				(TUint)Lib.WindowClass.InputOutput, 
				Lib.XDefaultVisual( Display.RawHandle, (TInt)Display.Screen.ScreenNumber ), 
				Lib.WindowAttributeMask.EventMask | Lib.WindowAttributeMask.OverrideRedirect, 
				ref attributes );

			Lib.XStoreName( Display.RawHandle, m_pHandle, m_strTitle );


			// Set default window name
			string windowName = m_strName;
			X11._internal.Lib.XTextProperty windowNameProperty = new Lib.XTextProperty(),
			iconNameProperty = new Lib.XTextProperty();
			if (0 == (int)X11._internal.Lib.XStringListToTextProperty(ref windowName, (TInt)1, ref windowNameProperty) ||
			    0 == (int)X11._internal.Lib.XStringListToTextProperty(ref windowName, (TInt)1, ref iconNameProperty))
			{
				// XDestroyWindow() can generate a BadWindow error.
				X11._internal.Lib.XDestroyWindow(m_pDisplay.RawHandle, m_pHandle);
				throw new XStringListToTextPropertyException(101, "Create");

			}
			// Set Size Hints
			if (!m_bIsResizeable)
			{


				X11._internal.Lib.XSizeHints sizeHints = new Lib.XSizeHints();
				sizeHints.flags =  Lib.XSizeHintFlags.PMinSize | Lib.XSizeHintFlags.PMaxSize | 
					Lib.XSizeHintFlags.PSize | Lib.XSizeHintFlags.PPosition;
				sizeHints.x = 0;
				sizeHints.y = 0;
				sizeHints.width = m_xRectangle.Width;
				sizeHints.height = m_xRectangle.Height;

				sizeHints.min_width = m_xRectangle.Width;
				sizeHints.min_height = m_xRectangle.Height;
				sizeHints.max_width = m_xRectangle.Width;
				sizeHints.max_height = m_xRectangle.Height;
				//Lib.XSetWMNormalHints(m_pDisplay.RawHandle, m_pHandle, ref sizeHints ); -- geht net ???? Execption DLL NotFound ....
				XSetWMNormalHints(m_pDisplay.RawHandle, m_pHandle, ref sizeHints);
			}

			// Set WM Hints
			X11._internal.Lib.XWMHints wmHints = Lib.XAllocWMHints();
			if (!System.IO.File.Exists(m_strIconPath))
				{
					wmHints.flags = Lib.XWMHintMask.StateHint | Lib.XWMHintMask.InputHint;
					wmHints.initial_state = Lib.XWindowState.NormalState;
					wmHints.input = (TBoolean)1;
				}
			else
				{
					var pIcon = new Icon(m_strIconPath, Name + "_Icon", m_pDisplay.Name, m_pDisplay.Screen.Name);
					wmHints.flags = Lib.XWMHintMask.StateHint | Lib.XWMHintMask.IconPixmapHint | Lib.XWMHintMask.IconMaskHint |
						Lib.XWMHintMask.IconWindowHint | Lib.XWMHintMask.InputHint;
					wmHints.initial_state = Lib.XWindowState.NormalState;
					wmHints.input = (TBoolean)1;
					wmHints.icon_pixmap = pIcon.RawHandle;
					wmHints.icon_mask = (IntPtr)pIcon.Mask;
					wmHints.icon_window = m_pHandle;
				}	
			Lib.XSetWMHints(m_pDisplay.RawHandle, m_pHandle, ref wmHints); 

			// Set Class Hints
			X11._internal.Lib.XClassHint classHints = new Lib.XClassHint();
			var className = m_strClassName;

			classHints.res_name = null;
			classHints.res_class = className;
			Lib.XSetClassHint(m_pDisplay.RawHandle, m_pHandle, ref classHints);

			if (null == m_pParentWindow)
			{
				var atom = Lib.XInternAtom(m_pDisplay.RawHandle, "WM_DELETE_WINDOW", false);
				Lib.XSetWMProtocols(m_pDisplay.RawHandle, m_pHandle, ref atom, (TInt)1);
			}
			else
			{
				Lib.XSetTransientForHint(m_pDisplay.RawHandle, m_pHandle, m_pParentWindow.RawHandle);
			}
			if (!m_bShowCursor)
				{
					var cursorPixmap = Lib.XCreatePixmap(m_pDisplay.RawHandle, 
						Lib.XRootWindow(m_pDisplay.RawHandle, 
							(TInt)m_pDisplay.Screen.ScreenNumber), 
						(TUint)1, (TUint)1, (TUint)1);
					Lib.XColor cursorColor = new Lib.XColor();

					var cursorId = Lib.XCreatePixmapCursor(m_pDisplay.RawHandle, cursorPixmap, cursorPixmap, ref cursorColor, ref cursorColor, 0, 0);

					Lib.XDefineCursor(m_pDisplay.RawHandle, m_pHandle, cursorId);

					//Lib.XFreeCursor(m_pDisplay.RawHandle, cursorId);
					Lib.XFreePixmap(m_pDisplay.RawHandle, cursorPixmap);
				}
			else
				{
					// XCreateFontCursor can generate BadAlloc and BadValue errors.
					var cursorId = Lib.XCreateFontCursor(m_pDisplay.RawHandle, Lib.CursorFontShape.top_left_arrow);
					// XDefineCursor can generate BadCursor and BadWindow errors.
					Lib.XDefineCursor(m_pDisplay.RawHandle, m_pHandle, cursorId);
					// XFreeCursor can generate a BadCursor error.
					//::XFreeCursor(m_pDisplay.RawHandle, cursorId);

				}
			Lib.XFree(windowNameProperty.val);
			Lib.XFree(iconNameProperty.val);

			if (m_bFullScreen)
			{
					Lib.XGrabPointer(m_pDisplay.RawHandle, m_pHandle, true, 0, Lib.GrabMode.Async, Lib.GrabMode.Async, 
						m_pHandle, IntPtr.Zero, Lib.CurrentTime);
					Lib.XGrabKeyboard(m_pDisplay.RawHandle, m_pHandle, true, Lib.GrabMode.Async, 
						Lib.GrabMode.Async, Lib.CurrentTime);
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
		[DllImport("libX11.so")]
		private static extern void XSetWMNormalHints (IntPtr display, IntPtr w, ref X11._internal.Lib.XSizeHints hints);
	}
}


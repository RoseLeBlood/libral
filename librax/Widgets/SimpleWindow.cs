//
//  SimpleWindow.cs
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
using X11._internal;
using System.Common;
using System.Runtime.InteropServices;
using System.Reflection;

namespace X11.Widgets
{
	class NULLPtrWindowException : Exception 
	{
		public NULLPtrWindowException(int iLine, string Function) : base("SimpleWindow.cs", iLine, Function) { }

	}

	class XStringListToTextPropertyException : Exception {
		public XStringListToTextPropertyException(int iLine, string Function) : base("SimpleWindow.cs", iLine, Function) { }

	}
	[Serializable]
	public abstract class SimpleWindow : Window
	{
		protected SimpleWindow()
		{
		}
		public SimpleWindow(string strDisplay, string strName, Color pBackgroundColor,TRectangle Rectangle,  string strTitle = "LibX# Window",  
			EventMask eEventMask = EventMask.All,
			uint iBorderWidth = 0, bool bIsResizeable = true, bool bShowCursor = true, string strIconPath = "", 
			Window pParentWindow = null, string ClassName = "__SIMPLEWINDOW_LIBX__")
			: base(strDisplay, strName, pBackgroundColor, Rectangle,strTitle, eEventMask, iBorderWidth,
				bIsResizeable, bShowCursor, strIconPath, pParentWindow, ClassName)
		{

		}
		public override void Create()
		{
			int posX = 0;
			int posY = 0;
			if (null != m_pParentWindow)
			{
				posX = Rectangle.Position.X;
				posY = Rectangle.Position.Y;
			}
			TPixel borderPixel = X11._internal.Lib.XBlackPixel(m_pDisplay.RawHandle, (TInt)m_pDisplay.Screen.ScreenNumber);

			var backgroundPixel = m_colBackground.ToXColor(m_pDisplay);

			if (null == m_pParentWindow)
			{
				// Border width must be nil for the top-level windows!
				// XCreateSimpleWindow can generate BadAlloc, BadMatch, BadValue, and BadWindow errors.
				m_pHandle = X11._internal.Lib.XCreateSimpleWindow(m_pDisplay.RawHandle, 
					X11._internal.Lib.XRootWindow(m_pDisplay.RawHandle, (TInt)m_pDisplay.Screen.ScreenNumber), 
					(TInt)posX, 
					(TInt)posY, 
					(TUint)Rectangle.Width, 
					(TUint)Rectangle.Height, 
					(TUint)m_iBorderWidth, 
					borderPixel, 
					backgroundPixel.pixel);
			}
			else
			{
				// XCreateSimpleWindow can generate BadAlloc, BadMatch, BadValue, and BadWindow errors.
				m_pHandle = X11._internal.Lib.XCreateSimpleWindow(m_pDisplay.RawHandle, 
					m_pParentWindow.RawHandle, 
					(TInt)posX, 
					(TInt)posY, 
					(TUint)Rectangle.Width, 
					(TUint)Rectangle.Height, 
					(TUint)0, 
					borderPixel, 
					backgroundPixel.pixel);
			}	
			// Set default window name
			string windowName = m_strName;
			X11._internal.Lib.XTextProperty windowNameProperty = new Lib.XTextProperty(),
			iconNameProperty = new Lib.XTextProperty();
			if (0 == (int)X11._internal.Lib.XStringListToTextProperty(ref windowName, (TInt)1, ref windowNameProperty) ||
			    0 == (int)X11._internal.Lib.XStringListToTextProperty(ref windowName, (TInt)1, ref iconNameProperty))
			{
				// XDestroyWindow() can generate a BadWindow error.
				X11._internal.Lib.XDestroyWindow(m_pDisplay.RawHandle, m_pHandle);
				throw new XStringListToTextPropertyException(121, "Create");

			}
			// Set Size Hints
			X11._internal.Lib.XSizeHints sizeHints = new Lib.XSizeHints();
			sizeHints.flags = Lib.XSizeHintFlags.PPosition | Lib.XSizeHintFlags.PSize;
			sizeHints.x = Rectangle.X;
			sizeHints.y = Rectangle.Y;
			sizeHints.width = Rectangle.Width;
			sizeHints.height = Rectangle.Height;
			if (!m_bIsResizeable)
			{
				// Min and max width and height are set to prevent window's resizing. In addition,
				// this hides the 'restore' button on the title bar of the window.
				sizeHints.flags = sizeHints.flags | Lib.XSizeHintFlags.PMinSize | Lib.XSizeHintFlags.PMaxSize;
				sizeHints.min_width = Rectangle.Width;
				sizeHints.min_height = Rectangle.Height;
				sizeHints.max_width = Rectangle.Width;
				sizeHints.max_height = Rectangle.Height;
			}
			//Lib.XSetWMNormalHints(m_pDisplay.RawHandle, m_pHandle, ref sizeHints ); -- geht net ???? Execption DLL NotFound ....
			XSetWMNormalHints(m_pDisplay.RawHandle, m_pHandle, ref sizeHints);

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
			// If the cursor must be hidden - create the transparent cursor.
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

			Register();

			if (null != m_pParentWindow)
			{
				m_id = RegisterChild(this);
					OnCreate(new XEventArgs());
			}
			else
			{
					OnCreate(new XEventArgs());
			}


			base.Create();
		}
		public override void Show()
		{
			//Lib.XSelectInput(m_pDisplay.RawHandle, m_pHandle, m_iEventMask);
			Lib.XMapWindow(m_pDisplay.RawHandle, m_pHandle);
			if (null == m_pParentWindow)
			{
				Lib.XMoveWindow(m_pDisplay.RawHandle, m_pHandle, (TInt)m_xRectangle.X, (TInt)m_xRectangle.Y);
			}
			OnShow(new XEventArgs());
			Lib.XFlush(m_pDisplay.RawHandle);

			if (null == m_pParentWindow)
			{
				foreach (var item in m_Windows)
				{
					item.Show();
				}
			}
		}
		public override void Hide()
		{
			OnHide(new XEventArgs());
			if (null == m_pParentWindow)
			{
				foreach (var item in m_Windows)
				{
					item.Hide();
				}
			}
			Lib.XUnmapWindow(m_pDisplay.RawHandle, m_pHandle);
			Lib.XFlush(m_pDisplay.RawHandle);
		}


		public override void Destroy()
		{
			OnDestroy(new XEventArgs());
			if (null == m_pParentWindow)
			{
				int windowsCount = m_Windows.Count;
				if (windowsCount > 0)
				{
					windowsCount--;
					for (int i = windowsCount; i >= 0; i--)
					{
						if (null != m_Windows[i])
						{
							m_Windows[i].UnregisterChild(i);
						}
					}
				}
			}
			else
			{
				m_pParentWindow.UnregisterChild(m_id);
			}
			Lib.XUnmapWindow(m_pDisplay.RawHandle, m_pHandle);
			Lib.XFlush(m_pDisplay.RawHandle);
			Lib.XUndefineCursor(m_pDisplay.RawHandle, m_pHandle);
			Lib.XDestroyWindow(m_pDisplay.RawHandle, m_pHandle);
		}
		protected override void CallHandler(string eventName, XEventArgs args)
		{
			if (m_handler.ContainsKey(eventName))
				{
					Type calcType = this.GetType();
					calcType.InvokeMember(m_handler[eventName],
						BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
						null, this, new object[] { this, args });
				}
		}

		[DllImport("libX11.so")]
		private static extern void XSetWMNormalHints (IntPtr display, IntPtr w, ref X11._internal.Lib.XSizeHints hints);
	}
}


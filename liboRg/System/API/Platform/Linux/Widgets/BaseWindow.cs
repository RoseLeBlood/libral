﻿//
//  IWindow.cs
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
using System.Common;
using System.Xml.Serialization;
using System.Reflection;
using System.IO;
using System.ComponentModel;

namespace System.API.Platform.Linux.Widgets
{
	public abstract class BaseWindow : XHandle, IHandle, IWindow
	{
		protected IDisplay    	m_pDisplay;
		protected Color	   		m_colBackground;
		protected Color			m_colBorder;
		protected Rectangle 	m_xRectangle;
		protected string 	   	m_strTitle;
		protected bool			m_bHaveFocus;
		protected string 		m_strIconPath;
		protected bool 			m_bHasParent;
		protected bool 			m_bShowCursor;
		protected EventMask     m_iEventMask;
		protected IWindow		m_pParentWindow;
		protected List<IWindow>	m_Windows;
		protected int			m_id;
		protected string		m_strClassName;
		protected bool			m_bIsResizeable;
		protected int 			m_iBorderWidth;
		private   bool			m_bIsCreated;
		private   string		m_strNamespace;

		public override string Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}
		public IDisplay			Display
		{
			get { return m_pDisplay; }
		}

		public Color		Background
		{
			get { return m_colBackground; }
			set { m_colBackground = value; setBackground(m_colBackground); }
		}
		public Color		Border
		{
			get { return m_colBorder; }
			set { m_colBorder = value; setBorder(m_colBorder); }
		}
		public Rectangle		Rectangle
		{
			get { return m_xRectangle; }
		}
		public string			Title
		{
			get { return m_strTitle; }
			set { SetTitle(value); }
		}
		public bool				 IsFocusable
		{
			get { return m_bHaveFocus; }
		}
		public string				Icon
		{
			get { return m_strIconPath; }
			set { m_strIconPath = value; }
		}
		public bool 				ShowCursor
		{
			get { return m_bShowCursor; }
			set { m_bShowCursor = value;}
		}
		public EventMask			EventMask
		{
			get { return m_iEventMask; }
			set { m_iEventMask = value; }
		}
		public int					ID
		{
			get { return m_id; }
		}
		public string				ClassName
		{
			get { return m_strClassName; }
			set { m_strClassName = value; }
		}
		public int					BorderWidth
		{
			get { return m_iBorderWidth; }
			set { m_iBorderWidth = value; }
		}
		public IWindow				Parent
		{
			get {  return (m_pParentWindow != null) ? m_pParentWindow : null; }
			set { m_pParentWindow = value; }
		}
		public string Namespace
		{
			get { return m_strNamespace; }
			set { m_strNamespace = value; }
		}

		public EventHandler<XEventArgs> Created; //
		public EventHandler<XEventArgs> Destroyed; //
		public EventHandler<XEventArgs> Showed;
		public EventHandler<XEventArgs> Hidded;
		public EventHandler<XEventArgs> ClientMessage; //
		public EventHandler<XEventArgs> UserEvent; //
		public EventHandler<XEventArgs> Expose; //
		public EventHandler<XEventArgs> Focus; //
		public EventHandler<XEventArgs> LostFocus; //
		public EventHandler<XMoveEventArgs> Move; //
		public EventHandler<XResizeEventArgs> Resize; //
		public EventHandler<XKeyEventArgs> KeyPress; //
		public EventHandler<XKeyEventArgs> KeyRelease; //
		public EventHandler<XMotionEventArgs> MouseMove; //
		public EventHandler<XEventArgs> MouseEnter; //
		public EventHandler<XEventArgs> MouseLeave; //
		public EventHandler<XMouseButtonPressArgs> MouseButtonPress; //
		public EventHandler<XMouseButtonReleaseArgs> MouseButtonRelease; //
		public EventHandler<XMouseScrollArgs> MouseScroll; //
		public EventHandler<XUnknownEventArgs> UnknownEvent;




		public bool Resizeable
		{
			get { return m_bIsResizeable; }
			set { m_bIsResizeable = value; }
		}
		public bool IsOpen
		{
			get { return m_bIsCreated; }
		}

		public virtual void Create()
		{
			m_bIsCreated = true;
		}
		public virtual void Destroy()
		{
			Application.Current.UnRegisterHandle(Name);
			m_bIsCreated = false;
		}
		public abstract void Show();
		public abstract void Hide();
		public abstract void EnableFullscreen( bool enabled, Size size);

		internal BaseWindow(string strDisplay)
		{
			m_pDisplay = Application.Current.GetHandle<IDisplay>(strDisplay);
			if (m_pDisplay == null)
				throw new NULLPtrConnectionException("IWindow.cs", 294, "Window::Window(string,string)");

			m_Windows = new List<IWindow>();
			m_xRectangle = new Rectangle(0,0,320,320);
			m_colBackground = Colors.LightBlue;
			m_colBorder = Colors.LightSteelBlue;
			m_strIconPath = null;
			m_bShowCursor = true;
			m_iEventMask = 0;
			m_pParentWindow = null;
			m_id = -1;
			m_strClassName = ClassName;
			m_bIsResizeable = true;
			m_iBorderWidth = 0;
		}
		public BaseWindow (string strDisplay, string strName, Color pBackgroundColor,Rectangle Rectangle, 
			string strTitle = "LibX# Window", EventMask eEventMask = EventMask.ButtonPressMask| EventMask.KeyPressMask,
			uint iBorderWidth = 0, bool bIsResizeable = true, bool bShowCursor = true, string strIconPath = null, 
			BaseWindow pParentWindow = null, string ClassName = "__SIMPLEWINDOW_LIBX__")
			: base(strName)
		{
			m_pDisplay = Application.Current.GetHandle<IDisplay>(strDisplay);
			if (m_pDisplay == null)
				throw new NULLPtrConnectionException("IWindow.cs", 320, "Window::Window(string,string)");

			m_xRectangle = Rectangle;
			m_colBackground = pBackgroundColor;
			m_colBorder = Colors.LightSteelBlue;
			m_strTitle = strTitle;
			m_strIconPath = null;
			m_bShowCursor = bShowCursor;
			m_iEventMask = eEventMask;
			m_pParentWindow = pParentWindow;
			m_Windows = new List<IWindow>();
			m_id = -1;
			m_strClassName = ClassName;
			m_bIsResizeable = bIsResizeable;
			m_iBorderWidth = 0;

		}


		protected virtual void OnCreate(XEvent args)
		{
			if (Created != null) Created(this, new XEventArgs());
		}

		protected virtual void OnDestroy(XEvent args)
		{
			if (Destroyed != null) Destroyed(this, new XEventArgs());
		}
		protected virtual void OnClientMessage(XEvent args)
		{
			if (ClientMessage != null)
				ClientMessage(this, new XEventArgs());
		}

		protected virtual void OnConfigureNotify(XEvent args)
		{
			if (m_xRectangle.Width != (int)args.ConfigureEvent.width ||
				m_xRectangle.Height != (int)args.ConfigureEvent.height)
			{
					m_xRectangle.Width = (int)args.ConfigureEvent.width;
					m_xRectangle.Height = (int)args.ConfigureEvent.height;
				
				OnResize(args);
			}
		}
		protected virtual void OnResize(XEvent args)
		{
			var arg = new XResizeEventArgs(Rectangle.Size);
			if (Resize != null)
				Resize(this, arg);
		}
		protected virtual void OnMove(XEvent args)
		{
			var arg = new XMoveEventArgs(Rectangle.Location);
			if (Move != null)
				Move(this, arg);
		}
		protected virtual void OnKeyPress(XEvent xevent)
		{
			if (KeyPress != null)
				KeyPress(this, new XKeyEventArgs((Keys)xevent.KeyEvent.keycode));
		}
		protected virtual void OnKeyRelease(XEvent xevent)
		{
			if (KeyPress != null)
				KeyRelease(this, new XKeyEventArgs((Keys)xevent.KeyEvent.keycode));
		}
		protected virtual void OnFocus(XEvent xevent)
		{
			m_bHaveFocus = true;
			if (Focus != null)
				Focus(this, new XEventArgs());
		}
		protected virtual void OnLostFocus(XEvent xevent)
		{
			m_bHaveFocus = false;
			if (LostFocus != null)
				LostFocus(this, new XEventArgs());
		}
		protected virtual void OnExpose(XEvent xevent)
		{
			if (Expose != null)
				Expose(this, new XEventArgs());
		}
		protected virtual void OnMouseMove(XEvent xevent)
		{
			if (MouseMove != null)
				MouseMove(this, new XMotionEventArgs(new Point(
					xevent.MotionEvent.x,
					xevent.MotionEvent.y)));
		}
		protected virtual void OnShow(XEvent xevent)
		{
			if (Showed != null)
				Showed(this, new XEventArgs());
		}
		protected virtual void OnHide(XEvent xevent)
		{
			if (Hidded != null)
				Hidded(this, new XEventArgs());
		}
		protected virtual void OnMouseEnter(XEvent args)
		{
			if (MouseEnter != null)
				MouseEnter(this, new XEventArgs());
		}
		protected virtual void OnMouseLeave(XEvent args)
		{
			if (MouseLeave != null)
				MouseLeave(this, new XEventArgs());
		}
		public int RegisterChild(IWindow pWindow)
		{
			if (null == pWindow)
			{
				throw new NULLPtrWindowException(223, "RegisterChild [" + Name + "]");
			}
			if (null == m_pParentWindow)
			{
					m_Windows.Add(pWindow);
				return m_Windows.Count - 1;
			}
			else
			{
				return m_pParentWindow.RegisterChild(pWindow);
			}
		}
		protected virtual void OnMouseButtonPress(XEvent xevent)
		{
			MouseButton button = (MouseButton)xevent.ButtonEvent.button;

			var mousex = xevent.ButtonEvent.x;
			var mousey = xevent.ButtonEvent.y;


			if (MouseButtonPress != null)
				MouseButtonPress(this, new XMouseButtonPressArgs(new Point(mousex,
					mousey), button));

		}
		protected virtual void OnMouseButtonRelease(XEvent xevent)
		{
			MouseButton button = (MouseButton)xevent.ButtonEvent.button;

			var mousex = xevent.ButtonEvent.x;
			var mousey = xevent.ButtonEvent.y;

			if (button == MouseButton.ScrollDown || button == MouseButton.ScrollUp)
				{
					var delta = button == MouseButton.ScrollUp ? 1 : -1;
					if (MouseScroll != null)
						MouseScroll(this, new XMouseScrollArgs(delta));
				}
			else
				{
					if (MouseButtonRelease != null)
						MouseButtonRelease(this, new XMouseButtonReleaseArgs(new Point(mousex, mousey), button));
				}

		}
		public void UnregisterChild(int iId)
		{
			if (null == m_pParentWindow)
			{
				m_Windows[iId] = null;
			}
			else
			{
				m_pParentWindow.UnregisterChild(iId);
				if (ID == iId)
				{
					m_id = -1;
				}
			}
		}
		public void SetWindowOpacity(float value)
		{
			if (value > 1.0f) value = 1.0f;
			if (value < 0.0f) value = 0.0f;

			m_colBackground.Alpha = value;
			uint opacity = (uint)(((double)value) * 0xffffffff);


			IntPtr atom = Lib.XInternAtom(m_pDisplay.RawHandle, "_NET_WM_WINDOW_OPACITY", false);
			Lib.XChangeProperty(m_pDisplay.RawHandle, m_pHandle, atom,
				Lib.AtomType.CARDINAL, (TInt)32, Lib.PropMode.Replace, ref opacity, (TInt)1);

		}

		private void SetTitle(string value)
		{
			if (m_bIsCreated)
			{
				Lib.XStoreName(m_pDisplay.RawHandle, m_pHandle, value);
			}
			m_strTitle = value;
		}

		private void SetRectangle(string value)
		{
			m_xRectangle = new System.Common.Rectangle(value);
			if (m_bIsCreated)
			{
					Lib.XMoveResizeWindow(m_pDisplay.RawHandle, m_pHandle, (TInt)m_xRectangle.X, (TInt)m_xRectangle.Y, 
						(TUint)m_xRectangle.Width, 
						(TUint)m_xRectangle.Height);
			}

		}

		private void setBorder(Color str)
		{
			if(m_colBorder != str)
				m_colBorder = str;
			if (m_bIsCreated)
			{
			}
		}

		private void setBackground(Color color)
		{
			if(m_colBackground != color)
				m_colBackground = color;
			if (m_bIsCreated)
			{
				SetWindowOpacity(m_colBackground.Alpha);
			}
		}
		public void OnIdle()
		{
			if (UserEvent != null)
				UserEvent(this, new XEventArgs());
		}
		public bool Event(Object xevent)
		{
			IWindow window = null;
			XEvent evvent = (XEvent)xevent;
			if (evvent.AnyEvent.window == m_pHandle)
			{
				window = this;
			}
			else
			{
				for (int i = 0; i < m_Windows.Count; i++)
				{
					if (null != m_Windows[i])
					{
						if (evvent.AnyEvent.window == m_Windows[i].RawHandle)
						{
							window = m_Windows[i];

							(window as BaseWindow).WndProc(evvent);
							break;
						}
					}
				}
			}
			if (m_pParentWindow == null)
				return WndProc(evvent);
			else
				return m_pParentWindow.Event(xevent);
		}
		protected virtual bool WndProc(XEvent xevent)
		{
			switch (xevent.type)
			{
				case XEventName.CreateNotify:
					OnCreate(xevent);
					break;
				case XEventName.DestroyNotify:
					OnDestroy(xevent);
					break;
				case XEventName.ButtonRelease:
					OnMouseButtonRelease(xevent);
					break;
				case XEventName.ClientMessage:
				{
					IntPtr protocolsAtom = Lib.XInternAtom(Display.RawHandle, "WM_PROTOCOLS", false);
					IntPtr deleteWindowAtom = Lib.XInternAtom(Display.RawHandle, "WM_DELETE_WINDOW", false);

					if (xevent.ClientMessageEvent.message_type == protocolsAtom &&
					    xevent.ClientMessageEvent.ptr1 == deleteWindowAtom)
					{
						return false;
					}
					OnClientMessage(xevent);
				}
				break;
		
				case XEventName.ConfigureNotify:
					{
						if (Rectangle.Width != (int)xevent.ConfigureEvent.width ||
						    Rectangle.Height != (int)xevent.ConfigureEvent.height)
						{
							Rectangle.Width = (int)xevent.ConfigureEvent.width;
							Rectangle.Height = (int)xevent.ConfigureEvent.height;

							OnResize(xevent);
						}
						else if ((int)xevent.ConfigureEvent.x != Rectangle.X ||
						         (int)xevent.ConfigureEvent.y != Rectangle.Y)
						{
							Rectangle.X = (int)xevent.ConfigureEvent.x;
							Rectangle.Y = (int)xevent.ConfigureEvent.y;
							OnMove(xevent);
						}
					}
					break;
				case XEventName.MapNotify:
					OnShow(xevent);
					break;
				case XEventName.UnmapNotify:
					OnHide(xevent);
					break;
				case XEventName.KeyPress:
					OnKeyPress(xevent);
					break;
				case XEventName.KeyRelease:
					OnKeyRelease(xevent);
					break;
				case XEventName.FocusOut:
					OnLostFocus(xevent);
					break;
				case XEventName.FocusIn:
					OnFocus(xevent);
					break;
				case XEventName.Expose:
					OnExpose(xevent);
					break;
				case XEventName.MotionNotify:
					OnMouseMove(xevent);
					break;
				case XEventName.EnterNotify:
					OnMouseEnter(xevent);
					break;
				case XEventName.LeaveNotify:
					OnMouseLeave(xevent);
					break;
				default:
					if (UnknownEvent != null)
						UnknownEvent(this, new XUnknownEventArgs(xevent));
					break;
			}
					

			return m_bIsCreated;
		}
	}
}


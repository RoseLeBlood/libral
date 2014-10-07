//
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
using X11._internal;
using System.Collections.Generic;
using System.Common;
using System.Xml.Serialization;
using System.Reflection;
using System.IO;
using libral;
using System.ComponentModel;
using X11.Widgets.Event;

namespace X11.Widgets
{
	public abstract class BaseWindow : XHandle, IHandle
	{
		protected Display    	m_pDisplay;
		protected Color	   		m_colBackground;
		protected Color			m_colBorder;
		protected Rectangle 	m_xRectangle;
		protected string 	   	m_strTitle;
		protected bool			m_bHaveFocus;
		protected string 		m_strIconPath;
		protected bool 			m_bHasParent;
		protected bool 			m_bShowCursor;
		protected EventMask     m_iEventMask;
		protected BaseWindow		m_pParentWindow;
		protected List<BaseWindow>	m_Windows;
		protected int			m_id;
		protected string		m_strClassName;
		protected bool			m_bIsResizeable;
		protected int 			m_iBorderWidth;
		private   bool			m_bIsCreated;
		private   string		m_strNamespace;
		private	  bool			m_bClose;

		public override string Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}
		public Display			Display
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
		public BaseWindow				Parent
		{
			get {  return (m_pParentWindow != null) ? m_pParentWindow : null; }
			set { m_pParentWindow = value; }
		}
		public string Namespace
		{
			get { return m_strNamespace; }
			set { m_strNamespace = value; }
		}

		public EventHandler<XEventArgs> Created;
		public EventHandler<XEventArgs> Destroyed;
		public EventHandler<XEventArgs> Showed;
		public EventHandler<XEventArgs> Hidded;
		public EventHandler<XEventArgs> ClientMessage;
		public EventHandler<XEventArgs> UserEvent;
		public EventHandler<XEventArgs> Expose;
		public EventHandler<XEventArgs> Focus;
		public EventHandler<XEventArgs> LostFocus;
		public EventHandler<XMoveEventArgs> Move;
		public EventHandler<XResizeEventArgs> Resize;
		public EventHandler<XKeyEventArgs> KeyPress;
		public EventHandler<XKeyEventArgs> KeyRelease;




		/*
		[XmlAttribute("MouseKeyPress")]
		public string MouseKeyPress
		{
			get { return m_handler.ContainsKey("MouseKeyPress") ? m_handler["MouseKeyPress"] : null; }
			set { SetHandler(value, "MouseKeyPress"); }
		}
		[XmlAttribute("MouseKeyRelease")]
		public string MouseKeyRelease
		{
			get { return m_handler.ContainsKey("MouseKeyRelease") ? m_handler["MouseKeyRelease"] : null; }
			set { SetHandler(value, "MouseKeyRelease"); }
		}

		[XmlAttribute("MouseEnter")]
		public string MouseEnter
		{
			get { return m_handler.ContainsKey("MouseEnter") ? m_handler["MouseEnter"] : null; }
			set { SetHandler(value, "MouseEnter"); }
		}
		[XmlAttribute("MouseMove")]
		public string MouseMove
		{
			get { return m_handler.ContainsKey("MouseMove") ? m_handler["MouseMove"] : null; }
			set { SetHandler(value, "MouseMove"); }
		}
		[XmlAttribute("MouseLeave")]
		public string MouseLeave
		{
			get { return m_handler.ContainsKey("MouseLeave") ? m_handler["MouseLeave"] : null; }
			set { SetHandler(value, "MouseLeave"); }
		}*/
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
			m_bIsCreated = false;
		}
		public abstract void Show();
		public abstract void Hide();

		internal BaseWindow(string strDisplay)
		{
			m_pDisplay = Application.Current.GetHandle<Display>(strDisplay);
			if (m_pDisplay == null)
				throw new NULLPtrConnectionException("IWindow.cs", 294, "Window::Window(string,string)");

			m_Windows = new List<BaseWindow>();
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
			m_pDisplay = Application.Current.GetHandle<Display>(strDisplay);
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
			m_Windows = new List<BaseWindow>();
			m_id = -1;
			m_strClassName = ClassName;
			m_bIsResizeable = bIsResizeable;
			m_iBorderWidth = 0;

		}
		protected virtual void OnCreated(XEvent args)
		{
			if (Created != null) Created(this, new XEventArgs());
		}

		protected virtual void OnDestroy(XEvent args)
		{
			if (Destroyed != null) Destroyed(this, new XEventArgs());
		}
		protected virtual void OnClientMessage(XEvent args)
		{
			IntPtr protocolsAtom = Lib.XInternAtom(m_pDisplay.RawHandle, "WM_PROTOCOLS", false);
			IntPtr deleteWindowAtom = Lib.XInternAtom(m_pDisplay.RawHandle, "WM_DELETE_WINDOW", false);

			if(args.ClientMessageEvent.message_type == protocolsAtom && 
			   args.ClientMessageEvent.ptr1 == deleteWindowAtom) 
			{
					//return false;

			}
					

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
			var arg = new XMoveEventArgs(Rectangle.Position);
			if (Move != null)
				Move(this, arg);
		}

		public int RegisterChild(BaseWindow pWindow)
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
				X11._internal.Lib.XStoreName(m_pDisplay.RawHandle, m_pHandle, value);
			}
			m_strTitle = value;
		}

		private void SetRectangle(string value)
		{
			m_xRectangle = new libral.Rectangle(value);
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
		internal void OnIdle()
		{
			if (UserEvent != null)
				UserEvent(this, new XEventArgs());
		}
		internal bool Event(XEvent xevent)
		{
			BaseWindow window = null;
			if (xevent.AnyEvent.window == m_pHandle)
			{
				window = this;
			}
			else
			{
				for (int i = 0; i < m_Windows.Count; i++)
				{
					if (null != m_Windows[i])
					{
						if (xevent.AnyEvent.window == m_Windows[i].RawHandle)
						{
							window = m_Windows[i];

							window.WndProc(xevent);
							break;
						}
					}
				}
			}
			if (m_pParentWindow == null)
				return WndProc(xevent);
			else
				return m_pParentWindow.Event(xevent);
		}
		protected virtual bool WndProc(XEvent xevent)
		{
			switch (xevent.type)
			{
				case XEventName.ClientMessage:
				{
					IntPtr protocolsAtom = Lib.XInternAtom(Display.RawHandle, "WM_PROTOCOLS", false);
					IntPtr deleteWindowAtom = Lib.XInternAtom(Display.RawHandle, "WM_DELETE_WINDOW", false);

					if (xevent.ClientMessageEvent.message_type == protocolsAtom &&
					    xevent.ClientMessageEvent.ptr1 == deleteWindowAtom)
					{
						OnClientMessage(xevent);

						return false;
					}
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
				default:
					break;
			}




			if (xevent.type == XEventName.FocusOut)
			{
				m_bHaveFocus = false;
				
			}
			else if (xevent.type == XEventName.FocusIn)
			{
				m_bHaveFocus = true;
			}

			return m_bIsCreated;
		}
	}
}


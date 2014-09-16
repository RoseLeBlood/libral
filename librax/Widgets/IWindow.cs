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

namespace X11.Widgets
{
	public class XEventArgs: EventArgs
	{
		public XEvent Event { get ; private set; }
		public XEventArgs() { }
		public XEventArgs(XEvent xevent) { Event = xevent; }
	}
	[Serializable]
	[XmlRoot(ElementName = "Window")]
	public abstract class Window : XHandle, IHandle
	{
		protected Display    	m_pDisplay;
		protected Color	   		m_colBackground;
		protected Color			m_colBorder;
		protected TRectangle 	m_xRectangle;
		protected string 	   	m_strTitle;
		protected bool			m_bHaveFocus;
		protected string 		m_strIconPath;
		protected bool 			m_bHasParent;
		protected bool 			m_bShowCursor;
		protected EventMask     m_iEventMask;
		protected Window		m_pParentWindow;
		protected List<Window>	m_Windows;
		protected SortedDictionary<string /*name */, string /* function */> m_handler;
		protected int			m_id;
		protected string		m_strClassName;
		protected bool			m_bIsResizeable;
		protected int 			m_iBorderWidth;
		private   bool			m_bIsCreated;
		private   string		m_strNamespace;

		[XmlIgnore]
		public Display			Display
		{
			get { return m_pDisplay; }
		}
		[XmlAttribute("Background", typeof(Color))]
		public Color			Background
		{
			get { return m_colBackground; }
			set { m_colBackground = value; }
		}
		[XmlAttribute("Border", typeof(Color))]
		public Color			Border
		{
			get { return m_colBorder; }
			set { m_colBorder = value; }
		}
		[XmlAttribute("Rectangle", typeof(TRectangle))]
		public TRectangle		Rectangle
		{
			get { return m_xRectangle; }
			set { SetRectangle(value); }
		}
		[XmlAttribute("Title")]
		public string			Title
		{
			get { return m_strTitle; }
			set { SetTitle(value); }
		}
		[XmlIgnore]
		public bool				 IsFocusable
		{
			get { return m_bHaveFocus; }
		}
		[XmlAttribute("Icon")]
		public string				Icon
		{
			get { return m_strIconPath; }
			set { m_strIconPath = value; }
		}
		[XmlAttribute("ShowCursor")]
		public bool 				ShowCursor
		{
			get { return m_bShowCursor; }
			set { }
		}
		[XmlAttribute("EventMask")]
		public EventMask			EventMask
		{
			get { return m_iEventMask; }
			set { m_iEventMask = value; }
		}
		[XmlIgnore]
		public int					ID
		{
			get { return m_id; }
		}
		[XmlAttribute("ClassName")]
		public string				ClassName
		{
			get { return m_strClassName; }
			set { m_strClassName = value; }
		}
		[XmlAttribute("BorderWidth")]
		public int					BorderWidth
		{
			get { return m_iBorderWidth; }
			set { BorderWidth = value; }
		}
		[XmlAttribute("Parent")]
		public string				Parent
		{
			get {  return (m_pParentWindow != null) ? m_pParentWindow.Name : null; }
			set { m_pParentWindow = Application.Current.GetHandle<Window>(value); }
		}
		[XmlAttribute("Namespace")]
		public string Namespace
		{
			get { return m_strNamespace; }
			set { m_strNamespace = value; }
		}
		[XmlAttribute("KeyPress")]
		public string KeyPress
		{
			get { return m_handler.ContainsKey("KeyPress") ? m_handler["KeyPress"] : null; }
			set { SetHandler(value, "KeyPress"); }
		}
		[XmlAttribute("KeyRelease")]
		public string KeyRelease
		{
			get { return m_handler.ContainsKey("KeyRelease") ? m_handler["KeyRelease"] : null; }
			set { SetHandler(value, "KeyRelease"); }
		}
		[XmlAttribute("Created")]
		public string Created
		{
			get { return m_handler.ContainsKey("Created") ? m_handler["Created"] : null; }
			set { SetHandler(value, "Created"); }
		}
		[XmlAttribute("Destroyed")]
		public string Destroyed
		{
			get { return m_handler.ContainsKey("Destroyed") ? m_handler["Destroyed"] : null; }
			set { SetHandler(value, "Destroyed"); }
		}
		[XmlAttribute("Showed")]
		public string Showed
		{
			get { return m_handler.ContainsKey("Showed") ? m_handler["Showed"] : null; }
			set { SetHandler(value, "Showed"); }
		}
		[XmlAttribute("Hidded")]
		public string Hidded
		{
			get { return m_handler.ContainsKey("Hidded") ? m_handler["Hidded"] : null; }
			set { SetHandler(value, "Hidded"); }
		}
		[XmlAttribute("ClientMessage")]
		public string ClientMessage
		{
			get { return m_handler.ContainsKey("ClientMessage") ? m_handler["ClientMessage"] : null; }
			set { SetHandler(value, "ClientMessage"); }
		}
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
		[XmlAttribute("Expose")]
		public string Expose
		{
			get { return m_handler.ContainsKey("Expose") ? m_handler["Expose"] : null; }
			set { SetHandler(value, "Expose"); }
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
		}
		[XmlAttribute("Focus")]
		public string Focus
		{
			get { return m_handler.ContainsKey("Focus") ? m_handler["Focus"] : null; }
			set { SetHandler(value, "Focus"); }
		}
		[XmlAttribute("LostFocus")]
		public string LostFocus
		{
			get { return m_handler.ContainsKey("LostFocus") ? m_handler["LostFocus"] : null; }
			set { SetHandler(value, "LostFocus"); }
		}
		[XmlAttribute("Resize")]
		public string Resize
		{
			get { return m_handler.ContainsKey("Resize") ? m_handler["Resize"] : null; }
			set { SetHandler(value, "Resize"); }
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

		internal Window()
		{
		}
		public Window (string strDisplay, string strName, Color pBackgroundColor,TRectangle Rectangle, 
			string strTitle = "LibX# Window", EventMask eEventMask = EventMask.All,
			uint iBorderWidth = 0, bool bIsResizeable = true, bool bShowCursor = true, string strIconPath = null, 
			Window pParentWindow = null, string ClassName = "__SIMPLEWINDOW_LIBX__")
			: base(strName)
		{
			m_pDisplay = Application.Current.GetHandle<Display>(strDisplay);
			if (m_pDisplay == null)
				throw new NULLPtrConnectionException("IWindow.cs", 87, "Window::Window(string,string)");

			m_xRectangle = Rectangle;
			m_colBackground = pBackgroundColor;
			m_colBorder = Colors.LightSteelBlue;
			m_strTitle = strTitle;
			m_strIconPath = null;
			m_bShowCursor = bShowCursor;
			m_iEventMask = eEventMask;
			m_pParentWindow = pParentWindow;
			m_Windows = new List<Window>();
			m_id = -1;
			m_strClassName = ClassName;
			m_bIsResizeable = bIsResizeable;
			m_handler = new SortedDictionary<string, string>();

		}
		protected virtual bool OnCreate(XEventArgs args)
		{
			CallHandler("Created", args);
			return true;
		}
		protected virtual bool OnDestroy(XEventArgs  args)
		{
			CallHandler("Destroyed", args);
			return true;
		}
		protected virtual bool OnClientMessage(XEventArgs args)
		{
			IntPtr protocolsAtom = Lib.XInternAtom(m_pDisplay.RawHandle, "WM_PROTOCOLS", false);
			IntPtr deleteWindowAtom = Lib.XInternAtom(m_pDisplay.RawHandle, "WM_DELETE_WINDOW", false);

			if(args.Event.ClientMessageEvent.message_type == protocolsAtom && 
			   args.Event.ClientMessageEvent.ptr1 == deleteWindowAtom) 
			{
				return false;
			}
					
			CallHandler("ClientMessage", args);
			return true;
		}
		protected virtual bool OnShow(XEventArgs args)
		{
			CallHandler("Showed", args);
			return true;
		}
		protected virtual bool OnHide(XEventArgs args)
		{
			CallHandler("Hidded", args);
			return true;
		}
		protected virtual bool OnExpose(XEventArgs args)
		{
			CallHandler("Expose", args);
			return true;
		}
		protected virtual bool OnButtonPress(XEventArgs args)
		{
			CallHandler("MouseKeyPress", args);
			return true;
		}
		protected virtual bool OnButtonRelease(XEventArgs args)
		{
			CallHandler("MouseKeyRelease", args);
			return true;
		}
		protected virtual bool OnKeyPress(XEventArgs args)
		{
			CallHandler("KeyPress", args);
			return true;
		}
		protected virtual bool OnKeyRelease(XEventArgs args)
		{
			CallHandler("KeyRelease", args);
			return true;
		}
		protected virtual bool OnMouseEnter(XEventArgs args)
		{
			CallHandler("MouseEnter", args);
			return true;
		}
		protected virtual bool OnMouseLeave(XEventArgs args)
		{
			CallHandler("MouseLeave", args);
			return true;
		}
		protected virtual bool OnMouseMove(XEventArgs args)
		{
			CallHandler("MouseMove", args);
			return true;
		}
		protected virtual bool OnGotFocus(XEventArgs args)
		{
			m_bHaveFocus = true;
			CallHandler("Focus", args);
			return true;
		}
		protected virtual bool OnLostFocus(XEventArgs args)
		{
			CallHandler("Focus", args);
			return true;
		}
		protected virtual bool OnConfigureNotify(XEventArgs args)
		{
			if (m_xRectangle.Width != (int)args.Event.ConfigureEvent.width ||
				m_xRectangle.Height != (int)args.Event.ConfigureEvent.height)
			{
					m_xRectangle.Width = (int)args.Event.ConfigureEvent.width;
					m_xRectangle.Height = (int)args.Event.ConfigureEvent.height;
				
				return OnResize(args);
			}
			return true;
		}
		protected virtual bool OnResize(XEventArgs args)
		{
			CallHandler("Resize", args);
			return true;
		}

		internal void NextEvent(ref XEvent xevent)
		{
			Lib.XNextEvent(m_pDisplay.RawHandle, ref xevent);
		}
		public int RegisterChild(Window pWindow)
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
		internal bool Event(XEvent xevent)
		{
			Window window = null;
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
							break;
						}
					}
				}
			}
			if (window != null)
				return window.HandleEvent(xevent);
			else
				return false;
		}
		protected virtual bool HandleEvent(XEvent xevent)
		{


			switch (xevent.type )
			{
				case XEventName.KeyPress:
					return OnKeyPress(new XEventArgs(xevent));
				case XEventName.KeyRelease:
					return OnKeyRelease(new XEventArgs(xevent));
				case XEventName.ClientMessage:
					return OnClientMessage(new XEventArgs(xevent));
				case XEventName.ButtonPress:
					return OnButtonPress(new XEventArgs(xevent));
				case XEventName.ButtonRelease:
					return OnButtonRelease(new XEventArgs(xevent));
				case XEventName.MotionNotify:
					return OnMouseMove(new XEventArgs(xevent));
				case XEventName.EnterNotify:
					return OnMouseEnter(new XEventArgs(xevent));
				case XEventName.LeaveNotify:
					return OnMouseLeave(new XEventArgs(xevent));
				case XEventName.FocusIn:
					return OnGotFocus(new XEventArgs(xevent));
				case XEventName.FocusOut:
					return OnLostFocus(new XEventArgs(xevent));
				case XEventName.MapNotify:
					return OnShow(new XEventArgs(xevent));
				case XEventName.UnmapNotify:
					return OnHide(new XEventArgs(xevent));
				case XEventName.ConfigureNotify:
					return OnConfigureNotify(new XEventArgs(xevent));
				default:
					Console.WriteLine("Not Handled Event: {0}", xevent.type);
					return true;
			}
		}


		void SetTitle(string value)
		{
			if (m_bIsCreated)
			{
			
			}
			m_strTitle = value;
		}

		void SetRectangle(TRectangle value)
		{
			if (m_bIsCreated)
			{
				Lib.XMoveResizeWindow(m_pDisplay.RawHandle, m_pHandle, (TInt)Rectangle.X, (TInt)Rectangle.Y, 
					(TUint)Rectangle.Width, 
					(TUint)Rectangle.Height);
			}
			m_xRectangle = value;
		}

		public static void SaveAsXml(Window wnd)
		{
			using (FileStream stream = new FileStream(wnd.Name + ".xml", FileMode.Create))
			{
				XmlSerializer x = new XmlSerializer(wnd.GetType());
				x.Serialize(stream, wnd);
			}
		}
		public static T OpenFromXml<T>(string name) where T : Window
		{
			T window = null;
			using (FileStream stream = new FileStream(name + ".xml", FileMode.Open))
			{
					XmlSerializer x = new XmlSerializer(typeof(T));
					window =  (T)x.Deserialize(stream);
			}
			return window;
		}
		internal void SetHandler(string name, string eventName)
		{
			if (m_handler.ContainsKey(eventName))
				m_handler[eventName] = name;
			else
				m_handler.Add(eventName, name);
		}
		protected abstract void CallHandler(string eventName, XEventArgs args);
	}
}


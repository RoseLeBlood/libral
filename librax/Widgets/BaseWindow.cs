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

namespace X11.Widgets
{
	public class XEventArgs: EventArgs
	{
		public XEvent Event { get ; private set; }
		public XEventArgs() { }
		public XEventArgs(XEvent xevent) { Event = xevent; }
	}
	[Serializable]
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
		protected SortedDictionary<string /*name */, string /* function */> m_handler;
		protected int			m_id;
		protected string		m_strClassName;
		protected bool			m_bIsResizeable;
		protected int 			m_iBorderWidth;
		protected IEventHandler m_pEventHandler;
		private   bool			m_bIsCreated;
		private   string		m_strNamespace;

		internal SortedDictionary<string /*name */, string /* function */> CallHandlers { get { return m_handler; } }

		[XmlAttribute("Name")]
		public override string Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}
		[XmlIgnore]
		public Display			Display
		{
			get { return m_pDisplay; }
		}
		[XmlAttribute("Background")]
		public string			strBackground
		{
			get { return m_colBackground.ToString(); }
			set { setBackground(value); }
		}
		[XmlAttribute("Border")]
		public string			strBorder
		{
			get { return m_colBorder.ToString(); }
			set { m_colBorder = new Color(value); }
		}
		[XmlIgnore]
		public Color		Background
		{
			get { return m_colBackground; }
			set { m_colBackground = value; setBackground(m_colBackground.ToString()); }
		}
		[XmlIgnore]
		public Color		Border
		{
			get { return m_colBorder; }
			set { m_colBorder = value; setBorder(m_colBorder.ToString()); }
		}
		[XmlAttribute("Rectangle")]
		public string		strRectangle
		{
			get { return m_xRectangle.ToString(); }
			set { SetRectangle(value); }
		}
		[XmlIgnore]
		public Rectangle		Rectangle
		{
			get { return m_xRectangle; }
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
			set { m_bShowCursor = value;}
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
			set { m_iBorderWidth = value; }
		}
		[XmlAttribute("Parent")]
		public string				Parent
		{
			get {  return (m_pParentWindow != null) ? m_pParentWindow.Name : null; }
			set { m_pParentWindow = Application.Current.GetHandle<BaseWindow>(value); }
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
		[XmlAttribute("UserEvents")]
		public string UserEvents
		{
			get { return m_handler.ContainsKey("UserEvents") ? m_handler["UserEvents"] : null; }
			set { SetHandler(value, "UserEvents"); }
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
		[XmlIgnore]
		public IEventHandler EventHandler
		{
			get { return m_pEventHandler; }
			set { m_pEventHandler = value; }
		}
		[XmlAttribute("Resizeable")]
		public bool Resizeable
		{
			get { return m_bIsResizeable; }
			set { m_bIsResizeable = value; }
		}
		[XmlIgnore]
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
			m_handler = new SortedDictionary<string, string>();
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
			m_handler = new SortedDictionary<string, string>();
			m_iBorderWidth = 0;
		}
		public BaseWindow (string strDisplay, string strName, Color pBackgroundColor,Rectangle Rectangle, IEventHandler pEventHandler,
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
			m_handler = new SortedDictionary<string, string>();
			m_pEventHandler = pEventHandler;
			m_iBorderWidth = 0;

		}
		protected virtual bool OnCreate(XEventArgs args)
		{

			return m_pEventHandler.CallHandler("Created", args, this);
		}
		protected virtual bool OnDestroy(XEventArgs  args)
		{
			return m_pEventHandler.CallHandler("Destroyed", args, this);
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
					
			return m_pEventHandler.CallHandler("ClientMessage", args, this);
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
			m_pEventHandler.CallHandler("Resize", args, this);
			return true;
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
							break;
						}
					}
				}
			}
			if (m_pParentWindow == null)
				return m_pEventHandler.WndProc(xevent, this);
			else
				return m_pParentWindow.Event(xevent);
		}
		void SetTitle(string value)
		{
			if (m_bIsCreated)
			{
			
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

		private void setBorder(string str)
		{
			if(m_colBorder.ToString() != str)
				m_colBorder = new Color(str);
			if (m_bIsCreated)
			{
			}
		}

		private void setBackground(string color)
		{
			if(m_colBackground.ToString() != color)
				m_colBackground = new Color(color);
			if (m_bIsCreated)
			{
				SetWindowOpacity(m_colBackground.Alpha);
			}
		}
		public static void SaveAsXml(BaseWindow wnd)
		{
			string name = wnd.Name + ".xml";
			if (File.Exists(name))
				File.Delete(name);

			using(FileStream stream = new FileStream(wnd.Name + ".xml", FileMode.CreateNew))
			{
				XmlSerializer x = new XmlSerializer(wnd.GetType());
				x.Serialize(stream, wnd);
			}
		}
		public static T OpenFromXml<T>(string name) where T : BaseWindow
		{
			T window = null;
			FileStream stream = new FileStream(name + ".xml", FileMode.Open, 
				FileAccess.Read, 
				FileShare.None);
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
	}
}


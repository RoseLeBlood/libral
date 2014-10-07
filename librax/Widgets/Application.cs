//
//  Application.cs
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
using X11._internal;
using System.Xml.Serialization;
using System.IO;

namespace X11.Widgets
{
	public class Application 
	{
		private static Application							m_current;
		private  Object										m_look = null;
	
		private SortedDictionary<string, Handle>		m_handles;
		private BaseWindow								m_NainWindow;
		private string									m_strNamespace;
		private Display									m_currentDisplay;
		private bool 									m_bOpen = true;

		protected Application()
		{
			m_look = new object();
			m_strNamespace = "";
			m_handles = new SortedDictionary<string, Handle>();
		}
		protected Application(string _namespace)
		{
			m_look = new object();
			m_strNamespace = _namespace;
			m_handles = new SortedDictionary<string, Handle>();

		}
		public static Application Current
		{
			get { return m_current; }
		}
		public Display Display
		{
			get { return m_current.m_currentDisplay; }
		}
		public BaseWindow MainWindow
		{
			get { return m_NainWindow; }
			set { m_NainWindow = value; }
		}
		public string DisplayName
		{
			get { return m_current.m_currentDisplay.Name; }
			set { m_current.m_currentDisplay = Current.GetHandle<Display>( value); }
		}
		public string Namespace
		{
			get { return m_strNamespace; }
			set { m_strNamespace = value; }
		}
		public Handle GetHandle(string key)
		{
			Handle hdl = null;
			lock (m_look)
			{
				if (m_handles.ContainsKey(key))
				{
					hdl = Application.Current.m_handles[key];
				}
			}
			return hdl;
		}
		public T GetHandle<T>(string key) where T : Handle
		{
			T vari = null;
			lock (m_look)
			{
				if (m_handles.ContainsKey(key))
				{
							vari = Application.Current.m_handles[key] as T;
				}
			}
			return vari;
		}
		public bool IsHandleContain(string key)
		{
			bool con = false;
			lock (m_look)
			{
				con = m_handles.ContainsKey(key);
			}
			return con;
		}
		public bool RegisterHandle(Handle handle, bool eindeutig = false)
		{
			bool ok = false;
			string name = handle.Name;
			int i = 0;

			lock (m_look)
			{
				if (!eindeutig)
				{
					do
					{
						name = string.Format("{0}{1}", name, i++);
					} while(m_handles.ContainsKey(name));
				}
				else if (m_handles.ContainsKey(name))
					return false;

				handle.Name = name;
				Console.WriteLine("RegisterHandle: {0}", handle.Name);

				Application.Current.m_handles.Add(handle.Name, handle);
				ok = true;
				
			}
			return ok;
		}
		public bool UnRegisterHandle(string name)
		{
			bool ok = false;
			lock (m_look)
			{
				if (m_handles.ContainsKey(name))
				{
							Application.Current.m_handles.Remove(name);
					ok = true;
				}
			}
			return ok;
		}

		public static void Init (string _namespace)
		{
			m_current = new Application(_namespace);
			m_current.m_currentDisplay = new Display(":0");
		}
		public void Exit()
		{
			m_bOpen = false;
		}
		public static void Run()
		{
			XEvent xevent = new XEvent();
			var MainWindow = Current.MainWindow;
			MainWindow.Show();


			while (Current.m_bOpen)
			{
				while (Lib.XCheckIfEvent(MainWindow.Display.RawHandle, 
					       ref xevent, CheckEvent, MainWindow.RawHandle))
				{
					Current.m_bOpen = MainWindow.Event(xevent);
				}
				MainWindow.OnIdle();
					//MainWindow.EventHandler.CallHandler("UserEvents", null, MainWindow);
			}
			MainWindow.Destroy();
		}

		static bool CheckEvent(IntPtr display, ref XEvent xevent, IntPtr userData)
		{
			return xevent.AnyEvent.window ==  userData;
		}
	}
}


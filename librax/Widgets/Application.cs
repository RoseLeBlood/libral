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
	[Serializable]
	public class Application 
	{
		private static Application							m_current;
		private  Object										m_look = null;
	
		private SortedDictionary<string, Handle>		m_handles;
		private string									m_strNainWindow;
		private string									m_strNamespace;

		protected Application()
		{
		}
		protected Application(string _namespace)
		{
			m_look = new object();
			m_strNamespace = _namespace;
			m_handles = new SortedDictionary<string, Handle>();
		}
		[XmlIgnore]
		public static Application Current
		{
			get { return m_current; }
		}
		[XmlAttribute("MainWindow")]
		public string MainWindow
		{
			get { return m_strNainWindow; }
			set { m_strNainWindow = value; }
		}
		[XmlAttribute("Namespace")]
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
		public bool RegisterHandle(Handle handle)
		{
			bool ok = false;
			lock (m_look)
			{
				Console.WriteLine("RegisterHandle: {0}", handle.Name);
				if (!m_handles.ContainsKey(handle.Name))
				{
							Application.Current.m_handles.Add(handle.Name, handle);
					ok = true;
				}
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
		}
		public static void Run()
		{
			XEvent xevent = new XEvent();
			var MainWindow = Application.Current.GetHandle<BaseWindow>(Application.Current.MainWindow);
			MainWindow.Show();
			while (Application.Current.MainWindow != null)
			{
				MainWindow.NextEvent(ref xevent);
				if (MainWindow.Event(xevent) == false)
					break;
			}
			MainWindow.Destroy();
		}
		public static void SaveAsXml(Application app, string file = "app.xml")
		{
			using (FileStream stream = new FileStream(file, FileMode.Create))
			{
				XmlSerializer x = new XmlSerializer(app.GetType());
				x.Serialize(stream, app);
			}
		}
	}
}


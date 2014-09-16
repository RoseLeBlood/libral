//
//  Handle.cs
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
using System.Xml.Serialization;

namespace X11
{
	public interface IHandle : IDisposable
	{
	}
	[Serializable]
	public class Handle : IHandle
	{
		private bool m_bIsDisposed=false;
		protected string m_strName;
		protected IntPtr m_pHandle;

		[XmlAttribute("Name")]
		public string Name { get { return m_strName; } }
		[XmlIgnore]
		public IntPtr RawHandle { get { return m_pHandle; } }

		internal Handle()
		{
		}
		public Handle(string name)
		{
			m_strName = name;
		}
		public Handle(string name, IntPtr handle)
		{
			m_strName = name;
			m_pHandle = handle;
		}
		public void Dispose()
		{
			Dispose(true);
			//GC.SupressFinalize(this);
		}
		public void Register()
		{
			X11.Widgets.Application.Current.RegisterHandle(this);
		}
		public void Register(IntPtr handle)
		{
			m_pHandle = handle;
			X11.Widgets.Application.Current.RegisterHandle(this);
		}
		protected virtual void CleanUpManagedResources()
		{

		}
		protected virtual void CleanUpUnManagedResources()
		{

		}
		protected void Dispose(bool Diposing)
		{
			if (!m_bIsDisposed)
			{
				if (Diposing)
				{
					CleanUpManagedResources();
				}
				CleanUpUnManagedResources();
			}
			m_bIsDisposed = true;
		}
	}
	[Serializable]
	public class XHandle : Handle
	{
		internal XHandle()
		{
		}
		public XHandle(string name) 
			: base(name)
		{
		}
		public XHandle(string name, IntPtr handle)
			: base(name, handle)
		{
		}
		protected override void CleanUpUnManagedResources()
		{
			X11.Widgets.Application.Current.UnRegisterHandle(Name);
			X11._internal.Lib.XFree(m_pHandle);
		}
	}
}


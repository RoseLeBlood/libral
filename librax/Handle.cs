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
using X11.Widgets;

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

		[XmlAttribute("Name")]
		public virtual string Name { get { return m_strName; } set { m_strName = value; } }

		internal Handle()
		{
		}
		public Handle(string name)
		{
			m_strName = name;
		}
		public void Dispose()
		{
			Dispose(true);
			//GC.SupressFinalize(this);
		}
		public virtual bool Register(bool eindeutig = false)
		{
			return X11.Widgets.Application.Current.RegisterHandle(this, eindeutig);
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
			X11.Widgets.Application.Current.UnRegisterHandle(Name);
		}
	}
	public class UnmanagedHandle : Handle
	{
		protected IntPtr m_pHandle;

		public IntPtr RawHandle { get { return m_pHandle; } }

		internal UnmanagedHandle()
		{

		}
		public UnmanagedHandle(string strName, IntPtr pHandle)  : base(strName)
		{
			m_pHandle = pHandle;
		}
		public virtual void Register(IntPtr handle, bool eindeutig = false)
		{
			m_pHandle = handle;
			Register(eindeutig);
		}
	}

	[Serializable]
	public class XHandle : UnmanagedHandle
	{
		internal XHandle() : base("", IntPtr.Zero)
		{
		}
		public XHandle(string name) 
			: base(name, IntPtr.Zero)
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
		public override void Register(IntPtr handle, bool e)
		{
			base.Register(handle);
			X11.Widgets.Application.Current.RegisterHandle(this, e);
		}
	}
}


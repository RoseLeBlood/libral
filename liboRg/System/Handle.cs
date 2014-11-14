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
using System.Collections.Generic;
using System.API.Platform.Linux;
using System.IO;

namespace System
{
	public interface IHandle : IDisposable
	{

	}
	[Serializable]
	public class Handle : Stream, IHandle
	{
		private MemoryStream m_pStream;
		private Object m_pLock;

		private bool m_bIsDisposed=false;
		protected string m_strName;

		[XmlAttribute("Name")]
		public virtual string Name { get { return m_strName; } set { m_strName = value; } }

		public override bool CanRead
		{
			get {  return m_pStream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return m_pStream.CanSeek; }
		}

		public override bool CanWrite
		{
			get { return m_pStream.CanWrite; }
		}

		public override long Length
		{
			get { return m_pStream.Length; }
		}

		public override long Position
		{
			get { return m_pStream.Position; }
			set { m_pStream.Position = value; }
		}

		internal Handle()
		{
			m_pStream = new MemoryStream();
			m_pLock = new object();
		}
		public Handle(string name)
		{
			m_pStream = new MemoryStream();
			m_strName = name;
		}
		public new void Dispose()
		{
			Dispose(true);
			//GC.SupressFinalize(this);
		}
		public virtual bool Register(bool eindeutig = false)
		{
			return Application.Current.RegisterHandle(this, eindeutig);
		}

		protected virtual void CleanUpManagedResources()
		{
			m_pStream.Dispose();
		}
		protected virtual void CleanUpUnManagedResources()
		{

		}
		protected override void Dispose(bool Diposing)
		{
			base.Dispose(Diposing);
			if (!m_bIsDisposed)
			{
				if (Diposing)
				{
					CleanUpManagedResources();
				}
				CleanUpUnManagedResources();
			}
			m_bIsDisposed = true;
			Application.Current.UnRegisterHandle(Name);
		}
			
		public override void Flush()
		{
			lock (m_pLock)
			{
				m_pStream.Flush();
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int read = 0;
			lock (m_pLock)
			{
				read = m_pStream.Read(buffer, offset, count);
			}
			return read;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			long seek = 0;
			lock (m_pLock)
			{
				seek = m_pStream.Seek(offset, origin);
			}
			return seek;
		}

		public override void SetLength(long value)
		{
			lock (m_pLock)
			{
				m_pStream.SetLength(value);
			}
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			lock (m_pLock)
			{
				m_pStream.Write(buffer, offset, count);
			}
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

		public static implicit operator IntPtr ( UnmanagedHandle handle)
		{
			return handle.m_pHandle;
		}
	}
	public class UnmanagedHandles : Handle, IList<IntPtr>
	{
		private List<IntPtr> m_pListHandles;

		public IntPtr[] Handles
		{
			get
			{
				return m_pListHandles.ToArray();
			}
		}

		public IntPtr[] ToArray()
		{
			return m_pListHandles.ToArray();
		}

		public int Count
		{
			get { return m_pListHandles.Count; }
		}
		public bool IsReadOnly
		{
			get { return (m_pListHandles as IList<IntPtr>).IsReadOnly; }
		}
		public IntPtr this[int index]
		{
			get { return m_pListHandles[index]; }
			set { m_pListHandles[index] = value; }
		}

		internal UnmanagedHandles() : base()
		{
			m_pListHandles = new List<IntPtr>();
		}
		public UnmanagedHandles(string strName)  
			: base(strName)
		{
			m_pListHandles = new List<IntPtr>();
		}

		public int IndexOf(IntPtr item)
		{
			return m_pListHandles.IndexOf(item);
		}
		public void Insert(int index, IntPtr item)
		{
			m_pListHandles.Insert(index, item);
		}
		public void RemoveAt(int index)
		{
			m_pListHandles.RemoveAt(index);
		}

		public void Add(IntPtr item)
		{
			m_pListHandles.Add(item);
		}
		public void AddRange(IntPtr[] pDevices)
		{
			m_pListHandles.AddRange(pDevices);
		}
		public void Clear()
		{
			m_pListHandles.Clear();
		}
		public bool Contains(IntPtr item)
		{
			return m_pListHandles.Contains(item);
		}
		public void CopyTo(IntPtr[] array, int arrayIndex)
		{
			m_pListHandles.CopyTo(array, arrayIndex);
		}
		public bool Remove(IntPtr item)
		{
			return m_pListHandles.Remove(item);
		}

		public IEnumerator<IntPtr> GetEnumerator()
		{
			return m_pListHandles.GetEnumerator();
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return (System.Collections.IEnumerator)m_pListHandles.GetEnumerator();
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
			Application.Current.UnRegisterHandle(Name);
			Lib.XFree(m_pHandle);
		}
		public override void Register(IntPtr handle, bool e)
		{
			base.Register(handle);
			Application.Current.RegisterHandle(this, e);
		}
	}
}


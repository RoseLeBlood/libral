//
//  OpenCLHandle.cs
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
using System.Collections.ObjectModel;
using System.API.Platform.Linux;

namespace System.API.OpenCL
{
	public class OpenCLHandle : UnmanagedHandle
	{
		protected uint       m_iErrorCode;

		public uint ErrorCode
		{ 
			get { return m_iErrorCode; } 
		}
		protected OpenCLHandle(string strName)
			: this(strName, IntPtr.Zero)
		{
		}
		protected OpenCLHandle(string strName, IntPtr pHandle)
			: base((strName.Contains("OpenCL_") ? strName : ("OpenCL_" + strName)), pHandle)
		{
		}

	}

	public class OpenCLHandles<T> : OpenCLHandle, IList<T>
		where T : OpenCLHandle
	{
		protected List<T> m_pHandles;

		public IntPtr[] Handles
		{
			get
			{
				IntPtr[] devices = new IntPtr[this.Count];
				for (int i = 0; i < Count; i++)
					{
						devices[i] = this[i].RawHandle;
					}
				return devices;
			}
		}

		public int Count
		{
			get { return m_pHandles.Count; }
		}
		public T this[int index]
		{
			get { return (T)m_pHandles[index]; }
			set { m_pHandles[index] = value; }
		}
		public bool IsReadOnly
		{
			get { return ((IList<T>)(m_pHandles)).IsReadOnly;  }
		}

		protected OpenCLHandles(string strName)
			: base(strName)
		{
			m_pHandles = new List<T>();
			Register(true);
		}

		public int IndexOf(T item)
		{
			return m_pHandles.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			m_pHandles.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			T item = m_pHandles[index];
			item.Dispose();
			m_pHandles.RemoveAt(index);
		}
		public void Add(T item)
		{
			m_pHandles.Add(item);
		}

		public void Clear()
		{
			foreach (var item in m_pHandles)
				{
					item.Dispose();
				}
			m_pHandles.Clear();
		}

		public bool Contains(T item)
		{
			return m_pHandles.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			m_pHandles.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			item.Dispose();
			return m_pHandles.Remove(item);
		}
		public IEnumerator<T> GetEnumerator()
		{
			return m_pHandles.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return (System.Collections.IEnumerator)GetEnumerator();
		}

		public void AddRange(IEnumerable<T> collection)
		{
			m_pHandles.AddRange(collection);
		}
		public ReadOnlyCollection<T> AsReadOnly ()
		{
			return m_pHandles.AsReadOnly();
		}

		public int BinarySearch (int index, int count, T item, IComparer<T> comparer)
		{
			return m_pHandles.BinarySearch(index, count, item, comparer);
		}

		public int BinarySearch (T item, IComparer<T> comparer)
		{
			return m_pHandles.BinarySearch(item, comparer);
		}

		public int BinarySearch (T item)
		{
			return m_pHandles.BinarySearch(item);
		}
		public T[] ToArray()
		{
			return m_pHandles.ToArray();
		}
	}
}


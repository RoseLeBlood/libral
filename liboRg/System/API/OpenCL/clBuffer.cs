﻿//
//  clBuffer.cs
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

namespace System.API.OpenCL
{
	[Flags()]
	public enum BufferFlags : uint
	{
		ReadWrite = CL.MEM_READ_WRITE,
		WriteOnly = CL.MEM_WRITE_ONLY,
		ReadOnly = CL.MEM_READ_ONLY,
		UseHostPtr = CL.MEM_USE_HOST_PTR,
		AllocHostPtr = CL.MEM_ALLOC_HOST_PTR,
		CopyHostPtr = CL.MEM_COPY_HOST_PTR,
		Reserved = (1 << 6),

		HostWriteOnly = CL.MEM_HOST_WRITE_ONLY,
		HostReadOnly = CL.MEM_HOST_READ_ONLY,
		HostNoAccess = CL.MEM_HOST_NO_ACCESS,

	}
	public enum BufferType
	{
		Float,
		Double,
		Int64,
		Int32,
		Int16,
		UInt64,
		UInt32,
		UInt16,
		Byte,
		SByte
	}
	public interface IBuffer : IHandle
	{
		int Size { get; }
		Context Context  { get; }
	}
	public class Buffer : OpenCLHandle, IBuffer
	{
		private BufferFlags m_pBufferFlags;
		private int m_iSize;
		private Context m_pContext;
		private BufferType m_eBufferType;

		public BufferFlags BufferFlags
		{
			get { return m_pBufferFlags; }
		}

		public BufferType BufferType
		{
			get { return m_eBufferType; }
		}

		public int Size
		{
			get { return m_iSize; }
		}

		public Context Context
		{
			get { return m_pContext; }
		}

		internal Buffer(string strName, Context pContext, BufferFlags flags, int iSize,
			BufferType eBufferType, Object host_ptr = null)
			: base(strName)
		{
			m_pBufferFlags = flags;
			m_iSize = iSize;
			m_pContext = pContext;
			m_eBufferType = eBufferType;
			if (host_ptr != null)
			{
				using (var xa = host_ptr.Pin())
				{
					m_pHandle = cl.clCreateBuffer(pContext.RawHandle, (uint)(flags), (IntPtr)iSize, xa, out m_iErrorCode);
				}
			}
			else
			{
				m_pHandle = cl.clCreateBuffer(pContext.RawHandle, (uint)(flags), (IntPtr)iSize, IntPtr.Zero, out m_iErrorCode);
			}
			Register(true);
		}
	}


	public class FloatBuffer : Buffer
	{
		internal FloatBuffer(string strName, Context pContext, BufferFlags flags, int iSize, Object host_ptr = null)
			: base(strName, pContext, flags, iSize * sizeof(float),BufferType.Float, host_ptr)
		{

		}
	}
	public class DoubleBuffer : Buffer
	{
		internal DoubleBuffer(string strName, Context pContext, BufferFlags flags, int iSize, Object host_ptr = null)
			: base(strName, pContext, flags, iSize * sizeof(double),BufferType.Double, host_ptr)
		{

		}
	}
	public class Int64Buffer : Buffer
	{
		internal Int64Buffer(string strName, Context pContext, BufferFlags flags, int iSize, Object host_ptr = null)
			: base(strName, pContext, flags, iSize * sizeof(Int64), BufferType.Int64, host_ptr)
		{

		}
	}
	public class Int32Buffer : Buffer
	{
		internal Int32Buffer(string strName, Context pContext, BufferFlags flags, int iSize, Object host_ptr = null)
			: base(strName, pContext, flags, iSize * sizeof(Int32), BufferType.Int32, host_ptr)
		{

		}
	}
	public class Int16Buffer : Buffer
	{
		internal Int16Buffer(string strName, Context pContext, BufferFlags flags, int iSize, Object host_ptr = null)
			: base(strName, pContext, flags, iSize * sizeof(Int16), BufferType.Int16, host_ptr)
		{

		}
	}



	public class UInt64Buffer : Buffer
	{
		internal UInt64Buffer(string strName, Context pContext, BufferFlags flags, int iSize, Object host_ptr = null)
			: base(strName, pContext, flags, iSize * sizeof(UInt64), BufferType.UInt64, host_ptr)
		{

		}
	}
	public class UInt32Buffer : Buffer
	{
		internal UInt32Buffer(string strName, Context pContext, BufferFlags flags, int iSize, Object host_ptr = null)
			: base(strName, pContext, flags, iSize * sizeof(UInt32), BufferType.UInt32, host_ptr)
		{

		}
	}
	public class UInt16Buffer : Buffer
	{
		internal UInt16Buffer(string strName, Context pContext, BufferFlags flags, int iSize, Object host_ptr = null)
			: base(strName, pContext, flags, iSize * sizeof(UInt16), BufferType.UInt16, host_ptr)
		{

		}
	}

	public class ByteBuffer : Buffer
	{
		internal ByteBuffer(string strName, Context pContext, BufferFlags flags, int iSize, Object host_ptr = null)
			: base(strName, pContext, flags, iSize * sizeof(byte), BufferType.Byte, host_ptr)
		{

		}
	}
	public class SByteBuffer : Buffer
	{
		internal SByteBuffer(string strName, Context pContext, BufferFlags flags, int iSize, Object host_ptr = null)
			: base(strName, pContext, flags, iSize * sizeof(SByte), BufferType.SByte, host_ptr)
		{

		}
	}
}


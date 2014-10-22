﻿//
//  clContext.cs
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
using X11;

namespace liboRg.OpenCL
{


	public class clContext : UnmanagedHandle
	{
		private uint m_iErrorCode;
		private long m_lNumDevices;
		private clDevices m_pDevices;

		public long NumDevices
		{
			get { return GetContextInfoAsLong(CL.CONTEXT_NUM_DEVICES); }
		}

		public CL ErrorCode
		{
			get { return (CL)m_iErrorCode; }
		}

		public clContext(clDevice pDevice)
			: base("Context_of_" + pDevice.Name, IntPtr.Zero)
		{
			m_pDevices = new clDevices();
			m_pDevices.Add(pDevice);

			IntPtr[] rawDevices = new IntPtr[m_pDevices.Count];
			for (int i = 0; i < rawDevices.Length; i++)
				rawDevices[i] = m_pDevices[i].RawHandle;

			m_pHandle = cl.clCreateContext(IntPtr.Zero, 
				(uint)m_pDevices.Count, rawDevices, IntPtr.Zero, IntPtr.Zero, out m_iErrorCode);
			Register(true);
		}
		public clContext(string strName, clDevices pDevices)
			: base("clContext_" + strName, IntPtr.Zero)
		{
			m_pDevices = pDevices;

			IntPtr[] rawDevices = new IntPtr[pDevices.Count];
			for (int i = 0; i < rawDevices.Length; i++)
				rawDevices[i] = pDevices[i].RawHandle;

			m_pHandle = cl.clCreateContext(IntPtr.Zero, 
				(uint)pDevices.Count, rawDevices, IntPtr.Zero, IntPtr.Zero, out m_iErrorCode);
	
			Register(true);
		}
		public void Retain()
		{
			cl.clRetainContext(RawHandle);
		}
		public void Release()
		{
			cl.clReleaseContext(RawHandle);
		}

		public clProgram CreateProgramFromSource(string source, string name)
		{
			return CreateProgramFromSource(new string[] { source }, name);
		}
		public clProgram CreateProgramFromSource(string[] sources, string name)
		{
			uint errorCode = 0;
			var x = cl.clCreateProgramWithSource(RawHandle, (uint)sources.Length, sources, 
				null, out errorCode);

			if (errorCode != 0)
				throw new System.Exception(errorCode.ToString());

			return new clProgram(name, x);
		}
		public CommandQueue CreateCommandQueue()
		{
			CommandQueue queue = new CommandQueue(Name.Replace("Context_of", "CommandQueue_of"));

			foreach (var item in m_pDevices)
			{
				uint errorCode = 0;
				IntPtr pHandle = cl.clCreateCommandQueue(this, item, 0, out errorCode);
				if (pHandle != IntPtr.Zero)
					queue.Add(pHandle);

			}
			return queue;
		}
		public IntPtr CreateBuffer(BufferFlags flags, int size, Object host_ptr = null)
		{
			if (host_ptr != null)
			{
				using (var xa = host_ptr.Pin())
				{
					return cl.clCreateBuffer(this, (uint)(flags), (IntPtr)size, xa, out m_iErrorCode);
				}
			}
			else
			{
				return cl.clCreateBuffer(this, (uint)(flags), (IntPtr)size, IntPtr.Zero, out m_iErrorCode);
			}
		}
		public string GetContextInfo(CL param_name)
		{
			uint sizeBuffer = 0;
			string ret = "";
			cl.clGetContextInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
		public int GetContextInfoAsInt(CL param_name)
		{
			uint sizeBuffer = 0;
			int ret = 0;
			cl.clGetContextInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
		public long GetContextInfoAsLong(CL param_name)
		{
			uint sizeBuffer = 0;
			long ret = 0;
			cl.clGetContextInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
	}
}


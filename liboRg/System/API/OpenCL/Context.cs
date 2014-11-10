//
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

namespace System.API.OpenCL
{


	public class Context : UnmanagedHandle
	{
		private uint m_iErrorCode;
		private Devices m_pDevices;

		public long NumDevices
		{
			get { return GetContextInfoAsLong(CL.CONTEXT_NUM_DEVICES); }
		}

		public Devices Devices
		{
			get { return m_pDevices; }
		}

		public CL ErrorCode
		{
			get { return (CL)m_iErrorCode; }
		}

		public Context(Device pDevice)
			: base("Context_of_" + pDevice.Name, IntPtr.Zero)
		{
			m_pDevices = new Devices();
			m_pDevices.Add(pDevice);

			IntPtr[] rawDevices = new IntPtr[m_pDevices.Count];
			for (int i = 0; i < rawDevices.Length; i++)
				rawDevices[i] = m_pDevices[i].RawHandle;

			m_pHandle = cl.clCreateContext(IntPtr.Zero, 
				(uint)m_pDevices.Count, rawDevices, IntPtr.Zero, IntPtr.Zero, out m_iErrorCode);

			cl.LoadExtensions();

			Register(true);
		}
		public Context(string strName, Devices pDevices)
			: base("clContext_" + strName, IntPtr.Zero)
		{
			m_pDevices = pDevices;

			IntPtr[] rawDevices = new IntPtr[pDevices.Count];
			for (int i = 0; i < rawDevices.Length; i++)
				rawDevices[i] = pDevices[i].RawHandle;

			m_pHandle = cl.clCreateContext(IntPtr.Zero, 
				(uint)pDevices.Count, rawDevices, IntPtr.Zero, IntPtr.Zero, out m_iErrorCode);

			cl.LoadExtensions();

			Register(true);
		}
		~Context()
		{
			Dispose();
		}
		public void Retain()
		{
			cl.clRetainContext(RawHandle);
		}
		public void Release()
		{
			cl.clReleaseContext(RawHandle);
		}

		public Program CreateProgramFromSource(string source, string name)
		{
			return CreateProgramFromSource(new string[] { source }, name);
		}
		public Program CreateProgramFromSource(string[] sources, string name)
		{
			uint errorCode = 0;
			var x = cl.clCreateProgramWithSource(RawHandle, (uint)sources.Length, sources, 
				null, out errorCode);

			if (errorCode != 0)
				throw new System.Exception(errorCode.ToString());

			return new Program(name, x, this);

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
		public System.API.OpenCL.Buffer CreateBuffer(string strName, BufferFlags flags, int size, Object host_ptr = null)
		{
			return new System.API.OpenCL.Buffer(strName, this, flags, size, host_ptr);
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


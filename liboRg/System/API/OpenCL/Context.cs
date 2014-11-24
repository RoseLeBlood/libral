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
using System.Runtime.InteropServices;
using System.API.Platform.Linux;
using liboRg.Context;
using System.API.Platform;

namespace System.API.OpenCL
{
	public delegate IntPtr XCurentContext();

	public class Context : UnmanagedHandle
	{


		private uint m_iErrorCode;
		private Devices m_pDevices;
		private XCurentContext m_contextget;

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


		public Context(Device pDevice, Platform pPlatform, IGLNativeContext pContext)
			: base("Context_of_" + pDevice.Name, IntPtr.Zero)
		{
			m_pDevices = new Devices();
			m_pDevices.Add(pDevice);

			IntPtr[] rawDevices = new IntPtr[m_pDevices.Count];
			for (int i = 0; i < rawDevices.Length; i++)
				rawDevices[i] = m_pDevices[i].RawHandle;

			m_pHandle = cl.clCreateContext(new IntPtr[] { (IntPtr)CL.CONTEXT_PLATFORM, pPlatform.RawHandle, IntPtr.Zero}, 
				(uint)m_pDevices.Count, rawDevices, IntPtr.Zero, IntPtr.Zero, out m_iErrorCode);
			cl.LoadExtensions();



			Register(true);
		}
		public Context(string strName, Devices pDevices, Platform pPlatform)
			: base("clContext_" + strName, IntPtr.Zero)
		{
			m_pDevices = pDevices;

			IntPtr[] rawDevices = new IntPtr[pDevices.Count];
			for (int i = 0; i < rawDevices.Length; i++)
				rawDevices[i] = pDevices[i].RawHandle;

			IntPtr GL_CONTEXT_KHR = Marshal.AllocHGlobal(glxNativeContext.glXGetCurrentContext());
			IntPtr GLX_DISPLAY_KHR = Marshal.AllocHGlobal(glxNativeContext.glXGetCurrentDisplay());;

			IntPtr[] props = new IntPtr[]
				{
					(IntPtr)CL.CONTEXT_PLATFORM, pPlatform.RawHandle,
					(IntPtr)CL.GL_CONTEXT_KHR, GL_CONTEXT_KHR, 
					(IntPtr)CL.GLX_DISPLAY_KHR, GLX_DISPLAY_KHR,
					IntPtr.Zero
				};


			m_pHandle = cl.clCreateContext(props, 
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
		public System.API.OpenCL.Buffer CreateBuffer(string strName, BufferFlags flags, int size, BufferType eBufferType,
			Object host_ptr = null)
		{
			switch (eBufferType)
			{
				case BufferType.Float:
					return (Buffer)new FloatBuffer(strName, this, flags, size, host_ptr);
				case BufferType.Double:
					return (Buffer)new DoubleBuffer(strName, this, flags, size, host_ptr);
				case BufferType.Int64:
					return (Buffer)new Int64Buffer(strName, this, flags, size, host_ptr);
				case BufferType.Int32:
					return (Buffer)new Int32Buffer(strName, this, flags, size, host_ptr);
				case BufferType.Int16:
					return (Buffer)new Int16Buffer(strName, this, flags, size, host_ptr);
				case BufferType.UInt64:
					return (Buffer)new UInt64Buffer(strName, this, flags, size, host_ptr);
				case BufferType.UInt32:
					return (Buffer)new UInt32Buffer(strName, this, flags, size, host_ptr);
				case BufferType.UInt16:
					return (Buffer)new UInt16Buffer(strName, this, flags, size, host_ptr);
				case BufferType.Byte:
					return (Buffer)new ByteBuffer(strName, this, flags, size, host_ptr);
				case BufferType.SByte:
					return (Buffer)new SByteBuffer(strName, this, flags, size, host_ptr);
			}
			return null;
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


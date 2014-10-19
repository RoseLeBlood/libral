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
using X11;

namespace liboRg.OpenCL
{
	public class clContext : UnmanagedHandle
	{
		private uint m_iErrorCode;
		private long m_lNumDevices;

		public long NumDevices
		{
			get { return GetContextInfoAsLong(CL.CONTEXT_NUM_DEVICES); }
		}

		public CL ErrorCode
		{
			get { return (CL)m_iErrorCode; }
		}

		public clContext(clDevice pDevice)
			: this("Context_of_" + pDevice.Name, new clDevice[] { pDevice })
		{
		}
		public clContext(string strName, clDevice[] pDevice)
			: base("clContext_" + strName, IntPtr.Zero)
		{
			IntPtr[] rawDevices = new IntPtr[pDevice.Length];
			for (int i = 0; i < rawDevices.Length; i++)
				rawDevices[i] = pDevice[i].RawHandle;

			m_pHandle = cl.clCreateContext(IntPtr.Zero, 
				(uint)pDevice.Length, rawDevices, IntPtr.Zero, IntPtr.Zero, out m_iErrorCode);
	
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

		public string GetContextInfo(CL param_name)
		{
			int sizeBuffer = 0;
			string ret = "";
			cl.clGetContextInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
		public int GetContextInfoAsInt(CL param_name)
		{
			int sizeBuffer = 0;
			int ret = 0;
			cl.clGetContextInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
		public long GetContextInfoAsLong(CL param_name)
		{
			int sizeBuffer = 0;
			long ret = 0;
			cl.clGetContextInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
	}
}


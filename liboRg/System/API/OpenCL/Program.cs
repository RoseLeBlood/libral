//
//  Program.cs
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

namespace System.API.OpenCL
{
	public class Program : UnmanagedHandle
	{
		private int[] 		m_iBuildStatus;
		private string[] 	m_strBuildLog;
		private Devices 	m_pDevices;
		private uint        m_iErrorCode;
		private Context 	m_pContext;

		public uint ErrorCode
		{
			get { return m_iErrorCode; }
		}

		public int[] BuildStatus
		{
			get { return m_iBuildStatus; }
		}

		public string[] BuildLog
		{
			get { return m_strBuildLog; }
		}

		public Devices Devices
		{
			get { return m_pDevices; }
		}			

		internal Program(string strName, IntPtr pHandle, Context pContext)
			: base("Program_" + strName, pHandle)
		{
			m_iErrorCode = 0;
			m_pContext = pContext;
			Register(true);
		}
		public int Build(int iDevice, IntPtr? userdata, string options = "")
		{
			return Build(new IntPtr[] { m_pContext.Devices[iDevice] }, userdata, options);
		}
		public int Build(Device pDevice, IntPtr? userdata, string options = "")
		{
			Devices devices = new Devices();
			devices.Add(pDevice);

			return Build(devices, userdata, options);

		}
		public int Build(Devices pDevice, IntPtr? userdata, string options = "")
		{
			m_pDevices = pDevice;
			var x = cl.clBuildProgram(RawHandle, (uint)pDevice.Count, pDevice.Handles, 
				options, null, (userdata.HasValue ? userdata.Value : IntPtr.Zero));
				
			m_iBuildStatus = new int[pDevice.Count];
			m_strBuildLog = new string[pDevice.Count];

			for (int i = 0; i < pDevice.Count; i++)
			{
				cl.clGetProgramBuildInfo(RawHandle, pDevice.Handles[i], (uint)CL.PROGRAM_BUILD_STATUS, out m_iBuildStatus[i], ref m_iErrorCode);
				cl.clGetProgramBuildInfo(RawHandle, pDevice.Handles[i], (uint)CL.PROGRAM_BUILD_LOG, out m_strBuildLog[i], ref m_iErrorCode);
			}
			 
			return (int)x;
		}
		internal int Build(IntPtr[] pDevices, IntPtr? userdata, string options = "")
		{
			var x = cl.clBuildProgram(RawHandle, (uint)pDevices.Length, pDevices, 
				options, null, (userdata.HasValue ? userdata.Value : IntPtr.Zero));

			m_iBuildStatus = new int[pDevices.Length];
			m_strBuildLog = new string[pDevices.Length];

			for (int i = 0; i < pDevices.Length; i++)
				{
					cl.clGetProgramBuildInfo(RawHandle, pDevices[i], (uint)CL.PROGRAM_BUILD_STATUS, out m_iBuildStatus[i], ref m_iErrorCode);
					cl.clGetProgramBuildInfo(RawHandle, pDevices[i], (uint)CL.PROGRAM_BUILD_LOG, out m_strBuildLog[i], ref m_iErrorCode);
				}

			return (int)x;

		}
		public Kernel CreateKernel(string strKernelName)
		{
			return new Kernel(strKernelName, this);
		}
	}
}


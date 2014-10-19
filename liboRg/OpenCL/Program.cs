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
using X11;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace liboRg.OpenCL
{
	public class clProgram : UnmanagedHandle
	{
		private int[] m_iBuildStatus;
		private string[] m_strBuildLog;
		private clDevices m_pDevices;

		public int[] BuildStatus
		{
			get { return m_iBuildStatus; }
		}

		public string[] BuildLog
		{
			get { return m_strBuildLog; }
		}

		public IList<clDevice> Devices
		{
			get { return m_pDevices.AsReadOnly(); }
		}

		internal clProgram(string strName, IntPtr pHandle)
			: base("clProgram_" + strName, pHandle)
		{
			Register(true);
		}

		public int Build(clDevice pDevice, IntPtr? userdata, string options = "")
		{
			clDevices devices = new clDevices();
			devices.Add(pDevice);

			return Build(devices, userdata, options);

		}
		public int Build(clDevices pDevice, IntPtr? userdata, string options = "")
		{
			m_pDevices = pDevice;
			var x = cl.clBuildProgram(RawHandle, (uint)pDevice.Count, pDevice.Handles, 
				options, null, (userdata.HasValue ? userdata.Value : IntPtr.Zero));

			int errorCode = 0;

			m_iBuildStatus = new int[pDevice.Count];
			m_strBuildLog = new string[pDevice.Count];

			for (int i = 0; i < pDevice.Count; i++)
			{
				cl.clGetProgramBuildInfo(RawHandle, pDevice.Handles[i], (uint)CL.PROGRAM_BUILD_STATUS, out m_iBuildStatus[i], ref errorCode);
				cl.clGetProgramBuildInfo(RawHandle, pDevice.Handles[i], (uint)CL.PROGRAM_BUILD_LOG, out m_strBuildLog[i], ref errorCode);
			}
			 
			return (int)x;
		}
	}
}


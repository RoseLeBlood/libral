//
//  Environment.cs
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
using System.API.OpenCL;

namespace System.Framework.OpenCL
{
	public static class OpenCLEnvironment
	{
		private static Device	m_pDevice;
		private static Context 	m_pContext;

		public static Context Context
		{
			get { return m_pContext; }
		}

		public static void SetupSingleDevice(string strVendorFilter, OpenCLDeviceTyp deviceType)
		{
			Platforms platforms = new Platforms();
			foreach (var platform in platforms)
			{
				if (platform.HaveDevices && platform.Vendor.Contains(strVendorFilter))
				{
					foreach (var item in platform.Devices)
					{
						if ( item.DeviceType == deviceType)
						{
							m_pDevice = item;
							break;
						}

					}
					if (m_pDevice != null)
						break;
				}
			}
			if (m_pDevice == null)
				throw new System.Exception("No OpenCL Device found ");

			m_pContext = m_pDevice.CreateContext();
		}
		public static Context SetupSingleDeviceAndGetSystemContext(string strVendorFilter, OpenCLDeviceTyp deviceType)
		{
			SetupSingleDevice(strVendorFilter, deviceType);
			return m_pContext;
		}

		public static CommandQueue CreateCommandQueue()
		{
			return m_pContext.CreateCommandQueue();
		}
		public static System.API.OpenCL.Program CreateProgramFromSource(string source, string name)
		{
			return CreateProgramFromSource(new string[] { source }, name);
		}
		public static System.API.OpenCL.Program CreateProgramFromSource(string[] sources, string name)
		{
			return m_pContext.CreateProgramFromSource(sources, name);
		}

		public static System.API.OpenCL.Buffer CreateBuffer(string strName, BufferFlags bufferFlags, int size, Object host_ptr = null)
		{
			return m_pContext.CreateBuffer(strName, bufferFlags, size, host_ptr);
		}
	}
}


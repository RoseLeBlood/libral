//
//  Device.cs
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

namespace liboRg.OpenCL
{
	public enum OpenCLDeviceTyp : uint
	{
		Default = CL.DEVICE_TYPE_DEFAULT,
		Cpu = CL.DEVICE_TYPE_CPU,
		Gpu = CL.DEVICE_TYPE_GPU,
		Accelerator = CL.DEVICE_TYPE_ACCELERATOR,
		Custom = CL.DEVICE_TYPE_CUSTOM,
		All = CL.DEVICE_TYPE_ALL,
	}

	public class clDevice : UnmanagedHandle
	{
		private string m_strDeviceName;
		private string m_strDeviceVendor;
		private string m_strDeviceVersion;
		private string m_strDriverVersion;
		private int m_uiMaxComputeUnits;
		private int m_uiMaxClockFrequency;
		private long m_ulGlobalMemSize;

		public string DeviceName
		{
			get { return m_strDeviceName; }
		}
		public string DeviceVendor
		{
			get { return m_strDeviceVendor; }
		}
		public string DeviceVersion
		{
			get { return m_strDeviceVersion; }
		}
		public string DriverVersion
		{
			get { return m_strDriverVersion; }
		}
		public int MaxComputeUnits
		{
			get { return m_uiMaxComputeUnits; }
		}
		public int MaxClockFrequency
		{
			get { return m_uiMaxClockFrequency; }
		}
		public long GlobalMemSize
		{
			get { return m_ulGlobalMemSize; }
		}

		internal clDevice(IntPtr pHandle)
			: base("", pHandle)
		{
			int sizeBuffer = 0;
			cl.clGetDeviceInfo(m_pHandle, (uint)CL.DEVICE_NAME, 10240, out m_strDeviceName, ref sizeBuffer);
			cl.clGetDeviceInfo(m_pHandle, (uint)CL.DEVICE_VENDOR, 10240,out m_strDeviceVendor, ref sizeBuffer);
			cl.clGetDeviceInfo(m_pHandle, (uint)CL.DEVICE_VERSION, 10240, out m_strDeviceVersion, ref sizeBuffer);
			cl.clGetDeviceInfo(m_pHandle, (uint)CL.DRIVER_VERSION, 10240, out m_strDriverVersion, ref sizeBuffer);

			cl.clGetDeviceInfo(m_pHandle, (uint)CL.DEVICE_MAX_COMPUTE_UNITS, 10240, out m_uiMaxComputeUnits, ref sizeBuffer);
			cl.clGetDeviceInfo(m_pHandle, (uint)CL.DEVICE_MAX_CLOCK_FREQUENCY, 10240, out m_uiMaxClockFrequency, ref sizeBuffer);
			cl.clGetDeviceInfo(m_pHandle, (uint)CL.DEVICE_GLOBAL_MEM_SIZE, 10240, out m_ulGlobalMemSize, ref sizeBuffer);

			Name = string.Format("clDevice_{0}_{1}", m_strDeviceVendor, m_strDeviceName);
			Register();
		}
		public override string ToString()
		{
			return string.Format("OpenCL Device:\n\tDeviceName={0}\n\tDeviceVendor={1}\n\tDeviceVersion={2}\n\tDriverVersion={3}\n\tMaxComputeUnits={4}\n\t" +
				"MaxClockFrequency={5}\n\tGlobalMemSize={6}]", DeviceName, DeviceVendor, DeviceVersion, DriverVersion, MaxComputeUnits, MaxClockFrequency, GlobalMemSize);
		}
	}
	public class clDevices : List<clDevice>
	{
		public clDevices(clPlatform pPlatform, OpenCLDeviceTyp type)
		{
			IntPtr[] devices = new IntPtr[100];
			uint numDevices;

			cl.clGetDeviceIDs(pPlatform.RawHandle, (uint)type, (uint)100, devices, out numDevices);
			for (int i = 0; i < numDevices; i++)
			{
				this.Add(new clDevice(devices[i]));
			}
		}
	}
}


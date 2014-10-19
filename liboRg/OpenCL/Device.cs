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
		private OpenCLDeviceTyp m_enumType;
		private clPlatform m_pPlatform;

		public string DeviceName
		{
			get { return GetDeviceInfo(CL.DEVICE_NAME); }
		}

		public clPlatform Platform
		{
			get { return m_pPlatform; }
		}

		public OpenCLDeviceTyp DeviceType
		{
			get { return m_enumType; }
			internal set { m_enumType = value; }
		}
		public string DeviceVendor
		{
			get { return GetDeviceInfo(CL.DEVICE_VENDOR); }
		}
		public string DeviceVersion
		{
			get { return GetDeviceInfo(CL.DEVICE_VERSION); }
		}
		public string DriverVersion
		{
			get { return GetDeviceInfo(CL.DRIVER_VERSION); }
		}
		public int MaxComputeUnits
		{
			get { return GetDeviceInfoAsInt(CL.DEVICE_MAX_COMPUTE_UNITS); }
		}
		public int MaxClockFrequency
		{
			get { return GetDeviceInfoAsInt(CL.DEVICE_MAX_CLOCK_FREQUENCY); }
		}
		public long GlobalMemSize
		{
			get { return GetDeviceInfoAsLong(CL.DEVICE_GLOBAL_MEM_SIZE); }
		}

		internal clDevice(IntPtr pHandle, clPlatform pPlatform)
			: base("", pHandle)
		{
			m_pPlatform = pPlatform;
			Name = string.Format("clDevice_{0}_{1}", DeviceVendor, DeviceName).Replace(" ", "_");
			Register();
		}
		public clContext CreateContext()
		{
			return new clContext(this);
		}
		public override string ToString()
		{
			return string.Format("{7} Device:\n\tDeviceName={0}\n\tDeviceVendor={1}\n\tDeviceVersion={2}\n\tDriverVersion={3}\n\tMaxComputeUnits={4}\n\t" +
				"MaxClockFrequency={5}\n\tGlobalMemSize={6}", DeviceName, DeviceVendor, DeviceVersion, DriverVersion, MaxComputeUnits, 
				MaxClockFrequency, GlobalMemSize, m_enumType);
		}
		public string GetDeviceInfo(CL param_name)
		{
			int sizeBuffer = 0;
			string ret = "";
			cl.clGetDeviceInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
		public int GetDeviceInfoAsInt(CL param_name)
		{
			int sizeBuffer = 0;
			int ret = 0;
			cl.clGetDeviceInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
		public long GetDeviceInfoAsLong(CL param_name)
		{
			int sizeBuffer = 0;
			long ret = 0;
			cl.clGetDeviceInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
	}
	public class clDevices : List<clDevice>
	{
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

		public clDevices()
		{
		}

		public clDevices(clPlatform pPlatform, OpenCLDeviceTyp type)
		{
			IntPtr[] devices = new IntPtr[100];
			uint numDevices;

			cl.clGetDeviceIDs(pPlatform.RawHandle, (uint)type, (uint)100, devices, out numDevices);
			for (int i = 0; i < numDevices; i++)
			{
				var x = new clDevice(devices[i], pPlatform);
				x.DeviceType = type;
				this.Add(x);
			}
		}
		public clContext CreateContext(string strName)
		{
			return new clContext(strName ,this.ToArray());
		}
	}
}


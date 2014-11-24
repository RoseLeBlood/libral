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
using System.Collections.Generic;
using System.API.Platform;

namespace System.API.OpenCL
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

	public class Device : OpenCLHandle
	{
		private OpenCLDeviceTyp m_enumType;
		private Platform m_pPlatform;
		private OpenCLDeviceExtension m_pExtensions;

		//public cl.GetGLContextInfoKHR clGetGLContextInfoKHR;

		public string DeviceName
		{
			get { return GetDeviceInfo(CL.DEVICE_NAME); }
		}
		public string OpenCLVersion
		{
			get { return GetDeviceInfo(CL.DEVICE_OPENCL_C_VERSION); }
		}
		public Platform Platform
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
		public OpenCLDeviceExtension Extensions
		{
			get { return m_pExtensions; }
		}
		internal Device(IntPtr pHandle, Platform pPlatform)
			: base("", pHandle)
		{
			m_pPlatform = pPlatform;
			Name += string.Format("Device_{0}_{1}", DeviceVendor, DeviceName).Replace(" ", "_");
			m_pExtensions = new OpenCLDeviceExtension(GetDeviceInfo(CL.DEVICE_EXTENSIONS));
			Register();
		}
		public Context CreateContext(IGLNativeContext pContext)
		{
			return new Context(this, m_pPlatform, pContext);
		}
		public override string ToString()
		{
			return string.Format("{7} Device:\n\tDeviceName={0}\n\tDeviceVendor={1}\n\tDeviceVersion={2}\n\tDriverVersion={3}\n\tMaxComputeUnits={4}\n\t" +
				"MaxClockFrequency={5}\n\tGlobalMemSize={6}\n\tExtensions={8}\n\tOpenCLVersion={9}", DeviceName, DeviceVendor, DeviceVersion, DriverVersion, MaxComputeUnits, 
				MaxClockFrequency, GlobalMemSize, m_enumType, Extensions, OpenCLVersion);
		}
		public string GetDeviceInfo(CL param_name)
		{
			uint sizeBuffer = 0;
			string ret = "";
			cl.clGetDeviceInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
		public int GetDeviceInfoAsInt(CL param_name)
		{
			uint sizeBuffer = 0;
			int ret = 0;
			cl.clGetDeviceInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}
		public long GetDeviceInfoAsLong(CL param_name)
		{
			uint sizeBuffer = 0;
			long ret = 0;
			cl.clGetDeviceInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}

	}
	public class Devices : OpenCLHandles<Device>
	{
		public Devices()
			: base("Devices")
		{

		}
		public Devices(Platform pPlatform, OpenCLDeviceTyp type)
			: base("Devices")
		{
			IntPtr[] devices = new IntPtr[100];
			uint numDevices;

			cl.clGetDeviceIDs(pPlatform.RawHandle, (uint)type, (uint)100, devices, out numDevices);
			for (int i = 0; i < numDevices; i++)
				{
					var x = new Device(devices[i], pPlatform);
					x.DeviceType = type;
					this.Add(x);
				}
		}
		public Context CreateContext(string strName, Platform pPlatform)
		{
			return new Context(strName,this, pPlatform);
		}
	}
}


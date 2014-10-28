//
//  Platform.cs
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

namespace System.API.OpenCL
{
	public class Platform : OpenCLHandle
	{
		private SortedDictionary<OpenCLDeviceTyp, Devices> m_ddDevices;
		private OpenCLPlatformExtension	m_pExtensions;

		public bool HaveDevices
		{
			get { return Devices.Count != 0; }
		}
		public Devices DevicesCPU
		{
			get { return m_ddDevices[OpenCLDeviceTyp.Cpu]; }
		}
		public Devices DevicesGPU
		{
			get { return m_ddDevices[OpenCLDeviceTyp.Gpu]; }
		}
		public Devices DevicesAccelerator
		{
			get { return m_ddDevices[OpenCLDeviceTyp.Accelerator]; }
		}
		public Devices DevicesCustom
		{
			get { return m_ddDevices[OpenCLDeviceTyp.Accelerator]; }
		}
		public Devices Devices
		{
			get
			{
				Devices ret = new Devices();
				ret.AddRange(DevicesCPU.ToArray());
				ret.AddRange(DevicesGPU.ToArray());
				ret.AddRange(DevicesAccelerator.ToArray());
				ret.AddRange(DevicesCustom.ToArray());
				return ret;
			}
		}
		public string Profil
		{
			get { return GetPlatformInfo(CL.PLATFORM_PROFILE); }
		}
		public string Version
		{
			get { return GetPlatformInfo(CL.PLATFORM_VERSION); }
		}
		public string CLName
		{
			get { return GetPlatformInfo(CL.PLATFORM_NAME); }
		}
		public string Vendor
		{
			get { return GetPlatformInfo(CL.PLATFORM_VENDOR); }
		}
		public OpenCLPlatformExtension Extension
		{
			get { return m_pExtensions; }
		}
		internal Platform(IntPtr pPlatform)
			: base("", pPlatform)
		{
			m_ddDevices = new SortedDictionary<OpenCLDeviceTyp, Devices>();
			Name += string.Format("Platform_{0}_{1}", Vendor, CLName).Replace(" ", "_");


			m_ddDevices.Add(OpenCLDeviceTyp.Gpu, new Devices(this, OpenCLDeviceTyp.Gpu));
			m_ddDevices.Add(OpenCLDeviceTyp.Cpu, new Devices(this, OpenCLDeviceTyp.Cpu));
			m_ddDevices.Add(OpenCLDeviceTyp.Accelerator, new Devices(this, OpenCLDeviceTyp.Accelerator));
			m_ddDevices.Add(OpenCLDeviceTyp.Custom, new Devices(this, OpenCLDeviceTyp.Custom));


			m_pExtensions = new OpenCLPlatformExtension(GetPlatformInfo(CL.PLATFORM_EXTENSIONS));
			Register(true);
		}
		public override string ToString()
		{
			return string.Format("OpenCL Platform\n\tProfil:{0}\n\tVersion:{1}\n\tName:{2}\n\tVendor:{3}" +
				"\n\tExtension:{4}\n\tHaveDevices:{5}", Profil,Version, CLName, Vendor, Extension, HaveDevices);
		}
		public string GetPlatformInfo(CL param_name)
		{
			string ret = ""; uint sizeBuffer = 0;
			cl.clGetPlatformInfo(m_pHandle, (uint)param_name, out ret, ref sizeBuffer);
			return ret;
		}

	}
	public class Platforms : OpenCLHandles<Platform>
	{
		public Platforms()
			: base("Palatforms")
		{
			IntPtr[] platforms = new IntPtr[100];
			uint  platforms_n = 0;

			cl.clGetPlatformIDs(100, platforms, out platforms_n);

			for (int i = 0; i < platforms_n; i++)
			{
					this.Add(new Platform(platforms[i]));
			}
		}
	}
}


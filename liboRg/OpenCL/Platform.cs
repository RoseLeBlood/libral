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
using X11;

namespace liboRg.OpenCL
{
	public class clPlatform : UnmanagedHandle
	{
		private SortedDictionary<OpenCLDeviceTyp, clDevices> m_ddDevices;

		public bool HaveDevices
		{
			get { return Devices.Count != 0; }
		}
		public clDevices DevicesCPU
		{
			get { return m_ddDevices[OpenCLDeviceTyp.Cpu]; }
		}
		public clDevices DevicesGPU
		{
			get { return m_ddDevices[OpenCLDeviceTyp.Gpu]; }
		}
		public clDevices DevicesAccelerator
		{
			get { return m_ddDevices[OpenCLDeviceTyp.Accelerator]; }
		}
		public clDevices DevicesCustom
		{
			get { return m_ddDevices[OpenCLDeviceTyp.Accelerator]; }
		}
		public clDevices Devices
		{
			get
			{
				clDevices ret = new clDevices();
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
		public string Extension
		{
			get { return GetPlatformInfo(CL.PLATFORM_EXTENSIONS); }
		}
		internal clPlatform(IntPtr pPlatform)
			: base("", pPlatform)
		{
			m_ddDevices = new SortedDictionary<OpenCLDeviceTyp, clDevices>();
			Name = string.Format("clPlatform_{0}_{1}", Vendor, CLName).Replace(" ", "_");


			m_ddDevices.Add(OpenCLDeviceTyp.Gpu, new clDevices(this, OpenCLDeviceTyp.Gpu));
			m_ddDevices.Add(OpenCLDeviceTyp.Cpu, new clDevices(this, OpenCLDeviceTyp.Cpu));
			m_ddDevices.Add(OpenCLDeviceTyp.Accelerator, new clDevices(this, OpenCLDeviceTyp.Accelerator));
			m_ddDevices.Add(OpenCLDeviceTyp.Custom, new clDevices(this, OpenCLDeviceTyp.Custom));

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
	public class clPlatforms : List<clPlatform>
	{
		public clPlatforms()
		{
			IntPtr[] platforms = new IntPtr[100];
			uint  platforms_n = 0;

			cl.clGetPlatformIDs(100, platforms, out platforms_n);

			for (int i = 0; i < platforms_n; i++)
			{
					this.Add(new clPlatform(platforms[i]));
			}
		}
	}
}


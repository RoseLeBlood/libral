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
		private string m_strProfil;
		private string m_strVersion;
		private string m_strclName;
		private string m_strVendor;
		private string m_strExtension;

		public string Profil
		{
			get { return m_strProfil; }
		}
		public string Version
		{
			get { return m_strVersion; }
		}
		public string CLName
		{
			get { return m_strclName; }
		}
		public string Vendor
		{
			get { return m_strVendor; }
		}
		public string Extension
		{
			get { return m_strExtension; }
		}
		internal clPlatform(IntPtr pPlatform)
			: base("", pPlatform)
		{
			int sizeBuffer = 0;
			cl.clGetPlatformInfo(m_pHandle, (uint)CL.PLATFORM_PROFILE, 10240, out m_strProfil, ref sizeBuffer);
			cl.clGetPlatformInfo(m_pHandle, (uint)CL.PLATFORM_VERSION, 10240, out m_strVersion, ref sizeBuffer);
			cl.clGetPlatformInfo(m_pHandle, (uint)CL.PLATFORM_NAME, 10240, out m_strclName, ref sizeBuffer);
			cl.clGetPlatformInfo(m_pHandle, (uint)CL.PLATFORM_VENDOR, 10240, out m_strVendor, ref sizeBuffer);
			cl.clGetPlatformInfo(m_pHandle, (uint)CL.PLATFORM_EXTENSIONS, 10240, out m_strExtension, ref sizeBuffer);

			Name = string.Format("clPlatform_{0}_{1}", m_strVendor, m_strclName);
			Register(true);
		}
		public override string ToString()
		{
			return string.Format("OpenCL Platform\n\tProfil:{0}\n\tVersion:{1}\n\tName:{2}\n\tVendor:{3}" +
				"\n\tExtension:{4}", Profil,Version, CLName, Vendor, Extension);
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


//
//  OpenCLExtension.cs
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
using System.Reflection;
using System.Text;

namespace System.API.OpenCL
{
	public class OpenCLPlatformExtension
	{
		public bool Iskhr_icd = false;
		public bool Isamd_event_callback = false;
		public bool Isamd_offline_devices = false;
		public bool Iskhr_global_int32_base_atomics = false;
		public bool Iskhr_global_int32_extended_atomics = false;
		public bool Iskhr_local_int32_base_atomics = false;
		public bool Iskhr_local_int32_extended_atomics = false;
		public bool Iskhr_byte_addressable_store = false;
		public bool Iskhr_spir = false;
		public bool Isintel_exec_by_local_thread = false;
		public bool Iskhr_depth_images = false;
		public bool Iskhr_3d_image_writes = false;
		public bool Iskhr_fp64 = false;

		internal OpenCLPlatformExtension(string strExtension)
		{
			string[] extensions = strExtension.Split(new char[] { ' ' });

			foreach (string extension in extensions)
			{
				string extensionName = extension;
				if (extensionName.StartsWith("public bool Is"))
					extensionName = extensionName.Substring(3);
				var info = this.GetType().GetField("Is" + extensionName);
				if (info != null)
				{
					info.SetValue(this, true);
				}
			}
		}
		public override string ToString()
		{
			StringBuilder strBuilder = new StringBuilder();

			FieldInfo[] info = this.GetType().GetFields();
			foreach (var item in info)
			{
				if ((bool)item.GetValue(this))
						strBuilder.Append(string.Format(" {0}", item.Name.Substring(2)));
			}
			return strBuilder.ToString();
		}
	}

	public class OpenCLDeviceExtension 
	{
		public bool IsAPPLE_SetMemObjectDestructor= false;
		public bool IsAPPLE_ContextLoggingFunctions = false;
		public bool IsAPPLE_clut= false;
		public bool IsAPPLE_query_kernel_names= false;
		public bool IsAPPLE_gl_sharing= false;
		public bool IsAPPLE_fp64_basic_ops= false;
		public bool IsAPPLE_ycbcr_422 = false;
		public bool IsAPPLE_rgb_422= false;

		public bool Iskhr_fp64= false;
		public bool Isamd_fp64= false;
		public bool Isamd_device_attribute_query= false;
		public bool Isamd_vec3= false;
		public bool Isamd_printf= false;
		public bool Isamd_media_ops= false;
		public bool Isamd_popcnt= false;
		public bool Isamd_media_ops2 = false;

		public bool Isnv_d3d9_sharing = false;
		public bool Isnv_d3d10_sharing = false;
		public bool Isnv_d3d11_sharing = false;
		public bool Isnv_compiler_options= false; 
		public bool Isnv_device_attribute_query = false;
		public bool Isnv_pragma_unroll = false;

		public bool Isext_device_fission= false;
		public bool Isext_atomic_counters_32 = false;

		public bool Isintel_exec_by_local_thread= false;

		public bool Iskhr_fp16= false;
		public bool Iskhr_gl_event= false;
		public bool Iskhr_d3d10_sharing= false;
		public bool Iskhr_dx9_media_sharing= false;
		public bool Iskhr_d3d11_sharing= false;
		public bool Iskhr_select_fprounding_mode= false;
		public bool Iskhr_global_int32_base_atomics= false;
		public bool Iskhr_global_int32_extended_atomics= false;
		public bool Iskhr_local_int32_base_atomics= false;
		public bool Iskhr_local_int32_extended_atomics= false;
		public bool Iskhr_int64_base_atomics= false;
		public bool Iskhr_int64_extended_atomics= false;
		public bool Iskhr_byte_addressable_store= false;
		public bool Iskhr_gl_sharing= false;
		public bool Iskhr_icd= false;
		public bool Iskhr_spir= false;
		public bool Iskhr_depth_images= false;
		public bool Iskhr_3d_image_writes= false;
		public bool Iskhr_image2d_from_buffer = false;
		public bool Iskhr_subgroups = false;
		public bool Iskhr_gl_depth_images = false;
		public bool Iskhr_gl_msaa_sharing = false;
	

		public bool Isamd_svm = false;

		internal OpenCLDeviceExtension(string strExtension)
		{
			string[] extensions = strExtension.Split(new char[] { ' ' });

			foreach (string extension in extensions)
			{
				string extensionName = extension;
				if (extensionName.StartsWith("cl_"))
					extensionName = extensionName.Substring(3);
				string name = "Is" + extensionName;

				var info = this.GetType().GetField(name);
				if (info != null)
				{
					info.SetValue(this, true);
				}
				
			}
		}
		public override string ToString()
		{
			StringBuilder strBuilder = new StringBuilder();

			FieldInfo[] info = this.GetType().GetFields();
			foreach (var item in info)
				{
					if ( ((bool)(item.GetValue(this))) == true)
						strBuilder.Append(string.Format(" {0}", item.Name.Substring(2)));
				}
			return strBuilder.ToString();
		}
	}
}


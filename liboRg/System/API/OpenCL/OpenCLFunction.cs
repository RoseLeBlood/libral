//
//  OpenCLFunction.cs
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
using System.Runtime.InteropServices;

namespace System.API.OpenCL
{
	public partial class cl
	{
		public static int clGetPlatformInfo(IntPtr platform, uint param_name, out string param_value, ref uint param_value_size_ret) 
		{
			param_value = null;
			var ptr = Marshal.AllocHGlobal(10240);
			try 
			{
				var ret = clGetPlatformInfo(platform, param_name, 10240, ptr, ref param_value_size_ret);
				param_value = Marshal.PtrToStringAnsi(ptr);
				return ret;
			} 
			finally 
			{ 
				Marshal.FreeHGlobal(ptr);
			}
		}
		public static int clGetDeviceInfo(IntPtr device, uint param_name, out string param_value, ref uint param_value_size_ret)
		{
			param_value = null;
			var ptr = Marshal.AllocHGlobal(10240);
			try 
			{
				var ret = clGetDeviceInfo(device, param_name, 10240, ptr, ref param_value_size_ret);
				param_value = Marshal.PtrToStringAnsi(ptr);
				return ret;
			} 
			finally 
			{ 
				Marshal.FreeHGlobal(ptr);
			}

		}
		public static int clGetDeviceInfo(IntPtr device, uint param_name, out int param_value, ref uint param_value_size_ret)
		{
			param_value = 0;
			var ptr = Marshal.AllocHGlobal(sizeof(int));
			try 
			{
				var ret = clGetDeviceInfo(device, param_name, sizeof(int), ptr, ref param_value_size_ret);
				param_value = Marshal.ReadInt32(ptr);
				return ret;
			} 
			finally 
			{ 
				Marshal.FreeHGlobal(ptr);
			}
		}
		public static int clGetDeviceInfo(IntPtr device, uint param_name, out long param_value, ref uint param_value_size_ret)
		{
			param_value = 0;
			var ptr = Marshal.AllocHGlobal(sizeof(long));
			try 
			{
				var ret = clGetDeviceInfo(device, param_name, sizeof(long), ptr, ref param_value_size_ret);
				param_value = Marshal.ReadInt64(ptr);
				return ret;
			} 
			finally 
			{ 
				Marshal.FreeHGlobal(ptr);
			}
		}

		public static int clGetContextInfo(IntPtr platform, uint param_name,  out long param_value, ref uint param_value_size_ret)
		{
			param_value = 0;
			var ptr = Marshal.AllocHGlobal(sizeof(long));
			try 
			{
				var ret = clGetContextInfo(platform, param_name, sizeof(long), ptr, ref param_value_size_ret);
				param_value = Marshal.ReadInt64(ptr);
				return ret;
			} 
			finally 
			{ 
				Marshal.FreeHGlobal(ptr);
			}
		}
		public static int clGetContextInfo(IntPtr platform, uint param_name, out int param_value, ref uint param_value_size_ret)
		{
			param_value = 0;
			var ptr = Marshal.AllocHGlobal(sizeof(int));
			try 
			{
				var ret = clGetContextInfo(platform, param_name, sizeof(int), ptr, ref param_value_size_ret);
				param_value = Marshal.ReadInt32(ptr);
				return ret;
			} 
			finally 
			{ 
				Marshal.FreeHGlobal(ptr);
			}

		}
		public static int clGetContextInfo(IntPtr platform, uint param_name, out string param_value, ref uint param_value_size_ret)
		{
			param_value = null;
			var ptr = Marshal.AllocHGlobal(10240);
			try 
			{
				var ret = clGetContextInfo(platform, param_name, 10240, ptr, ref param_value_size_ret);
				param_value = Marshal.PtrToStringAnsi(ptr);
				return ret;
			} 
			finally 
			{ 
				Marshal.FreeHGlobal(ptr);
			}

		}
		public static int clGetProgramBuildInfo(IntPtr platform, IntPtr device, uint param_name, out string param_value, 
			ref uint param_value_size_ret)
		{
			param_value = null;
			var ptr = Marshal.AllocHGlobal(10240);
			try 
			{
				var ret = clGetProgramBuildInfo(platform, device, param_name, 10240, ptr, ref param_value_size_ret);
				param_value = Marshal.PtrToStringAnsi(ptr);
				return ret;
			} 
			finally 
			{ 
				Marshal.FreeHGlobal(ptr);
			}
		}
		public static int clGetProgramBuildInfo(IntPtr platform, IntPtr device, uint param_name,  out long param_value, ref uint param_value_size_ret)
		{
			param_value = 0;
			var ptr = Marshal.AllocHGlobal(sizeof(long));
			try 
			{
				var ret = clGetProgramBuildInfo(platform,device, param_name, sizeof(long), ptr, ref param_value_size_ret);
				param_value = Marshal.ReadInt64(ptr);
				return ret;
			} 
			finally 
			{ 
				Marshal.FreeHGlobal(ptr);
			}
		}
		public static int clGetProgramBuildInfo(IntPtr platform, IntPtr device, uint param_name, out int param_value, ref uint param_value_size_ret)
		{
			param_value = 0;
			var ptr = Marshal.AllocHGlobal(sizeof(int));
			try 
			{
				var ret = clGetProgramBuildInfo(platform,device, param_name, sizeof(int), ptr, ref param_value_size_ret);
				param_value = Marshal.ReadInt32(ptr);
				return ret;
			} 
			finally 
			{ 
				Marshal.FreeHGlobal(ptr);
			}

		}
	}
}


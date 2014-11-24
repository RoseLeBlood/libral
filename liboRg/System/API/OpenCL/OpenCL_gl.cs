//
//  OpenCL_gl.cs
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
using System.API.Platform.Linux;
using System.API.Platform;

namespace System.API.OpenCL
{
	public enum clgl
	{
		OBJECT_BUFFER                     = 0x2000,
		OBJECT_TEXTURE2D                  = 0x2001,
		OBJECT_TEXTURE3D                  = 0x2002,
		OBJECT_RENDERBUFFER               = 0x2003,
		OBJECT_TEXTURE2D_ARRAY            = 0x200E,
		OBJECT_TEXTURE1D                  = 0x200F,
		OBJECT_TEXTURE1D_ARRAY            = 0x2010,
		OBJECT_TEXTURE_BUFFER             = 0x2011,
		       
		TEXTURE_TARGET                    = 0x2004,
		MIPMAP_LEVEL                      = 0x2005,
		NUM_SAMPLES                       = 0x2012,
	}

	public partial class cl
	{
		[DllImport(DllName)]
		public extern static IntPtr clCreateFromGLBuffer(IntPtr context, uint flags, uint bufobj, out uint error_code);
		[DllImport(DllName)]
		public extern static IntPtr clCreateFromGLTexture(IntPtr context, uint flags, uint target, int  miplevel, uint texture, out uint errcode_ret) ;
		[DllImport(DllName)]
		public extern static IntPtr clCreateFromGLRenderbuffer(IntPtr context, uint flags, uint renderbuffer, out uint  errcode_ret);
		[DllImport(DllName)]
		public extern static int clGetGLObjectInfo(IntPtr memobj, uint  gl_object_type, IntPtr gl_object_name) ;
		[DllImport(DllName)]
		public extern static int clGetGLTextureInfo(IntPtr memobj, uint param_name, uint param_value_size, IntPtr param_value, out uint param_value_size_ret) ;
		[DllImport(DllName)]
		public extern static int clEnqueueAcquireGLObjects(IntPtr command_queue , uint   num_objects , IntPtr mem_objects ,
			uint   num_events_in_wait_list , IntPtr event_wait_list , out IntPtr xevent ) ;
		[DllImport(DllName)]
		public extern static int clEnqueueReleaseGLObjects(IntPtr command_queue ,
			uint   num_objects , IntPtr mem_objects , uint   num_events_in_wait_list ,
			IntPtr event_wait_list , out IntPtr xevent ) ;
		[DllImport(DllName)]
		public extern static IntPtr clCreateFromGLTexture2D(IntPtr context,
			uint flags, uint target, int  miplevel, uint texture, out uint errcode_ret);
		[DllImport(DllName)]
		public extern static IntPtr clCreateFromGLTexture3D(IntPtr context,
			uint flags, uint target, int  miplevel, uint texture, out uint         errcode_ret );

	
		public delegate int GetGLContextInfoKHR(IntPtr[] properties, uint param_name,
			int param_value_size, IntPtr[] param_value, out uint param_value_size_ret);

		public static GetGLContextInfoKHR clGetGLContextInfoKHR;

		internal static void LoadExtensions()
		{
			clGetGLContextInfoKHR = (GetGLContextInfoKHR)PlatformFactory.GetProcAdressOpenCL<GetGLContextInfoKHR>("clGetGLContextInfoKHR");
			if (clGetGLContextInfoKHR == null)
				Console.WriteLine("OpenCL_gl not supported");
		}
	}
}


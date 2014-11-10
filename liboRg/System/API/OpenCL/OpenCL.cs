//
//  OpenCL.cs
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
		public const string DllName = "libOpenCL.so";

		public delegate void ProgramNotify(IntPtr pProgram, IntPtr user_data);

		[DllImport(DllName)]
		public static extern int clGetPlatformIDs(uint num_entries, IntPtr cl_platform_id, out uint num_platforms);
		[DllImport(DllName)]
		public static extern int clGetPlatformIDs( uint num_entries, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] platforms, 
			out uint num_platforms);

		[DllImport(DllName)]
		public static extern uint clBuildProgram(IntPtr program,
			uint numDevices,
			[In] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysUInt, SizeParamIndex = 1)] IntPtr[] deviceList,
			[In] [MarshalAs(UnmanagedType.LPStr)] string options,
			ProgramNotify pfnNotify,
			IntPtr userData);

		[DllImport(DllName)]
		public extern static unsafe IntPtr clCreateBuffer(IntPtr context, uint flags, IntPtr size, IntPtr host_ptr, out uint errcode_ret);

		[DllImport(DllName)]
		public extern static unsafe IntPtr clCreateCommandQueue(IntPtr context, IntPtr device, uint properties, out uint errcode_ret);

		[DllImport(DllName)]
		public extern static unsafe IntPtr clCreateContext([MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties, uint num_devices, 
			[MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices, IntPtr pfn_notify, IntPtr user_data, out uint errcode_ret);

		[DllImport(DllName)]
		public extern static unsafe IntPtr clCreateContext(IntPtr properties, uint num_devices, 
			[MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices, IntPtr pfn_notify, IntPtr user_data, out uint errcode_ret);

		[DllImport(DllName)]
		public extern static unsafe IntPtr clCreateContextFromType(IntPtr* properties, uint device_type, IntPtr pfn_notify, IntPtr user_data, out uint errcode_ret);

		[DllImport(DllName, EntryPoint = "clCreateImage2D")]
		public extern static unsafe IntPtr clCreateImage2D(IntPtr context, uint flags, uint* image_format, IntPtr image_width, IntPtr image_height, IntPtr image_row_pitch, IntPtr host_ptr, out int errcode_ret);

		[DllImport(DllName, EntryPoint = "clCreateImage3D")]
		public extern static unsafe IntPtr clCreateImage3D(IntPtr context, uint flags, uint* image_format, IntPtr image_width, IntPtr image_height, IntPtr image_depth, IntPtr image_row_pitch, IntPtr image_slice_pitch, IntPtr host_ptr, out int errcode_ret);

		[DllImport(DllName, EntryPoint = "clCreateKernel")]
		public extern static unsafe IntPtr clCreateKernel(IntPtr program, String kernel_name, out uint errcode_ret);

		[DllImport(DllName, EntryPoint = "clCreateKernelsInProgram")]
		public extern static unsafe int clCreateKernelsInProgram(IntPtr program, uint num_kernels, IntPtr* kernels, [OutAttribute] uint* num_kernels_ret);

		[DllImport(DllName, EntryPoint = "clCreateProgramWithBinary")]
		public extern static unsafe IntPtr clCreateProgramWithBinary(IntPtr context, uint num_devices, IntPtr* device_list, IntPtr* lengths, byte** binaries, int* binary_status, out uint errcode_ret);

		[DllImport(DllName)]
		public static extern IntPtr clCreateProgramWithSource(IntPtr context,
			uint count,
			[In] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 1)] string[] strings,
			[In] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysUInt, SizeParamIndex = 1)] IntPtr[] lengths,
			out uint errcodeRet);

		[DllImport(DllName, EntryPoint = "clCreateSampler")]
		public extern static unsafe IntPtr clCreateSampler(IntPtr context, bool normalized_coords, uint addressing_mode, uint filter_mode, out int errcode_ret);

		[DllImport(DllName, EntryPoint = "clEnqueueBarrier")]
		public extern static int clEnqueueBarrier(IntPtr command_queue);

		[DllImport(DllName, EntryPoint = "clEnqueueCopyBuffer")]
		public extern static unsafe int EnqueueCopyBuffer(IntPtr command_queue, IntPtr src_buffer, IntPtr dst_buffer, IntPtr src_offset, IntPtr dst_offset, IntPtr cb, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event);

		[DllImport(DllName, EntryPoint = "clEnqueueCopyBufferToImage")]
		public extern static unsafe int clEnqueueCopyBufferToImage(IntPtr command_queue, IntPtr src_buffer, IntPtr dst_image, IntPtr src_offset, IntPtr** dst_origin, IntPtr** region, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event);

		[DllImport(DllName, EntryPoint = "clEnqueueCopyImage")]
		public extern static unsafe int clEnqueueCopyImage(IntPtr command_queue, IntPtr src_image, IntPtr dst_image, IntPtr** src_origin, IntPtr** dst_origin, IntPtr** region, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event);

		[DllImport(DllName, EntryPoint = "clEnqueueCopyImageToBuffer")]
		public extern static unsafe int clEnqueueCopyImageToBuffer(IntPtr command_queue, IntPtr src_image, IntPtr dst_buffer, IntPtr** src_origin, IntPtr** region, IntPtr dst_offset, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event);

		[DllImport(DllName, EntryPoint = "clEnqueueMapBuffer")]
		public extern static unsafe System.IntPtr clEnqueueMapBuffer(IntPtr command_queue, IntPtr buffer, bool blocking_map, uint map_flags, IntPtr offset, IntPtr cb, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event, out int errcode_ret);

		[DllImport(DllName, EntryPoint = "clEnqueueMapImage")]
		public extern static unsafe System.IntPtr clEnqueueMapImage(IntPtr command_queue, IntPtr image, bool blocking_map, uint map_flags, IntPtr** origin, IntPtr** region, IntPtr* image_row_pitch, IntPtr* image_slice_pitch, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event, out int errcode_ret);

		[DllImport(DllName, EntryPoint = "clEnqueueMarker")]
		public extern static unsafe int clEnqueueMarker(IntPtr command_queue, IntPtr* @event);

		[DllImport(DllName, EntryPoint = "clEnqueueNativeKernel")]
		public extern static unsafe int clEnqueueNativeKernel(IntPtr command_queue, IntPtr user_func, IntPtr args, IntPtr cb_args, uint num_mem_objects, IntPtr* mem_list, IntPtr args_mem_loc, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event);
		[DllImport(DllName)]
		public static extern uint clEnqueueNDRangeKernel(IntPtr commandQueue,
			IntPtr kernel,
			uint workDim,
			[In] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] globalWorkOffset,
			[In] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] globalWorkSize,
			[In] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] localWorkSize,
			uint numEventsInWaitList,
			[In] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysUInt, SizeParamIndex = 6)] IntPtr[] eventWaitList,
			[Out] [MarshalAs(UnmanagedType.Struct)] out IntPtr e);
	

		[DllImport(DllName, EntryPoint = "clEnqueueReadBuffer")]
		public extern static unsafe int clEnqueueReadBuffer(IntPtr commandQueue, 
		                                                    IntPtr buffer,
		                                                    bool blockingRead,
		                                                    IntPtr offsetInBytes,
		                                                    IntPtr lengthInBytes,
		                                                    IntPtr ptr,
		                                                    uint numEventsInWaitList,
		                                                    [In] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysUInt, SizeParamIndex = 6)] IntPtr[] eventWaitList,
		                                                    [Out] [MarshalAs(UnmanagedType.Struct)] out IntPtr e);
		public static int clEnqueueReadBuffer(IntPtr commandQueue,
											IntPtr buffer,
											bool blockingRead,
											IntPtr offsetInBytes,
											IntPtr lengthInBytes,
											object data,
											uint numEventsInWaitList,
											IntPtr[] eventWaitList,
											out IntPtr e)
		{
			using (var dataPtr = data.Pin())
				return clEnqueueReadBuffer(commandQueue, buffer, 
					blockingRead, offsetInBytes, lengthInBytes, dataPtr, numEventsInWaitList, eventWaitList, out e);
		}

		[DllImport(DllName, EntryPoint = "clEnqueueReadImage")]
		public extern static unsafe int clEnqueueReadImage(IntPtr command_queue, IntPtr image, bool blocking_read, IntPtr** origin, IntPtr** region, IntPtr row_pitch, IntPtr slice_pitch, IntPtr ptr, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event);

		[DllImport(DllName, EntryPoint = "clEnqueueTask")]
		public extern static unsafe int clEnqueueTask(IntPtr command_queue, IntPtr kernel, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event);

		[DllImport(DllName, EntryPoint = "clEnqueueUnmapMemObject")]
		public extern static unsafe int clEnqueueUnmapMemObject(IntPtr command_queue, IntPtr memobj, IntPtr mapped_ptr, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event);

		[DllImport(DllName, EntryPoint = "clEnqueueWaitForEvents")]
		public extern static unsafe int clEnqueueWaitForEvents(IntPtr command_queue, uint num_events, IntPtr* event_list);

		[DllImport(DllName, EntryPoint = "clEnqueueWriteBuffer")]
		public extern static unsafe int clEnqueueWriteBuffer(IntPtr commandQueue,
			IntPtr buffer,
			bool blockingWrite,
			IntPtr offsetInBytes,
			IntPtr lengthInBytes,
			IntPtr ptr,
			uint numEventsInWaitList,
			[In] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysUInt, SizeParamIndex = 6)] IntPtr[] eventWaitList,
			[Out] [MarshalAs(UnmanagedType.Struct)] out IntPtr e);

		[DllImport(DllName, EntryPoint = "clEnqueueWriteImage")]
		public extern static unsafe int clEnqueueWriteImage(IntPtr command_queue, IntPtr image, bool blocking_write, IntPtr** origin, IntPtr** region, IntPtr input_row_pitch, IntPtr input_slice_pitch, IntPtr ptr, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event);

		[DllImport(DllName, EntryPoint = "clFinish")]
		public extern static int clFinish(IntPtr command_queue);

		[DllImport(DllName, EntryPoint = "clFlush")]
		public extern static int clFlush(IntPtr command_queue);

		[DllImport(DllName, EntryPoint = "clGetCommandQueueInfo")]
		public extern static unsafe int clGetCommandQueueInfo(IntPtr command_queue, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetContextInfo")]
		public extern static unsafe int clGetContextInfo(IntPtr context, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetDeviceIDs")]
		public extern static unsafe int clGetDeviceIDs(IntPtr platform, uint device_type, uint num_entries, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices, out uint num_devices);

		[DllImport(DllName, EntryPoint = "clGetDeviceInfo")]
		public extern static unsafe int clGetDeviceInfo(IntPtr device, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetEventInfo")]
		public extern static unsafe int clGetEventInfo(IntPtr @event, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetEventProfilingInfo")]
		public extern static unsafe int clGetEventProfilingInfo(IntPtr @event, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetImageInfo")]
		public extern static unsafe int clGetImageInfo(IntPtr image, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetKernelInfo")]
		public extern static unsafe int clGetKernelInfo(IntPtr kernel, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetKernelWorkGroupInfo")]
		public extern static unsafe int clGetKernelWorkGroupInfo(IntPtr kernel, IntPtr device, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetMemObjectInfo")]
		public extern static unsafe int clGetMemObjectInfo(IntPtr memobj, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetPlatformInfo")]
		internal extern static unsafe int clGetPlatformInfo(IntPtr platform, uint param_name, int param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetProgramBuildInfo")]
		public extern static unsafe int clGetProgramBuildInfo(IntPtr program, IntPtr device, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetProgramInfo")]
		public extern static unsafe int clGetProgramInfo(IntPtr program, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetSamplerInfo")]
		public extern static unsafe int clGetSamplerInfo(IntPtr sampler, uint param_name, uint param_value_size, IntPtr param_value, ref uint param_value_size_ret);

		[DllImport(DllName, EntryPoint = "clGetSupportedImageFormats")]
		public extern static unsafe int clGetSupportedImageFormats(IntPtr context, uint flags, uint image_type, uint num_entries, uint* image_formats, uint* num_image_formats);

		[DllImport(DllName, EntryPoint = "clReleaseCommandQueue")]
		public extern static int clReleaseCommandQueue(IntPtr command_queue);

		[DllImport(DllName, EntryPoint = "clReleaseContext")]
		public extern static int clReleaseContext(IntPtr context);

		[DllImport(DllName, EntryPoint = "clReleaseEvent")]
		public extern static int clReleaseEvent(IntPtr @event);

		[DllImport(DllName, EntryPoint = "clReleaseKernel")]
		public extern static int clReleaseKernel(IntPtr kernel);

		[DllImport(DllName, EntryPoint = "clReleaseMemObject")]
		public extern static int clReleaseMemObject(IntPtr memobj);

		[DllImport(DllName, EntryPoint = "clReleaseProgram")]
		public extern static int clReleaseProgram(IntPtr program);

		[DllImport(DllName, EntryPoint = "clReleaseSampler")]
		public extern static int clReleaseSampler(IntPtr sampler);

		[DllImport(DllName, EntryPoint = "clRetainCommandQueue")]
		public extern static int clRetainCommandQueue(IntPtr command_queue);

		[DllImport(DllName, EntryPoint = "clRetainContext")]
		public extern static int clRetainContext(IntPtr context);

		[DllImport(DllName, EntryPoint = "clRetainEvent")]
		public extern static int clRetainEvent(IntPtr @event);

		[DllImport(DllName, EntryPoint = "clRetainKernel")]
		public extern static int clRetainKernel(IntPtr kernel);

		[DllImport(DllName, EntryPoint = "clRetainMemObject")]
		public extern static int clRetainMemObject(IntPtr memobj);

		[DllImport(DllName, EntryPoint = "clRetainProgram")]
		public extern static int clRetainProgram(IntPtr program);

		[DllImport(DllName, EntryPoint = "clRetainSampler")]
		public extern static int clRetainSampler(IntPtr sampler);

		[DllImport(DllName, EntryPoint = "clSetCommandQueueProperty")]
		public extern static unsafe int clSetCommandQueueProperty(IntPtr command_queue, uint properties, bool enable, uint* old_properties);

		[DllImport(DllName, EntryPoint = "clSetKernelArg")]
		private static extern uint SetKernelArg(IntPtr kernel, uint argIndex, IntPtr argSize, IntPtr argValue);
		public static uint clSetKernelArg(IntPtr kernel, uint argIndex, IntPtr argSize, object argValue)
		{
			using (var argPtr = argValue.Pin())
				return SetKernelArg(kernel, argIndex, argSize, argValue == null ? IntPtr.Zero : argPtr);
		}

		[DllImport(DllName, EntryPoint = "clUnloadCompiler")]
		public extern static int clUnloadCompiler();

		[DllImport(DllName, EntryPoint = "clWaitForEvents")]
		public extern static unsafe int clWaitForEvents(uint num_events, IntPtr* event_list);

		[DllImport(DllName)]
		public extern static IntPtr clGetExtensionFunctionAddress (string 	 funcname);
		[DllImport(DllName)]
		public extern static  void clContextFree(IntPtr pHandle);

	}
}


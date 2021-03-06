﻿//
//  CLEnum.cs
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

namespace System.API.OpenCL
{
	public enum CL : uint
	{
		FALSE                                   = 0,
		TRUE                                    = 1,
		BLOCKING                                = TRUE,
		NON_BLOCKING                            = FALSE,

		/* cl_platform_info */
		PLATFORM_PROFILE                         = 0x0900,
		PLATFORM_VERSION                         = 0x0901,
		PLATFORM_NAME                            = 0x0902,
		PLATFORM_VENDOR                          = 0x0903,
		PLATFORM_EXTENSIONS                      = 0x0904,

		/* cl_device_type - bitfield */
		DEVICE_TYPE_DEFAULT                      = (1 << 0),
		DEVICE_TYPE_CPU                          = (1 << 1),
		DEVICE_TYPE_GPU                          = (1 << 2),
		DEVICE_TYPE_ACCELERATOR                  = (1 << 3),
		DEVICE_TYPE_CUSTOM                       = (1 << 4),
		DEVICE_TYPE_ALL                          = 0xFFFFFFFF,

		/* cl_device_info */
		DEVICE_TYPE                                  = 0x1000,
		DEVICE_VENDOR_ID                             = 0x1001,
		DEVICE_MAX_COMPUTE_UNITS                     = 0x1002,
		DEVICE_MAX_WORK_ITEM_DIMENSIONS              = 0x1003,
		DEVICE_MAX_WORK_GROUP_SIZE                   = 0x1004,
		DEVICE_MAX_WORK_ITEM_SIZES                   = 0x1005,
		DEVICE_PREFERRED_VECTOR_WIDTH_CHAR           = 0x1006,
		DEVICE_PREFERRED_VECTOR_WIDTH_SHORT          = 0x1007,
		DEVICE_PREFERRED_VECTOR_WIDTH_INT            = 0x1008,
		DEVICE_PREFERRED_VECTOR_WIDTH_LONG           = 0x1009,
		DEVICE_PREFERRED_VECTOR_WIDTH_FLOAT          = 0x100A,
		DEVICE_PREFERRED_VECTOR_WIDTH_DOUBLE         = 0x100B,
		DEVICE_MAX_CLOCK_FREQUENCY                   = 0x100C,
		DEVICE_ADDRESS_BITS                          = 0x100D,
		DEVICE_MAX_READ_IMAGE_ARGS                   = 0x100E,
		DEVICE_MAX_WRITE_IMAGE_ARGS                  = 0x100F,
		DEVICE_MAX_MEM_ALLOC_SIZE                    = 0x1010,
		DEVICE_IMAGE2D_MAX_WIDTH                     = 0x1011,
		DEVICE_IMAGE2D_MAX_HEIGHT                    = 0x1012,
		DEVICE_IMAGE3D_MAX_WIDTH                     = 0x1013,
		DEVICE_IMAGE3D_MAX_HEIGHT                    = 0x1014,
		DEVICE_IMAGE3D_MAX_DEPTH                     = 0x1015,
		DEVICE_IMAGE_SUPPORT                         = 0x1016,
		DEVICE_MAX_PARAMETER_SIZE                    = 0x1017,
		DEVICE_MAX_SAMPLERS                          = 0x1018,
		DEVICE_MEM_BASE_ADDR_ALIGN                   = 0x1019,
		DEVICE_MIN_DATA_TYPE_ALIGN_SIZE              = 0x101A,
		DEVICE_SINGLE_FP_CONFIG                      = 0x101B,
		DEVICE_GLOBAL_MEM_CACHE_TYPE                 = 0x101C,
		DEVICE_GLOBAL_MEM_CACHELINE_SIZE             = 0x101D,
		DEVICE_GLOBAL_MEM_CACHE_SIZE                 = 0x101E,
		DEVICE_GLOBAL_MEM_SIZE                       = 0x101F,
		DEVICE_MAX_CONSTANT_BUFFER_SIZE              = 0x1020,
		DEVICE_MAX_CONSTANT_ARGS                     = 0x1021,
		DEVICE_LOCAL_MEM_TYPE                        = 0x1022,
		DEVICE_LOCAL_MEM_SIZE                        = 0x1023,
		DEVICE_ERROR_CORRECTION_SUPPORT              = 0x1024,
		DEVICE_PROFILING_TIMER_RESOLUTION            = 0x1025,
		DEVICE_ENDIAN_LITTLE                         = 0x1026,
		DEVICE_AVAILABLE                             = 0x1027,
		DEVICE_COMPILER_AVAILABLE                    = 0x1028,
		DEVICE_EXECUTION_CAPABILITIES                = 0x1029,
		DEVICE_QUEUE_PROPERTIES                      = 0x102A ,   /* deprecated */
		DEVICE_QUEUE_ON_HOST_PROPERTIES              = 0x102A,
		DEVICE_NAME                                  = 0x102B,
		DEVICE_VENDOR                                = 0x102C,
		DRIVER_VERSION                               = 0x102D,
		DEVICE_PROFILE                               = 0x102E,
		DEVICE_VERSION                               = 0x102F,
		DEVICE_EXTENSIONS                            = 0x1030,
		DEVICE_PLATFORM                              = 0x1031,
		DEVICE_DOUBLE_FP_CONFIG                      = 0x1032,

		DEVICE_PREFERRED_VECTOR_WIDTH_HALF           = 0x1034,
		DEVICE_HOST_UNIFIED_MEMORY                   = 0x1035 ,  /* deprecated */
		DEVICE_NATIVE_VECTOR_WIDTH_CHAR              = 0x1036,
		DEVICE_NATIVE_VECTOR_WIDTH_SHORT             = 0x1037,
		DEVICE_NATIVE_VECTOR_WIDTH_INT               = 0x1038,
		DEVICE_NATIVE_VECTOR_WIDTH_LONG              = 0x1039,
		DEVICE_NATIVE_VECTOR_WIDTH_FLOAT             = 0x103A,
		DEVICE_NATIVE_VECTOR_WIDTH_DOUBLE            = 0x103B,
		DEVICE_NATIVE_VECTOR_WIDTH_HALF              = 0x103C,
		DEVICE_OPENCL_C_VERSION                      = 0x103D,
		DEVICE_LINKER_AVAILABLE                      = 0x103E,
		DEVICE_BUILT_IN_KERNELS                      = 0x103F,
		DEVICE_IMAGE_MAX_BUFFER_SIZE                 = 0x1040,
		DEVICE_IMAGE_MAX_ARRAY_SIZE                  = 0x1041,
		DEVICE_PARENT_DEVICE                         = 0x1042,
		DEVICE_PARTITION_MAX_SUB_DEVICES             = 0x1043,
		DEVICE_PARTITION_PROPERTIES                  = 0x1044,
		DEVICE_PARTITION_AFFINITY_DOMAIN             = 0x1045,
		DEVICE_PARTITION_TYPE                        = 0x1046,
		DEVICE_REFERENCE_COUNT                       = 0x1047,
		DEVICE_PREFERRED_INTEROP_USER_SYNC           = 0x1048,
		DEVICE_PRINTF_BUFFER_SIZE                    = 0x1049,
		DEVICE_IMAGE_PITCH_ALIGNMENT                 = 0x104A,
		DEVICE_IMAGE_BASE_ADDRESS_ALIGNMENT          = 0x104B,
		DEVICE_MAX_READ_WRITE_IMAGE_ARGS             = 0x104C,
		DEVICE_MAX_GLOBAL_VARIABLE_SIZE              = 0x104D,
		DEVICE_QUEUE_ON_DEVICE_PROPERTIES            = 0x104E,
		DEVICE_QUEUE_ON_DEVICE_PREFERRED_SIZE        = 0x104F,
		DEVICE_QUEUE_ON_DEVICE_MAX_SIZE              = 0x1050,
		DEVICE_MAX_ON_DEVICE_QUEUES                  = 0x1051,
		DEVICE_MAX_ON_DEVICE_EVENTS                  = 0x1052,
		DEVICE_SVM_CAPABILITIES                      = 0x1053,
		DEVICE_GLOBAL_VARIABLE_PREFERRED_TOTAL_SIZE  = 0x1054,
		DEVICE_MAX_PIPE_ARGS                         = 0x1055,
		DEVICE_PIPE_MAX_ACTIVE_RESERVATIONS          = 0x1056,
		DEVICE_PIPE_MAX_PACKET_SIZE                  = 0x1057,
		DEVICE_PREFERRED_PLATFORM_ATOMIC_ALIGNMENT   = 0x1058,
		DEVICE_PREFERRED_GLOBAL_ATOMIC_ALIGNMENT     = 0x1059,
		DEVICE_PREFERRED_LOCAL_ATOMIC_ALIGNMENT      = 0x105A,

		NONE                                     	= 0x0,
		READ_ONLY_CACHE                          	= 0x1,
		READ_WRITE_CACHE                         	= 0x2,
		LOCAL                                    	= 0x1,
		GLOBAL                                   	= 0x2,
		EXEC_KERNEL                              = (1 << 0),
		EXEC_NATIVE_KERNEL                       = (1 << 1),
		QUEUE_OUT_OF_ORDER_EXEC_MODE_ENABLE      = (1 << 0),
		QUEUE_PROFILING_ENABLE                   = (1 << 1),
		QUEUE_ON_DEVICE                          = (1 << 2),
		QUEUE_ON_DEVICE_DEFAULT                  = (1 << 3),

		/* cl_context_info  */
		CONTEXT_REFERENCE_COUNT                  = 0x1080,
		CONTEXT_DEVICES                          = 0x1081,
		CONTEXT_PROPERTIES                       = 0x1082,
		CONTEXT_NUM_DEVICES                      = 0x1083,

		/* cl_context_properties */
		CONTEXT_PLATFORM                         = 0x1084,
		CONTEXT_INTEROP_USER_SYNC                = 0x1085,

		/* cl_device_partition_property */
		DEVICE_PARTITION_EQUALLY                 = 0x1086,
		DEVICE_PARTITION_BY_COUNTS               = 0x1087,
		DEVICE_PARTITION_BY_COUNTS_LIST_END      = 0x0,
		DEVICE_PARTITION_BY_AFFINITY_DOMAIN      = 0x1088,

		/* cl_device_affinity_domain */
		DEVICE_AFFINITY_DOMAIN_NUMA               = (1 << 0),
		DEVICE_AFFINITY_DOMAIN_L4_CACHE           = (1 << 1),
		DEVICE_AFFINITY_DOMAIN_L3_CACHE            = (1 << 2),
		DEVICE_AFFINITY_DOMAIN_L2_CACHE           = (1 << 3),
		DEVICE_AFFINITY_DOMAIN_L1_CACHE           = (1 << 4),
		DEVICE_AFFINITY_DOMAIN_NEXT_PARTITIONABLE = (1 << 5),

		/* cl_device_svm_capabilities */
		DEVICE_SVM_COARSE_GRAIN_BUFFER           = (1 << 0),
		DEVICE_SVM_FINE_GRAIN_BUFFER             = (1 << 1),
		DEVICE_SVM_FINE_GRAIN_SYSTEM             = (1 << 2),
		DEVICE_SVM_ATOMICS                       = (1 << 3),

		/* cl_command_queue_info */
		QUEUE_CONTEXT                            = 0x1090,
		QUEUE_DEVICE                             = 0x1091,
		QUEUE_REFERENCE_COUNT                    = 0x1092,
		QUEUE_PROPERTIES                         = 0x1093,
		QUEUE_SIZE                               = 0x1094,

		/* cl_mem_flags and cl_svm_mem_flags - bitfield */
		MEM_READ_WRITE                           = (1 << 0),
		MEM_WRITE_ONLY                           = (1 << 1),
		MEM_READ_ONLY                            = (1 << 2),
		MEM_USE_HOST_PTR                         = (1 << 3),
		MEM_ALLOC_HOST_PTR                       = (1 << 4),
		MEM_COPY_HOST_PTR                        = (1 << 5),
		/* reserved                                         (1 << 6),    */
		MEM_HOST_WRITE_ONLY                      = (1 << 7),
		MEM_HOST_READ_ONLY                       = (1 << 8),
		MEM_HOST_NO_ACCESS                       = (1 << 9),
		MEM_SVM_FINE_GRAIN_BUFFER                = (1 << 10),   /* used by cl_svm_mem_flags only */
		MEM_SVM_ATOMICS                          = (1 << 11),   /* used by cl_svm_mem_flags only */

		/* cl_mem_migration_flags - bitfield */
		MIGRATE_MEM_OBJECT_HOST                  = (1 << 0),
		MIGRATE_MEM_OBJECT_CONTENT_UNDEFINED     = (1 << 1),

		/* cl_channel_order */
		R                                        = 0x10B0,
		A                                        = 0x10B1,
		RG                                       = 0x10B2,
		RA                                       = 0x10B3,
		RGB                                      = 0x10B4,
		RGBA                                     = 0x10B5,
		BGRA                                     = 0x10B6,
		ARGB                                     = 0x10B7,
		INTENSITY                                = 0x10B8,
		LUMINANCE                                = 0x10B9,
		Rx                                       = 0x10BA,
		RGx                                      = 0x10BB,
		RGBx                                     = 0x10BC,
		DEPTH                                    = 0x10BD,
		DEPTH_STENCIL                            = 0x10BE,
		sRGB                                     = 0x10BF,
		sRGBx                                    = 0x10C0,
		sRGBA                                    = 0x10C1,
		sBGRA                                    = 0x10C2,
		ABGR                                     = 0x10C3,

		/* cl_channel_type */
		SNORM_INT8                               = 0x10D0,
		SNORM_INT16                              = 0x10D1,
		UNORM_INT8                               = 0x10D2,
		UNORM_INT16                              = 0x10D3,
		UNORM_SHORT_565                          = 0x10D4,
		UNORM_SHORT_555                          = 0x10D5,
		UNORM_INT_101010                         = 0x10D6,
		SIGNED_INT8                              = 0x10D7,
		SIGNED_INT16                             = 0x10D8,
		SIGNED_INT32                             = 0x10D9,
		UNSIGNED_INT8                            = 0x10DA,
		UNSIGNED_INT16                           = 0x10DB,
		UNSIGNED_INT32                           = 0x10DC,
		HALF_FLOAT                               = 0x10DD,
		FLOAT                                    = 0x10DE,
		UNORM_INT24                              = 0x10DF,

		/* cl_mem_object_type */
		MEM_OBJECT_BUFFER                        = 0x10F0,
		MEM_OBJECT_IMAGE2D                       = 0x10F1,
		MEM_OBJECT_IMAGE3D                       = 0x10F2,
		MEM_OBJECT_IMAGE2D_ARRAY                 = 0x10F3,
		MEM_OBJECT_IMAGE1D                       = 0x10F4,
		MEM_OBJECT_IMAGE1D_ARRAY                 = 0x10F5,
		MEM_OBJECT_IMAGE1D_BUFFER                = 0x10F6,
		MEM_OBJECT_PIPE                          = 0x10F7,

		/* cl_mem_info */
		MEM_TYPE                                 = 0x1100,
		MEM_FLAGS                                = 0x1101,
		MEM_SIZE                                 = 0x1102,
		MEM_HOST_PTR                             = 0x1103,
		MEM_MAP_COUNT                            = 0x1104,
		MEM_REFERENCE_COUNT                      = 0x1105,
		MEM_CONTEXT                              = 0x1106,
		MEM_ASSOCIATED_MEMOBJECT                 = 0x1107,
		MEM_OFFSET                               = 0x1108,
		MEM_USES_SVM_POINTER                     = 0x1109,

		/* cl_image_info */
		IMAGE_FORMAT                             = 0x1110,
		IMAGE_ELEMENT_SIZE                       = 0x1111,
		IMAGE_ROW_PITCH                          = 0x1112,
		IMAGE_SLICE_PITCH                        = 0x1113,
		IMAGE_WIDTH                              = 0x1114,
		IMAGE_HEIGHT                             = 0x1115,
		IMAGE_DEPTH                              = 0x1116,
		IMAGE_ARRAY_SIZE                         = 0x1117,
		IMAGE_BUFFER                             = 0x1118,
		IMAGE_NUM_MIP_LEVELS                     = 0x1119,
		IMAGE_NUM_SAMPLES                        = 0x111A,

		/* cl_pipe_info */
		PIPE_PACKET_SIZE                         = 0x1120,
		PIPE_MAX_PACKETS                         = 0x1121,

		/* cl_addressing_mode */
		ADDRESS_NONE                             = 0x1130,
		ADDRESS_CLAMP_TO_EDGE                    = 0x1131,
		ADDRESS_CLAMP                            = 0x1132,
		ADDRESS_REPEAT                           = 0x1133,
		ADDRESS_MIRRORED_REPEAT                  = 0x1134,

		/* cl_filter_mode */
		FILTER_NEAREST                           = 0x1140,
		FILTER_LINEAR                            = 0x1141,

		/* cl_sampler_info */
		SAMPLER_REFERENCE_COUNT                  = 0x1150,
		SAMPLER_CONTEXT                          = 0x1151,
		SAMPLER_NORMALIZED_COORDS                = 0x1152,
		SAMPLER_ADDRESSING_MODE                  = 0x1153,
		SAMPLER_FILTER_MODE                      = 0x1154,
		SAMPLER_MIP_FILTER_MODE                  = 0x1155,
		SAMPLER_LOD_MIN                          = 0x1156,
		SAMPLER_LOD_MAX                          = 0x1157,

		/* cl_map_flags - bitfield */
		MAP_READ                                 = (1 << 0),
		MAP_WRITE                                = (1 << 1),
		MAP_WRITE_INVALIDATE_REGION              = (1 << 2),

		/* cl_program_info */
		PROGRAM_REFERENCE_COUNT                  = 0x1160,
		PROGRAM_CONTEXT                          = 0x1161,
		PROGRAM_NUM_DEVICES                      = 0x1162,
		PROGRAM_DEVICES                          = 0x1163,
		PROGRAM_SOURCE                           = 0x1164,
		PROGRAM_BINARY_SIZES                     = 0x1165,
		PROGRAM_BINARIES                         = 0x1166,
		PROGRAM_NUM_KERNELS                      = 0x1167,
		PROGRAM_KERNEL_NAMES                     = 0x1168,

		/* cl_program_build_info */
		PROGRAM_BUILD_STATUS                     = 0x1181,
		PROGRAM_BUILD_OPTIONS                    = 0x1182,
		PROGRAM_BUILD_LOG                        = 0x1183,
		PROGRAM_BINARY_TYPE                      = 0x1184,
		PROGRAM_BUILD_GLOBAL_VARIABLE_TOTAL_SIZE = 0x1185,

		/* cl_program_binary_type */
		PROGRAM_BINARY_TYPE_NONE                 = 0x0,
		PROGRAM_BINARY_TYPE_COMPILED_OBJECT      = 0x1,
		PROGRAM_BINARY_TYPE_LIBRARY              = 0x2,
		PROGRAM_BINARY_TYPE_EXECUTABLE           = 0x4,

		/* cl_kernel_info */
		KERNEL_FUNCTION_NAME                     = 0x1190,
		KERNEL_NUM_ARGS                          = 0x1191,
		KERNEL_REFERENCE_COUNT                   = 0x1192,
		KERNEL_CONTEXT                           = 0x1193,
		KERNEL_PROGRAM                           = 0x1194,
		KERNEL_ATTRIBUTES                        = 0x1195,

		/* cl_kernel_arg_info */
		KERNEL_ARG_ADDRESS_QUALIFIER             = 0x1196,
		KERNEL_ARG_ACCESS_QUALIFIER              = 0x1197,
		KERNEL_ARG_TYPE_NAME                     = 0x1198,
		KERNEL_ARG_TYPE_QUALIFIER                = 0x1199,
		KERNEL_ARG_NAME                          = 0x119A,

		/* cl_kernel_arg_address_qualifier */
		KERNEL_ARG_ADDRESS_GLOBAL                = 0x119B,
		KERNEL_ARG_ADDRESS_LOCAL                 = 0x119C,
		KERNEL_ARG_ADDRESS_CONSTANT              = 0x119D,
		KERNEL_ARG_ADDRESS_PRIVATE               = 0x119E,

		/* cl_kernel_arg_access_qualifier */
		KERNEL_ARG_ACCESS_READ_ONLY              = 0x11A0,
		KERNEL_ARG_ACCESS_WRITE_ONLY             = 0x11A1,
		KERNEL_ARG_ACCESS_READ_WRITE             = 0x11A2,
		KERNEL_ARG_ACCESS_NONE                   = 0x11A3,

		/* cl_kernel_arg_type_qualifer */
		KERNEL_ARG_TYPE_NONE                     = 0,
		KERNEL_ARG_TYPE_CONST                    = (1 << 0),
		KERNEL_ARG_TYPE_RESTRICT                 = (1 << 1),
		KERNEL_ARG_TYPE_VOLATILE                 = (1 << 2),
		KERNEL_ARG_TYPE_PIPE                     = (1 << 3),

		/* cl_kernel_work_group_info */
		KERNEL_WORK_GROUP_SIZE                   = 0x11B0,
		KERNEL_COMPILE_WORK_GROUP_SIZE           = 0x11B1,
		KERNEL_LOCAL_MEM_SIZE                    = 0x11B2,
		KERNEL_PREFERRED_WORK_GROUP_SIZE_MULTIPLE = 0x11B3,
		KERNEL_PRIVATE_MEM_SIZE                  = 0x11B4,
		KERNEL_GLOBAL_WORK_SIZE                  = 0x11B5,

		/* cl_kernel_exec_info */
		KERNEL_EXEC_INFO_SVM_PTRS                = 0x11B6,
		KERNEL_EXEC_INFO_SVM_FINE_GRAIN_SYSTEM   = 0x11B7,

		/* cl_event_info  */
		EVENT_COMMAND_QUEUE                      = 0x11D0,
		EVENT_COMMAND_TYPE                       = 0x11D1,
		EVENT_REFERENCE_COUNT                    = 0x11D2,
		EVENT_COMMAND_EXECUTION_STATUS           = 0x11D3,
		EVENT_CONTEXT                            = 0x11D4,

		/* cl_command_type */
		COMMAND_NDRANGE_KERNEL                   = 0x11F0,
		COMMAND_TASK                             = 0x11F1,
		COMMAND_NATIVE_KERNEL                    = 0x11F2,
		COMMAND_READ_BUFFER                      = 0x11F3,
		COMMAND_WRITE_BUFFER                     = 0x11F4,
		COMMAND_COPY_BUFFER                      = 0x11F5,
		COMMAND_READ_IMAGE                       = 0x11F6,
		COMMAND_WRITE_IMAGE                      = 0x11F7,
		COMMAND_COPY_IMAGE                       = 0x11F8,
		COMMAND_COPY_IMAGE_TO_BUFFER             = 0x11F9,
		COMMAND_COPY_BUFFER_TO_IMAGE             = 0x11FA,
		COMMAND_MAP_BUFFER                       = 0x11FB,
		COMMAND_MAP_IMAGE                        = 0x11FC,
		COMMAND_UNMAP_MEM_OBJECT                 = 0x11FD,
		COMMAND_MARKER                           = 0x11FE,
		COMMAND_ACQUIRE_GL_OBJECTS               = 0x11FF,
		COMMAND_RELEASE_GL_OBJECTS               = 0x1200,
		COMMAND_READ_BUFFER_RECT                 = 0x1201,
		COMMAND_WRITE_BUFFER_RECT                = 0x1202,
		COMMAND_COPY_BUFFER_RECT                 = 0x1203,
		COMMAND_USER                             = 0x1204,
		COMMAND_BARRIER                          = 0x1205,
		COMMAND_MIGRATE_MEM_OBJECTS              = 0x1206,
		COMMAND_FILL_BUFFER                      = 0x1207,
		COMMAND_FILL_IMAGE                       = 0x1208,
		COMMAND_SVM_FREE                         = 0x1209,
		COMMAND_SVM_MEMCPY                       = 0x120A,
		COMMAND_SVM_MEMFILL                      = 0x120B,
		COMMAND_SVM_MAP                          = 0x120C,
		COMMAND_SVM_UNMAP                        = 0x120D,

		/* command execution status */
		COMPLETE                                 = 0x0,
		RUNNING                                  = 0x1,
		SUBMITTED                                = 0x2,
		QUEUED                                   = 0x3,

		/* cl_buffer_create_type  */
		BUFFER_CREATE_TYPE_REGION                = 0x1220,

		/* cl_profiling_info  */
		PROFILING_COMMAND_QUEUED                 = 0x1280,
		PROFILING_COMMAND_SUBMIT                 = 0x1281,
		PROFILING_COMMAND_START                  = 0x1282,
		PROFILING_COMMAND_END                    = 0x1283,
		PROFILING_COMMAND_COMPLETE               = 0x1284,


		/* Additional Error Codes  */
		//INVALID_GL_SHAREGROUP_REFERENCE_KHR  -1000

		/* cl_gl_context_info  */
		CURRENT_DEVICE_FOR_GL_CONTEXT_KHR   = 0x2006,
		DEVICES_FOR_GL_CONTEXT_KHR          = 0x2007,

		/* Additional cl_context_properties  */
		GL_CONTEXT_KHR                      = 0x2008,
		EGL_DISPLAY_KHR                     = 0x2009,
		GLX_DISPLAY_KHR                     = 0x200A,
		WGL_HDC_KHR                         = 0x200B,
		CGL_SHAREGROUP_KHR                  = 0x200C,

	}


	public enum CLBUILD : int
	{
		/* cl_build_status */
		SUCCESS                            = 0,
		NONE                               = -1,
		ERROR                              = -2,
		 IN_PROGRESS                        = -3,
	}
}


//
//  GLXEnum.cs
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

namespace liboRg.OpenGL
{
	public enum GLX : int
	{
		CONTEXT_MAJOR_VERSION_ARB      = 0x2091,
		CONTEXT_MINOR_VERSION_ARB      = 0x2092,

		CONTEXT_FLAGS_ARB                   = 0x2094,
		CONTEXT_PROFILE_MASK_ARB            = 0x9126,

		CONTEXT_CORE_PROFILE_BIT_ARB         = 0x00000001,
		CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB = 0x00000002,

		USE_GL							= 1,	/* support GLX rendering */
		BUFFER_SIZE						= 2,	/* depth of the color buffer */
		LEVEL							= 3,	/* level in plane stacking */
		RGBA							= 4,	/* true if RGBA mode */
		DOUBLEBUFFER					= 5,	/* double buffering supported */
		STEREO							= 6,	/* stereo buffering supported */
		AUX_BUFFERS						= 7,	/* number of aux buffers */
		RED_SIZE						= 8,	/* number of red component bits */
		GREEN_SIZE						= 9,	/* number of green component bits */
		BLUE_SIZE						= 10,	/* number of blue component bits */
		ALPHA_SIZE						= 11,	/* number of alpha component bits */
		DEPTH_SIZE						= 12,	/* number of depth bits */
		STENCIL_SIZE					= 13,	/* number of stencil bits */
		ACCUM_RED_SIZE					= 14,	/* number of red accum bits */
		ACCUM_GREEN_SIZE				= 15,	/* number of green accum bits */
		ACCUM_BLUE_SIZE					= 16,	/* number of blue accum bits */
		ACCUM_ALPHA_SIZE				= 17,	/* number of alpha accum bits */

		WINDOW_BIT                     = 0x00000001,
		PIXMAP_BIT                     = 0x00000002,
		PBUFFER_BIT                    = 0x00000004,
		RGBA_BIT                       = 0x00000001,
		COLOR_INDEX_BIT                = 0x00000002,
		PBUFFER_CLOBBER_MASK           = 0x08000000,
		FRONT_LEFT_BUFFER_BIT          = 0x00000001,
		FRONT_RIGHT_BUFFER_BIT         = 0x00000002,
		BACK_LEFT_BUFFER_BIT           = 0x00000004,
		BACK_RIGHT_BUFFER_BIT          = 0x00000008,
		AUX_BUFFERS_BIT                = 0x00000010,
		DEPTH_BUFFER_BIT               = 0x00000020,
		STENCIL_BUFFER_BIT             = 0x00000040,
		ACCUM_BUFFER_BIT               = 0x00000080,
		CONFIG_CAVEAT                  = 0x20,
		X_VISUAL_TYPE                  = 0x22,
		TRANSPARENT_TYPE               = 0x23,
		TRANSPARENT_INDEX_VALUE        = 0x24,
		TRANSPARENT_RED_VALUE          = 0x25,
		TRANSPARENT_GREEN_VALUE        = 0x26,
		TRANSPARENT_BLUE_VALUE         = 0x27,
		TRANSPARENT_ALPHA_VALUE        = 0x28,
		//DONT_CARE                      = 0xFFFFFFFF,
		NONE                           = 0x8000,
		SLOW_CONFIG                    = 0x8001,
		TRUE_COLOR                     = 0x8002,
		DIRECT_COLOR                   = 0x8003,
		PSEUDO_COLOR                   = 0x8004,
		STATIC_COLOR                   = 0x8005,
		GRAY_SCALE                     = 0x8006,
		STATIC_GRAY                    = 0x8007,
		TRANSPARENT_RGB                = 0x8008,
		TRANSPARENT_INDEX              = 0x8009,
		VISUAL_ID                      = 0x800B,
		SCREEN                         = 0x800C,
		NON_CONFORMANT_CONFIG          = 0x800D,
		DRAWABLE_TYPE                  = 0x8010,
		RENDER_TYPE                    = 0x8011,
		X_RENDERABLE                   = 0x8012,
		FBCONFIG_ID                    = 0x8013,
		RGBA_TYPE                      = 0x8014,
		COLOR_INDEX_TYPE               = 0x8015,
		MAX_PBUFFER_WIDTH              = 0x8016,
		MAX_PBUFFER_HEIGHT             = 0x8017,
		MAX_PBUFFER_PIXELS             = 0x8018,
		PRESERVED_CONTENTS             = 0x801B,
		LARGEST_PBUFFER                = 0x801C,
		WIDTH                          = 0x801D,
		HEIGHT                         = 0x801E,
		EVENT_MASK                     = 0x801F,
		DAMAGED                        = 0x8020,
		SAVED                          = 0x8021,
		WINDOW                         = 0x8022,
		PBUFFER                        = 0x8023,
		PBUFFER_HEIGHT                 = 0x8040,
		PBUFFER_WIDTH                  = 0x8041,

		OPTIMAL_PBUFFER_WIDTH_SGIX	= 0x8019,
		OPTIMAL_PBUFFER_HEIGHT_SGIX	= 0x801A,


		BAD_SCREEN		= 1,	/* screen # is bad */
		BAD_ATTRIBUTE	= 2,	/* attribute to get is bad */
		NO_EXTENSION	= 3,	/* no glx extension on server */
		BAD_VISUAL		= 4,	/* visual # not known by GLX */
		BAD_CONTEXT		= 5,	/* returned only by import_context EXT? */
		BAD_VALUE		= 6,	/* returned only by glXSwapIntervalSGI? */
		BAD_ENUM		= 7,	/* unused? */

		X_VISUAL_TYPE_EXT	= 0x22,	
		TRANSPARENT_TYPE_EXT = 0x23,	/* visual_info extension */
		TRANSPARENT_INDEX_VALUE_EXT = 0x24,	/* visual_info extension */
		TRANSPARENT_RED_VALUE_EXT	= 0x25,	/* visual_info extension */
		TRANSPARENT_GREEN_VALUE_EXT = 0x26,	/* visual_info extension */
		TRANSPARENT_BLUE_VALUE_EXT	= 0x27,	/* visual_info extension */
		TRANSPARENT_ALPHA_VALUE_EXT = 0x28,	
		TRUE_COLOR_EXT	= 0x8002,
		DIRECT_COLOR_EXT	= 0x8003,
		PSEUDO_COLOR_EXT	= 0x8004,
		STATIC_COLOR_EXT	= 0x8005,
		GRAY_SCALE_EXT	= 0x8006,
		STATIC_GRAY_EXT	= 0x8007,
		NONE_EXT		= 0x8000,
		TRANSPARENT_RGB_EXT		= 0x8008,
		TRANSPARENT_INDEX_EXT	= 0x8009,
		VISUAL_CAVEAT_EXT		= 0x20,  
		SLOW_VISUAL_EXT		= 0x8001,
		NON_CONFORMANT_VISUAL_EXT	= 0x800D,
		SWAP_METHOD_OML                = 0x8060,
		SWAP_EXCHANGE_OML              = 0x8061,
		SWAP_COPY_OML                  = 0x8062,
		SWAP_UNDEFINED_OML             = 0x8063,
		VISUAL_SELECT_GROUP_SGIX	= 0x8028,	
		VENDOR		= 0x1,
		VERSION		= 0x2,
		EXTENSIONS		= 0x3,
		SHARE_CONTEXT_EXT	= 0x800A,	/* id of share context */
		VISUAL_ID_EXT	= 0x800B,	/* id of context's visual */
		SCREEN_EXT		= 0x800C,	/* screen number */
		BIND_TO_TEXTURE_RGB_EXT        = 0x20D0,
		BIND_TO_TEXTURE_RGBA_EXT       = 0x20D1,
		BIND_TO_MIPMAP_TEXTURE_EXT     = 0x20D2,
		BIND_TO_TEXTURE_TARGETS_EXT    = 0x20D3,
		Y_INVERTED_EXT                 = 0x20D4,

		TEXTURE_FORMAT_EXT             = 0x20D5,
		TEXTURE_TARGET_EXT             = 0x20D6,
		MIPMAP_TEXTURE_EXT             = 0x20D7,

		TEXTURE_FORMAT_NONE_EXT        = 0x20D8,
		TEXTURE_FORMAT_RGB_EXT         = 0x20D9,
		TEXTURE_FORMAT_RGBA_EXT        = 0x20DA,

		TEXTURE_1D_BIT_EXT             = 0x00000001,
		TEXTURE_2D_BIT_EXT             = 0x00000002,
		TEXTURE_RECTANGLE_BIT_EXT      = 0x00000004,

		TEXTURE_1D_EXT                 = 0x20DB,
		TEXTURE_2D_EXT                 = 0x20DC,
		TEXTURE_RECTANGLE_EXT          = 0x20DD,

		FRONT_LEFT_EXT                 = 0x20DE,
		FRONT_RIGHT_EXT                = 0x20DF,
		BACK_LEFT_EXT                  = 0x20E0,
		BACK_RIGHT_EXT                 = 0x20E1,
		FRONT_EXT                      = FRONT_LEFT_EXT,
		BACK_EXT                       = BACK_LEFT_EXT,
		AUX0_EXT                       = 0x20E2,
		AUX1_EXT                       = 0x20E3, 
		AUX2_EXT                       = 0x20E4, 
		AUX3_EXT                       = 0x20E5, 
		AUX4_EXT                       = 0x20E6, 
		AUX5_EXT                       = 0x20E7, 
		AUX6_EXT                       = 0x20E8,
		AUX7_EXT                       = 0x20E9, 
		AUX8_EXT                       = 0x20EA, 
		AUX9_EXT                       = 0x20EB,
		SAMPLE_BUFFERS            		= 100000,
		SAMPLES                        = 100001,
	}
}


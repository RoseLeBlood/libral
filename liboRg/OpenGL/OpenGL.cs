//
//  OpenGL.cs
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
using System.Reflection;
using System.Runtime.InteropServices;

namespace liboRg.OpenGL
{
	public partial class gl
	{
		public const string DllName = "libGL.so";

		public enum boolean : byte
		{
			TRUE = 1,
			FALSE = 0
		}

		static HashSet<string> m_lstExtensions;

		public static int versionMajor = 0;
		public static int versionMinor = 0;

		public static  bool IsVERSION_1_0 = false;
		public static  bool IsVERSION_1_1 = false;
		public static  bool IsVERSION_1_2 = false;
		public static  bool IsVERSION_1_3 = false;
		public static  bool IsVERSION_1_4 = false;
		public static  bool IsVERSION_1_5 = false;
		public static  bool IsVERSION_2_0 = false;
		public static  bool IsVERSION_2_1 = false;
		public static  bool IsVERSION_3_0 = false;
		public static  bool IsVERSION_3_1 = false;
		public static  bool IsVERSION_3_2 = false;
		public static  bool IsVERSION_3_3 = false;
		public static  bool IsVERSION_4_0 = false;
		public static  bool IsVERSION_4_1 = false;
		public static  bool IsVERSION_4_2 = false;
		public static  bool IsVERSION_4_3 = false;
		public static  bool IsARB_multitexture = false;
		public static  bool IsARB_transpose_matrix = false;
		public static  bool IsARB_multisample = false;
		public static  bool IsARB_texture_env_add = false;
		public static  bool IsARB_texture_cube_map = false;
		public static  bool IsARB_texture_compression = false;
		public static  bool IsARB_texture_border_clamp = false;
		public static  bool IsARB_point_parameters = false;
		public static  bool IsARB_vertex_blend = false;
		public static  bool IsARB_matrix_palette = false;
		public static  bool IsARB_texture_env_combine = false;
		public static  bool IsARB_texture_env_crossbar = false;
		public static  bool IsARB_texture_env_dot3 = false;
		public static  bool IsARB_texture_mirrored_repeat = false;
		public static  bool IsARB_depth_texture = false;
		public static  bool IsARB_shadow = false;
		public static  bool IsARB_shadow_ambient = false;
		public static  bool IsARB_window_pos = false;
		public static  bool IsARB_vertex_program = false;
		public static  bool IsARB_fragment_program = false;
		public static  bool IsARB_vertex_buffer_object = false;
		public static  bool IsARB_occlusion_query = false;
		public static  bool IsARB_shader_objects = false;
		public static  bool IsARB_vertex_shader = false;
		public static  bool IsARB_fragment_shader = false;
		public static  bool IsARB_shading_language_100 = false;
		public static  bool IsARB_texture_non_power_of_two = false;
		public static  bool IsARB_point_sprite = false;
		public static  bool IsARB_fragment_program_shadow = false;
		public static  bool IsARB_draw_buffers = false;
		public static  bool IsARB_texture_rectangle = false;
		public static  bool IsARB_color_buffer_float = false;
		public static  bool IsARB_half_float_pixel = false;
		public static  bool IsARB_texture_float = false;
		public static  bool IsARB_pixel_buffer_object = false;
		public static  bool IsARB_depth_buffer_float = false;
		public static  bool IsARB_draw_instanced = false;
		public static  bool IsARB_framebuffer_object = false;
		public static  bool IsARB_framebuffer_sRGB = false;
		public static  bool IsARB_geometry_shader4 = false;
		public static  bool IsARB_half_float_vertex = false;
		public static  bool IsARB_instanced_arrays = false;
		public static  bool IsARB_map_buffer_range = false;
		public static  bool IsARB_texture_buffer_object = false;
		public static  bool IsARB_texture_compression_rgtc = false;
		public static  bool IsARB_texture_rg = false;
		public static  bool IsARB_vertex_array_object = false;
		public static  bool IsARB_uniform_buffer_object = false;
		public static  bool IsARB_compatibility = false;
		public static  bool IsARB_copy_buffer = false;
		public static  bool IsARB_shader_texture_lod = false;
		public static  bool IsARB_depth_clamp = false;
		public static  bool IsARB_draw_elements_base_vertex = false;
		public static  bool IsARB_fragment_coord_conventions = false;
		public static  bool IsARB_provoking_vertex = false;
		public static  bool IsARB_seamless_cube_map = false;
		public static  bool IsARB_sync = false;
		public static  bool IsARB_texture_multisample = false;
		public static  bool IsARB_vertex_array_bgra = false;
		public static  bool IsARB_draw_buffers_blend = false;
		public static  bool IsARB_sample_shading = false;
		public static  bool IsARB_texture_cube_map_array = false;
		public static  bool IsARB_texture_gather = false;
		public static  bool IsARB_texture_query_lod = false;
		public static  bool IsARB_shading_language_include = false;
		public static  bool IsARB_texture_compression_bptc = false;
		public static  bool IsARB_blend_func_extended = false;
		public static  bool IsARB_explicit_attrib_location = false;
		public static  bool IsARB_occlusion_query2 = false;
		public static  bool IsARB_sampler_objects = false;
		public static  bool IsARB_shader_bit_encoding = false;
		public static  bool IsARB_texture_rgb10_a2ui = false;
		public static  bool IsARB_texture_swizzle = false;
		public static  bool IsARB_timer_query = false;
		public static  bool IsARB_vertex_type_2_10_10_10_rev = false;
		public static  bool IsARB_draw_indirect = false;
		public static  bool IsARB_gpu_shader5 = false;
		public static  bool IsARB_gpu_shader_fp64 = false;
		public static  bool IsARB_shader_subroutine = false;
		public static  bool IsARB_tessellation_shader = false;
		public static  bool IsARB_texture_buffer_object_rgb32 = false;
		public static  bool IsARB_transform_feedback2 = false;
		public static  bool IsARB_transform_feedback3 = false;
		public static  bool IsARB_ES2_compatibility = false;
		public static  bool IsARB_get_program_binary = false;
		public static  bool IsARB_separate_shader_objects = false;
		public static  bool IsARB_vertex_attrib_64bit = false;
		public static  bool IsARB_viewport_array = false;
		public static  bool IsARB_cl_event = false;
		public static  bool IsARB_debug_output = false;
		public static  bool IsARB_robustness = false;
		public static  bool IsARB_shader_stencil_export = false;
		public static  bool IsARB_base_instance = false;
		public static  bool IsARB_shading_language_420pack = false;
		public static  bool IsARB_transform_feedback_instanced = false;
		public static  bool IsARB_compressed_texture_pixel_storage = false;
		public static  bool IsARB_conservative_depth = false;
		public static  bool IsARB_internalformat_query = false;
		public static  bool IsARB_map_buffer_alignment = false;
		public static  bool IsARB_shader_atomic_counters = false;
		public static  bool IsARB_shader_image_load_store = false;
		public static  bool IsARB_shading_language_packing = false;
		public static  bool IsARB_texture_storage = false;
		public static  bool IsKHR_texture_compression_astc_ldr = false;
		public static  bool IsKHR_debug = false;
		public static  bool IsARB_arrays_of_arrays = false;
		public static  bool IsARB_clear_buffer_object = false;
		public static  bool IsARB_compute_shader = false;
		public static  bool IsARB_copy_image = false;
		public static  bool IsARB_texture_view = false;
		public static  bool IsARB_vertex_attrib_binding = false;
		public static  bool IsARB_robustness_isolation = false;
		public static  bool IsARB_ES3_compatibility = false;
		public static  bool IsARB_explicit_uniform_location = false;
		public static  bool IsARB_fragment_layer_viewport = false;
		public static  bool IsARB_framebuffer_no_attachments = false;
		public static  bool IsARB_internalformat_query2 = false;
		public static  bool IsARB_invalidate_subdata = false;
		public static  bool IsARB_multi_draw_indirect = false;
		public static  bool IsARB_program_interface_query = false;
		public static  bool IsARB_robust_buffer_access_behavior = false;
		public static  bool IsARB_shader_image_size = false;
		public static  bool IsARB_shader_storage_buffer_object = false;
		public static  bool IsARB_stencil_texturing = false;
		public static  bool IsARB_texture_buffer_range = false;
		public static  bool IsARB_texture_query_levels = false;
		public static  bool IsARB_texture_storage_multisample = false;
		public static  bool IsEXT_abgr = false;
		public static  bool IsEXT_blend_color = false;
		public static  bool IsEXT_polygon_offset = false;
		public static  bool IsEXT_texture = false;
		public static  bool IsEXT_texture3D = false;
		public static  bool IsSGIS_texture_filter4 = false;
		public static  bool IsEXT_subtexture = false;
		public static  bool IsEXT_copy_texture = false;
		public static  bool IsEXT_histogram = false;
		public static  bool IsEXT_convolution = false;
		public static  bool IsSGI_color_matrix = false;
		public static  bool IsSGI_color_table = false;
		public static  bool IsSGIX_pixel_texture = false;
		public static  bool IsSGIS_pixel_texture = false;
		public static  bool IsSGIS_texture4D = false;
		public static  bool IsSGI_texture_color_table = false;
		public static  bool IsEXT_cmyka = false;
		public static  bool IsEXT_texture_object = false;
		public static  bool IsSGIS_detail_texture = false;
		public static  bool IsSGIS_sharpen_texture = false;
		public static  bool IsEXT_packed_pixels = false;
		public static  bool IsSGIS_texture_lod = false;
		public static  bool IsSGIS_multisample = false;
		public static  bool IsEXT_rescale_normal = false;
		public static  bool IsEXT_vertex_array = false;
		public static  bool IsEXT_misc_attribute = false;
		public static  bool IsSGIS_generate_mipmap = false;
		public static  bool IsSGIX_clipmap = false;
		public static  bool IsSGIX_shadow = false;
		public static  bool IsSGIS_texture_edge_clamp = false;
		public static  bool IsSGIS_texture_border_clamp = false;
		public static  bool IsEXT_blend_minmax = false;
		public static  bool IsEXT_blend_subtract = false;
		public static  bool IsEXT_blend_logic_op = false;
		public static  bool IsSGIX_interlace = false;
		public static  bool IsSGIX_pixel_tiles = false;
		public static  bool IsSGIX_texture_select = false;
		public static  bool IsSGIX_sprite = false;
		public static  bool IsSGIX_texture_multi_buffer = false;
		public static  bool IsEXT_point_parameters = false;
		public static  bool IsSGIS_point_parameters = false;
		public static  bool IsSGIX_instruments = false;
		public static  bool IsSGIX_texture_scale_bias = false;
		public static  bool IsSGIX_framezoom = false;
		public static  bool IsSGIX_tag_sample_buffer = false;
		public static  bool IsSGIX_polynomial_ffd = false;
		public static  bool IsSGIX_reference_plane = false;
		public static  bool IsSGIX_flush_raster = false;
		public static  bool IsSGIX_depth_texture = false;
		public static  bool IsSGIS_fog_function = false;
		public static  bool IsSGIX_fog_offset = false;
		public static  bool IsHP_image_transform = false;
		public static  bool IsHP_convolution_border_modes = false;
		public static  bool IsSGIX_texture_add_env = false;
		public static  bool IsEXT_color_subtable = false;
		public static  bool IsPGI_vertex_hints = false;
		public static  bool IsPGI_misc_hints = false;
		public static  bool IsEXT_paletted_texture = false;
		public static  bool IsEXT_clip_volume_hint = false;
		public static  bool IsSGIX_list_priority = false;
		public static  bool IsSGIX_ir_instrument1 = false;
		public static  bool IsSGIX_calligraphic_fragment = false;
		public static  bool IsSGIX_texture_lod_bias = false;
		public static  bool IsSGIX_shadow_ambient = false;
		public static  bool IsEXT_index_texture = false;
		public static  bool IsEXT_index_material = false;
		public static  bool IsEXT_index_func = false;
		public static  bool IsEXT_index_array_formats = false;
		public static  bool IsEXT_compiled_vertex_array = false;
		public static  bool IsEXT_cull_vertex = false;
		public static  bool IsSGIX_ycrcb = false;
		public static  bool IsSGIX_fragment_lighting = false;
		public static  bool IsIBM_rasterpos_clip = false;
		public static  bool IsHP_texture_lighting = false;
		public static  bool IsEXT_draw_range_elements = false;
		public static  bool IsWIN_phong_shading = false;
		public static  bool IsWIN_specular_fog = false;
		public static  bool IsEXT_light_texture = false;
		public static  bool IsSGIX_blend_alpha_minmax = false;
		public static  bool IsEXT_bgra = false;
		public static  bool IsSGIX_async = false;
		public static  bool IsSGIX_async_pixel = false;
		public static  bool IsSGIX_async_histogram = false;
		public static  bool IsINTEL_parallel_arrays = false;
		public static  bool IsHP_occlusion_test = false;
		public static  bool IsEXT_pixel_transform = false;
		public static  bool IsEXT_pixel_transform_color_table = false;
		public static  bool IsEXT_shared_texture_palette = false;
		public static  bool IsEXT_separate_specular_color = false;
		public static  bool IsEXT_secondary_color = false;
		public static  bool IsEXT_texture_perturb_normal = false;
		public static  bool IsEXT_multi_draw_arrays = false;
		public static  bool IsEXT_fog_coord = false;
		public static  bool IsREND_screen_coordinates = false;
		public static  bool IsEXT_coordinate_frame = false;
		public static  bool IsEXT_texture_env_combine = false;
		public static  bool IsAPPLE_specular_vector = false;
		public static  bool IsAPPLE_transform_hint = false;
		public static  bool IsSGIX_fog_scale = false;
		public static  bool IsSUNX_constant_data = false;
		public static  bool IsSUN_global_alpha = false;
		public static  bool IsSUN_triangle_list = false;
		public static  bool IsSUN_vertex = false;
		public static  bool IsEXT_blend_func_separate = false;
		public static  bool IsINGR_blend_func_separate = false;
		public static  bool IsINGR_color_clamp = false;
		public static  bool IsINGR_interlace_read = false;
		public static  bool IsEXT_stencil_wrap = false;
		public static  bool IsEXT_422_pixels = false;
		public static  bool IsNV_texgen_reflection = false;
		public static  bool IsSUN_convolution_border_modes = false;
		public static  bool IsEXT_texture_env_add = false;
		public static  bool IsEXT_texture_lod_bias = false;
		public static  bool IsEXT_texture_filter_anisotropic = false;
		public static  bool IsEXT_vertex_weighting = false;
		public static  bool IsNV_light_max_exponent = false;
		public static  bool IsNV_vertex_array_range = false;
		public static  bool IsNV_register_combiners = false;
		public static  bool IsNV_fog_distance = false;
		public static  bool IsNV_texgen_emboss = false;
		public static  bool IsNV_blend_square = false;
		public static  bool IsNV_texture_env_combine4 = false;
		public static  bool IsMESA_resize_buffers = false;
		public static  bool IsMESA_window_pos = false;
		public static  bool IsIBM_cull_vertex = false;
		public static  bool IsIBM_multimode_draw_arrays = false;
		public static  bool IsIBM_vertex_array_lists = false;
		public static  bool IsSGIX_subsample = false;
		public static  bool IsSGIX_ycrcba = false;
		public static  bool IsSGIX_ycrcb_subsample = false;
		public static  bool IsSGIX_depth_pass_instrument = false;
		public static  bool Is3DFX_texture_compression_FXT1 = false;
		public static  bool Is3DFX_multisample = false;
		public static  bool Is3DFX_tbuffer = false;
		public static  bool IsEXT_multisample = false;
		public static  bool IsSGIX_vertex_preclip = false;
		public static  bool IsSGIX_convolution_accuracy = false;
		public static  bool IsSGIX_resample = false;
		public static  bool IsSGIS_point_line_texgen = false;
		public static  bool IsSGIS_texture_color_mask = false;
		public static  bool IsSGIX_igloo_interface = false;
		public static  bool IsEXT_texture_env_dot3 = false;
		public static  bool IsATI_texture_mirror_once = false;
		public static  bool IsNV_fence = false;
		public static  bool IsNV_evaluators = false;
		public static  bool IsNV_packed_depth_stencil = false;
		public static  bool IsNV_register_combiners2 = false;
		public static  bool IsNV_texture_compression_vtc = false;
		public static  bool IsNV_texture_rectangle = false;
		public static  bool IsNV_texture_shader = false;
		public static  bool IsNV_texture_shader2 = false;
		public static  bool IsNV_vertex_array_range2 = false;
		public static  bool IsNV_vertex_program = false;
		public static  bool IsSGIX_texture_coordinate_clamp = false;
		public static  bool IsSGIX_scalebias_hint = false;
		public static  bool IsOML_interlace = false;
		public static  bool IsOML_subsample = false;
		public static  bool IsOML_resample = false;
		public static  bool IsNV_copy_depth_to_color = false;
		public static  bool IsATI_envmap_bumpmap = false;
		public static  bool IsATI_fragment_shader = false;
		public static  bool IsATI_pn_triangles = false;
		public static  bool IsATI_vertex_array_object = false;
		public static  bool IsEXT_vertex_shader = false;
		public static  bool IsATI_vertex_streams = false;
		public static  bool IsATI_element_array = false;
		public static  bool IsSUN_mesh_array = false;
		public static  bool IsSUN_slice_accum = false;
		public static  bool IsNV_multisample_filter_hint = false;
		public static  bool IsNV_depth_clamp = false;
		public static  bool IsNV_occlusion_query = false;
		public static  bool IsNV_point_sprite = false;
		public static  bool IsNV_texture_shader3 = false;
		public static  bool IsNV_vertex_program1_1 = false;
		public static  bool IsEXT_shadow_funcs = false;
		public static  bool IsEXT_stencil_two_side = false;
		public static  bool IsATI_text_fragment_shader = false;
		public static  bool IsAPPLE_client_storage = false;
		public static  bool IsAPPLE_element_array = false;
		public static  bool IsAPPLE_fence = false;
		public static  bool IsAPPLE_vertex_array_object = false;
		public static  bool IsAPPLE_vertex_array_range = false;
		public static  bool IsAPPLE_ycbcr_422 = false;
		public static  bool IsS3_s3tc = false;
		public static  bool IsATI_draw_buffers = false;
		public static  bool IsATI_pixel_format_float = false;
		public static  bool IsATI_texture_env_combine3 = false;
		public static  bool IsATI_texture_float = false;
		public static  bool IsNV_float_buffer = false;
		public static  bool IsNV_fragment_program = false;
		public static  bool IsNV_half_float = false;
		public static  bool IsNV_pixel_data_range = false;
		public static  bool IsNV_primitive_restart = false;
		public static  bool IsNV_texture_expand_normal = false;
		public static  bool IsNV_vertex_program2 = false;
		public static  bool IsATI_map_object_buffer = false;
		public static  bool IsATI_separate_stencil = false;
		public static  bool IsATI_vertex_attrib_array_object = false;
		public static  bool IsOES_read_format = false;
		public static  bool IsEXT_depth_bounds_test = false;
		public static  bool IsEXT_texture_mirror_clamp = false;
		public static  bool IsEXT_blend_equation_separate = false;
		public static  bool IsMESA_pack_invert = false;
		public static  bool IsMESA_ycbcr_texture = false;
		public static  bool IsEXT_pixel_buffer_object = false;
		public static  bool IsNV_fragment_program_option = false;
		public static  bool IsNV_fragment_program2 = false;
		public static  bool IsNV_vertex_program2_option = false;
		public static  bool IsNV_vertex_program3 = false;
		public static  bool IsEXT_framebuffer_object = false;
		public static  bool IsGREMEDY_string_marker = false;
		public static  bool IsEXT_packed_depth_stencil = false;
		public static  bool IsEXT_stencil_clear_tag = false;
		public static  bool IsEXT_texture_sRGB = false;
		public static  bool IsEXT_framebuffer_blit = false;
		public static  bool IsEXT_framebuffer_multisample = false;
		public static  bool IsMESAX_texture_stack = false;
		public static  bool IsEXT_timer_query = false;
		public static  bool IsEXT_gpu_program_parameters = false;
		public static  bool IsAPPLE_flush_buffer_range = false;
		public static  bool IsNV_gpu_program4 = false;
		public static  bool IsNV_geometry_program4 = false;
		public static  bool IsEXT_geometry_shader4 = false;
		public static  bool IsNV_vertex_program4 = false;
		public static  bool IsEXT_gpu_shader4 = false;
		public static  bool IsEXT_draw_instanced = false;
		public static  bool IsEXT_packed_float = false;
		public static  bool IsEXT_texture_array = false;
		public static  bool IsEXT_texture_buffer_object = false;
		public static  bool IsEXT_texture_compression_latc = false;
		public static  bool IsEXT_texture_compression_rgtc = false;
		public static  bool IsEXT_texture_shared_exponent = false;
		public static  bool IsNV_depth_buffer_float = false;
		public static  bool IsNV_fragment_program4 = false;
		public static  bool IsNV_framebuffer_multisample_coverage = false;
		public static  bool IsEXT_framebuffer_sRGB = false;
		public static  bool IsNV_geometry_shader4 = false;
		public static  bool IsNV_parameter_buffer_object = false;
		public static  bool IsEXT_draw_buffers2 = false;
		public static  bool IsNV_transform_feedback = false;
		public static  bool IsEXT_bindable_uniform = false;
		public static  bool IsEXT_texture_integer = false;
		public static  bool IsGREMEDY_frame_terminator = false;
		public static  bool IsNV_conditional_render = false;
		public static  bool IsNV_present_video = false;
		public static  bool IsEXT_transform_feedback = false;
		public static  bool IsEXT_direct_state_access = false;
		public static  bool IsEXT_vertex_array_bgra = false;
		public static  bool IsEXT_texture_swizzle = false;
		public static  bool IsNV_explicit_multisample = false;
		public static  bool IsNV_transform_feedback2 = false;
		public static  bool IsATI_meminfo = false;
		public static  bool IsAMD_performance_monitor = false;
		public static  bool IsAMD_texture_texture4 = false;
		public static  bool IsAMD_vertex_shader_tesselator = false;
		public static  bool IsEXT_provoking_vertex = false;
		public static  bool IsEXT_texture_snorm = false;
		public static  bool IsAMD_draw_buffers_blend = false;
		public static  bool IsAPPLE_texture_range = false;
		public static  bool IsAPPLE_float_pixels = false;
		public static  bool IsAPPLE_vertex_program_evaluators = false;
		public static  bool IsAPPLE_aux_depth_stencil = false;
		public static  bool IsAPPLE_object_purgeable = false;
		public static  bool IsAPPLE_row_bytes = false;
		public static  bool IsAPPLE_rgb_422 = false;
		public static  bool IsNV_video_capture = false;
		public static  bool IsNV_copy_image = false;
		public static  bool IsEXT_separate_shader_objects = false;
		public static  bool IsNV_parameter_buffer_object2 = false;
		public static  bool IsNV_shader_buffer_load = false;
		public static  bool IsNV_vertex_buffer_unified_memory = false;
		public static  bool IsNV_texture_barrier = false;
		public static  bool IsAMD_shader_stencil_export = false;
		public static  bool IsAMD_seamless_cubemap_per_texture = false;
		public static  bool IsAMD_conservative_depth = false;
		public static  bool IsEXT_shader_image_load_store = false;
		public static  bool IsEXT_vertex_attrib_64bit = false;
		public static  bool IsNV_gpu_program5 = false;
		public static  bool IsNV_gpu_shader5 = false;
		public static  bool IsNV_shader_buffer_store = false;
		public static  bool IsNV_tessellation_program5 = false;
		public static  bool IsNV_vertex_attrib_integer_64bit = false;
		public static  bool IsNV_multisample_coverage = false;
		public static  bool IsAMD_name_gen_delete = false;
		public static  bool IsAMD_debug_output = false;
		public static  bool IsNV_vdpau_interop = false;
		public static  bool IsAMD_transform_feedback3_lines_triangles = false;
		public static  bool IsAMD_depth_clamp_separate = false;
		public static  bool IsEXT_texture_sRGB_decode = false;
		public static  bool IsNV_texture_multisample = false;
		public static  bool IsAMD_blend_minmax_factor = false;
		public static  bool IsAMD_sample_positions = false;
		public static  bool IsEXT_x11_sync_object = false;
		public static  bool IsAMD_multi_draw_indirect = false;
		public static  bool IsEXT_framebuffer_multisample_blit_scaled = false;
		public static  bool IsNV_path_rendering = false;
		public static  bool IsAMD_pinned_memory = false;
		public static  bool IsAMD_stencil_operation_extended = false;
		public static  bool IsAMD_vertex_shader_viewport_index = false;
		public static  bool IsAMD_vertex_shader_layer = false;
		public static  bool IsNV_bindless_texture = false;
		public static  bool IsNV_shader_atomic_float = false;
		public static  bool IsAMD_query_buffer_object = false;


		public static bool IsExtensionSupported(string extension) 
		{
			return m_lstExtensions.Contains(extension);
		}

		[DllImport(DllName)]
		public static extern void glAccum(uint op, float value);
		[DllImport(DllName)]
		public static extern void glAlphaFunc(uint func, float reference);
		[DllImport(DllName)]
		public static extern boolean glAreTexturesResident(int n, uint[] textures, boolean[] residences);
		[DllImport(DllName)]
		public static extern void glArrayElement(int i);
		[DllImport(DllName)]
		public static extern void glBegin(uint mode);
		[DllImport(DllName)]
		public static extern void glBindTexture(uint target, uint texture);
		[DllImport(DllName)]
		public static unsafe extern void glBitmap(int width, int height, float xorig, float yorig, float xmove, float ymove, byte[] bitmap);
		[DllImport(DllName)]
		public static extern void glBlendFunc(uint sfactor, uint dfactor);
		[DllImport(DllName)]
		public static extern void glCallList(uint list);
		[DllImport(DllName)]
		public static unsafe extern void glCallLists(int n, uint type, void* lists);
		[DllImport(DllName)]
		public static extern void glBindFramebuffer(uint target, uint framebuffer);
		[DllImport(DllName)]
		public static extern void glClear(int mask);
		[DllImport(DllName)]
		public static extern void glClearAccum(float red, float green, float blue, float alpha);
		[DllImport(DllName)]
		public static extern void glClearColor(float red, float green, float blue, float alpha);
		[DllImport(DllName)]
		public static extern void glClearDepth(double depth);
		[DllImport(DllName)]
		public static extern void glClearIndex(float c);
		[DllImport(DllName)]
		public static extern void glClearStencil(int s);
		[DllImport(DllName)]
		public static extern void glClipPlane(uint plane, double[] equation);
		[DllImport(DllName)]
		public static extern void glColor3b(sbyte red, sbyte green, sbyte blue);
		[DllImport(DllName)]
		public static extern void glColor3bv(sbyte[] v);
		[DllImport(DllName)]
		public static extern void glColor3d(double red, double green, double blue);
		[DllImport(DllName)]
		public static extern void glColor3dv(double[] v);
		[DllImport(DllName)]
		public static extern void glColor3f(float red, float green, float blue);
		[DllImport(DllName)]
		public static extern void glColor3fv(float[] v);
		[DllImport(DllName)]
		public static extern void glColor3i(int red, int green, int blue);
		[DllImport(DllName)]
		public static extern void glColor3iv(int[] v);
		[DllImport(DllName)]
		public static extern void glColor3s(short red, short green, short blue);
		[DllImport(DllName)]
		public static extern void glColor3sv(short[] v);
		[DllImport(DllName)]
		public static extern void glColor3ub(byte red, byte green, byte blue);
		[DllImport(DllName)]
		public static extern void glColor3ubv(byte[] v);
		[DllImport(DllName)]
		public static extern void glColor3ui(uint red, uint green, uint blue);
		[DllImport(DllName)]
		public static extern void glColor3uiv(uint[] v);
		[DllImport(DllName)]
		public static extern void glColor3us(ushort red, ushort green, ushort blue);
		[DllImport(DllName)]
		public static extern void glColor3usv(ushort[] v);
		[DllImport(DllName)]
		public static extern void glColor4b(sbyte red, sbyte green, sbyte blue, sbyte alpha);
		[DllImport(DllName)]
		public static extern void glColor4bv(sbyte[] v);
		[DllImport(DllName)]
		public static extern void glColor4d(double red, double green, double blue, double alpha);
		[DllImport(DllName)]
		public static extern void glColor4dv(double[] v);
		[DllImport(DllName)]
		public static extern void glColor4f(float red, float green, float blue, float alpha);
		[DllImport(DllName)]
		public static extern void glColor4fv(float[] v);
		[DllImport(DllName)]
		public static extern void glColor4i(int red, int green, int blue, int alpha);
		[DllImport(DllName)]
		public static extern void glColor4iv(int[] v);
		[DllImport(DllName)]
		public static extern void glColor4s(short red, short green, short blue, short alpha);
		[DllImport(DllName)]
		public static extern void glColor4sv(short[] v);
		[DllImport(DllName)]
		public static extern void glColor4ub(byte red, byte green, byte blue, byte alpha);
		[DllImport(DllName)]
		public static extern void glColor4ubv(byte[] v);
		[DllImport(DllName)]
		public static extern void glColor4ui(uint red, uint green, uint blue, uint alpha);
		[DllImport(DllName)]
		public static extern void glColor4uiv(uint[] v);
		[DllImport(DllName)]
		public static extern void glColor4us(ushort red, ushort green, ushort blue, ushort alpha);
		[DllImport(DllName)]
		public static extern void glColor4usv(ushort[] v);
		[DllImport(DllName)]
		public static extern void glColorMask(boolean red, boolean green, boolean blue, boolean alpha);
		[DllImport(DllName)]
		public static extern void glColorMaterial(uint face, uint mode);
		[DllImport(DllName)]
		public static extern void glColorPointer(int size, uint type, int stride, IntPtr pointer);
		[DllImport(DllName)]
		public static extern void glCopyPixels(int x, int y, int width, int height, uint type);
		[DllImport(DllName)]
		public static extern void glCopyTexImage1D(uint target, int level, uint internalFormat, int x, int y, int width, int border);
		[DllImport(DllName)]
		public static extern void glCopyTexImage2D(uint target, int level, uint internalFormat, int x, int y, int width, int height, int border);
		[DllImport(DllName)]
		public static extern void glCopyTexSubImage1D(uint target, int level, int xoffset, int x, int y, int width);
		[DllImport(DllName)]
		public static extern void glCopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height);
		[DllImport(DllName)]
		public static extern void glCullFace(uint mode);
		[DllImport(DllName)]
		public static extern void glDeleteLists(uint list, int range);
		[DllImport(DllName)]
		public static extern void glDeleteTextures(int n, uint[] textures);
		[DllImport(DllName)]
		public static extern void glDepthFunc(uint func);
		[DllImport(DllName)]
		public static extern void glDepthMask(boolean flag);
		[DllImport(DllName)]
		public static extern void glDepthRange(double zNear, double zFar);
		[DllImport(DllName)]
		public static extern void glDisable(uint cap);
		[DllImport(DllName)]
		public static extern void glDisableClientState(uint array);
		[DllImport(DllName)]
		public static extern void glDrawArrays(uint mode, int first, int count);
		[DllImport(DllName)]
		public static extern void glDrawBuffer(uint mode);
		[DllImport(DllName)]
		public static extern void glDrawElements(uint mode, int count, uint type, IntPtr indices);
		[DllImport(DllName)]
		public static unsafe extern void glDrawPixels(int width, int height, uint format, uint type, void* pixels);
		[DllImport(DllName)]
		public static extern void glEdgeFlag(boolean flag);
		[DllImport(DllName)]
		public static unsafe extern void glEdgeFlagPointer(int stride, void* pointer);
		[DllImport(DllName)]
		public static extern void glEdgeFlagv(boolean[] flag);
		[DllImport(DllName)]
		public static extern void glEnable(uint cap);
		[DllImport(DllName)]
		public static extern void glEnableClientState(uint array);
		[DllImport(DllName)]
		public static extern void glEnd();
		[DllImport(DllName)]
		public static extern void glEndList();
		[DllImport(DllName)]
		public static extern void glEvalCoord1d(double u);
		[DllImport(DllName)]
		public static extern void glEvalCoord1dv(double[] u);
		[DllImport(DllName)]
		public static extern void glEvalCoord1f(float u);
		[DllImport(DllName)]
		public static extern void glEvalCoord1fv(float[] u);
		[DllImport(DllName)]
		public static extern void glEvalCoord2d(double u, double v);
		[DllImport(DllName)]
		public static extern void glEvalCoord2dv(double[] u);
		[DllImport(DllName)]
		public static extern void glEvalCoord2f(float u, float v);
		[DllImport(DllName)]
		public static extern void glEvalCoord2fv(float[] u);
		[DllImport(DllName)]
		public static extern void glEvalMesh1(uint mode, int i1, int i2);
		[DllImport(DllName)]
		public static extern void glEvalMesh2(uint mode, int i1, int i2, int j1, int j2);
		[DllImport(DllName)]
		public static extern void glEvalPoint1(int i);
		[DllImport(DllName)]
		public static extern void glEvalPoint2(int i, int j);
		[DllImport(DllName)]
		public static extern void glFeedbackBuffer(int size, uint type, float[] buffer);
		[DllImport(DllName)]
		public static extern void glFinish();
		[DllImport(DllName)]
		public static extern void glFlush();
		[DllImport(DllName)]
		public static extern void glFogf(uint pname, float param);
		[DllImport(DllName)]
		public static extern void glFogfv(uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glFogi(uint pname, int param);
		[DllImport(DllName)]
		public static extern void glFogiv(uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glFrontFace(uint mode);
		[DllImport(DllName)]
		public static extern void glFrustum(double left, double right, double bottom, double top, double zNear, double zFar);
		[DllImport(DllName)]
		public static extern uint glGenLists(int range);
		[DllImport(DllName)]
		public static extern void glGenTextures(int n, uint[] textures);
		[DllImport(DllName)]
		public static extern void glGetBooleanv(uint pname, boolean[] parameters);
		[DllImport(DllName)]
		public static extern void glGetClipPlane(uint plane, double[] equation);
		[DllImport(DllName)]
		public static extern void glGetDoublev(uint pname, double[] parameters);
		[DllImport(DllName)]
		public static extern uint glGetError();
		[DllImport(DllName)]
		public static extern void glGetFloatv(uint pname, float[] parameters);
		//[DllImport(DllName)]
		//public static extern void glGetFloatv(uint pname, ref Matrix parameters);
		[DllImport(DllName)]
		public static unsafe extern void glGetFloatv(uint pname, float* parameters);
		[DllImport(DllName)]
		public static extern void glGetIntegerv(uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glGetLightfv(uint light, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glGetLightiv(uint light, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glGetMapdv(uint target, uint query, double[] v);
		[DllImport(DllName)]
		public static extern void glGetMapfv(uint target, uint query, float[] v);
		[DllImport(DllName)]
		public static extern void glGetMapiv(uint target, uint query, int[] v);
		[DllImport(DllName)]
		public static extern void glGetMaterialfv(uint face, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glGetMaterialiv(uint face, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glGetPixelMapfv(uint map, float[] values);
		[DllImport(DllName)]
		public static extern void glGetPixelMapuiv(uint map, uint[] values);
		[DllImport(DllName)]
		public static extern void glGetPixelMapusv(uint map, ushort[] values);
		[DllImport(DllName)]
		public static unsafe extern void glGetPointerv(uint pname, void** par);
		[DllImport(DllName)]
		public static extern void glGetPolygonStipple(byte[] mask);
		[DllImport(DllName, EntryPoint="glGetString")]
		private static extern IntPtr _glGetString(uint name);
		[DllImport(DllName)]
		public static extern void glGetTexEnvfv(uint target, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glGetTexEnviv(uint target, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glGetTexGendv(uint coord, uint pname, double[] parameters);
		[DllImport(DllName)]
		public static extern void glGetTexGenfv(uint coord, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glGetTexGeniv(uint coord, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static unsafe extern void glGetTexImage(uint target, int level, uint format, uint type, void* pixels);
		[DllImport(DllName)]
		public static extern void glGetTexLevelParameterfv(uint target, int level, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glGetTexLevelParameteriv(uint target, int level, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glGetTexParameterfv(uint target, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glGetTexParameteriv(uint target, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glHint(uint target, uint mode);
		[DllImport(DllName)]
		public static extern void glIndexMask(uint mask);
		[DllImport(DllName)]
		public static unsafe extern void glIndexPointer(uint type, int stride, void* pointer);
		[DllImport(DllName)]
		public static extern void glIndexd(double c);
		[DllImport(DllName)]
		public static extern void glIndexdv(double[] c);
		[DllImport(DllName)]
		public static extern void glIndexf(float c);
		[DllImport(DllName)]
		public static extern void glIndexfv(float[] c);
		[DllImport(DllName)]
		public static extern void glIndexi(int c);
		[DllImport(DllName)]
		public static extern void glIndexiv(int[] c);
		[DllImport(DllName)]
		public static extern void glIndexs(short c);
		[DllImport(DllName)]
		public static extern void glIndexsv(short[] c);
		[DllImport(DllName)]
		public static extern void glIndexub(byte c);
		[DllImport(DllName)]
		public static extern void glIndexubv(byte[] c);
		[DllImport(DllName)]
		public static extern void glInitNames();
		[DllImport(DllName)]
		public static unsafe extern void glInterleavedArrays(uint format, int stride, void* pointer);
		[DllImport(DllName)]
		public static extern boolean glIsEnabled(uint cap);
		[DllImport(DllName)]
		public static extern boolean glIsList(uint list);
		[DllImport(DllName)]
		public static extern boolean glIsTexture(uint texture);
		[DllImport(DllName)]
		public static extern void glLightModelf(uint pname, float param);
		[DllImport(DllName)]
		public static extern void glLightModelfv(uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glLightModeli(uint pname, int param);
		[DllImport(DllName)]
		public static extern void glLightModeliv(uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glLightf(uint light, uint pname, float param);
		[DllImport(DllName)]
		public static extern void glLightfv(uint light, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glLighti(uint light, uint pname, int param);
		[DllImport(DllName)]
		public static extern void glLightiv(uint light, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glLineStipple(int factor, ushort pattern);
		[DllImport(DllName)]
		public static extern void glLineWidth(float width);
		[DllImport(DllName)]
		public static extern void glListBase(uint base_index);
		[DllImport(DllName)]
		public static extern void glLoadIdentity();
		[DllImport(DllName)]
		public static extern void glLoadMatrixd(double[] m);
		[DllImport(DllName)]
		public static extern void glLoadMatrixf(float[] m);
		//[DllImport(DllName)]
		//public static extern void glLoadMatrixf(ref Matrix m);
		[DllImport(DllName)]
		public static unsafe extern void glLoadMatrixf(float* m);
		[DllImport(DllName)]
		public static extern void glLoadName(uint name);
		[DllImport(DllName)]
		public static extern void glLogicOp(uint opcode);
		[DllImport(DllName)]
		public static extern void glMap1d(uint target, double u1, double u2, int stride, int order, double[] points);
		[DllImport(DllName)]
		public static extern void glMap1f(uint target, float u1, float u2, int stride, int order, float[] points);
		[DllImport(DllName)]
		public static extern void glMap2d(uint target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, double[] points);
		[DllImport(DllName)]
		public static extern void glMap2f(uint target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, float[] points);
		[DllImport(DllName)]
		public static extern void glMapGrid1d(int un, double u1, double u2);
		[DllImport(DllName)]
		public static extern void glMapGrid1f(int un, float u1, float u2);
		[DllImport(DllName)]
		public static extern void glMapGrid2d(int un, double u1, double u2, int vn, double v1, double v2);
		[DllImport(DllName)]
		public static extern void glMapGrid2f(int un, float u1, float u2, int vn, float v1, float v2);
		[DllImport(DllName)]
		public static extern void glMaterialf(uint face, uint pname, float param);
		[DllImport(DllName)]
		public static extern void glMaterialfv(uint face, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glMateriali(uint face, uint pname, int param);
		[DllImport(DllName)]
		public static extern void glMaterialiv(uint face, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glMatrixMode(uint mode);
		[DllImport(DllName)]
		public static extern void glMultMatrixd(double[] m);
		[DllImport(DllName)]
		public static extern void glMultMatrixf(float[] m);
		[DllImport(DllName)]
		public static extern void glNewList(uint list, uint mode);
		[DllImport(DllName)]
		public static extern void glNormal3b(sbyte nx, sbyte ny, sbyte nz);
		[DllImport(DllName)]
		public static extern void glNormal3bv(sbyte[] v);
		[DllImport(DllName)]
		public static extern void glNormal3d(double nx, double ny, double nz);
		[DllImport(DllName)]
		public static extern void glNormal3dv(double[] v);
		[DllImport(DllName)]
		public static extern void glNormal3f(float nx, float ny, float nz);
		[DllImport(DllName)]
		public static extern void glNormal3fv(float[] v);
		[DllImport(DllName)]
		public static extern void glNormal3i(int nx, int ny, int nz);
		[DllImport(DllName)]
		public static extern void glNormal3iv(int[] v);
		[DllImport(DllName)]
		public static extern void glNormal3s(short nx, short ny, short nz);
		[DllImport(DllName)]
		public static extern void glNormal3sv(short[] v);
		[DllImport(DllName)]
		public static extern void glNormalPointer(uint type, int stride, IntPtr pointer);
		[DllImport(DllName)]
		public static extern void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar);
		[DllImport(DllName)]
		public static extern void glPassThrough(float token);
		[DllImport(DllName)]
		public static extern void glPixelMapfv(uint map, int mapsize, float[] values);
		[DllImport(DllName)]
		public static extern void glPixelMapuiv(uint map, int mapsize, uint[] values);
		[DllImport(DllName)]
		public static extern void glPixelMapusv(uint map, int mapsize, ushort[] values);
		[DllImport(DllName)]
		public static extern void glPixelStoref(uint pname, float param);
		[DllImport(DllName)]
		public static extern void glPixelStorei(uint pname, int param);
		[DllImport(DllName)]
		public static extern void glPixelTransferf(uint pname, float param);
		[DllImport(DllName)]
		public static extern void glPixelTransferi(uint pname, int param);
		[DllImport(DllName)]
		public static extern void glPixelZoom(float xfactor, float yfactor);
		[DllImport(DllName)]
		public static extern void glPointSize(float size);
		[DllImport(DllName)]
		public static extern void glPolygonMode(uint face, uint mode);
		[DllImport(DllName)]
		public static extern void glPolygonOffset(float factor, float units);
		[DllImport(DllName)]
		public static extern void glPolygonStipple(byte[] mask);
		[DllImport(DllName)]
		public static extern void glPopAttrib();
		[DllImport(DllName)]
		public static extern void glPopClientAttrib();
		[DllImport(DllName)]
		public static extern void glPopMatrix();
		[DllImport(DllName)]
		public static extern void glPopName();
		[DllImport(DllName)]
		public static extern void glPrioritizeTextures(int n, uint[] textures, float[] priorities);
		[DllImport(DllName)]
		public static extern void glPushAttrib(uint mask);
		[DllImport(DllName)]
		public static extern void glPushClientAttrib(uint mask);
		[DllImport(DllName)]
		public static extern void glPushMatrix();
		[DllImport(DllName)]
		public static extern void glPushName(uint name);
		[DllImport(DllName)]
		public static extern void glRasterPos2d(double x, double y);
		[DllImport(DllName)]
		public static extern void glRasterPos2dv(double[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos2f(float x, float y);
		[DllImport(DllName)]
		public static extern void glRasterPos2fv(float[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos2i(int x, int y);
		[DllImport(DllName)]
		public static extern void glRasterPos2iv(int[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos2s(short x, short y);
		[DllImport(DllName)]
		public static extern void glRasterPos2sv(short[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos3d(double x, double y, double z);
		[DllImport(DllName)]
		public static extern void glRasterPos3dv(double[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos3f(float x, float y, float z);
		[DllImport(DllName)]
		public static extern void glRasterPos3fv(float[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos3i(int x, int y, int z);
		[DllImport(DllName)]
		public static extern void glRasterPos3iv(int[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos3s(short x, short y, short z);
		[DllImport(DllName)]
		public static extern void glRasterPos3sv(short[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos4d(double x, double y, double z, double w);
		[DllImport(DllName)]
		public static extern void glRasterPos4dv(double[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos4f(float x, float y, float z, float w);
		[DllImport(DllName)]
		public static extern void glRasterPos4fv(float[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos4i(int x, int y, int z, int w);
		[DllImport(DllName)]
		public static extern void glRasterPos4iv(int[] v);
		[DllImport(DllName)]
		public static extern void glRasterPos4s(short x, short y, short z, short w);
		[DllImport(DllName)]
		public static extern void glRasterPos4sv(short[] v);
		[DllImport(DllName)]
		public static extern void glReadBuffer(uint mode);
		[DllImport(DllName)]
		public static unsafe extern void glReadPixels(int x, int y, int width, int height, uint format, uint type, void* pixels);
		[DllImport(DllName)]
		public static extern void glRectd(double x1, double y1, double x2, double y2);
		[DllImport(DllName)]
		public static extern void glRectdv(double[] v1, double[] v2);
		[DllImport(DllName)]
		public static extern void glRectf(float x1, float y1, float x2, float y2);
		[DllImport(DllName)]
		public static extern void glRectfv(float[] v1, float[] v2);
		[DllImport(DllName)]
		public static extern void glRecti(int x1, int y1, int x2, int y2);
		[DllImport(DllName)]
		public static extern void glRectiv(int[] v1, int[] v2);
		[DllImport(DllName)]
		public static extern void glRects(short x1, short y1, short x2, short y2);
		[DllImport(DllName)]
		public static extern void glRectsv(short[] v1, short[] v2);
		[DllImport(DllName)]
		public static extern int glRenderMode(uint mode);
		[DllImport(DllName)]
		public static extern void glRotated(double angle, double x, double y, double z);
		[DllImport(DllName)]
		public static extern void glRotatef(float angle, float x, float y, float z);
		[DllImport(DllName)]
		public static extern void glScaled(double x, double y, double z);
		[DllImport(DllName)]
		public static extern void glScalef(float x, float y, float z);
		[DllImport(DllName)]
		public static extern void glScissor(int x, int y, int width, int height);
		[DllImport(DllName)]
		public static extern void glSelectBuffer(int size, uint[] buffer);
		[DllImport(DllName)]
		public static extern void glShadeModel(uint mode);
		[DllImport(DllName)]
		public static extern void glStencilFunc(uint func, int reference, uint mask);
		[DllImport(DllName)]
		public static extern void glStencilMask(uint mask);
		[DllImport(DllName)]
		public static extern void glStencilOp(uint fail, uint zfail, uint zpass);
		[DllImport(DllName)]
		public static extern void glTexCoord1d(double s);
		[DllImport(DllName)]
		public static extern void glTexCoord1dv(double[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord1f(float s);
		[DllImport(DllName)]
		public static extern void glTexCoord1fv(float[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord1i(int s);
		[DllImport(DllName)]
		public static extern void glTexCoord1iv(int[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord1s(short s);
		[DllImport(DllName)]
		public static extern void glTexCoord1sv(short[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord2d(double s, double t);
		[DllImport(DllName)]
		public static extern void glTexCoord2dv(double[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord2f(float s, float t);
		[DllImport(DllName)]
		public static extern void glTexCoord2fv(float[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord2i(int s, int t);
		[DllImport(DllName)]
		public static extern void glTexCoord2iv(int[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord2s(short s, short t);
		[DllImport(DllName)]
		public static extern void glTexCoord2sv(short[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord3d(double s, double t, double r);
		[DllImport(DllName)]
		public static extern void glTexCoord3dv(double[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord3f(float s, float t, float r);
		[DllImport(DllName)]
		public static extern void glTexCoord3fv(float[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord3i(int s, int t, int r);
		[DllImport(DllName)]
		public static extern void glTexCoord3iv(int[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord3s(short s, short t, short r);
		[DllImport(DllName)]
		public static extern void glTexCoord3sv(short[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord4d(double s, double t, double r, double q);
		[DllImport(DllName)]
		public static extern void glTexCoord4dv(double[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord4f(float s, float t, float r, float q);
		[DllImport(DllName)]
		public static extern void glTexCoord4fv(float[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord4i(int s, int t, int r, int q);
		[DllImport(DllName)]
		public static extern void glTexCoord4iv(int[] v);
		[DllImport(DllName)]
		public static extern void glTexCoord4s(short s, short t, short r, short q);
		[DllImport(DllName)]
		public static extern void glTexCoord4sv(short[] v);
		[DllImport(DllName)]
		public static extern void glTexCoordPointer(int size, uint type, int stride, IntPtr pointer);
		[DllImport(DllName)]
		public static extern void glTexEnvf(uint target, uint pname, float param);
		[DllImport(DllName)]
		public static extern void glTexEnvfv(uint target, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glTexEnvi(uint target, uint pname, int param);
		[DllImport(DllName)]
		public static extern void glTexEnviv(uint target, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static extern void glTexGend(uint coord, uint pname, double param);
		[DllImport(DllName)]
		public static extern void glTexGendv(uint coord, uint pname, double[] parameters);
		[DllImport(DllName)]
		public static extern void glTexGenf(uint coord, uint pname, float param);
		[DllImport(DllName)]
		public static extern void glTexGenfv(uint coord, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glTexGeni(uint coord, uint pname, int param);
		[DllImport(DllName)]
		public static extern void glTexGeniv(uint coord, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static unsafe extern void glTexImage1D(uint target, int level, int internalformat, int width, int border, uint format, uint type, void* pixels);
		[DllImport(DllName)]
		public static extern void glTexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, [Out] byte[] pixels);
		[DllImport(DllName)]
		public static extern void glTexParameterf(uint target, uint pname, float param);
		[DllImport(DllName)]
		public static extern void glTexParameterfv(uint target, uint pname, float[] parameters);
		[DllImport(DllName)]
		public static extern void glTexParameteri(uint target, uint pname, int param);
		[DllImport(DllName)]
		public static extern void glTexParameteriv(uint target, uint pname, int[] parameters);
		[DllImport(DllName)]
		public static unsafe extern void glTexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type, void* pixels);
		[DllImport(DllName)]
		public static unsafe extern void glTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, void* pixels);
		[DllImport(DllName)]
		public static extern void glTranslated(double x, double y, double z);
		[DllImport(DllName)]
		public static extern void glTranslatef(float x, float y, float z);
		[DllImport(DllName)]
		public static extern void glVertex2d(double x, double y);
		[DllImport(DllName)]
		public static extern void glVertex2dv(double[] v);
		[DllImport(DllName)]
		public static extern void glVertex2f(float x, float y);
		[DllImport(DllName)]
		public static extern void glVertex2fv(float[] v);
		[DllImport(DllName)]
		public static extern void glVertex2i(int x, int y);
		[DllImport(DllName)]
		public static extern void glVertex2iv(int[] v);
		[DllImport(DllName)]
		public static extern void glVertex2s(short x, short y);
		[DllImport(DllName)]
		public static extern void glVertex2sv(short[] v);
		[DllImport(DllName)]
		public static extern void glVertex3d(double x, double y, double z);
		[DllImport(DllName)]
		public static extern void glVertex3dv(double[] v);
		[DllImport(DllName)]
		public static extern void glVertex3f(float x, float y, float z);
		[DllImport(DllName)]
		public static extern void glVertex3fv(float[] v);
		[DllImport(DllName)]
		public static extern void glVertex3i(int x, int y, int z);
		[DllImport(DllName)]
		public static extern void glVertex3iv(int[] v);
		[DllImport(DllName)]
		public static extern void glVertex3s(short x, short y, short z);
		[DllImport(DllName)]
		public static extern void glVertex3sv(short[] v);
		[DllImport(DllName)]
		public static extern void glVertex4d(double x, double y, double z, double w);
		[DllImport(DllName)]
		public static extern void glVertex4dv(double[] v);
		[DllImport(DllName)]
		public static extern void glVertex4f(float x, float y, float z, float w);
		[DllImport(DllName)]
		public static extern void glVertex4fv(float[] v);
		[DllImport(DllName)]
		public static extern void glVertex4i(int x, int y, int z, int w);
		[DllImport(DllName)]
		public static extern void glVertex4iv(int[] v);
		[DllImport(DllName)]
		public static extern void glVertex4s(short x, short y, short z, short w);
		[DllImport(DllName)]
		public static extern void glVertex4sv(short[] v);
		[DllImport(DllName)]
		public static extern void glVertexPointer(int size, uint type, int stride, IntPtr pointer);
		[DllImport(DllName)]
		public static extern void glViewport(int x, int y, int width, int height);
		[DllImport(DllName)]
		public static extern void glBeginTransformFeedback(uint i);
		[DllImport(DllName)]
		public static extern void glEndTransformFeedback();
		[DllImport(DllName)]
		public static extern void glGenerateMipmap(uint i);

		public static string glGetString(int name)
		{
			return Marshal.PtrToStringAnsi(_glGetString((uint)name));
		}

	}
}


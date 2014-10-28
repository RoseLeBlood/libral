//
//  Extensions.cs
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
using System.Collections.Generic;

namespace System.API.OpenGL
{
	public class Extensions
	{

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
		public static  bool IsEXT_blend_equation_separate = false;//#6495ED
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
		public static  bool IsOpenCL_gl_supported = false;

		internal static void LoadExtensionsList()
		{
			FieldInfo info;
			string[] extensions;
			string extensionlist = gl.glGetString((int)GL.EXTENSIONS); 

			if (extensionlist != null) 
			{
				extensions = extensionlist.Split(new char[] { ' ' });
			}
			else
			{
				// OpenGL 3.1 core does not support GetString with GL_EXTENSIONS parameter.
				int[] num = new int[1];
				gl.glGetIntegerv((uint)GL.NUM_EXTENSIONS, num);
				extensions = new string[num[0]];
				for (uint i = 0; i < num[0]; i++)
				{
					extensions[i] = gl.glGetString((int)GL.EXTENSIONS, i);
				}
			}
			foreach (string extension in extensions)
			{
				string extensionName = extension;
				if (extensionName.StartsWith("GL_")) extensionName = extensionName.Substring(3);
				info = typeof(Extensions).GetField("Is" + extensionName);
				if (info != null)
				{
					info.SetValue(null, true);
				}
			}
			string[] version = gl.glGetString((int)GL.VERSION).Split(new char[] { '.' });
			List<int> v = new List<int>();

			foreach (var item in version)
			{
				int x = 0;
				if (int.TryParse(item, out x))
					v.Add(x);
			}

			info = typeof(Extensions).GetField("IsVERSION_" + v[0] + "_" + v[1]);
			if (info != null)
			{
				versionMajor = v[0];
				versionMinor = v[1];
				info.SetValue(null, true);
			}
		}
	}
}


//
//  OpenGlExt.cs
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

namespace liboRg.OpenGL
{
	public partial class gl
	{
		public enum VboTarget : uint
		{
			ArrayBuffer				= (uint)GL.ARRAY_BUFFER_ARB,
			ElementArrayBuffer		= (uint)GL.ELEMENT_ARRAY_BUFFER_ARB
		}
		public enum VboAccess : int
		{
			ReadOnly 				= (int)GL.READ_ONLY_ARB,
			WriteOnly 				= (int)GL.WRITE_ONLY_ARB,
			ReadWrite 				= (int)GL.READ_WRITE_ARB,
		}
		public enum VboUsage : uint
		{
			StreamDraw 				= (uint)GL.STREAM_DRAW_ARB,
			StreamRead 				= (uint)GL.STREAM_READ_ARB,
			StreamCopy 				= (uint)GL.STREAM_COPY_ARB,
			StaticDraw 				= (uint)GL.STATIC_DRAW_ARB,
			StaticRead 				= (uint)GL.STATIC_READ_ARB,
			StaticCopy 				= (uint)GL.STATIC_COPY_ARB,
			DynamicDraw 			= (uint)GL.DYNAMIC_DRAW_ARB,
			DynamicRead 			= (uint)GL.DYNAMIC_READ_ARB,
			DynamicCopy 			= (uint)GL.DYNAMIC_COPY_ARB,
		}
		private static bool m_bLoaded = false;

		public static void LoadExtension(IGLNativeContext ctx)
		{
			if (m_bLoaded)
				return;

			InitVbo(ctx);
			InitShaderProgram(ctx);

			m_bLoaded = true;
		}
		private static void InitVbo(IGLNativeContext ctx)
		{
			glGenBuffersARB = (GenBuffersARB)ctx.GetProc<GenBuffersARB>("glGenBuffersARB");
			glBindBufferARB = (BindBufferARB)ctx.GetProc<BindBufferARB>("glBindBufferARB");
			glBufferDataARB = (BufferDataARB)ctx.GetProc<BufferDataARB>("glBufferDataARB");
			glBufferSubDataARB = (BufferSubDataARB)ctx.GetProc<BufferSubDataARB>("glBufferSubDataARB");
			glDeleteBuffersARB = (DeleteBuffersARB)ctx.GetProc<DeleteBuffersARB>("glDeleteBuffersARB");
			glMapBufferARB = (MapBufferARB)ctx.GetProc<MapBufferARB>("glMapBufferARB");
			glUnmapBufferARB = (UnmapBufferARB)ctx.GetProc<UnmapBufferARB>("glUnmapBufferARB");
			#if DEBUG
			Console.WriteLine("INIT VBO : OK");
			#endif
		}
		private static void InitShaderProgram(IGLNativeContext ctx)
		{

			glVertexAttrib1dARB = (VertexAttrib1dARB)ctx.GetProc<VertexAttrib1dARB>("glVertexAttrib1dARB");
			glVertexAttrib1dvARB = (VertexAttrib1dvARB)ctx.GetProc<VertexAttrib1dvARB>("glVertexAttrib1dvARB");
			glVertexAttrib1fARB = (VertexAttrib1fARB)ctx.GetProc<VertexAttrib1fARB>("glVertexAttrib1fARB");
			glVertexAttrib1fvARB = (VertexAttrib1fvARB)ctx.GetProc<VertexAttrib1fvARB>("glVertexAttrib1fvARB");
			glVertexAttrib1sARB = (VertexAttrib1sARB)ctx.GetProc<VertexAttrib1sARB>("glVertexAttrib1sARB");
			glVertexAttrib1svARB = (VertexAttrib1svARB)ctx.GetProc<VertexAttrib1svARB>("glVertexAttrib1svARB");
			glVertexAttrib2dARB = (VertexAttrib2dARB)ctx.GetProc<VertexAttrib2dARB>("glVertexAttrib2dARB");
			glVertexAttrib2dvARB = (VertexAttrib2dvARB)ctx.GetProc<VertexAttrib2dvARB>("glVertexAttrib2dvARB");
			glVertexAttrib2fARB = (VertexAttrib2fARB)ctx.GetProc<VertexAttrib2fARB>("glVertexAttrib2fARB");
			glVertexAttrib2fvARB = (VertexAttrib2fvARB)ctx.GetProc<VertexAttrib2fvARB>("glVertexAttrib2fvARB");
			glVertexAttrib2sARB = (VertexAttrib2sARB)ctx.GetProc<VertexAttrib2sARB>("glVertexAttrib2sARB");
			glVertexAttrib2svARB = (VertexAttrib2svARB)ctx.GetProc<VertexAttrib2svARB>("glVertexAttrib2svARB");
			glVertexAttrib3dARB = (VertexAttrib3dARB)ctx.GetProc<VertexAttrib3dARB>("glVertexAttrib3dARB");
			glVertexAttrib3dvARB = (VertexAttrib3dvARB)ctx.GetProc<VertexAttrib3dvARB>("glVertexAttrib3dvARB");
			glVertexAttrib3fARB = (VertexAttrib3fARB)ctx.GetProc<VertexAttrib3fARB>("glVertexAttrib3fARB");
			glVertexAttrib3fvARB = (VertexAttrib3fvARB)ctx.GetProc<VertexAttrib3fvARB>("glVertexAttrib3fvARB");
			glVertexAttrib3sARB = (VertexAttrib3sARB)ctx.GetProc<VertexAttrib3sARB>("glVertexAttrib3sARB");
			glVertexAttrib3svARB = (VertexAttrib3svARB)ctx.GetProc<VertexAttrib3svARB>("glVertexAttrib3svARB");
			glVertexAttrib4NbvARB = (VertexAttrib4NbvARB)ctx.GetProc<VertexAttrib4NbvARB>("glVertexAttrib4NbvARB");
			glVertexAttrib4NivARB = (VertexAttrib4NivARB)ctx.GetProc<VertexAttrib4NivARB>("glVertexAttrib4NivARB");
			glVertexAttrib4NsvARB = (VertexAttrib4NsvARB)ctx.GetProc<VertexAttrib4NsvARB>("glVertexAttrib4NsvARB");
			glVertexAttrib4NubARB = (VertexAttrib4NubARB)ctx.GetProc<VertexAttrib4NubARB>("glVertexAttrib4NubARB");
			glVertexAttrib4NubvARB = (VertexAttrib4NubvARB)ctx.GetProc<VertexAttrib4NubvARB>("glVertexAttrib4NubvARB");
			glVertexAttrib4NuivARB = (VertexAttrib4NuivARB)ctx.GetProc<VertexAttrib4NuivARB>("glVertexAttrib4NuivARB");
			glVertexAttrib4NusvARB = (VertexAttrib4NusvARB)ctx.GetProc<VertexAttrib4NusvARB>("glVertexAttrib4NusvARB");
			glVertexAttrib4bvARB = (VertexAttrib4bvARB)ctx.GetProc<VertexAttrib4bvARB>("glVertexAttrib4bvARB");
			glVertexAttrib4dARB = (VertexAttrib4dARB)ctx.GetProc<VertexAttrib4dARB>("glVertexAttrib4dARB");
			glVertexAttrib4dvARB = (VertexAttrib4dvARB)ctx.GetProc<VertexAttrib4dvARB>("glVertexAttrib4dvARB");
			glVertexAttrib4fARB = (VertexAttrib4fARB)ctx.GetProc<VertexAttrib4fARB>("glVertexAttrib4fARB");
			glVertexAttrib4fvARB = (VertexAttrib4fvARB)ctx.GetProc<VertexAttrib4fvARB>("glVertexAttrib4fvARB");
			glVertexAttrib4ivARB = (VertexAttrib4ivARB)ctx.GetProc<VertexAttrib4ivARB>("glVertexAttrib4ivARB");
			glVertexAttrib4sARB = (VertexAttrib4sARB)ctx.GetProc<VertexAttrib4sARB>("glVertexAttrib4sARB");
			glVertexAttrib4svARB = (VertexAttrib4svARB)ctx.GetProc<VertexAttrib4svARB>("glVertexAttrib4svARB");
			glVertexAttrib4ubvARB = (VertexAttrib4ubvARB)ctx.GetProc<VertexAttrib4ubvARB>("glVertexAttrib4ubvARB");
			glVertexAttrib4uivARB = (VertexAttrib4uivARB)ctx.GetProc<VertexAttrib4uivARB>("glVertexAttrib4uivARB");
			glVertexAttrib4usvARB = (VertexAttrib4usvARB)ctx.GetProc<VertexAttrib4usvARB>("glVertexAttrib4usvARB");
			glVertexAttribPointerARB = (VertexAttribPointerARB)ctx.GetProc<VertexAttribPointerARB>("glVertexAttribPointerARB");
			glEnableVertexAttribArrayARB = (EnableVertexAttribArrayARB)ctx.GetProc<EnableVertexAttribArrayARB>("glEnableVertexAttribArrayARB");
			glDisableVertexAttribArrayARB = (DisableVertexAttribArrayARB)ctx.GetProc<DisableVertexAttribArrayARB>("glDisableVertexAttribArrayARB");
			glProgramStringARB = (ProgramStringARB)ctx.GetProc<ProgramStringARB>("glProgramStringARB");
			glBindProgramARB = (BindProgramARB)ctx.GetProc<BindProgramARB>("glBindProgramARB");
			glDeleteProgramsARB = (DeleteProgramsARB)ctx.GetProc<DeleteProgramsARB>("glDeleteProgramsARB");
			glGenProgramsARB = (GenProgramsARB)ctx.GetProc<GenProgramsARB>("glGenProgramsARB");
			glProgramEnvParameter4dARB = (ProgramEnvParameter4dARB)ctx.GetProc<ProgramEnvParameter4dARB>("glProgramEnvParameter4dARB");
			glProgramEnvParameter4dvARB = (ProgramEnvParameter4dvARB)ctx.GetProc<ProgramEnvParameter4dvARB>("glProgramEnvParameter4dvARB");
			glProgramEnvParameter4fARB = (ProgramEnvParameter4fARB)ctx.GetProc<ProgramEnvParameter4fARB>("glProgramEnvParameter4fARB");
			glProgramEnvParameter4fvARB = (ProgramEnvParameter4fvARB)ctx.GetProc<ProgramEnvParameter4fvARB>("glProgramEnvParameter4fvARB");
			glProgramLocalParameter4dARB = (ProgramLocalParameter4dARB)ctx.GetProc<ProgramLocalParameter4dARB>("glProgramLocalParameter4dARB");
			glProgramLocalParameter4dvARB = (ProgramLocalParameter4dvARB)ctx.GetProc<ProgramLocalParameter4dvARB>("glProgramLocalParameter4dvARB");
			glProgramLocalParameter4fARB = (ProgramLocalParameter4fARB)ctx.GetProc<ProgramLocalParameter4fARB>("glProgramLocalParameter4fARB");
			glProgramLocalParameter4fvARB = (ProgramLocalParameter4fvARB)ctx.GetProc<ProgramLocalParameter4fvARB>("glProgramLocalParameter4fvARB");
			glGetProgramEnvParameterdvARB = (GetProgramEnvParameterdvARB)ctx.GetProc<GetProgramEnvParameterdvARB>("glGetProgramEnvParameterdvARB");
			glGetProgramEnvParameterfvARB = (GetProgramEnvParameterfvARB)ctx.GetProc<GetProgramEnvParameterfvARB>("glGetProgramEnvParameterfvARB");
			glGetProgramLocalParameterdvARB = (GetProgramLocalParameterdvARB)ctx.GetProc<GetProgramLocalParameterdvARB>("glGetProgramLocalParameterdvARB");
			glGetProgramLocalParameterfvARB = (GetProgramLocalParameterfvARB)ctx.GetProc<GetProgramLocalParameterfvARB>("glGetProgramLocalParameterfvARB");
			glGetProgramivARB = (GetProgramivARB)ctx.GetProc<GetProgramivARB>("glGetProgramivARB");
			glGetProgramStringARB = (GetProgramStringARB)ctx.GetProc<GetProgramStringARB>("glGetProgramStringARB");
			glGetVertexAttribdvARB = (GetVertexAttribdvARB)ctx.GetProc<GetVertexAttribdvARB>("glGetVertexAttribdvARB");
			glGetVertexAttribfvARB = (GetVertexAttribfvARB)ctx.GetProc<GetVertexAttribfvARB>("glGetVertexAttribfvARB");
			glGetVertexAttribivARB = (GetVertexAttribivARB)ctx.GetProc<GetVertexAttribivARB>("glGetVertexAttribivARB");
			glGetVertexAttribPointervARB = (GetVertexAttribPointervARB)ctx.GetProc<GetVertexAttribPointervARB>("glGetVertexAttribPointervARB");
			glIsProgramARB = (IsProgramARB)ctx.GetProc<IsProgramARB>("glIsProgramARB");

			glDeleteObjectARB = (DeleteObjectARB)ctx.GetProc<DeleteObjectARB>("glDeleteObjectARB");
			glGetHandleARB = (GetHandleARB)ctx.GetProc<GetHandleARB>("glGetHandleARB");
			glDetachObjectARB = (DetachObjectARB)ctx.GetProc<DetachObjectARB>("glDetachObjectARB");
			glCreateShaderObjectARB = (CreateShaderObjectARB)ctx.GetProc<CreateShaderObjectARB>("glCreateShaderObjectARB");
			glShaderSourceARB = (ShaderSourceARB)ctx.GetProc<ShaderSourceARB>("glShaderSourceARB");
			glCompileShaderARB = (CompileShaderARB)ctx.GetProc<CompileShaderARB>("glCompileShaderARB");
			glCreateProgramObjectARB = (CreateProgramObjectARB)ctx.GetProc<CreateProgramObjectARB>("glCreateProgramObjectARB");
			glAttachObjectARB = (AttachObjectARB)ctx.GetProc<AttachObjectARB>("glAttachObjectARB");
			glLinkProgramARB = (LinkProgramARB)ctx.GetProc<LinkProgramARB>("glLinkProgramARB");
			glUseProgramObjectARB = (UseProgramObjectARB)ctx.GetProc<UseProgramObjectARB>("glUseProgramObjectARB");
			glValidateProgramARB = (ValidateProgramARB)ctx.GetProc<ValidateProgramARB>("glValidateProgramARB");
			glUniform1fARB = (Uniform1fARB)ctx.GetProc<Uniform1fARB>("glUniform1fARB");
			glUniform2fARB = (Uniform2fARB)ctx.GetProc<Uniform2fARB>("glUniform2fARB");
			glUniform3fARB = (Uniform3fARB)ctx.GetProc<Uniform3fARB>("glUniform3fARB");
			glUniform4fARB = (Uniform4fARB)ctx.GetProc<Uniform4fARB>("glUniform4fARB");
			glUniform1iARB = (Uniform1iARB)ctx.GetProc<Uniform1iARB>("glUniform1iARB");
			glUniform2iARB = (Uniform2iARB)ctx.GetProc<Uniform2iARB>("glUniform2iARB");
			glUniform3iARB = (Uniform3iARB)ctx.GetProc<Uniform3iARB>("glUniform3iARB");
			glUniform4iARB = (Uniform4iARB)ctx.GetProc<Uniform4iARB>("glUniform4iARB");
			glUniform1fvARB = (Uniform1fvARB)ctx.GetProc<Uniform1fvARB>("glUniform1fvARB");

			//glUniform2fvARB = ctx.GetProc<Uniform2fvARB>("glUniform2fvARB");
			//glUniform3fvARB = ctx.GetProc<Uniform3fvARB>("glUniform3fvARB");
			//glUniform4fvARB = ctx.GetProc<Uniform4fvARB>("glUniform4fvARB");

			glUniform1ivARB = (Uniform1ivARB)ctx.GetProc<Uniform1ivARB>("glUniform1ivARB");
			glUniform2ivARB = (Uniform2ivARB)ctx.GetProc<Uniform2ivARB>("glUniform2ivARB");
			glUniform3ivARB = (Uniform3ivARB)ctx.GetProc<Uniform3ivARB>("glUniform3ivARB");
			glUniform4ivARB = (Uniform4ivARB)ctx.GetProc<Uniform4ivARB>("glUniform4ivARB");
			glUniformMatrix2fvARB = (UniformMatrix2fvARB)ctx.GetProc<UniformMatrix2fvARB>("glUniformMatrix2fvARB");
			glUniformMatrix3fvARB = (UniformMatrix3fvARB)ctx.GetProc<UniformMatrix3fvARB>("glUniformMatrix3fvARB");

			//glUniformMatrix4fvARB = ctx.GetProc<UniformMatrix4fvARB>("glUniformMatrix4fvARB");

			glGetObjectParameterfvARB = (GetObjectParameterfvARB)ctx.GetProc<GetObjectParameterfvARB>("glGetObjectParameterfvARB");
			glGetObjectParameterivARB = (GetObjectParameterivARB)ctx.GetProc<GetObjectParameterivARB>("glGetObjectParameterivARB");
			glGetInfoLogARB = (GetInfoLogARB)ctx.GetProc<GetInfoLogARB>("glGetInfoLogARB");
			glGetAttachedObjectsARB = (GetAttachedObjectsARB)ctx.GetProc<GetAttachedObjectsARB>("glGetAttachedObjectsARB");
			glGetUniformLocationARB = (GetUniformLocationARB)ctx.GetProc<GetUniformLocationARB>("glGetUniformLocationARB");
			glGetActiveUniformARB = (GetActiveUniformARB)ctx.GetProc<GetActiveUniformARB>("glGetActiveUniformARB");
			glGetUniformfvARB = (GetUniformfvARB)ctx.GetProc<GetUniformfvARB>("glGetUniformfvARB");
			glGetUniformivARB = (GetUniformivARB)ctx.GetProc<GetUniformivARB>("glGetUniformivARB");
			glGetShaderSourceARB = (GetShaderSourceARB)ctx.GetProc<GetShaderSourceARB>("glGetShaderSourceARB");

			glProgramParameteriARB = (ProgramParameteriARB)ctx.GetProc<ProgramParameteriARB>("glProgramParameteriARB");
			glFramebufferTextureARB = (FramebufferTextureARB)ctx.GetProc<FramebufferTextureARB>("glFramebufferTextureARB");
			glFramebufferTextureLayerARB = (FramebufferTextureLayerARB)ctx.GetProc<FramebufferTextureLayerARB>("glFramebufferTextureLayerARB");
			glFramebufferTextureFaceARB = (FramebufferTextureFaceARB)ctx.GetProc<FramebufferTextureFaceARB>("glFramebufferTextureFaceARB");
		
			#if DEBUG
			Console.WriteLine("INIT ShaderProgram : OK");
			#endif
		}

		public delegate void GenBuffersARB(int n, uint[] ids);
		public delegate void BindBufferARB(VboTarget target, uint id);
		public unsafe delegate void BufferDataARB(VboTarget target, int size, IntPtr data, VboUsage usage);
		public unsafe delegate void BufferSubDataARB(VboTarget target, int offset, int size, void* data);
		public delegate void DeleteBuffersARB(int n, uint[] ids);
		public unsafe delegate void* MapBufferARB(VboTarget target, VboAccess access);
		public delegate boolean UnmapBufferARB(VboTarget target);
		public delegate void VertexAttrib1dARB(uint index, double x);
		public delegate void VertexAttrib1dvARB(uint index, double[] v);
		public delegate void VertexAttrib1fARB(uint index, float x);
		public delegate void VertexAttrib1fvARB(uint index, float[] v);
		public delegate void VertexAttrib1sARB(uint index, short x);
		public delegate void VertexAttrib1svARB(uint index, short[] v);
		public delegate void VertexAttrib2dARB(uint index, double x, double y);
		public delegate void VertexAttrib2dvARB(uint index, double[] v);
		public delegate void VertexAttrib2fARB(uint index, float x, float y);
		public delegate void VertexAttrib2fvARB(uint index, float[] v);
		public delegate void VertexAttrib2sARB(uint index, short x, short y);
		public delegate void VertexAttrib2svARB(uint index, short[] v);
		public delegate void VertexAttrib3dARB(uint index, double x, double y, double z);
		public delegate void VertexAttrib3dvARB(uint index, double[] v);
		public delegate void VertexAttrib3fARB(uint index, float x, float y, float z);
		public delegate void VertexAttrib3fvARB(uint index, float[] v);
		public delegate void VertexAttrib3sARB(uint index, short x, short y, short z);
		public delegate void VertexAttrib3svARB(uint index, short[] v);
		public delegate void VertexAttrib4NbvARB(uint index, byte[] v);
		public delegate void VertexAttrib4NivARB(uint index, int[] v);
		public delegate void VertexAttrib4NsvARB(uint index, short[] v);
		public delegate void VertexAttrib4NubARB(uint index, byte x, byte y, byte z, byte w);
		public delegate void VertexAttrib4NubvARB(uint index, byte[] v);
		public delegate void VertexAttrib4NuivARB(uint index, uint[] v);
		public delegate void VertexAttrib4NusvARB(uint index, ushort[] v);
		public delegate void VertexAttrib4bvARB(uint index, byte[] v);
		public delegate void VertexAttrib4dARB(uint index, double x, double y, double z, double w);
		public delegate void VertexAttrib4dvARB(uint index, double[] v);
		public delegate void VertexAttrib4fARB(uint index, float x, float y, float z, float w);
		public delegate void VertexAttrib4fvARB(uint index, float[] v);
		public delegate void VertexAttrib4ivARB(uint index, int[] v);
		public delegate void VertexAttrib4sARB(uint index, short x, short y, short z, short w);
		public delegate void VertexAttrib4svARB(uint index, short[] v);
		public delegate void VertexAttrib4ubvARB(uint index, byte[] v);
		public delegate void VertexAttrib4uivARB(uint index, uint[] v);
		public delegate void VertexAttrib4usvARB(uint index, ushort[] v);
		public unsafe delegate void VertexAttribPointerARB(uint index, int size, uint type, boolean normalized, int stride, void* pointer);
		public delegate void EnableVertexAttribArrayARB(uint index);
		public delegate void DisableVertexAttribArrayARB(uint index);
		public delegate void ProgramStringARB(uint target, uint format, int len, [MarshalAs(UnmanagedType.LPWStr)] string code);
		public delegate void BindProgramARB(uint target, uint program);
		public delegate void DeleteProgramsARB(int n, uint[] programs);
		public delegate void GenProgramsARB(int n, uint[] programs);
		public delegate void ProgramEnvParameter4dARB(uint target, uint index, double x, double y, double z, double w);
		public delegate void ProgramEnvParameter4dvARB(uint target, uint index, double[] parameters);
		public delegate void ProgramEnvParameter4fARB(uint target, uint index, float x, float y, float z, float w);
		public delegate void ProgramEnvParameter4fvARB(uint target, uint index, float[] parameters);
		public delegate void ProgramLocalParameter4dARB(uint target, uint index, double x, double y, double z, double w);
		public delegate void ProgramLocalParameter4dvARB(uint target, uint index, double[] parameters);
		public delegate void ProgramLocalParameter4fARB(uint target, uint index, float x, float y, float z, float w);
		public delegate void ProgramLocalParameter4fvARB(uint target, uint index, float[] parameters);
		public delegate void GetProgramEnvParameterdvARB(uint target, uint index, ref double parameters);
		public delegate void GetProgramEnvParameterfvARB(uint target, uint index, float[] parameters);
		public delegate void GetProgramLocalParameterdvARB(uint target, uint index, ref double parameters);
		public delegate void GetProgramLocalParameterfvARB(uint target, uint index, ref float parameters);
		public delegate void GetProgramivARB(uint target, uint pname, ref int parameters);
		public delegate void GetProgramStringARB(uint target, uint pname, out string code);
		public delegate void GetVertexAttribdvARB(uint index, uint pname, ref double parameters);
		public delegate void GetVertexAttribfvARB(uint index, uint pname, ref float parameters);
		public delegate void GetVertexAttribivARB(uint index, uint pname, ref int parameters);
		public unsafe delegate void GetVertexAttribPointervARB(uint index, uint pname, void** pointer);
		public delegate boolean IsProgramARB(uint program);

		public delegate void DeleteObjectARB(IntPtr obj);
		public delegate IntPtr GetHandleARB(uint pname);
		public delegate void DetachObjectARB(IntPtr containerObj, IntPtr attachedObj);
		public delegate IntPtr CreateShaderObjectARB(uint shaderType);
		public delegate void ShaderSourceARB(IntPtr shaderObj, int count, string[] code, int[] length);
		public delegate void CompileShaderARB(IntPtr shaderObj);
		public delegate IntPtr CreateProgramObjectARB();
		public delegate void AttachObjectARB(IntPtr containerObj, IntPtr obj);
		public delegate void LinkProgramARB(IntPtr programObj);
		public delegate void UseProgramObjectARB(IntPtr programObj);
		public delegate void ValidateProgramARB(IntPtr programObj);
		public delegate void Uniform1fARB(int location, float v0);
		public delegate void Uniform2fARB(int location, float v0, float v1);
		public delegate void Uniform3fARB(int location, float v0, float v1, float v2);
		public delegate void Uniform4fARB(int location, float v0, float v1, float v2, float v3);
		public delegate void Uniform1iARB(int location, int v0);
		public delegate void Uniform2iARB(int location, int v0, int v1);
		public delegate void Uniform3iARB(int location, int v0, int v1, int v2);
		public delegate void Uniform4iARB(int location, int v0, int v1, int v2, int v3);
		public delegate void Uniform1fvARB(int location, int count, ref float value);
		//public delegate void Uniform2fvARB(int location, int count, ref Vector2 value);
		//public delegate void Uniform3fvARB(int location, int count, ref Vector3 value);
		//public delegate void Uniform4fvARB(int location, int count, ref Vector4 value);
		public delegate void Uniform1ivARB(int location, int count, int[] value);
		public delegate void Uniform2ivARB(int location, int count, int[] value);
		public delegate void Uniform3ivARB(int location, int count, int[] value);
		public delegate void Uniform4ivARB(int location, int count, int[] value);
		public delegate void UniformMatrix2fvARB(int location, int count, boolean transpose, float[] value);
		public delegate void UniformMatrix3fvARB(int location, int count, boolean transpose, float[] value);
		//public delegate void UniformMatrix4fvARB(int location, int count, boolean transpose, ref Matrix value);
		public delegate void GetObjectParameterfvARB(IntPtr obj, uint pname, float[] parameters);
		public delegate void GetObjectParameterivARB(IntPtr obj, uint pname, int[] parameters);
		public delegate void GetInfoLogARB(IntPtr obj, int maxLength, ref int length, byte[] infoLog);
		public delegate void GetAttachedObjectsARB(IntPtr containerObj, int maxCount, ref int count, IntPtr[] obj);
		public delegate int GetUniformLocationARB(IntPtr programObj, string name);
		public delegate void GetActiveUniformARB(IntPtr programObj, uint index, int maxLength, ref int length, ref int size, ref uint type, byte[] name);
		public delegate void GetUniformfvARB(IntPtr programObj, int location, float[] parameters);
		public delegate void GetUniformivARB(IntPtr programObj, int location, int[] parameters);
		public delegate void GetShaderSourceARB(IntPtr obj, int maxLength, ref int length, byte[] source);

		public delegate void ProgramParameteriARB(uint program, uint pname, int value);
		public delegate void FramebufferTextureARB(uint target, uint attachment, uint texture, int level);
		public delegate void FramebufferTextureLayerARB(uint target, uint attachment, uint texture, int level, int layer);
		public delegate void FramebufferTextureFaceARB(uint target, uint attachment, uint texture, int level, uint face);

		public static GenBuffersARB glGenBuffersARB;
		public static BindBufferARB glBindBufferARB;
		public static BufferDataARB glBufferDataARB;
		public static BufferSubDataARB glBufferSubDataARB;
		public static DeleteBuffersARB glDeleteBuffersARB;
		public static MapBufferARB glMapBufferARB;
		public static UnmapBufferARB glUnmapBufferARB;


		public static VertexAttrib1dARB glVertexAttrib1dARB;
		public static VertexAttrib1dvARB glVertexAttrib1dvARB;
		public static VertexAttrib1fARB glVertexAttrib1fARB;
		public static VertexAttrib1fvARB glVertexAttrib1fvARB;
		public static VertexAttrib1sARB glVertexAttrib1sARB;
		public static VertexAttrib1svARB glVertexAttrib1svARB;
		public static VertexAttrib2dARB glVertexAttrib2dARB;
		public static VertexAttrib2dvARB glVertexAttrib2dvARB;
		public static VertexAttrib2fARB glVertexAttrib2fARB;
		public static VertexAttrib2fvARB glVertexAttrib2fvARB;
		public static VertexAttrib2sARB glVertexAttrib2sARB;
		public static VertexAttrib2svARB glVertexAttrib2svARB;
		public static VertexAttrib3dARB glVertexAttrib3dARB;
		public static VertexAttrib3dvARB glVertexAttrib3dvARB;
		public static VertexAttrib3fARB glVertexAttrib3fARB;
		public static VertexAttrib3fvARB glVertexAttrib3fvARB;
		public static VertexAttrib3sARB glVertexAttrib3sARB;
		public static VertexAttrib3svARB glVertexAttrib3svARB;
		public static VertexAttrib4NbvARB glVertexAttrib4NbvARB;
		public static VertexAttrib4NivARB glVertexAttrib4NivARB;
		public static VertexAttrib4NsvARB glVertexAttrib4NsvARB;
		public static VertexAttrib4NubARB glVertexAttrib4NubARB;
		public static VertexAttrib4NubvARB glVertexAttrib4NubvARB;
		public static VertexAttrib4NuivARB glVertexAttrib4NuivARB;
		public static VertexAttrib4NusvARB glVertexAttrib4NusvARB;
		public static VertexAttrib4bvARB glVertexAttrib4bvARB;
		public static VertexAttrib4dARB glVertexAttrib4dARB;
		public static VertexAttrib4dvARB glVertexAttrib4dvARB;
		public static VertexAttrib4fARB glVertexAttrib4fARB;
		public static VertexAttrib4fvARB glVertexAttrib4fvARB;
		public static VertexAttrib4ivARB glVertexAttrib4ivARB;
		public static VertexAttrib4sARB glVertexAttrib4sARB;
		public static VertexAttrib4svARB glVertexAttrib4svARB;
		public static VertexAttrib4ubvARB glVertexAttrib4ubvARB;
		public static VertexAttrib4uivARB glVertexAttrib4uivARB;
		public static VertexAttrib4usvARB glVertexAttrib4usvARB;
		public static VertexAttribPointerARB glVertexAttribPointerARB;
		public static EnableVertexAttribArrayARB glEnableVertexAttribArrayARB;
		public static DisableVertexAttribArrayARB glDisableVertexAttribArrayARB;
		public static ProgramStringARB glProgramStringARB;
		public static BindProgramARB glBindProgramARB;
		public static DeleteProgramsARB glDeleteProgramsARB;
		public static GenProgramsARB glGenProgramsARB;
		public static ProgramEnvParameter4dARB glProgramEnvParameter4dARB;
		public static ProgramEnvParameter4dvARB glProgramEnvParameter4dvARB;
		public static ProgramEnvParameter4fARB glProgramEnvParameter4fARB;
		public static ProgramEnvParameter4fvARB glProgramEnvParameter4fvARB;
		public static ProgramLocalParameter4dARB glProgramLocalParameter4dARB;
		public static ProgramLocalParameter4dvARB glProgramLocalParameter4dvARB;
		public static ProgramLocalParameter4fARB glProgramLocalParameter4fARB;
		public static ProgramLocalParameter4fvARB glProgramLocalParameter4fvARB;
		public static GetProgramEnvParameterdvARB glGetProgramEnvParameterdvARB;
		public static GetProgramEnvParameterfvARB glGetProgramEnvParameterfvARB;
		public static GetProgramLocalParameterdvARB glGetProgramLocalParameterdvARB;
		public static GetProgramLocalParameterfvARB glGetProgramLocalParameterfvARB;
		public static GetProgramivARB glGetProgramivARB;
		public static GetProgramStringARB glGetProgramStringARB;
		public static GetVertexAttribdvARB glGetVertexAttribdvARB;
		public static GetVertexAttribfvARB glGetVertexAttribfvARB;
		public static GetVertexAttribivARB glGetVertexAttribivARB;
		public static GetVertexAttribPointervARB glGetVertexAttribPointervARB;
		public static IsProgramARB glIsProgramARB;

		public static DeleteObjectARB glDeleteObjectARB;
		public static GetHandleARB glGetHandleARB;
		public static DetachObjectARB glDetachObjectARB;
		public static CreateShaderObjectARB glCreateShaderObjectARB;
		public static ShaderSourceARB glShaderSourceARB;
		public static CompileShaderARB glCompileShaderARB;
		public static CreateProgramObjectARB glCreateProgramObjectARB;
		public static AttachObjectARB glAttachObjectARB;
		public static LinkProgramARB glLinkProgramARB;
		public static UseProgramObjectARB glUseProgramObjectARB;
		public static ValidateProgramARB glValidateProgramARB;
		public static Uniform1fARB glUniform1fARB;
		public static Uniform2fARB glUniform2fARB;
		public static Uniform3fARB glUniform3fARB;
		public static Uniform4fARB glUniform4fARB;
		public static Uniform1iARB glUniform1iARB;
		public static Uniform2iARB glUniform2iARB;
		public static Uniform3iARB glUniform3iARB;
		public static Uniform4iARB glUniform4iARB;
		public static Uniform1fvARB glUniform1fvARB;
		//public static Uniform2fvARB glUniform2fvARB;
		//public static Uniform3fvARB glUniform3fvARB;
		//public static Uniform4fvARB glUniform4fvARB;
		public static Uniform1ivARB glUniform1ivARB;
		public static Uniform2ivARB glUniform2ivARB;
		public static Uniform3ivARB glUniform3ivARB;
		public static Uniform4ivARB glUniform4ivARB;
		public static UniformMatrix2fvARB glUniformMatrix2fvARB;
		public static UniformMatrix3fvARB glUniformMatrix3fvARB;
		//public static UniformMatrix4fvARB glUniformMatrix4fvARB;
		public static GetObjectParameterfvARB glGetObjectParameterfvARB;
		public static GetObjectParameterivARB glGetObjectParameterivARB;
		public static GetInfoLogARB glGetInfoLogARB;
		public static GetAttachedObjectsARB glGetAttachedObjectsARB;
		public static GetUniformLocationARB glGetUniformLocationARB;
		public static GetActiveUniformARB glGetActiveUniformARB;
		public static GetUniformfvARB glGetUniformfvARB;
		public static GetUniformivARB glGetUniformivARB;
		public static GetShaderSourceARB glGetShaderSourceARB;

		public static ProgramParameteriARB glProgramParameteriARB;
		public static FramebufferTextureARB glFramebufferTextureARB;
		public static FramebufferTextureLayerARB glFramebufferTextureLayerARB;
		public static FramebufferTextureFaceARB glFramebufferTextureFaceARB;



	}
}


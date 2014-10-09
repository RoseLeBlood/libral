//
//  OpenGLExtension.cs
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
		private static bool m_bLoaded = false;

		internal static void LoadExtension()
		{
			if (m_bLoaded) return;
			_LoadExtension();

			m_bLoaded = true;
		}
		internal static Delegate GetProc<T>(string name)
		{
			return GetProcUnix<T>(name);
		}
		internal static Delegate GetProcUnix<T>(string name)
		{
			var ptr = glxNativeContext.glXGetProcAddressARB(name);
			if (ptr == IntPtr.Zero)
				return null;

			return Marshal.GetDelegateForFunctionPointer(ptr, typeof(T));
		}
		internal static void _LoadExtension()
		{
			glGetStringi = (GetStringi)GetProc<GetStringi>("glGetStringi");
			glGenBuffersARB = (GenBuffersARB)GetProc<GenBuffersARB>("glGenBuffersARB");
			glBindBufferARB = (BindBufferARB)GetProc<BindBufferARB>("glBindBufferARB");
			glBufferDataARB = (BufferDataARB)GetProc<BufferDataARB>("glBufferDataARB");
			glBufferSubDataARB = (BufferSubDataARB)GetProc<BufferSubDataARB>("glBufferSubDataARB");
			glDeleteBuffersARB = (DeleteBuffersARB)GetProc<DeleteBuffersARB>("glDeleteBuffersARB");
			glMapBufferARB = (MapBufferARB)GetProc<MapBufferARB>("glMapBufferARB");
			glUnmapBufferARB = (UnmapBufferARB)GetProc<UnmapBufferARB>("glUnmapBufferARB");

			glVertexAttrib1dARB = (VertexAttrib1dARB)GetProc<VertexAttrib1dARB>("glVertexAttrib1dARB");
			glVertexAttrib1dvARB = (VertexAttrib1dvARB)GetProc<VertexAttrib1dvARB>("glVertexAttrib1dvARB");
			glVertexAttrib1fARB = (VertexAttrib1fARB)GetProc<VertexAttrib1fARB>("glVertexAttrib1fARB");
			glVertexAttrib1fvARB = (VertexAttrib1fvARB)GetProc<VertexAttrib1fvARB>("glVertexAttrib1fvARB");
			glVertexAttrib1sARB = (VertexAttrib1sARB)GetProc<VertexAttrib1sARB>("glVertexAttrib1sARB");
			glVertexAttrib1svARB = (VertexAttrib1svARB)GetProc<VertexAttrib1svARB>("glVertexAttrib1svARB");
			glVertexAttrib2dARB = (VertexAttrib2dARB)GetProc<VertexAttrib2dARB>("glVertexAttrib2dARB");
			glVertexAttrib2dvARB = (VertexAttrib2dvARB)GetProc<VertexAttrib2dvARB>("glVertexAttrib2dvARB");
			glVertexAttrib2fARB = (VertexAttrib2fARB)GetProc<VertexAttrib2fARB>("glVertexAttrib2fARB");
			glVertexAttrib2fvARB = (VertexAttrib2fvARB)GetProc<VertexAttrib2fvARB>("glVertexAttrib2fvARB");
			glVertexAttrib2sARB = (VertexAttrib2sARB)GetProc<VertexAttrib2sARB>("glVertexAttrib2sARB");
			glVertexAttrib2svARB = (VertexAttrib2svARB)GetProc<VertexAttrib2svARB>("glVertexAttrib2svARB");
			glVertexAttrib3dARB = (VertexAttrib3dARB)GetProc<VertexAttrib3dARB>("glVertexAttrib3dARB");
			glVertexAttrib3dvARB = (VertexAttrib3dvARB)GetProc<VertexAttrib3dvARB>("glVertexAttrib3dvARB");
			glVertexAttrib3fARB = (VertexAttrib3fARB)GetProc<VertexAttrib3fARB>("glVertexAttrib3fARB");
			glVertexAttrib3fvARB = (VertexAttrib3fvARB)GetProc<VertexAttrib3fvARB>("glVertexAttrib3fvARB");
			glVertexAttrib3sARB = (VertexAttrib3sARB)GetProc<VertexAttrib3sARB>("glVertexAttrib3sARB");
			glVertexAttrib3svARB = (VertexAttrib3svARB)GetProc<VertexAttrib3svARB>("glVertexAttrib3svARB");
			glVertexAttrib4NbvARB = (VertexAttrib4NbvARB)GetProc<VertexAttrib4NbvARB>("glVertexAttrib4NbvARB");
			glVertexAttrib4NivARB = (VertexAttrib4NivARB)GetProc<VertexAttrib4NivARB>("glVertexAttrib4NivARB");
			glVertexAttrib4NsvARB = (VertexAttrib4NsvARB)GetProc<VertexAttrib4NsvARB>("glVertexAttrib4NsvARB");
			glVertexAttrib4NubARB = (VertexAttrib4NubARB)GetProc<VertexAttrib4NubARB>("glVertexAttrib4NubARB");
			glVertexAttrib4NubvARB = (VertexAttrib4NubvARB)GetProc<VertexAttrib4NubvARB>("glVertexAttrib4NubvARB");
			glVertexAttrib4NuivARB = (VertexAttrib4NuivARB)GetProc<VertexAttrib4NuivARB>("glVertexAttrib4NuivARB");
			glVertexAttrib4NusvARB = (VertexAttrib4NusvARB)GetProc<VertexAttrib4NusvARB>("glVertexAttrib4NusvARB");
			glVertexAttrib4bvARB = (VertexAttrib4bvARB)GetProc<VertexAttrib4bvARB>("glVertexAttrib4bvARB");
			glVertexAttrib4dARB = (VertexAttrib4dARB)GetProc<VertexAttrib4dARB>("glVertexAttrib4dARB");
			glVertexAttrib4dvARB = (VertexAttrib4dvARB)GetProc<VertexAttrib4dvARB>("glVertexAttrib4dvARB");
			glVertexAttrib4fARB = (VertexAttrib4fARB)GetProc<VertexAttrib4fARB>("glVertexAttrib4fARB");
			glVertexAttrib4fvARB = (VertexAttrib4fvARB)GetProc<VertexAttrib4fvARB>("glVertexAttrib4fvARB");
			glVertexAttrib4ivARB = (VertexAttrib4ivARB)GetProc<VertexAttrib4ivARB>("glVertexAttrib4ivARB");
			glVertexAttrib4sARB = (VertexAttrib4sARB)GetProc<VertexAttrib4sARB>("glVertexAttrib4sARB");
			glVertexAttrib4svARB = (VertexAttrib4svARB)GetProc<VertexAttrib4svARB>("glVertexAttrib4svARB");
			glVertexAttrib4ubvARB = (VertexAttrib4ubvARB)GetProc<VertexAttrib4ubvARB>("glVertexAttrib4ubvARB");
			glVertexAttrib4uivARB = (VertexAttrib4uivARB)GetProc<VertexAttrib4uivARB>("glVertexAttrib4uivARB");
			glVertexAttrib4usvARB = (VertexAttrib4usvARB)GetProc<VertexAttrib4usvARB>("glVertexAttrib4usvARB");
			glVertexAttribPointerARB = (VertexAttribPointerARB)GetProc<VertexAttribPointerARB>("glVertexAttribPointerARB");
			glEnableVertexAttribArrayARB = (EnableVertexAttribArrayARB)GetProc<EnableVertexAttribArrayARB>("glEnableVertexAttribArrayARB");
			glDisableVertexAttribArrayARB = (DisableVertexAttribArrayARB)GetProc<DisableVertexAttribArrayARB>("glDisableVertexAttribArrayARB");
			glProgramStringARB = (ProgramStringARB)GetProc<ProgramStringARB>("glProgramStringARB");
			glBindProgramARB = (BindProgramARB)GetProc<BindProgramARB>("glBindProgramARB");
			glDeleteProgramsARB = (DeleteProgramsARB)GetProc<DeleteProgramsARB>("glDeleteProgramsARB");
			glGenProgramsARB = (GenProgramsARB)GetProc<GenProgramsARB>("glGenProgramsARB");
			glProgramEnvParameter4dARB = (ProgramEnvParameter4dARB)GetProc<ProgramEnvParameter4dARB>("glProgramEnvParameter4dARB");
			glProgramEnvParameter4dvARB = (ProgramEnvParameter4dvARB)GetProc<ProgramEnvParameter4dvARB>("glProgramEnvParameter4dvARB");
			glProgramEnvParameter4fARB = (ProgramEnvParameter4fARB)GetProc<ProgramEnvParameter4fARB>("glProgramEnvParameter4fARB");
			glProgramEnvParameter4fvARB = (ProgramEnvParameter4fvARB)GetProc<ProgramEnvParameter4fvARB>("glProgramEnvParameter4fvARB");
			glProgramLocalParameter4dARB = (ProgramLocalParameter4dARB)GetProc<ProgramLocalParameter4dARB>("glProgramLocalParameter4dARB");
			glProgramLocalParameter4dvARB = (ProgramLocalParameter4dvARB)GetProc<ProgramLocalParameter4dvARB>("glProgramLocalParameter4dvARB");
			glProgramLocalParameter4fARB = (ProgramLocalParameter4fARB)GetProc<ProgramLocalParameter4fARB>("glProgramLocalParameter4fARB");
			glProgramLocalParameter4fvARB = (ProgramLocalParameter4fvARB)GetProc<ProgramLocalParameter4fvARB>("glProgramLocalParameter4fvARB");
			glGetProgramEnvParameterdvARB = (GetProgramEnvParameterdvARB)GetProc<GetProgramEnvParameterdvARB>("glGetProgramEnvParameterdvARB");
			glGetProgramEnvParameterfvARB = (GetProgramEnvParameterfvARB)GetProc<GetProgramEnvParameterfvARB>("glGetProgramEnvParameterfvARB");
			glGetProgramLocalParameterdvARB = (GetProgramLocalParameterdvARB)GetProc<GetProgramLocalParameterdvARB>("glGetProgramLocalParameterdvARB");
			glGetProgramLocalParameterfvARB = (GetProgramLocalParameterfvARB)GetProc<GetProgramLocalParameterfvARB>("glGetProgramLocalParameterfvARB");
			glGetProgramivARB = (GetProgramivARB)GetProc<GetProgramivARB>("glGetProgramivARB");
			glGetProgramStringARB = (GetProgramStringARB)GetProc<GetProgramStringARB>("glGetProgramStringARB");
			glGetVertexAttribdvARB = (GetVertexAttribdvARB)GetProc<GetVertexAttribdvARB>("glGetVertexAttribdvARB");
			glGetVertexAttribfvARB = (GetVertexAttribfvARB)GetProc<GetVertexAttribfvARB>("glGetVertexAttribfvARB");
			glGetVertexAttribivARB = (GetVertexAttribivARB)GetProc<GetVertexAttribivARB>("glGetVertexAttribivARB");
			glGetVertexAttribPointervARB = (GetVertexAttribPointervARB)GetProc<GetVertexAttribPointervARB>("glGetVertexAttribPointervARB");
			glIsProgramARB = (IsProgramARB)GetProc<IsProgramARB>("glIsProgramARB");

			glDeleteObjectARB = (DeleteObjectARB)GetProc<DeleteObjectARB>("glDeleteObjectARB");
			glGetHandleARB = (GetHandleARB)GetProc<GetHandleARB>("glGetHandleARB");
			glDetachObjectARB = (DetachObjectARB)GetProc<DetachObjectARB>("glDetachObjectARB");
			glCreateShaderObjectARB = (CreateShaderObjectARB)GetProc<CreateShaderObjectARB>("glCreateShaderObjectARB");
			glShaderSourceARB = (ShaderSourceARB)GetProc<ShaderSourceARB>("glShaderSourceARB");
			glCompileShaderARB = (CompileShaderARB)GetProc<CompileShaderARB>("glCompileShaderARB");
			glCreateProgramObjectARB = (CreateProgramObjectARB)GetProc<CreateProgramObjectARB>("glCreateProgramObjectARB");
			glAttachObjectARB = (AttachObjectARB)GetProc<AttachObjectARB>("glAttachObjectARB");
			glLinkProgramARB = (LinkProgramARB)GetProc<LinkProgramARB>("glLinkProgramARB");
			glUseProgramObjectARB = (UseProgramObjectARB)GetProc<UseProgramObjectARB>("glUseProgramObjectARB");
			glValidateProgramARB = (ValidateProgramARB)GetProc<ValidateProgramARB>("glValidateProgramARB");
			glUniform1fARB = (Uniform1fARB)GetProc<Uniform1fARB>("glUniform1fARB");
			glUniform2fARB = (Uniform2fARB)GetProc<Uniform2fARB>("glUniform2fARB");
			glUniform3fARB = (Uniform3fARB)GetProc<Uniform3fARB>("glUniform3fARB");
			glUniform4fARB = (Uniform4fARB)GetProc<Uniform4fARB>("glUniform4fARB");
			glUniform1iARB = (Uniform1iARB)GetProc<Uniform1iARB>("glUniform1iARB");
			glUniform2iARB = (Uniform2iARB)GetProc<Uniform2iARB>("glUniform2iARB");
			glUniform3iARB = (Uniform3iARB)GetProc<Uniform3iARB>("glUniform3iARB");
			glUniform4iARB = (Uniform4iARB)GetProc<Uniform4iARB>("glUniform4iARB");
			glUniform1fvARB = (Uniform1fvARB)GetProc<Uniform1fvARB>("glUniform1fvARB");

			//glUniform2fvARB = GetProc<Uniform2fvARB>("glUniform2fvARB");
			//glUniform3fvARB = GetProc<Uniform3fvARB>("glUniform3fvARB");
			//glUniform4fvARB = GetProc<Uniform4fvARB>("glUniform4fvARB");

			glUniform1ivARB = (Uniform1ivARB)GetProc<Uniform1ivARB>("glUniform1ivARB");
			glUniform2ivARB = (Uniform2ivARB)GetProc<Uniform2ivARB>("glUniform2ivARB");
			glUniform3ivARB = (Uniform3ivARB)GetProc<Uniform3ivARB>("glUniform3ivARB");
			glUniform4ivARB = (Uniform4ivARB)GetProc<Uniform4ivARB>("glUniform4ivARB");
			glUniformMatrix2fvARB = (UniformMatrix2fvARB)GetProc<UniformMatrix2fvARB>("glUniformMatrix2fvARB");
			glUniformMatrix3fvARB = (UniformMatrix3fvARB)GetProc<UniformMatrix3fvARB>("glUniformMatrix3fvARB");

			//glUniformMatrix4fvARB = GetProc<UniformMatrix4fvARB>("glUniformMatrix4fvARB");

			glGetObjectParameterfvARB = (GetObjectParameterfvARB)GetProc<GetObjectParameterfvARB>("glGetObjectParameterfvARB");
			glGetObjectParameterivARB = (GetObjectParameterivARB)GetProc<GetObjectParameterivARB>("glGetObjectParameterivARB");
			glGetInfoLogARB = (GetInfoLogARB)GetProc<GetInfoLogARB>("glGetInfoLogARB");
			glGetAttachedObjectsARB = (GetAttachedObjectsARB)GetProc<GetAttachedObjectsARB>("glGetAttachedObjectsARB");
			glGetUniformLocationARB = (GetUniformLocationARB)GetProc<GetUniformLocationARB>("glGetUniformLocationARB");
			glGetActiveUniformARB = (GetActiveUniformARB)GetProc<GetActiveUniformARB>("glGetActiveUniformARB");
			glGetUniformfvARB = (GetUniformfvARB)GetProc<GetUniformfvARB>("glGetUniformfvARB");
			glGetUniformivARB = (GetUniformivARB)GetProc<GetUniformivARB>("glGetUniformivARB");
			glGetShaderSourceARB = (GetShaderSourceARB)GetProc<GetShaderSourceARB>("glGetShaderSourceARB");

			glProgramParameteriARB = (ProgramParameteriARB)GetProc<ProgramParameteriARB>("glProgramParameteriARB");
			glFramebufferTextureARB = (FramebufferTextureARB)GetProc<FramebufferTextureARB>("glFramebufferTextureARB");
			glFramebufferTextureLayerARB = (FramebufferTextureLayerARB)GetProc<FramebufferTextureLayerARB>("glFramebufferTextureLayerARB");
			glFramebufferTextureFaceARB = (FramebufferTextureFaceARB)GetProc<FramebufferTextureFaceARB>("glFramebufferTextureFaceARB");
		}
	}
}


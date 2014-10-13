//
//  Program.cs
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
using X11;
using liboRg.OpenGL;
using System.Runtime.InteropServices;
using System.Common;
using System.Collections.Generic;

namespace liboRg.Framework
{
	public enum TransformFeedbackMode : uint
	{
		InterleavedAttribs = (uint)GL.INTERLEAVED_ATTRIBS,
		SeparateAttribs = (uint)GL.SEPARATE_ATTRIBS,
	}
	public class GlProgramHandle : Handle
	{
		protected IntPtr 		 m_iObject;

		public IntPtr glObject
		{
			get { return m_iObject; }
		}
		protected GlProgramHandle(string strName)
			: base(strName)
		{
			m_iObject = gl.glCreateProgramObjectARB();

			Register(true);
		}
	}

	public class Program : GlProgramHandle
	{
		public Program(string strName, Shader vertex, Shader fragment = null, Shader geometry = null)
			: base(strName)
		{
			Attach(vertex);
			if (fragment != null) Attach(fragment);
			if (geometry != null) Attach(geometry);
			Link();
			gl.glUseProgramObjectARB(m_iObject);

		}
		public void Attach( Shader shader )
		{
			gl.glAttachObjectARB(m_iObject, shader.glObject);
		}
		public void Link()
		{
			int res = 0;

			gl.glLinkProgramARB(m_iObject);
			gl.glGetProgramiv( m_iObject, (uint)GL.LINK_STATUS, ref res );

			if (res != (int)GL.TRUE)
				throw new System.Exception(GetInfoLog());

		}
		public string GetInfoLog()
		{
			int bufSize = 0;
			gl.glGetShaderiv( m_iObject, (uint)GL.INFO_LOG_LENGTH, ref bufSize );

			return (bufSize > 0 ? gl.glGetProgramInfoLogARB(m_iObject, bufSize) : "");
		}
		public void TransformFeedbackVaryings(string strvaryings, uint count, TransformFeedbackMode mode = TransformFeedbackMode.InterleavedAttribs )
		{

		}
		public int Attribute( string name )
		{
			return gl.glGetAttribLocation( m_iObject, name );
		}
		public int Uniform( string name )
		{
			return gl.glGetUniformLocationARB(m_iObject, name);
		}
		public void Uniform( int  uniform, int value )
		{
			gl.glUniform1iARB( uniform, value );
		}

		public void Uniform( int  uniform, float value )
		{
			gl.glUniform1fARB(uniform, value);
		}

		public void Uniform( int  uniform, Vector2 value )
		{
			gl.glUniform2fARB( uniform, value.X, value.Y );
		}

		public void Uniform( int  uniform, Vector3 value )
		{
			gl.glUniform3fARB( uniform, value.X, value.Y, value.Z );
		}

		public void Uniform( int  uniform, Vector4 value )
		{
			gl.glUniform4fARB( uniform, value.X, value.Y, value.Z, value.W );
		}
		public void Uniform( int  uniform, Color value )
		{
			gl.glUniform4fARB( uniform, value.R, value.G, value.B, value.A );
		}
		public void Uniform( int  uniform, float[] values, int count )
		{
			gl.glUniform1fvARB(uniform, count, values);
		}

		public void Uniform( int  uniform,  Vector2[] values, int count )
		{
			List<float> val = new List<float>();
			for (int i = 0; i < count; i++)
				{
					val.AddRange(values[i].ToArray());
				}
			gl.glUniform2fvARB( uniform, count, val.ToArray() );
		}

		public void Uniform( int  uniform,  Vector3[] values, int count )
		{
			List<float> val = new List<float>();
			for (int i = 0; i < count; i++)
			{
					val.AddRange(values[i].ToArray());
			}
			gl.glUniform3fvARB( uniform, count, val.ToArray() );
		}

		public void Uniform( int  uniform,  Vector4[] values, int count )
		{
			List<float> val = new List<float>();
			for (int i = 0; i < count; i++)
			{
				val.AddRange(values[i].ToArray());
			}
			gl.glUniform4fvARB(uniform, count, val.ToArray());
		}

	
		public void Uniform( int  uniform,  Matrix value )
		{
			gl.glUniformMatrix4fvARB( uniform, 1, (uint)GL.FALSE, value.ToArray() );
		}
		public void Uniform( int  uniform,  Matrix[] values, int count )
		{
			List<float> val = new List<float>();
			for (int i = 0; i < count; i++)
			{
				val.AddRange(values[i].ToArray());
			}
			gl.glUniformMatrix4fvARB( uniform, count, (uint)GL.FALSE, val.ToArray() );
		}
	}
}


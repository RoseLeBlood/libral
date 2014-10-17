//
//  Shader.cs
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
using liboRg.OpenGL;
using X11;
using System.Text;

namespace liboRg.Framework
{
	public enum ShaderType : uint
	{
		Vertex = (uint)GL.VERTEX_SHADER,
		Fragment = (uint)GL.FRAGMENT_SHADER,
		Geometry = (uint)GL.GEOMETRY_SHADER
	};

	public class GlShaderHandle : Handle
	{
		protected IntPtr 		 m_iObject;

		public IntPtr glObject
		{
			get { return m_iObject; }
		}
		protected GlShaderHandle(string strName, ShaderType type)
			: base(strName)
		{
			m_iObject = gl.glCreateShaderObjectARB((uint)type);

			Register(true);
		}
	}

	public class Shader : GlShaderHandle
	{
		private string m_strSource;

		public string Source
		{
			get { return m_strSource; }
			set
			{
				m_strSource = value;
				gl.glShaderSourceARB(m_iObject, 1, new string[] { m_strSource }, null);
				Compile();
			}
		}

		public Shader(string strName, ShaderType type, string source)
			: base("sh_" + strName, type)
		{
			m_strSource = source;
			gl.glShaderSourceARB(m_iObject, 1, new string[] { m_strSource }, null);
			Compile();
		}
		public Shader(string file, ShaderType type)
			: base("sh_" + System.IO.Path.GetFileName(file), type)
		{
			m_strSource = System.IO.File.ReadAllText(file);
			gl.glShaderSourceARB(m_iObject, 1, new string[] { m_strSource }, null);
			Compile();
		}

		public void Compile()
		{
			int res = 0;
			gl.glCompileShaderARB(m_iObject);

			gl.glGetShaderiv( m_iObject, (uint)GL.COMPILE_STATUS, ref res );

			if (res != (int)GL.TRUE)
			{
				var str = GetInfoLog();
				throw new System.Exception(GetInfoLog());
			}

		}
		public string GetInfoLog()
		{
			int bufSize = 0;
			gl.glGetShaderiv( m_iObject, (uint)GL.INFO_LOG_LENGTH, ref bufSize );

			return (bufSize > 0 ? gl.glGetShaderInfoLogARB(m_iObject, bufSize) : "");
		}
	}
}


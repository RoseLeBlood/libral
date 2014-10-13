//
//  GLUtil.cs
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
		public static string glGetShaderInfoLogARB(IntPtr shader, Int32 bufSize)
		{
			byte[] infoLog = new byte[bufSize];
			gl.glGetShaderInfoLog(shader, bufSize, IntPtr.Zero, infoLog);
			return System.Text.Encoding.UTF8.GetString(infoLog);
		}
		public static string glGetProgramInfoLogARB(IntPtr program, Int32 bufSize)
		{
			byte[] infoLog = new byte[bufSize];
			gl.glGetProgramInfoLog(program, bufSize, IntPtr.Zero, infoLog);
			return System.Text.Encoding.UTF8.GetString(infoLog);
		}
	}
}


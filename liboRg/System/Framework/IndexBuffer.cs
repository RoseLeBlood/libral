//
//  IndexBuffer.cs
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
using System.API.OpenGL;

namespace System.Framework
{
	public class IndexBuffer : GlHandle
	{
		public IndexBuffer(string strName)
			: base(strName, GlHandleType.Buffer)
		{
		}
		public IndexBuffer(string strName, IndexDataBuffer data, BufferUsage usage )
			: base(strName, GlHandleType.Buffer)
		{
			Data(data, usage);
		}
		public void Data( IndexDataBuffer data, BufferUsage usage )
		{
			byte[] d = data.ToArray();

			gl.glBindBufferARB(gl.VboTarget.ElementArrayBuffer, glObject);
			gl.glBufferDataARB(gl.VboTarget.ElementArrayBuffer, d.Length, d, (gl.VboUsage) usage);
		}
		public void Data( int lenght, BufferUsage usage )
		{
			gl.glBindBufferARB(gl.VboTarget.ElementArrayBuffer, glObject);
			gl.glBufferDataARB(gl.VboTarget.ElementArrayBuffer, lenght, null, (gl.VboUsage) usage);
		}
	}
}


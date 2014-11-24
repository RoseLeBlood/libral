//
//  VertexBuffer.cs
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
using System.IO;
using System.Common;
using System.Runtime.InteropServices;
using System.API.OpenGL;

namespace System.Framework
{
	public enum BufferUsage : uint
	{
		StreamDraw = (uint) GL.STREAM_DRAW,
		StreamRead = (uint) GL.STREAM_READ,
		StreamCopy = (uint) GL.STREAM_COPY,
		StaticDraw = (uint) GL.STATIC_DRAW,
		StaticRead = (uint) GL.STATIC_READ,
		StaticCopy = (uint) GL.STATIC_COPY,
		DynamicDraw = (uint) GL.DYNAMIC_DRAW,
		DynamicRead = (uint) GL.DYNAMIC_READ,
		DynamicCopy = (uint) GL.DYNAMIC_COPY
	}

	public delegate void MeshFunction(Vertex v, VertexDataBuffer data);
	public class VertexBuffer : GlHandle
	{
		public VertexBuffer(string strName)
			: base(strName, GlHandleType.Buffer)
		{
		}
		public VertexBuffer(string strName, VertexDataBuffer data, BufferUsage usage )
			: base(strName, GlHandleType.Buffer)
		{
			Data(data, usage);
		}
		public  VertexBuffer(Mesh mesh, BufferUsage usage, MeshFunction f )
			: base(mesh.Name, GlHandleType.Buffer)
		{

		}


		public void Data( VertexDataBuffer data, BufferUsage usage )
		{
			byte[] d = data.ToArray();

			gl.glBindBufferARB(gl.VboTarget.ArrayBuffer, glObject);
			gl.glBufferDataARB(gl.VboTarget.ArrayBuffer, d.Length, d, (gl.VboUsage) usage);
		}
		public void Data( int lenght, BufferUsage usage )
		{
			gl.glBindBufferARB(gl.VboTarget.ArrayBuffer, glObject);
			gl.glBufferDataARB(gl.VboTarget.ArrayBuffer, lenght, null, (gl.VboUsage) usage);
		}
		public void BindBuffer()
		{
			gl.glBindBufferARB(gl.VboTarget.ArrayBuffer, glObject);
		}
		public void EnableVertexAttribArray(uint attr)
		{
			gl.glEnableVertexAttribArray(attr);
		}
		public void SubData( VertexDataBuffer data, int offset )
		{
			byte[] d = data.ToArray();

			gl.glBindBufferARB(gl.VboTarget.ArrayBuffer, glObject);
			gl.glBufferSubDataARB(gl.VboTarget.ArrayBuffer, offset, d.Length, d);
		}
	}
}


//
//  VertexArray.cs
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
	public enum DataType : uint
	{
		Byte = (uint)GL.BYTE,
		UnsignedByte = (uint)GL.UNSIGNED_BYTE,
		Short = (uint)GL.SHORT,
		UnsignedShort = (uint)GL.UNSIGNED_SHORT,
		Int = (uint)GL.INT,
		UnsignedInt = (uint)GL.UNSIGNED_INT,
		Float = (uint)GL.FLOAT,
		Double = (uint)GL.DOUBLE
	}
	public class VertexArray : GlHandle
	{
		public VertexArray(string strName)
			: base(strName, GlHandleType.VertexArray)
		{

		}

		public void BindAttribute( int attribute, VertexBuffer buffer, DataType type, int count,
			int stride, IntPtr offset )
		{

			gl.glBindVertexArray(glObject);
			gl.glBindBufferARB(gl.VboTarget.ArrayBuffer, buffer.glObject);
			gl.glEnableVertexAttribArrayARB((uint)attribute);
			gl.glVertexAttribPointerARB((uint)attribute, count, (uint)type, (uint)GL.FALSE, stride, offset);
		}
		public void BindElements(VertexBuffer elements )
		{
			gl.glBindVertexArray(glObject);
			gl.glBindBufferARB( gl.VboTarget.ElementArrayBuffer, elements.glObject );
		}

		public void  BindTransformFeedback( int index, VertexBuffer buffer )
		{
			gl.glBindVertexArray(glObject);
			gl.glBindBufferBase((uint)GL.TRANSFORM_FEEDBACK_BUFFER, index, buffer.glObject);
		}
	}
}


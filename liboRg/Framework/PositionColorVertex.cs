//
//  PositionColorVertex.cs
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
using System.Common;

namespace liboRg.Framework
{
	public class PositionColorVertex
	{
		VertexDataBuffer m_vboDataPosition = new VertexDataBuffer();
		VertexDataBuffer m_vboDataColor = new VertexDataBuffer();

		public void Add(Vector3 vPosition, Color cColor)
		{
			m_vboDataPosition.Vector3(vPosition);
			m_vboDataColor.Color3(cColor);
		}
		public virtual void BindAttribute(Program program, VertexArray vao)
		{
			vao.BindAttribute( 0, 
				new VertexBuffer("vbo", m_vboDataPosition, BufferUsage.StaticDraw), 
				DataType.Float, 3, 0, IntPtr.Zero );

			vao.BindAttribute( 1, 
				new VertexBuffer("vbo", m_vboDataColor, BufferUsage.StaticDraw), 
				DataType.Float, 3, 0, IntPtr.Zero );
		}
	}
}


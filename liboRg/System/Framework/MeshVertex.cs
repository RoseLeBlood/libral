//
//  PositionColorNormalVertexTextured.cs
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
using System.Common;
using System.Collections.Generic;

namespace System.Framework
{
	public class MeshVertex : Vertex
	{
		VertexDataBuffer m_vboDataPosition = new VertexDataBuffer();
		VertexDataBuffer m_vboDataTexture = new VertexDataBuffer();
		VertexDataBuffer m_vboDataNormal = new VertexDataBuffer();

		public MeshVertex(Mesh mesh)
			: base("MeshVertex_" + mesh.Name)
		{

		}
		public void Add(Vector3 vPosition, Vector2 vTexture, Vector3 vNormal)
		{
			m_vboDataPosition.Vector3(vPosition);
			m_vboDataTexture.Vector2(vTexture);
			m_vboDataNormal.Vector3(vNormal);
		}

		public override void BindAttribute(Program program, VertexArray vao)
		{
			vao.BindAttribute( program.Attribute("inPosition"), 
				new VertexBuffer(this.Name + "_Position", m_vboDataPosition, BufferUsage.StaticDraw), 
				DataType.Float, 3, 0, IntPtr.Zero );

			vao.BindAttribute( program.Attribute("inTexture"), 
				new VertexBuffer(this.Name + "_Texture", m_vboDataTexture, BufferUsage.StaticDraw), 
				DataType.Float, 2, 0, IntPtr.Zero );

			vao.BindAttribute( program.Attribute("inNormal"), 
				new VertexBuffer(this.Name + "_Normal", m_vboDataNormal, BufferUsage.StaticDraw), 
				DataType.Float, 3, 0, IntPtr.Zero );
		}
	}
}


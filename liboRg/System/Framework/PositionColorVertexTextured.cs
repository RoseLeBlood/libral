//
//  PositionColorVertexTextured.cs
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
	public class PositionColorVertexTextured : Vertex
	{
		VertexDataBuffer m_vboDataPosition = new VertexDataBuffer();
		VertexDataBuffer m_vboDataColor = new VertexDataBuffer();

		public PositionColorVertexTextured(string strName)
			: base(strName)
		{

		}
		public void Add(Vector3 vPosition, Color cColor)
		{
			m_vboDataPosition.Vector3(vPosition);
			m_vboDataColor.Color3(cColor);
		}

		public override void BindAttribute(Program program, VertexArray vao)
		{
			vao.BindAttribute( program.Attribute("inPosition"), 
				new VertexBuffer(this.Name + "_Position", m_vboDataPosition, BufferUsage.StaticDraw), 
				DataType.Float, 3, 0, IntPtr.Zero );

			vao.BindAttribute( program.Attribute("inColor"), 
				new VertexBuffer(this.Name + "_Color", m_vboDataColor, BufferUsage.StaticDraw), 
				DataType.Float, 3, 0, IntPtr.Zero );
		}

		public virtual void SetTexture(Program program, string ShaderName, GL TextureID, Texture texture)
		{
			gl.glActiveTexture((uint)GL.TEXTURE0);
			gl.glBindTexture((uint)GL.TEXTURE_2D, texture.glObject);

			var g = program.Uniform(ShaderName);
			program.Uniform(g, texture.glObject);
		}
	}
}


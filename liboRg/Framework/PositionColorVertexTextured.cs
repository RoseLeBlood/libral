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
using liboRg.OpenGL;

namespace liboRg.Framework
{
	public class PositionColorVertexTextured : PositionColorVertex
	{
		VertexDataBuffer m_vboDataPosition = new VertexDataBuffer();
		VertexDataBuffer m_vboDataColor = new VertexDataBuffer();


		public virtual void SetTexture(Program program, string ShaderName, GL TextureID, Texture texture)
		{
			gl.glActiveTexture((uint)GL.TEXTURE0);
			gl.glBindTexture((uint)GL.TEXTURE_2D, texture.glObject);

			var g = program.Uniform(ShaderName);
			program.Uniform(g, texture.glObject);
		}
	}
}


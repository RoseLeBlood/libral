//
//  Renderbuffer.cs
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
	public class Renderbuffer : GlHandle
	{
		public Renderbuffer(string strName, uint width, uint height, TextureInternalFormat format )
			: base(strName, GlHandleType.Renderbuffer)
		{
			Storage( width, height, format );
		}


		public void Storage( uint width, uint height, TextureInternalFormat format )
		{
			gl.glBindRenderbuffer( (uint)GL.RENDERBUFFER, glObject );
			gl.glRenderbufferStorage( (uint)GL.RENDERBUFFER, format, width, height );


		}
	}
}


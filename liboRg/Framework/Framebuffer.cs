//
//  Framebuffer.cs
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
using System.Common;

namespace liboRg.Framework
{
	public class Framebuffer :GlHandle
	{
		private Texture m_texColor;
		private Texture m_texDepth;

		public Texture Texture
		{
			get { return m_texColor; }
		}
		public Texture DepthTexture
		{
			get { return m_texDepth; }
		}
		public Framebuffer(string strName)
			: base(strName, GlHandleType.Framebuffer)
		{
		}
		public Framebuffer(string strName, uint width, uint height, byte color = 32, byte depth = 24)
			: base(strName, GlHandleType.Framebuffer)
		{
			var id = PushState();
			TextureInternalFormat colorFormat;
			if ( color == 24 ) colorFormat = TextureInternalFormat.RGB;
			else if ( color == 32 ) colorFormat = TextureInternalFormat.RGBA;
			else throw new System.Exception("colorFormat");

			TextureInternalFormat depthFormat;
			if ( depth == 8 ) depthFormat = TextureInternalFormat.DepthComponent;
			else if ( depth == 16 ) depthFormat = TextureInternalFormat.DepthComponent16;
			else if ( depth == 24 ) depthFormat = TextureInternalFormat.DepthComponent24;
			else if ( depth == 32 ) depthFormat = TextureInternalFormat.DepthComponent32F;
			else throw new System.Exception("depthFormat");

			gl.glBindFramebuffer( (uint)GL.DRAW_FRAMEBUFFER, base.glObject );
			m_texColor = new Texture("Color" + strName, null, TextureDataType.Byte, TextureFormat.RGBA, 
				new Size((int)width, (int)height), colorFormat);
			m_texColor.SetWrapping(TextureWrapping.ClampEdge, TextureWrapping.ClampEdge );
			m_texColor.SetFilters( TextureFilter.Linear, TextureFilter.Linear );
			gl.glFramebufferTexture2D( (uint)GL.DRAW_FRAMEBUFFER, (uint)GL.COLOR_ATTACHMENT0, (uint)GL.TEXTURE_2D, m_texColor.glObject, 0 );

			if ( depth > 0 ) 
			{
				m_texDepth = new Texture("Depth" + strName);
				gl.glBindTexture( (uint)GL.TEXTURE_2D, m_texDepth.glObject );
					gl.glTexImage2D( (uint)GL.TEXTURE_2D, 0,(int) depthFormat,
						(int)width, (int)height, 0, (uint)GL.DEPTH_COMPONENT, (uint)GL.UNSIGNED_BYTE, null );
				m_texDepth.SetWrapping( TextureWrapping.ClampEdge, TextureWrapping.ClampEdge );
				m_texDepth.SetFilters( TextureFilter.Nearest, TextureFilter.Nearest );
				gl.glFramebufferTexture2D( (uint)GL.DRAW_FRAMEBUFFER, (uint)GL.DEPTH_ATTACHMENT, (uint)GL.TEXTURE_2D, m_texDepth.glObject, 0 );
			}
			GL t = (GL)gl.glCheckFramebufferStatus((uint)GL.DRAW_FRAMEBUFFER);

			if ( t != GL.FRAMEBUFFER_COMPLETE )
				throw new System.Exception("glCheckFramebufferStatus");

			PopState(id);

		}
		~Framebuffer()
		{
			Dispose(false);
		}

		protected uint PushState()
		{
			int[] restoreId = new int[1]; 
			gl.glGetIntegerv( (uint)GL.DRAW_FRAMEBUFFER_BINDING, restoreId );
			return (uint)restoreId[0];
		}
		protected void PopState(uint restoreId)
		{
			gl.glBindFramebuffer( (uint)GL.DRAW_FRAMEBUFFER, restoreId );
		}
	}
}


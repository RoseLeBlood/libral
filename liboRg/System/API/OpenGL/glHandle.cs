//
//  glHandle.cs
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
using System.Framework;

namespace System.API.OpenGL
{
	public enum GlHandleType
	{
		Texture,
		Framebuffer,
		VertexArray,
		Renderbuffer,
		Buffer

	}
	public class GlHandle : OpenGLHandle
	{
		protected uint[] 		 m_iObject;
		protected GlHandleType 	 m_eType;

		public uint glObject
		{
			get { return m_iObject[0]; }
		}
		public int Length
		{
			get { return m_iObject.Length; }
		}

		protected GlHandle(string strName, GlHandleType type, int num = 1)
			: base(strName)
		{
			if (num <= 0) num = 1;
			m_iObject = new uint[num];
			m_eType = type;

			switch (m_eType)
			{
				case GlHandleType.Texture:
					gl.glGenTextures(Length, m_iObject);
					break;
				case GlHandleType.Framebuffer:
					gl.glGenFramebuffers(Length, m_iObject);
					break;
				case GlHandleType.VertexArray:
					gl.glGenVertexArrays(Length, m_iObject);
					break;
				case GlHandleType.Buffer:
					gl.glGenBuffersARB(Length, m_iObject);
					break;
				case GlHandleType.Renderbuffer:
					gl.glGenRenderbuffers(Length, m_iObject);
					break;
				default:
					throw new System.Exception("");
				
			}
			Register(true);
		}


		protected override void CleanUpUnManagedResources()
		{
			switch (m_eType)
			{
				case GlHandleType.Texture:
					gl.glDeleteTextures(Length, m_iObject);
					break;
				case GlHandleType.Framebuffer:
					gl.glDeleteFramebuffers(Length, m_iObject);
					break;
				case GlHandleType.VertexArray:
					gl.glDeleteVertexArrays(Length, m_iObject);
					break;
				case GlHandleType.Buffer:
					gl.glDeleteBuffersARB(Length, m_iObject);
					break;
				case GlHandleType.Renderbuffer:
					gl.glDeleteRenderbuffers(Length, m_iObject);
					break;
			}
		}
	}
}


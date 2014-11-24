//
//  IContext.cs
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
using liboRg.Window;
using System.Framework;
using System.API.OpenGL;
using System.API.Platform.Linux;
using System.API.Platform;

namespace liboRg.Context
{

	public enum Buffer : ulong
	{
		Color = GL.COLOR_BUFFER_BIT,			
		Depth = GL.DEPTH_BUFFER_BIT,			
		Stencil = GL.STENCIL_BUFFER_BIT		
	}

	public enum Primitive : ulong
	{
		Quads = GL.QUADS,
		Triangles = GL.TRIANGLES,
		Lines = GL.LINES,
		Points = GL.POINTS,
		TrianglesStrip = GL.TRIANGLE_STRIP,
		LineStrip = GL.LINE_STRIP,
	}

	public enum Capability : ulong
	{
		DepthTest = GL.DEPTH_TEST,
		StencilTest = GL.STENCIL_TEST,
		CullFace = GL.CULL_FACE,
		RasterizerDiscard = GL.RASTERIZER_DISCARD
	}	

	public enum TestFunction : ulong
	{
		Never = GL.NEVER,
		Less = GL.LESS,
		LessEqual = GL.LEQUAL,
		Greater = GL.GREATER,
		GreaterEqual = GL.GEQUAL,
		Equal = GL.EQUAL,
		NotEqual = GL.NOTEQUAL,
		Always = GL.ALWAYS
	}

	public enum StencilAction : ulong
	{
		Keep = GL.KEEP,
		Zero = GL.ZERO,
		Replace = GL.REPLACE,
		Increase = GL.INCR,
		IncreaseWrap = GL.INCR_WRAP,
		Decrease = GL.DECR,
		DecreaseWrap = GL.DECR_WRAP,
		Invert = GL.INVERT
	};

	public class GameContext : OpenGLHandle
	{
		protected Color m_pClearColor;
		protected IGLNativeContext m_pNativeContext;
		private   bool			   m_bDepthMark;

		internal IGLNativeContext NativeContext
		{
			get { return m_pNativeContext; }
		}

		public Color ClearColor
		{
			get { return m_pClearColor; }
			set { m_pClearColor = value; gl.glClearColor(m_pClearColor.Red, m_pClearColor.Green, m_pClearColor.Blue, m_pClearColor.Alpha); }
		}

		public bool DepthMask
		{
			get 
			{
				return m_bDepthMark; 
			}
			set 
			{
				gl.glDepthMask( value ? gl.boolean.TRUE : gl.boolean.FALSE );
				m_bDepthMark = value;
			}
		}

		public VSyncMode VSync
		{
			get { return m_pNativeContext.VScyn; }
			set { m_pNativeContext.VScyn = value; }
		}


		public IGLNativeContext Handle
		{
			get { return m_pNativeContext; }
		}
		public IWindow Window
		{
			get { return m_pNativeContext.Window; }
		}
		public Rectangle Viewport
		{
			get { return m_pNativeContext.Viewport; }
			set { m_pNativeContext.Viewport = value; }
		}
		public GameContext(IWindow window, GameContextConfig pConfig)
			: base("Context")
		{
			m_pNativeContext = PlatformFactory.GetContext(window, pConfig);

			Register();
		}

		public void CreateContext()
		{
			m_pNativeContext.CreateContext();
		}
		public virtual void Activate()
		{
			m_pNativeContext.Activate();
		}
		public virtual void DeActivate()
		{
			m_pNativeContext.DeActivate();
		}
		public virtual void Enable( Capability capability )
		{
			gl.glEnable((uint)capability);
		}
		public virtual void Disable( Capability capability )
		{
			gl.glDisable((uint)capability);
		}
		public void Clear(Buffer buffers, Color color, double depth, int stencil)
		{
			Activate();
			gl.glClearColor(color.R, color.G, color.B, color.A);
			gl.glClearDepth(depth);
			gl.glClearStencil(stencil);
			gl.glClear((int)buffers);
		}

		public void Clear(Buffer buffers, Color color, double depth)
		{
			Activate();
			gl.glClearColor(color.R, color.G, color.B, color.A);
			gl.glClearDepth(depth);
			gl.glClear((int)buffers);
		}
		public void Clear(Buffer buffers, Color color)
		{
			Activate();
			gl.glClearColor(color.R, color.G, color.B, color.A);
			gl.glClear((int)buffers);
		}

		public void Clear( Buffer buffers = Buffer.Color | Buffer.Depth )
		{
			Activate();
			gl.glClear((int)buffers);
		}

		public void Swap()
		{
			m_pNativeContext.Present();
		}

		public  virtual void StencilMask( bool writeEnabled )
		{
			unchecked
			{
				gl.glStencilMask(writeEnabled ? (uint)~0 : (uint)0);
			}
		}
		public virtual void StencilMask( uint mask )
		{
			gl.glStencilMask( mask );
		}

		public virtual void StencilFunc( TestFunction function, int reference, int mask = ~0 )
		{
			gl.glStencilFunc((uint) function, reference, (uint)mask );
		}
		public virtual void StencilOp( StencilAction fail, StencilAction zfail, StencilAction pass )
		{
			gl.glStencilOp((uint) fail, (uint)zfail, (uint)pass );
		}
		public void UseProgram( Program program )
		{
			gl.glUseProgramObjectARB(program.RawHandle);
		}
		public void BindFramebuffer( Framebuffer framebuffer )
		{
			gl.glBindFramebuffer( (uint)GL.DRAW_FRAMEBUFFER, framebuffer.glObject );

			// Set viewport to frame buffer size
			uint[] obj = new uint[1]; 
			gl.glGetFramebufferAttachmentParameteriv( (uint)GL.DRAW_FRAMEBUFFER, (uint)GL.COLOR_ATTACHMENT0, 
				(uint)GL.FRAMEBUFFER_ATTACHMENT_OBJECT_NAME, obj );

			int[] res = new int[1]; 
			int[] width = new int[1];
			int[] height = new int[1];

			gl.glGetIntegerv( (uint)GL.TEXTURE_BINDING_2D, res );
			gl.glBindTexture( (uint)GL.TEXTURE_BINDING_2D, obj[0] );
			gl.glGetTexLevelParameteriv((uint)GL.TEXTURE_BINDING_2D, 0, (uint)GL.TEXTURE_WIDTH, width); 
			gl.glGetTexLevelParameteriv((uint)GL.TEXTURE_BINDING_2D, 0, (uint)GL.TEXTURE_HEIGHT, height);

			gl.glBindTexture( (uint)GL.TEXTURE_BINDING_2D,(uint)res[0] );

			gl.glViewport( 0, 0, width[0], height[0] );
		}
		public virtual void BindFramebuffer()
		{
			gl.glBindFramebuffer( (uint)GL.DRAW_FRAMEBUFFER, 0 );

			// Set viewport to default frame buffer size
			gl.glViewport( m_pNativeContext.DefaultViewport.X, 
				m_pNativeContext.DefaultViewport.Y, 
				m_pNativeContext.DefaultViewport.Width, 
				m_pNativeContext.DefaultViewport.Height );
		}

		public virtual void BeginTransformFeedback( Primitive mode )
		{
			gl.glBeginTransformFeedback( (uint)mode );
		}
		public virtual void EndTransformFeedback()
		{
			gl.glEndTransformFeedback();
		}

		public void DrawArrays( VertexArray vao, Primitive mode, int offset, int vertices )
		{
			gl.glBindVertexArray( vao.glObject );
			gl.glDrawArrays( (uint) mode, offset, vertices );
		}
		public void DrawElements( VertexArray vao, Primitive mode, int[] indices, int count, uint type )
		{
			gl.glBindVertexArray( vao.glObject );
			gl.glDrawElements( (uint) mode, count, type, indices );
		}

	}
}


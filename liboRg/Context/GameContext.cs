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
using X11;
using System.Common;
using liboRg.OpenGL;
using liboRg.Window;
using X11.Widgets;

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
		Triangles = GL.TRIANGLES,
		Lines = GL.LINES,
		Points = GL.POINTS,
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

	public class GameContext : Handle
	{
		protected Color m_pClearColor;
		protected IGLNativeContext m_pNativeContext;
		private   bool			   m_bDepthMark;

		public Color ClearColor
		{
			get { return m_pClearColor; }
			set { m_pClearColor = value; OpenGL.gl.glClearColor(m_pClearColor.Red, m_pClearColor.Green, m_pClearColor.Blue, m_pClearColor.Alpha); }
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
		public double Time
		{
			get { return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds; }
		}

		public IGLNativeContext Handle
		{
			get { return m_pNativeContext; }
		}
		public BaseWindow Window
		{
			get { return m_pNativeContext.Window; }
		}
		public Rectangle Viewport
		{
			get { return m_pNativeContext.Viewport; }
			set { m_pNativeContext.Viewport = value; }
		}
		public GameContext(string name)
			: base(name)
		{

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

		//void UseProgram(  ref Program program );

		//void BindTexture( ref Texture texture, byte unit );

		//void BindFramebuffer( ref Framebuffer framebuffer );
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

		//void DrawArrays( VertexArray vao, Primitive mode, uint offset, uint vertices );
		//void DrawElements( VertexArray vao, Primitive mode, IntPtr offset, uint count, uint type );

	}
}


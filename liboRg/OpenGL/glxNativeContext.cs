//
//  glxContext.cs
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
using System.Collections.Generic;
using System.Common;
using System.Runtime.InteropServices;
using System.Security;
using X11;
using X11.Widgets;
using X11._internal;
using liboRg.Context;
using liboRg.Window;

namespace liboRg.OpenGL
{

	public class glxNativeContext : UnmanagedHandle, IGLNativeContext
	{
		private BaseGameWindow m_pWindow;
		private bool           m_bOwned;
		private Rectangle      m_rDefaultViewport;
		private VSyncMode	   m_bVsync = VSyncMode.Disable;

		public BaseGameWindow Window
		{
			get { return m_pWindow; }
		}
		public bool			Owned 
		{ 
			get { return m_bOwned; }
		}
		public Rectangle	DefaultViewport 
		{ 
			get { return m_rDefaultViewport; } 
		}
		public VSyncMode VScyn 
		{ 
			get
			{
				return m_bVsync;
			}
			set
			{
				if (glXSwapIntervalEXT != null)
				{
					glXSwapIntervalEXT(m_pWindow.Display.RawHandle, m_pWindow.RawHandle, (int)value);
					m_bVsync = value;
				}
				else
				{
					#if DEBUG
					Console.WriteLine("SwapIntervalEXT not Supported Fallback to SwapIntervalSGI");
					#endif
					value = (value == VSyncMode.Adaptive || value == VSyncMode.Enable ? VSyncMode.Enable : VSyncMode.Disable);
					glXSwapIntervalSGI((int)value);
					m_bVsync = value;
				}
				#if DEBUG
				Console.WriteLine("[GLX] Set VSync Mode: {0}", value);
				#endif
			}
		}
		public Rectangle	Viewport 
		{ 
			get
			{
				int[] rect = new int[4];
				//struct { GLint x, y, w, h; } rect;
				gl.glGetIntegerv( (uint)GL.VIEWPORT, rect);

				return new Rectangle(rect);
			}
			set
			{
				gl.glViewport(value.X, value.Y, value.Width, value.Height);
			}
		}

		public glxNativeContext() 
			: base("OPENGL_CONTEXT", IntPtr.Zero)
		{
			LoadExtension();
			m_bOwned = false;
			m_rDefaultViewport = Viewport;
		}

		public glxNativeContext(BaseGameWindow handle, GameContextConfig pConfig) 
			: base("OPENGL_CONTEXT", IntPtr.Zero)
		{
			m_pWindow = handle;
			LoadExtension();

			CreateContext(pConfig);
			m_bOwned = true;
		
			m_rDefaultViewport = Viewport;
		}
		protected unsafe virtual void CreateContext(GameContextConfig pConfig)
		{
			int[] visual_attribs =
			{
				(int)GLX.X_RENDERABLE, (int)GL.TRUE,
				(int)GLX.DRAWABLE_TYPE, (int)GLX.WINDOW_BIT,
				(int)GLX.DOUBLEBUFFER, (int)GL.TRUE,
				(int)GLX.RENDER_TYPE, (int)GLX.RGBA_BIT,
				(int)GLX.X_VISUAL_TYPE, (int)GLX.TRUE_COLOR,
				(int)GLX.BUFFER_SIZE, pConfig.Color,
				(int)GLX.DEPTH_SIZE, pConfig.Depth,
				(int)GLX.STENCIL_SIZE, pConfig.Stencil,
				(int)GLX.SAMPLE_BUFFERS, pConfig.Antialias > 1 ? (int)GL.TRUE : (int)GL.FALSE,
				(int)GLX.SAMPLES, pConfig.Antialias > 1 ? (int)pConfig.Antialias : 0,
				0
			};
										
			int glx_major, glx_minor;
			if (!glXQueryVersion(m_pWindow.Display.RawHandle, out glx_major, out glx_minor) ||
			    ((glx_major == 1) && (glx_minor < 3)) || (glx_major < 1))
			{
				Console.WriteLine("Invalid GLX version");
					throw new System.Exception("[GLX] not supported");
				
				}
			#if DEBUG
			Console.WriteLine("glX String: {0}.{1}", glx_major, glx_minor);
			#endif

			int fbcount;
			IntPtr* fbc = glXChooseFBConfig(m_pWindow.Display.RawHandle, 
				m_pWindow.Display.Screen.ScreenNumber,
				visual_attribs, out fbcount);

			if ( fbcount == 0 ) throw new System.Exception();
			IntPtr config = fbc[0];



			int[] attribs = {
					(int)GLX.CONTEXT_MAJOR_VERSION_ARB, 3,
					(int)GLX.CONTEXT_MINOR_VERSION_ARB, 2,
					(int)GLX.CONTEXT_PROFILE_MASK_ARB, (int)GLX.CONTEXT_CORE_PROFILE_BIT_ARB,
					0
				};
			m_pHandle =  glXCreateContextAttribsARB( m_pWindow.Display.RawHandle, config, 
				IntPtr.Zero, true, attribs );
			if ( m_pHandle == IntPtr.Zero ) throw new System.Exception("[GLX] OpenGL Version 3.2 not supported");

			glXMakeCurrent( m_pWindow.Display.RawHandle, m_pWindow.RawHandle, m_pHandle );

			VScyn = pConfig.VSync;

			Register();

		}
		public virtual void Activate()
		{
			if (Owned && glXGetCurrentContext() != RawHandle)
				MakeCurrent();
		}
		public virtual void Present()
		{
			if ( RawHandle == IntPtr.Zero ) return;

			Activate();
			Swap();
		}

		public void MakeCurrent()
		{
			glXMakeCurrent( m_pWindow.Display.RawHandle, m_pWindow.RawHandle, m_pHandle );
		}
		public void Swap()
		{
			glXSwapBuffers(m_pWindow.Display.RawHandle, m_pWindow.RawHandle);
		}
		public Delegate GetProc<T>(string name)
		{
			var ptr = glXGetProcAddressARB(name);
			if (ptr == IntPtr.Zero)
				return null;

			return Marshal.GetDelegateForFunctionPointer(ptr, typeof(T));
		}
		protected override void CleanUpUnManagedResources()
		{
			if ( !m_bOwned ) return;

			glXMakeCurrent( Window.Display.RawHandle, IntPtr.Zero, IntPtr.Zero );
			glXDestroyContext( Window.Display.RawHandle, m_pHandle );
		}
		private void LoadExtension()
		{
			glXSwapIntervalSGI = (SwapIntervalSGI)GetProc<SwapIntervalSGI>("glXSwapIntervalSGI");
			glXSwapIntervalMESA = (SwapIntervalMESA)GetProc<SwapIntervalMESA>("glXSwapIntervalMESA");
			glXGetSwapIntervalMESA = (GetSwapIntervalMESA)GetProc<GetSwapIntervalMESA>("glXGetSwapIntervalMESA");
			glXSwapIntervalEXT = (SwapIntervalEXT)GetProc<SwapIntervalEXT>("glXSwapIntervalEXT");
			glXCreateContextAttribsARB = (CreateContextAttribsARB)GetProc<CreateContextAttribsARB>("glXCreateContextAttribsARB");

			gl.LoadExtension(this);
		}

		public const string DllName = "libGL.so";


		[DllImport(DllName, EntryPoint = "glXIsDirect")]
		public static extern bool IsDirect(IntPtr dpy, IntPtr context);

		//[DllImport(DllName, EntryPoint = "glXQueryDrawable")]
		//public static extern int QueryDrawable(IntPtr dpy, IntPtr drawable, GLXAttribute attribute, out int value);

		[DllImport(DllName, EntryPoint = "glXQueryExtension")]
		public static extern bool glXQueryExtension(IntPtr dpy, out int errorBase, out int eventBase);

		[DllImport(DllName, EntryPoint = "glXQueryExtensionsString")]
		static extern IntPtr QueryExtensionsString(IntPtr dpy, int screen);

		public static string glXQueryExtensionsString(IntPtr dpy, int screen)
		{
			return Marshal.PtrToStringAnsi(QueryExtensionsString(dpy, screen));
		}

		[DllImport(DllName, EntryPoint = "glXQueryVersion")]
		public static extern bool glXQueryVersion(IntPtr dpy, out int major, out int minor);

		[DllImport(DllName, EntryPoint = "glXCreateContext")]
		public static extern IntPtr glXCreateContext(IntPtr dpy, IntPtr vis, IntPtr shareList, bool direct);


		[DllImport(DllName, EntryPoint = "glXDestroyContext")]
		public static extern void glXDestroyContext(IntPtr dpy, IntPtr context);

	

		[DllImport(DllName, EntryPoint = "glXGetCurrentContext")]
		public static extern IntPtr glXGetCurrentContext();

		[DllImport(DllName, EntryPoint = "glXMakeCurrent")]
		public static extern bool glXMakeCurrent(IntPtr display, IntPtr drawable, IntPtr context);

		[DllImport(DllName, EntryPoint = "glXSwapBuffers")]
		public static extern void glXSwapBuffers(IntPtr display, IntPtr drawable);

		[DllImport(DllName, EntryPoint = "glXChooseVisual")]
		public extern static IntPtr ChooseVisual(IntPtr dpy, int screen, IntPtr attriblist);

		/*[DllImport(DllName, EntryPoint = "glXChooseVisual")]
		public extern static IntPtr ChooseVisual(IntPtr dpy, int screen, ref int attriblist);*/

		public static IntPtr glXChooseVisual(IntPtr dpy, int screen, int[] attriblist)
		{
			unsafe
			{
				fixed (int* attriblist_ptr = attriblist)
				{
					return ChooseVisual(dpy, screen, (IntPtr)attriblist_ptr);
				}
			}
		}

		// Returns an array of GLXFBConfig structures.
		[DllImport(DllName, EntryPoint = "glXChooseFBConfig")]
		unsafe public extern static IntPtr* glXChooseFBConfig(IntPtr dpy, int screen, int[] attriblist, out int fbount);


		// Returns a pointer to an XVisualInfo structure.
		[DllImport(DllName, EntryPoint = "glXGetVisualFromFBConfig")]
		public unsafe extern static IntPtr glXGetVisualFromFBConfig(IntPtr dpy, IntPtr fbconfig);

		[DllImport(DllName, EntryPoint = "glXGetProcAddressARB")]
		public static extern IntPtr glXGetProcAddressARB([MarshalAs(UnmanagedType.LPTStr)] string procName);


		public delegate IntPtr CreateContextAttribsARB(IntPtr display, IntPtr fbconfig, IntPtr share_context, bool direct, int[] attribs);
		public static CreateContextAttribsARB glXCreateContextAttribsARB = null;

		public delegate int SwapIntervalSGI ( int interval );
		public static SwapIntervalSGI glXSwapIntervalSGI = null;

		public delegate int SwapIntervalMESA ( uint interval );
		public static SwapIntervalMESA glXSwapIntervalMESA = null;

		public delegate int GetSwapIntervalMESA ();
		public static GetSwapIntervalMESA glXGetSwapIntervalMESA = null;

		public delegate int SwapIntervalEXT(IntPtr display, IntPtr drawable, int interval);
		public static SwapIntervalEXT glXSwapIntervalEXT = null;

	}
}


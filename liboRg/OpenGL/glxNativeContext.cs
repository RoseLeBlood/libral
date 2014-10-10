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
using liboRg.GLX;

namespace liboRg.OpenGL
{
	[StructLayout(LayoutKind.Sequential)]
	public struct XVisualInfo
	{
		public IntPtr Visual;
		public IntPtr VisualID;
		public int Screen;
		public int Depth;
		public int Class;
		public long RedMask;
		public long GreenMask;
		public long blueMask;
		public int ColormapSize;
		public int BitsPerRgb;

		public override string ToString()
		{
			return String.Format("VisualID: {0}, RedMask: {1} GreenMask: {2} blueMask: {3}  Depth: {4}",
				VisualID,RedMask, GreenMask, blueMask, Depth);
		}
	}
	public class glxNativeContext : UnmanagedHandle, IGLNativeContext
	{
		private BaseWindow 		m_pWindow;
		private bool           		m_bOwned;
		private Rectangle      		m_rDefaultViewport;
		private VSyncMode	   		m_bVsync = VSyncMode.Disable;
		private GameContextConfig 	m_pGameConfig;
		private INativContextConfig m_pNativeConfig;
		private INativContextConfigs m_pConfigs;

		public GameContextConfig GameConfig
		{
			get { return m_pGameConfig; }
		}

		public INativContextConfig NativeConfig
		{
			get { return m_pNativeConfig; }
		}

		public BaseWindow Window
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
				Activate();
				gl.glGetIntegerv( (uint)GL.VIEWPORT, rect);
				DeActivate();
				return new Rectangle(rect);
			}
			set
			{
				Activate();
				gl.glViewport(value.X, value.Y, value.Width, value.Height);
				DeActivate();
			}
		}

		public glxNativeContext() 
			: base("OPENGL_CONTEXT", IntPtr.Zero)
		{
			LoadExtension();
			m_bOwned = false;
			m_rDefaultViewport = Viewport;
		}
		public glxNativeContext(BaseWindow handle, GameContextConfig pConfig)
			: base("OPENGL_CONTEXT", IntPtr.Zero)
		{
			m_pWindow = handle;
			m_bOwned = true;
			m_rDefaultViewport = Viewport;
			m_pGameConfig = pConfig;
			m_pConfigs = new FBConfigs(m_pWindow, pConfig);

			if (pConfig.GraphicConfigType == NativContextConfigTyp.Best)
			{
					m_pNativeConfig = m_pConfigs.Best;
			}
			else if (pConfig.GraphicConfigType == NativContextConfigTyp.Worst)
			{
				m_pNativeConfig = m_pConfigs.Worst;
			}
			else
			{
				m_pNativeConfig = m_pConfigs.Configs[pConfig.NormalGraphicConfigTypeNumber];
			}

			LoadExtension();
		}
		public unsafe virtual void CreateContext()
		{
			int glx_major, glx_minor;
			if (!glXQueryVersion(m_pWindow.Display.RawHandle, out glx_major, out glx_minor) ||
			    ((glx_major == 1) && (glx_minor < 3)) || (glx_major < 1))
			{
				Console.WriteLine("Invalid GLX version");
					throw new System.Exception("[GLX] not supported");
				
			}

			Console.WriteLine("glX String: {0}.{1}", glx_major, glx_minor);
		
			Console.WriteLine("Using {0} Graphic Config", m_pNativeConfig.Typ);

			IntPtr ctx_old = glXCreateContext( m_pWindow.Display.RawHandle, ((FBConfig)m_pNativeConfig).VisualInfo, IntPtr.Zero, true );
			glXMakeCurrent(m_pWindow.Display.RawHandle, m_pWindow.RawHandle, ctx_old);

			GLVersion(out glx_major, out glx_minor);

			if (glXCreateContextAttribsARB == null)
			{
				Console.WriteLine("glXCreateContextAttribsARB() not found using old-style GLX context");
				m_pHandle = ctx_old;
			}
			else
			{
				glXMakeCurrent( m_pWindow.Display.RawHandle, IntPtr.Zero, IntPtr.Zero );
				glXDestroyContext( m_pWindow.Display.RawHandle, ctx_old );

				IntPtr config = m_pNativeConfig.Config;

				

				int[] attribs =
				{
					(int)GLX.CONTEXT_MAJOR_VERSION_ARB, glx_major,
					(int)GLX.CONTEXT_MINOR_VERSION_ARB, glx_minor,
					
					(int)GLX.CONTEXT_FLAGS_ARB, (int)GLX.CONTEXT_FORWARD_COMPATIBLE_BIT_ARB |
					(int)GLX.CONTEXT_DEBUG_BIT_ARB,        
					(int)GLX.CONTEXT_PROFILE_MASK_ARB, (int)GLX.CONTEXT_CORE_PROFILE_BIT_ARB,
					0
				};
				m_pHandle = glXCreateContextAttribsARB(m_pWindow.Display.RawHandle, config, 
					IntPtr.Zero, true, attribs);
				if (m_pHandle == IntPtr.Zero)
				{
					Console.WriteLine("Failed to create GL {0}.{1} context using old-style GLX context",
						glx_major, glx_minor);
					m_pHandle = glXCreateContext( m_pWindow.Display.RawHandle, ((FBConfig)m_pNativeConfig).VisualInfo, IntPtr.Zero, true );
				}
				glXMakeCurrent(m_pWindow.Display.RawHandle, m_pWindow.RawHandle, m_pHandle);
			}
			if (!glXIsDirect(m_pWindow.Display.RawHandle, m_pHandle))
			{
				throw new System.Exception("Indirect GLX rendering context obtained");
			}

			VScyn = m_pGameConfig.VSync;
			Extensions.LoadExtensionsList();

			Register();
		}
		public virtual INativContextConfigs GetConfigs()
		{
			return m_pConfigs;
		}
		public virtual void Activate()
		{
			if (Owned && glXGetCurrentContext() != RawHandle)
				MakeCurrent();
		}
		public virtual void DeActivate()
		{
		if (Owned && glXGetCurrentContext() == RawHandle)
			MakeOutdated();
		}
		public virtual void Present()
		{
			if ( RawHandle == IntPtr.Zero ) return;

			gl.glFlush();
			Swap();
			DeActivate();
		}

		public void MakeCurrent()
		{
			glXMakeCurrent( m_pWindow.Display.RawHandle, m_pWindow.RawHandle, m_pHandle );
		}
		public void MakeOutdated()
		{
			glXMakeCurrent( m_pWindow.Display.RawHandle, IntPtr.Zero, IntPtr.Zero );
		}
		public void Swap()
		{
			glXSwapBuffers(m_pWindow.Display.RawHandle, m_pWindow.RawHandle);
		}
		protected void GLVersion(out int major, out int minor)
		{
			Extensions.LoadExtensionsList();

			if (Extensions.IsVERSION_3_0) { major = 3; minor = 0; }
			else if (Extensions.IsVERSION_3_1) { major = 3; minor = 1; }
			else if (Extensions.IsVERSION_3_2) { major = 3; minor = 2; }
			else if (Extensions.IsVERSION_3_3) { major = 3; minor = 3; }
			else if (Extensions.IsVERSION_4_0) { major = 4; minor = 0; }
			else if (Extensions.IsVERSION_4_1) { major = 4; minor = 1; }
			else if (Extensions.IsVERSION_4_2) { major = 4; minor = 2; }
			else if (Extensions.IsVERSION_4_3) { major = 4; minor = 3; }
			else { major = 0; minor = 0; }

			if (major != 0)
				Console.WriteLine("OpenGL Version: {0}.{1}", major, minor);
		}
		protected override void CleanUpUnManagedResources()
		{
			if ( !m_bOwned ) return;

			glXMakeCurrent( Window.Display.RawHandle, IntPtr.Zero, IntPtr.Zero );
			glXDestroyContext( Window.Display.RawHandle, m_pHandle );
		}
		private void LoadExtension()
		{
			glXSwapIntervalSGI = (SwapIntervalSGI)gl.GetProc<SwapIntervalSGI>("glXSwapIntervalSGI");
			glXSwapIntervalMESA = (SwapIntervalMESA)gl.GetProc<SwapIntervalMESA>("glXSwapIntervalMESA");
			glXGetSwapIntervalMESA = (GetSwapIntervalMESA)gl.GetProc<GetSwapIntervalMESA>("glXGetSwapIntervalMESA");
			glXSwapIntervalEXT = (SwapIntervalEXT)gl.GetProc<SwapIntervalEXT>("glXSwapIntervalEXT");
			glXCreateContextAttribsARB = (CreateContextAttribsARB)gl.GetProc<CreateContextAttribsARB>("glXCreateContextAttribsARB");
		
			gl.LoadExtension();
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

		[DllImport(DllName)]
		public static extern bool glXIsDirect(IntPtr dpy, IntPtr ctx);
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
		public unsafe extern static XVisualInfo *glXGetVisualFromFBConfig(IntPtr dpy, IntPtr fbconfig);

		[DllImport(DllName, EntryPoint = "glXGetProcAddressARB")]
		public static extern IntPtr glXGetProcAddressARB([MarshalAs(UnmanagedType.LPTStr)] string procName);


		[DllImport(DllName)]
		public static extern int glXGetFBConfigAttrib(IntPtr dpy, IntPtr config, int attribute, ref int value);
		[DllImport(DllName)]
		public static extern IntPtr glXGetFBConfigs(IntPtr dpy, int screen, ref int nelements);
		[DllImport(DllName)]
		public static extern IntPtr glXCreateWindow(IntPtr dpy, IntPtr config, IntPtr win, int[] attribList);
		[DllImport(DllName)]
		public static extern void glXDestroyWindow(IntPtr dpy, IntPtr window);

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


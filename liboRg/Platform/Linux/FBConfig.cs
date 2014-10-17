//
//  FBConfig.cs
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
using System.Collections.Generic;
using X11.Widgets;
using liboRg.Window;
using liboRg.Context;
using System.Runtime.InteropServices;

namespace liboRg.Platform.Linux
{


	public class FBConfig : INativContextConfig
	{
		private int 				  m_iID;
		private IntPtr      		  m_pConfig;
		private	int					  m_iSampleBuf;
		private	int 				  m_iSamples;
		private NativContextConfigTyp m_pType = NativContextConfigTyp.Normal;
		private XVisualInfo			  m_iInfo;

		public IntPtr Config
		{
			get { return m_pConfig; }
		}

		public IntPtr VisualInfo
		{
			get 
			{ 
				IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(m_iInfo));
				Marshal.StructureToPtr(m_iInfo, pnt, false);

				return pnt; 
			}
		}

		public NativContextConfigTyp Typ
		{
			get { return m_pType; } internal set { m_pType = value; }
		}
		public int ID
		{
			get { return m_iID; }
		}
		internal FBConfig(int iID, IntPtr pConfig, int iSampleBuf, int iSamples, XVisualInfo vi)
		{
			m_iID = iID;
			m_pConfig = pConfig;
			m_iSampleBuf = iSampleBuf;
			m_iSamples = iSamples;
			m_iInfo = vi;
		}
		public override string ToString()
		{
			return string.Format("  Matching fbconfig {0} ({1}): SAMPLE_BUFFERS = {2}," + 
				" SAMPLES = {3}", m_iID, m_iInfo.ToString(), m_iSampleBuf, m_iSamples);
		}
	}
	public class FBConfigs : INativContextConfigs
	{
		IList<INativContextConfig> m_pConfigs;
		INativContextConfig m_pBest;
		INativContextConfig m_pWorst;

		public INativContextConfig Best
		{
			get { return m_pBest; }
		}

		public INativContextConfig Worst
		{
			get { return m_pWorst; }
		}
		public INativContextConfig this[int fbConfigID]
		{
			get
			{
				for (int i = 0; i < m_pConfigs.Count; i++)
				{
					if (m_pConfigs[i].ID == fbConfigID)
					{
						return m_pConfigs[i];
					}
				}
				return null;
			}
			set
			{
				for (int i = 0; i < m_pConfigs.Count; i++)
				{
					if (m_pConfigs[i].ID == fbConfigID)
					{
						m_pConfigs[i] = value;
						break;
					}
				}
			}
		}
		public IList<INativContextConfig> Configs
		{
			get { return m_pConfigs; }
		}
		public unsafe FBConfigs(BaseWindow pWindow, GameContextConfig pConfig)
		{
			m_pConfigs = new List<INativContextConfig>();

			int[] visual_attribs =
			{
				(int)liboRg.OpenGL.GLX.X_RENDERABLE, (int)GL.TRUE,
				(int)liboRg.OpenGL.GLX.DRAWABLE_TYPE, (int)liboRg.OpenGL.GLX.WINDOW_BIT,
				(int)liboRg.OpenGL.GLX.DOUBLEBUFFER, (int)GL.TRUE,
				(int)liboRg.OpenGL.GLX.RENDER_TYPE, (int)liboRg.OpenGL.GLX.RGBA_BIT,
				(int)liboRg.OpenGL.GLX.X_VISUAL_TYPE, (int)liboRg.OpenGL.GLX.TRUE_COLOR,
				(int)liboRg.OpenGL.GLX.BUFFER_SIZE, pConfig.Color,
				(int)liboRg.OpenGL.GLX.DEPTH_SIZE, pConfig.Depth,
				(int)liboRg.OpenGL.GLX.STENCIL_SIZE, pConfig.Stencil,
				(int)liboRg.OpenGL.GLX.DOUBLEBUFFER, (int)GL.TRUE,
				(int)liboRg.OpenGL.GLX.SAMPLE_BUFFERS, pConfig.EnableSample ? (int)GL.TRUE : (int)GL.FALSE,
				0
			};

			int best_fbc = -1, worst_fbc = -1, best_num_samp = -1, worst_num_samp = 999;
			int fbcount;
			IntPtr* fbc = glxNativeContext.glXChooseFBConfig(pWindow.Display.RawHandle, 
				pWindow.Display.Screen.ScreenNumber, visual_attribs, out fbcount);


			for (int i = 0; i < fbcount; i++ )
			{
				IntPtr _config = fbc[i];
				XVisualInfo *vi = glxNativeContext.glXGetVisualFromFBConfig( pWindow.Display.RawHandle, _config );
				if ( vi != null )
				{
					int samp_buf = 0, samples = 0;
					glxNativeContext.glXGetFBConfigAttrib( pWindow.Display.RawHandle, fbc[i], (int)liboRg.OpenGL.GLX.SAMPLE_BUFFERS, ref samp_buf );
					glxNativeContext.glXGetFBConfigAttrib( pWindow.Display.RawHandle, fbc[i], (int)liboRg.OpenGL.GLX.SAMPLES       , ref samples  );

					if(vi->Depth == pConfig.Depth && ((pConfig.EnableSample &&  samp_buf >= 1) ||  (!pConfig.EnableSample &&  samp_buf == 0)))
					{
						
							//int iID, IntPtr pConfig, int iSampleBuf, int iSamples
							m_pConfigs.Add( new FBConfig(i, fbc[i], samp_buf, samples, *vi ));

						if ( best_fbc < 0 || samp_buf == 1 && samples > best_num_samp )
						{
							best_fbc = i; 
							best_num_samp = samples;
						}
						if ( worst_fbc < 0 || samp_buf == 0 || samples < worst_num_samp )
						{
							worst_fbc = i;
							worst_num_samp = samples;
						}
					}
				}
				//X11._internal.Lib.XFree( vi );
			}
			if(m_pConfigs.Count == 0)
				throw new System.Exception("No Configs found");

			#if DEBUG

			m_pBest = this[best_fbc]; ((FBConfig)m_pBest).Typ = NativContextConfigTyp.Best; 
			m_pWorst = this[worst_fbc]; ((FBConfig)m_pWorst).Typ = NativContextConfigTyp.Worst; 

			ConsoleColor oldForground = Console.ForegroundColor;
			foreach (var item in m_pConfigs) 
			{
				if(item.Typ == NativContextConfigTyp.Best)
					Console.ForegroundColor = ConsoleColor.Green;
				else if(item.Typ == NativContextConfigTyp.Worst)
					Console.ForegroundColor = ConsoleColor.Blue;
				else
					Console.ForegroundColor = oldForground;
				Console.WriteLine(item.ToString());
			}
			Console.ForegroundColor = oldForground;
			#endif
		}

	}
}


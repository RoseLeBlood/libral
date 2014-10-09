//
//  GameContextConfig.cs
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
using liboRg.Window;
using liboRg.OpenGL;

namespace liboRg.Context
{
	public class GameContextConfig
	{
		private int 			     	m_iColor;
		private int 		         	m_iDepth;
		private int 				 	m_iStencil;

		private GameResolution 		 	m_pResolution;
		private VSyncMode	   		 	m_pVSync;
		private NativContextConfigTyp 	m_pConfigTyp;
		private int  					m_pConfigInt;
		private bool					m_bSamples;

		public int Color
		{
			get { return m_iColor; }
			set { m_iColor = value; }
		}
		public int Depth
		{
			get { return m_iDepth; }
			set { m_iDepth = value; }
		}
		public int Stencil
		{
			get { return m_iStencil; }
			set { m_iStencil = value; }
		}
		public GameResolution Resolution
		{
			get { return m_pResolution; }
			set { m_pResolution = value; }
		}
		public VSyncMode VSync
		{
			get { return m_pVSync; }
			set { m_pVSync = value; }
		}
		public NativContextConfigTyp GraphicConfigType
		{
			get { return m_pConfigTyp; }
			set { m_pConfigTyp = value; }
		}
		public int NormalGraphicConfigTypeNumber
		{
			get { return m_pConfigInt; }
			set { m_pConfigInt = value; }
		}
		public bool EnableSample
		{
			get { return m_bSamples; }
			set { m_bSamples = value; }
		}
		public GameContextConfig(GameResolution pResolution, int iColor = 24, int iDepth = 24, int iStencil = 8, 
			bool bSamples = true, NativContextConfigTyp pGraphicType = NativContextConfigTyp.Best, VSyncMode pVsync = VSyncMode.Adaptive )
		{
			m_iColor = iColor;
			m_iDepth = iDepth;
			m_iStencil = iStencil;
			m_pResolution = pResolution;
			m_pVSync = pVsync;
			m_pConfigTyp = pGraphicType;
			m_pConfigInt = 0;
			m_bSamples = bSamples;
		}
	}
}


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
using System.API.Platform;
using System.API.Platform.Linux;

namespace liboRg.Context
{
	public class GameContextConfig
	{
		private int 		         	m_iDepth;
		private int 				 	m_iStencil;

		private GameResolution 		 	m_pResolution;
		private VSyncMode	   		 	m_pVSync;
		private NativContextConfigTyp 	m_pConfigTyp;
		private int  					m_pConfigInt;
		private bool					m_bSamples;

		public int Color
		{
			get { return m_pResolution.BitsPerPixel; }
			set { m_pResolution.BitsPerPixel = value; }
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
			set 
			{ 
				m_pConfigTyp = value; 
				m_bSamples = (value != NativContextConfigTyp.Worst);
			}
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
		public GameContextConfig(MonitorMode pResolution, int iDepth = 24, int iStencil = 8, 
			bool bSamples = true, NativContextConfigTyp pGraphicType = NativContextConfigTyp.Best, 
			VSyncMode pVsync = VSyncMode.Adaptive )
		{
			m_iDepth = iDepth;
			m_iStencil = iStencil;
			m_pResolution = new GameResolution(pResolution);
			m_pVSync = pVsync;
			m_pConfigTyp = pGraphicType;
			m_pConfigInt = 0;
			m_bSamples = bSamples;
		}
		public override string ToString()
		{
			return string.Format("{3}  VSync={4} Depth={1} Stencil={2}, GraphicConfigType={5}, EnableSample={7}", Color, Depth, Stencil, Resolution, VSync, GraphicConfigType, NormalGraphicConfigTypeNumber, EnableSample);
		}
	}
}


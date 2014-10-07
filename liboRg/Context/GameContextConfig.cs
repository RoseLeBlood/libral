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
		private int m_iColor;
		private int m_iDepth;
		private int m_iStencil;
		private int m_iAntialias;

		private GameResolution m_pResolution;
		private VSyncMode	   m_pVSync;

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
		public int Antialias
		{
			get { return m_iAntialias; }
			set { m_iAntialias = value; }
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

		public GameContextConfig(GameResolution pResolution, int iColor = 32, int iDepth = 24, int iStencil = 0,
			int iAntialias = 0, VSyncMode pVsync = VSyncMode.Adaptive )
		{
			m_iColor = iColor;
			m_iDepth = iDepth;
			m_iStencil = iStencil;
			m_iAntialias = iAntialias;
			m_pResolution = pResolution;
			m_pVSync = pVsync;
		}
	}
}


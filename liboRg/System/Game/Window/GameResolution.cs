//
//  GameResolution.cs
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
using System.API.Platform.Linux;

namespace liboRg.Window
{
	public class GameResolution
	{
		private MonitorMode 	m_pMonitorMode;
		private int 			m_iBpp;
	
		public Size Size
		{
			get { return m_pMonitorMode.Size; }
		}
		internal int BitsPerPixel
		{
			get { return m_iBpp; }
			set { m_iBpp = value; }
		}
		public double RefreshRate
		{
			get { return m_pMonitorMode.Rate; }
		}
		public GameResolution(MonitorMode pMonitorMode)
		{
			m_pMonitorMode = pMonitorMode;
		}
		public override string ToString()
		{
			return String.Format("{0} Bpp={1}", m_pMonitorMode, m_iBpp);
		}
	}
}


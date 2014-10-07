//
//  Monitor.cs
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
using System.Diagnostics;
using libral;

namespace X11
{
	public enum Placement
	{
		Default,
		Right,
		Above,
		Left,
		Below
	}

	public class Monitor : Handle
	{
		private List<MonitorMode>	m_lstModes;
		private MonitorMode 		m_sActiveMode;
		private MonitorMode			m_sPreferdMode;
		private string				m_strConnectionName;

		private Placement 			m_ePlacement;
		private Rectangle			m_rSize;
		private	bool				m_isLaptop;

		public Rectangle 	Bounds { get { return m_rSize; } internal set { setBounds(value); } }
		public Placement 	Placement { get  { return m_ePlacement; } }
		public bool      	IsLaptop { get { return m_isLaptop; }  }
		public MonitorMode 	ActiveMode { get { return m_sActiveMode; } internal set { m_sActiveMode = value; }  }
		public MonitorMode 	PreferdMode { get { return m_sPreferdMode; } internal set { m_sPreferdMode = value; }  }
		public string 		ConectionName { get { return m_strConnectionName; } }

		public MonitorMode this[int imode]
		{
			get { return m_lstModes[imode]; }
		}
		public Monitor(string name)
			: base(SetupNames(name))
		{
			m_isLaptop = (Name.Contains("LVDS") || Name.Contains("PANEL"));
			m_ePlacement = Placement.Default;
			m_lstModes = new List<MonitorMode>();
			m_strConnectionName = name;

			#if DEBUG
			Console.WriteLine("{1} found at {0}", m_strConnectionName, Name);
			#endif

		}

		internal void AddMode(MonitorMode mode)
		{
			m_lstModes.Add(mode);
		}
		private static string SetupNames(string name)
		{
			return "MONITOR_" + name.Replace("-", "");
		}
		private void setBounds(Rectangle rect)
		{
			m_rSize = rect;
			m_sActiveMode = new MonitorMode(m_rSize.Size, 0);

			if (m_rSize.X < 0 || m_rSize.Y < 0 || (m_rSize.X == 0 && m_rSize.Y == 0))
				m_ePlacement = Placement.Default;
			else if (m_rSize.X == 0)
				m_ePlacement = Placement.Below;
			else if (m_rSize.Y == 0)
				m_ePlacement = Placement.Right;
		}
	}
}


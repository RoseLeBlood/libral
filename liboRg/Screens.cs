//
//  Screen.cs
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
using X11.Widgets;
using X11._internal;
using X11;

namespace liboRg
{
	public static class Screens
	{
		private static Monitor[] m_arScreens = null;

		public static Monitor PrimaryScreen 
		{
			get
			{
				if (m_arScreens == null)
				{
					m_arScreens = MonitorList.GetScreens();
				}
				return m_arScreens[0];
			}
		}
	}
}


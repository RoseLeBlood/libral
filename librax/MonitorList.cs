//
//  MonitorList.cs
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
using System.Text.RegularExpressions;
using System.Common;

namespace X11
{
	public static class MonitorList 
	{
		public static Monitor[] GetScreens()
		{
			Monitor[] monitore = null;

			string output = Utils.CallProgram("xrandr", "");
			Regex reg4 = new Regex("\n([-a-zA-Z]+[-0-9]*) +connected ([^(\n ]*)[^\n]*" +
				"((\\n +[0-9]+x[0-9]+[^\\n]+)+)");
			MatchCollection reg = reg4.Matches(output);

			if (reg.Count >= 1)
				monitore = new Monitor[reg.Count];
			else
				return null;

			for (int i = 0; i < reg.Count; i++)
			{
				Match match = reg[i];

				Monitor monitor = new Monitor(match.Groups[1].Value);

				monitor.Bounds = CoordStringToRectangle(match.Groups[2].Value);

				string[] lines = match.Groups[3].Value.Split('\n');
				foreach (var item in lines)
				{
					string[] str = item.Split(' ');
					Size mode = new Size();
					for (int xi = 0; xi < str.Length; xi++)
					{
						string current = str[xi];

						if (current == "")
							continue;
						else if (current.Contains("x"))
						{
							mode = new Size(current);

							while (!current.Contains("."))
							{
								current = str[xi++];
							}
							if (current.Contains("+"))
								monitor.ActiveMode = new MonitorMode(mode, current);
							if (current.Contains("*"))
								monitor.PreferdMode = new MonitorMode(mode, current);
							monitor.AddMode(new MonitorMode(mode, current));
						}
					}
				}

				monitor.Register();
				monitore[i] = monitor;
			}
			return monitore;
		}

		private static Rectangle CoordStringToRectangle(string coords) // 800x600+0+0;
		{
			Rectangle output = null;

			string[] coordsplit = coords.Split('+', 'x');
			output = new Rectangle(
				int.Parse(coordsplit[2]),
				int.Parse(coordsplit[3]),
				int.Parse(coordsplit[0]),
				int.Parse(coordsplit[1]));


			return output;
		}
	}
}


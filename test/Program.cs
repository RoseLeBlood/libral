//#define CREATENEW
//
//  Program.cs
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
using System.Reflection;
using X11;
using X11.Widgets;
using liboRg;
using liboRg.Window;

namespace test
{
	public class Programm
	{
		public static void Main (string[] args)
		{
			Application.Init("test");

			var screenRes = new GameResolution(Screens.PrimaryScreen[3], 24);
			Console.WriteLine(screenRes);

			Game game = new Game(":0", screenRes, 
				screenRes.ToString(), WindowStyle.Resize);
			Application.Current.MainWindow = game.Window.Name;
			game.Init();

			Application.Run();
		}
	}
}

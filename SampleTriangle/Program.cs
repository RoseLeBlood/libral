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
using liboRg;

namespace SampleTriangle
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init("test");

			var x = Screens.PrimaryScreen.Modes;
			Console.WriteLine("Reselutions: ");
			for (int i = 0; i < x.Count; i++)
				{
					Console.WriteLine("\t{0}. {1}", i+1, x[i]);
				}
			int res = 3; int.TryParse(Console.ReadLine(), out res);

			Console.Write("FullScreen [Y/n]: ");
			bool fullscreen = (Console.ReadLine().ToUpper() == "Y");

			Console.Write("GraphicMode [Best/low]: ");
			bool mode = (Console.ReadLine().ToUpper() == "BEST");

			Game game = new TriangleGame(res, fullscreen, mode);
			Application.Run();
			game.Destroy();
		}
	}
}

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
using liboRg.OpenGL;
using liboRg.Context;


namespace test
{
	public class TestGame : Game
	{
		double lastTime;
		int nbFrames = 0;


		public TestGame() : base(":0", new GameContextConfig(new GameResolution(Screens.PrimaryScreen[3], 24)),
			"Hallo Welt", WindowStyle.Resize)
		{
			Init();
		}
		public override void Create()
		{
			base.Create();

			ClearColor = Colors.CornflowerBlue;
			lastTime = this.GameContext.Time;


		}

		protected override bool Move()
		{
			double currentTime = this.GameContext.Time;
			nbFrames++;
			if (currentTime - lastTime >= 1.0)
			{ 
				this.Window.Title = string.Format("FPS: {0}", nbFrames);
				nbFrames = 0;
				lastTime += 1;
			}
			if (Window.IsKeyDown(Keys.F1))
				Console.WriteLine("Hallo F1");

			return base.Move();
		}
		protected override bool Draw()
		{
			GameContext.Clear(liboRg.Context.Buffer.Color);


			return base.Draw();
		}
	}



	public class Programm
	{

		public static void Main (string[] args)
		{
			Application.Init("test");
			Game game = new TestGame();
			{
				Application.Run();
			}
			game.Destroy();
		}
	}
}

//
//  Game.cs
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

namespace SampleOpenCL2
{
	public class ExampleCLGame : Game
	{
		private Example m_pExample;

		public ExampleCLGame() : base(":0", 
			new liboRg.Context.GameContextConfig(Screens.PrimaryScreen[(int)(Screens.PrimaryScreen.Modes.Count - 1)]),
			"SampleOpenCL2", liboRg.Window.WindowStyle.Fixed)
		{

		}
		public override void Create()
		{
			base.Create();

			m_pExample = new Example("part1.cl", this);
			m_pExample.popCorn();
			m_pExample.runKernel();
		}
		protected override bool Draw()
		{
			GameContext.Clear(liboRg.Context.Buffer.Color, Colors.Black);

			return base.Draw();
		}
	}
}


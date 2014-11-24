//
//  SampleTextGame.cs
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
using liboRg.Context;
using liboRg.Window;
using System.Common;
using System.Framework;

namespace SampleText
{
	public class SampleTextGame : Game
	{
		private TextRenderer m_pText;
		private double lastTime;
		private int nbFrames = 0;
		private int renderFrame = 0;

		public SampleTextGame() : base(":0", 
			new GameContextConfig(Screens.PrimaryScreen.Modes[Screens.PrimaryScreen.Modes.Count - 1]),
			"SampleTextGame", WindowStyle.Fixed)
		{

		}
		public override void Create()
		{
			base.Create();

			lastTime = this.Time;
			m_pText = new TextRenderer("AuthenticHilton.ttf");
			m_pText.Create(this, 96);
		}
		protected override bool Move()
		{
			double currentTime = this.Time;
			nbFrames++;
			if (currentTime - lastTime >= 1.0)
				{ 

				renderFrame = nbFrames;
					nbFrames = 0;
					lastTime += 1;
				}
			return base.Move();
		}
		protected override bool Draw()
		{
			GameContext.Clear(liboRg.Context.Buffer.Color, Colors.Black);
			m_pText.Begin();
			m_pText.Write(string.Format("Hallo Welt   Fps: {0}", renderFrame),
				Colors.DarkOrange, new Vector2(8f, 100f));

			m_pText.Write("The Dark Green Fox Jumps Over The Lazy Dog",
				Colors.DarkGreen, new Vector2(8f, 380));
		

			m_pText.End();
			return base.Draw();
		}
	}
}


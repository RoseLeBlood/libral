//
//  TriangleGame.cs
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
using System.API.OpenGL;
using System.API.Platform;
using System.Common;
using System.Framework;
using liboRg;
using liboRg.Context;
using liboRg.Window;

namespace SampleTriangle
{
	public class TriangleGame : Game
	{
		double lastTime;
		int nbFrames = 0;

		VertexArray vao = null;
		Program program;

		public TriangleGame(int resid, bool fullscreen, bool mode) : base(":0", 
			new GameContextConfig(Screens.PrimaryScreen[resid-1]),
			"FPS: 0", fullscreen ? WindowStyle.Fullscreen :  WindowStyle.Resize)
		{
			this.ContextConfig.GraphicConfigType = (mode ? NativContextConfigTyp.Best : NativContextConfigTyp.Worst);
		}
		public override void Create()
		{
			base.Create();
			lastTime = this.Time;

			vao = new VertexArray("vao");

			PositionColorVertexTextured triangle = new PositionColorVertexTextured("TriangleGameVertex");
			triangle.Add(new Vector3(-0.8f, 0.0f, 0.0f), Colors.AliceBlue);
			triangle.Add(new Vector3(0.8f, 0.0f, 0.0f), Colors.FireBrick);
			triangle.Add(new Vector3(0.0f, 0.8f, 0.0f), Colors.Green);

			program = new Program("program", 
				new Shader("vertex.sh", ShaderType.Vertex), 
				new Shader("frag.sh", ShaderType.Fragment)); 
				
			triangle.SetTexture(
				program, "tex", GL.TEXTURE0_ARB, 
				new Texture(new Image("imgParticel", 10, 10, Colors.DarkGoldenrod)));
			triangle.BindAttribute(program, vao);
		}

		protected override bool Move()
		{
			double currentTime = this.Time;
			nbFrames++;
			if (currentTime - lastTime >= 1.0)
			{ 
				this.Window.Title = string.Format("FPS: {0} {1}", nbFrames, this.ContextConfig);
				nbFrames = 0;
				lastTime += 1;
			}
			return base.Move();
		}
		protected override bool Draw()
		{
			GameContext.Clear(liboRg.Context.Buffer.Color, Colors.Black);

			GameContext.DrawArrays( vao, Primitive.Triangles, 0, 3 );
			return base.Draw();
		}
	}
}


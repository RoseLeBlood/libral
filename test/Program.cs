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
using liboRg;
using liboRg.Context;
using liboRg.Window;
using liboRg.Context;
using System.Framework;
using System.API.OpenGL;
using System.API.Platform;
using System.Framework.Meshes;
using System.API.Platform.Linux;


namespace test
{
	public class TestGame : Game
	{
		VertexArray vao = null;
		Program program;

		public TestGame(int resid, bool fullscreen, bool mode) : base(":0", 
			new GameContextConfig(Screens.PrimaryScreen[resid-1]),
			"FPS: 0", fullscreen ? WindowStyle.Fullscreen :  WindowStyle.Fixed)
		{
			this.ContextConfig.GraphicConfigType = NativContextConfigTyp.Best;
		}

		uint[] texture_id = new uint[1];

		public override void Create()
		{
			base.Create();

			vao = new VertexArray("vao");

			PositionColorVertexTextured triangle = new PositionColorVertexTextured("TriangleGameVertex");
			triangle.Add(new Vector3(-0.5f, 0.0f, 0.0f), Colors.Black);
			triangle.Add(new Vector3(0.5f, 0.0f, 0.0f), Colors.Black);
			triangle.Add(new Vector3(-0.5f, 0.5f, 0.0f), Colors.Black);

			triangle.Add(new Vector3(0.5f, -0.5f, 0.0f), Colors.Wheat);
			triangle.Add(new Vector3(0.5f, 0.0f, 0.0f), Colors.Wheat);
			triangle.Add(new Vector3(-0.5f, 0.0f, 0.0f), Colors.Wheat);

			program = new Program("program", 
				new Shader("vertex.sh", ShaderType.Vertex), 
				new Shader("frag.sh", ShaderType.Fragment)); 

			triangle.SetTexture(
				program, "tex", GL.TEXTURE0_ARB, 
				new Texture(new Image("imgParticel", 10, 10, Colors.Goldenrod)));
			triangle.BindAttribute(program, vao);

		}

		protected override bool Move()
		{

			return base.Move();
		}
		protected override bool Draw()
		{
			GameContext.Clear(liboRg.Context.Buffer.Color | liboRg.Context.Buffer.Depth);
			GameContext.DrawArrays( vao, Primitive.TrianglesStrip, 0, 6 );
			return base.Draw();
		}
		public static void Main (string[] args)
		{
			Application.Init("test");

			Game game = new TestGame(8, false, true);
			Application.Run();
			game.Destroy();
		}
	}
}

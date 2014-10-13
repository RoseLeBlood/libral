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
using X11;
using X11.Widgets;
using liboRg;
using liboRg.Context;
using liboRg.Framework;
using liboRg.Window;


namespace test
{
	public class TestGame : Game
	{
		double lastTime;
		int nbFrames = 0;

		VertexArray vao = null;

		public TestGame() : base(":0", 
			new GameContextConfig(new GameResolution(Screens.PrimaryScreen[5], 24) ),
			"FPS: 0", WindowStyle.Normal)
		{
			Init();
		}
		public override void Create()
		{
			base.Create();

			ClearColor = Colors.AliceBlue;
			lastTime = this.Time;

			Shader vertex = new Shader("vertex", ShaderType.Vertex, 
				"#version 150\nout vec4 inColor;in vec2 position;  void main() { inColor = vec4( position*2, 0, 1.0 ); " +
				"gl_Position = vec4( position, 0.0, 1.0 ); }");
			Shader frag = new Shader( "frag", ShaderType.Fragment, 
				"#version 150\nin vec4 inColor; out vec4 outColor; void main() { outColor = inColor; }" );
			Program program = new Program("program", vertex, frag);

			float[] vertices = new float[]
			{
					0.5f,  -0.5f,
					0.5f,  0.5f,
					-0.5f, 0.5f,
			};

			VertexDataBuffer vboData = new VertexDataBuffer(); vboData.Floats(vertices);

			vao = new VertexArray("vao");
			vao.BindAttribute( program.Attribute( "position" ), 
				new VertexBuffer("vbo", vboData, BufferUsage.StaticDraw), 
				DataType.Float, 2, 0, IntPtr.Zero );
		}

		protected override bool Move()
		{
			double currentTime = this.Time;
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
			GameContext.Clear();
			GameContext.DrawArrays( vao, Primitive.Triangles, 0, 6 );
			return base.Draw();
		}
		public static void Main (string[] args)
		{
			Application.Init("test");
			Game game = new TestGame();
			Application.Run();
			game.Destroy();
		}
	}
}

//
//  TextRenderer.cs
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
using System.Common;
using System.Runtime.InteropServices;
using SharpFont;
using liboRg.Context;
using System.Collections.Generic;


namespace System.Framework
{
	public class TextRenderer : Handle
	{
		private bool    	m_bInit;
		private string  	m_strFontPath;
		private Game 		m_pGame;
		private Library 	m_pFt;
		private Face 		m_pFace;

		private Program m_pProgram;
		private int     m_iAttributeCoord;
		private int 	m_iUniformTex;
		private int 	m_iUniformColor;
		private int		m_iFontSize;

		private VertexBuffer m_pVbo;
		private VertexArray  m_pVao;
		private Texture      m_pTexture;
	
		private uint[] oldblend = new uint[1];

		public TextRenderer(string strFontPath = "FreeSans.ttf")
			: base("TextRanderer_" + strFontPath)
		{
			m_bInit = false;
			m_strFontPath = strFontPath;
		}

		public bool Create(Game pGame, int size)
		{
			if (m_bInit)
				return true;
			m_iFontSize = size;
			m_pGame = pGame;
			m_pVao = new VertexArray("TextRenderer_VAO");

			m_pFt = new Library();
			if (m_pFt == null)
				throw new Exception("Could not init freetype library");
				
			m_pFace = new Face(m_pFt, m_strFontPath);
			if (m_pFace == null)
				throw new Exception(string.Format("Could not open font: {0}", m_strFontPath));

			m_pFace.SetCharSize(size << 6, size << 6, 96, 96);
			m_pFace.SetPixelSizes(0,(uint) size);

			// Create OPENGL
			m_pProgram = new Program("TextRenderer", 
				new Shader("TextRenderer_Vertex", ShaderType.Vertex, vertexSHADER),
				new Shader("TextRenderer_Fragment", ShaderType.Fragment, fragmtSHADER));
		

			m_iAttributeCoord = m_pProgram.Attribute("coord");
			m_iUniformTex = m_pProgram.Uniform("tex");
			m_iUniformColor = m_pProgram.Uniform("color");

			if(m_iAttributeCoord == -1 || m_iUniformTex == -1 || m_iUniformColor == -1)
				return false;

			m_pVbo = new VertexBuffer("TextRenderer_VBO");
			m_bInit = true;

			m_pTexture = new Texture("TextRenderer_Texture");

			return true;
		}

		public void Begin()
		{
			gl.glGetBooleanv((uint)GL.BLEND, oldblend);

			gl.glEnable((uint)GL.BLEND);
			gl.glBlendFunc((uint)GL.SRC_ALPHA, (uint)GL.ONE_MINUS_SRC_ALPHA);


			gl.glPixelStorei((uint)GL.UNPACK_ALIGNMENT, 1);
			m_pVbo.EnableVertexAttribArray((uint)m_iAttributeCoord);
			m_pVbo.BindBuffer();

			gl.glVertexAttribPointer(m_iAttributeCoord, 4, (uint)GL.FLOAT, false, 0, 0);

		}
		public void Write(string strText, Color color, Vector2 vPosition)
		{
			float sx =  1.0f / m_pGame.Bounds.Width;
			float sy = 1.0f / m_pGame.Bounds.Height;
			float x = -1 + vPosition.X * sx;
			float y = 1 - vPosition.Y * sy;

			Write(strText, color, x, y, sx, sy);
		}

		public void Write(string strText, Color color, float x, float y, float sx, float sy)
		{
			m_pProgram.Use();
			m_pProgram.Uniform(m_iUniformColor, color);



			for (int i = 0; i < strText.Length; i++)
			{
				char c = strText[i];

				try
				{
					if (c == 32)
					{
						x += (m_iFontSize / 6) * sx;
						continue;
					}

					uint glyphIndex = m_pFace.GetCharIndex(c);
					m_pFace.LoadGlyph(glyphIndex, LoadFlags.Render, LoadTarget.Normal);
					m_pFace.Glyph.RenderGlyph(RenderMode.VerticalLcd);
			
					m_pTexture = new Texture("", m_pFace.Glyph.Bitmap.BufferData, TextureDataType.UnsignedByte,
						TextureFormat.Red, new Size(m_pFace.Glyph.Bitmap.Width, m_pFace.Glyph.Bitmap.Rows),
						TextureInternalFormat.R8);

					m_pTexture.SetWrapping(TextureWrapping.ClampEdge, TextureWrapping.ClampEdge, TextureWrapping.ClampEdge);
					m_pTexture.SetFilters(TextureFilter.Linear, TextureFilter.Linear);
					m_pTexture.Rectangle.Y = m_pFace.Glyph.BitmapTop;
					m_pTexture.Rectangle.X = m_pFace.Glyph.BitmapLeft;

					m_pProgram.Uniform(m_iUniformTex, m_pTexture.glObject);



				}
				catch (Exception)
				{

					continue;		
				}
				float x2 = x + m_pTexture.Rectangle.Left * sx;
				float y2 = -y - m_pTexture.Rectangle.Top * sy;
				float w = m_pTexture.Rectangle.Width * sx;
				float h = m_pTexture.Rectangle.Height * sy;

					VertexDataBuffer data = new VertexDataBuffer();
				data.Vector4(new Vector4(x2, -y2, 0, 0));
				data.Vector4(new Vector4(x2 + w, -y2, 1, 0));
				data.Vector4(new Vector4(x2, -y2 - h, 0, 1));
				data.Vector4(new Vector4(x2 + w, -y2 - h, 1, 1));

				
				m_pVbo.Data(data, BufferUsage.DynamicDraw);
				m_pVao.BindElements(m_pVbo);
				
				m_pGame.GameContext.DrawArrays(m_pVao, Primitive.TrianglesStrip, 0, 4);

				x += (m_pFace.Glyph.Advance.X >> 6) * sx;
				y += (m_pFace.Glyph.Advance.Y >> 6) * sy;
			}
		}

		public void End()
		{
			if(oldblend[0] == 0)
				gl.glDisable((uint)GL.BLEND);
			else
				gl.glEnable((uint)GL.BLEND);
		}



		private const string vertexSHADER = "#version 120\n \nattribute vec4 coord;\nvarying vec2 texcoord;\n \nvoid main(void) {\n  gl_Position = vec4(coord.xy, 0, 1);\n  texcoord = coord.zw;\n}";
		private const string fragmtSHADER = "" +
			"#version 120\n" +
			"varying vec2 texcoord;\n" +
			"uniform sampler2D tex;\n" +
			"uniform vec4 color;\n \n" +
			"void main(void) " +
			"{\n  " +
			"     gl_FragColor = vec4(1,1,1,texture2D(tex, texcoord).r) * color;" +
			"\n" +
			"}\n";
	}
}


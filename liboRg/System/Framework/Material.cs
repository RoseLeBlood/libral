//
//  Material.cs
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
using System.API.OpenGL;

namespace System.Framework
{
	public enum MaterialTextureType
	{
		NoTexture,
		Texture2D

	}
	public class Material
	{
		private static Program m_pShaderNoTexture;
		private static Program m_pShaderTexture2D;
		private static Program m_pShaderTextureRect;
		private static bool   m_bShaderInit = false;
		private static int 	  m_iLights = 8;

		private Color m_colAmbient;
		private Color m_colDiffuse;
		private Color m_colSpecular;
		private float m_fSchininess;
		private Color m_colEmission;

		private Material m_pevMaterialFront;
		private Material m_pevMaterialBack;

		public Color Ambient
		{
			get { return m_colAmbient; }
			set { m_colAmbient = value; }
		}
		public Color Diffuse
		{
			get { return m_colDiffuse; }
			set { m_colDiffuse = value; }
		}
		public Color Specular
		{
			get { return m_colSpecular; }
			set { m_colSpecular = value; }
		}
		public float Schininess
		{
			get { return m_fSchininess; }
			set { m_fSchininess = value; }
		}
		public Color Emission
		{
			get { return m_colEmission; }
			set { m_colEmission = value; }
		}

		public Material(Color colAmbient, Color colDiffuse, Color colSpecular, Color colEmission,  float fSchininess)
		{
			m_colAmbient = colAmbient;
			m_colDiffuse = colDiffuse;
			m_colSpecular = colSpecular;
			m_fSchininess = fSchininess;
			m_colEmission = colEmission;
		}
		public Material(float[] colAmbient, float[] colDiffuse, float[] colSpecular, float[] colEmission,  float fSchininess)
		{
			m_colAmbient = new Color(colAmbient);
			m_colDiffuse = new Color(colDiffuse);
			m_colSpecular = new Color(colSpecular);
			m_fSchininess = fSchininess;
			m_colEmission = new Color(colEmission);
		}
		public void Begin()
		{
			float[] DIFFUSE = new float[4];
			float[] SPECULAR = new float[4];
			float[] AMBIENT = new float[4];
			float[] EMISSION = new float[4];
			float[] SHININESS = new float[1];

			gl.glGetMaterialfv((uint)GL.FRONT, (uint)GL.DIFFUSE,	DIFFUSE);
			gl.glGetMaterialfv((uint)GL.FRONT, (uint)GL.SPECULAR,	SPECULAR);
			gl.glGetMaterialfv((uint)GL.FRONT, (uint)GL.AMBIENT,	AMBIENT);
			gl.glGetMaterialfv((uint)GL.FRONT, (uint)GL.EMISSION,	EMISSION);
			gl.glGetMaterialfv((uint)GL.FRONT, (uint)GL.SHININESS, 	SHININESS);

			m_pevMaterialFront = new Material(AMBIENT, DIFFUSE, SPECULAR, EMISSION, SHININESS[0]);

			gl.glGetMaterialfv((uint)GL.BACK, (uint)GL.DIFFUSE,	DIFFUSE);
			gl.glGetMaterialfv((uint)GL.BACK, (uint)GL.SPECULAR,	SPECULAR);
			gl.glGetMaterialfv((uint)GL.BACK, (uint)GL.AMBIENT,	AMBIENT);
			gl.glGetMaterialfv((uint)GL.BACK, (uint)GL.EMISSION,	EMISSION);
			gl.glGetMaterialfv((uint)GL.BACK, (uint)GL.SHININESS, 	SHININESS);

			m_pevMaterialBack = new Material(AMBIENT, DIFFUSE, SPECULAR, EMISSION, SHININESS[0]);


			gl.glMaterialfv((uint)GL.BACK, (uint)GL.DIFFUSE, 	m_colDiffuse.ToArray());
			gl.glMaterialfv((uint)GL.BACK, (uint)GL.SPECULAR, 	m_colSpecular.ToArray());
			gl.glMaterialfv((uint)GL.BACK, (uint)GL.AMBIENT, 	m_colAmbient.ToArray());
			gl.glMaterialfv((uint)GL.BACK, (uint)GL.EMISSION, 	m_colEmission.ToArray());
			gl.glMaterialfv((uint)GL.BACK, (uint)GL.SHININESS, new float[]{m_fSchininess});

			gl.glMaterialfv((uint)GL.FRONT, (uint)GL.DIFFUSE, 	m_colDiffuse.ToArray());
			gl.glMaterialfv((uint)GL.FRONT, (uint)GL.SPECULAR, 	m_colSpecular.ToArray());
			gl.glMaterialfv((uint)GL.FRONT, (uint)GL.AMBIENT, 	m_colAmbient.ToArray());
			gl.glMaterialfv((uint)GL.FRONT, (uint)GL.EMISSION, 	m_colEmission.ToArray());
			gl.glMaterialfv((uint)GL.FRONT, (uint)GL.SHININESS, new float[]{m_fSchininess});
		}
		public void End()
		{
			gl.glMaterialfv((uint)GL.BACK, (uint)GL.DIFFUSE, 	m_pevMaterialBack.m_colDiffuse.ToArray());
			gl.glMaterialfv((uint)GL.BACK, (uint)GL.SPECULAR, 	m_pevMaterialBack.m_colSpecular.ToArray());
			gl.glMaterialfv((uint)GL.BACK, (uint)GL.AMBIENT, 	m_pevMaterialBack.m_colAmbient.ToArray());
			gl.glMaterialfv((uint)GL.BACK, (uint)GL.EMISSION, 	m_pevMaterialBack.m_colEmission.ToArray());
			gl.glMaterialfv((uint)GL.BACK, (uint)GL.SHININESS, new float[]{m_pevMaterialBack.m_fSchininess});

			gl.glMaterialfv((uint)GL.FRONT, (uint)GL.DIFFUSE, 	m_pevMaterialFront.m_colDiffuse.ToArray());
			gl.glMaterialfv((uint)GL.FRONT, (uint)GL.SPECULAR, 	m_pevMaterialFront.m_colSpecular.ToArray());
			gl.glMaterialfv((uint)GL.FRONT, (uint)GL.AMBIENT, 	m_pevMaterialFront.m_colAmbient.ToArray());
			gl.glMaterialfv((uint)GL.FRONT, (uint)GL.EMISSION, 	m_pevMaterialFront.m_colEmission.ToArray());
			gl.glMaterialfv((uint)GL.FRONT, (uint)GL.SHININESS, new float[]{m_pevMaterialFront.m_fSchininess});
		}


		private void CreateShaders()
		{
			if (Material.m_bShaderInit == false)
			{
				Material.m_pShaderNoTexture = new Program("Material_Program", 
					new Shader("Material_SahderNoTexture", 
						ShaderType.Vertex, ""),
					new Shader("Material_ShaderNoTexture_Fragment", 
						ShaderType.Fragment, ""));
				Material.m_pShaderNoTexture.Use();
				Material.m_pShaderNoTexture.Link();

				
				Material.m_pShaderTexture2D = new Program("Material_Program_m_pShaderTexture2D", 
					new Shader("Material_ShaderTexture2D", 
						ShaderType.Vertex, ""),
					new Shader("Material_ShaderTexture2D_Fragment", 
						ShaderType.Fragment, ""));
				Material.m_pShaderTexture2D.Use();
				Material.m_pShaderTexture2D.Link();

				Material.m_bShaderInit = true;
			}
		}
		private void BeginShader(MaterialTextureType texType)
		{
			CreateShaders();

			//if(texType == MaterialTextureType.NoTexture)

		}
	}
}


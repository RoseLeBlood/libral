//
//  IPhysikRenderObject.cs
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

namespace System.Common
{
	public class Light
	{
		private Vector3 	m_Position;
		private Vector3 	m_Direction;
		private Color	  	m_DiffuseColor;
		private Color   	m_AmbientColor;
		//PointLight
		private Vector4 	m_Attenuation;
		//SpotLight
		private float 		m_cosHalfPhi;
		private float 		m_cosHalfTheta;

		private Matrix		m_mProj;
		private float		m_fFOV;
		private float		m_fAspect;
		private float		m_fNearPlane;
		private float		m_fFarPlane;

		public Matrix	    View
		{ 
			get { return Matrix.CreateLookAt(m_Position, m_Position + m_Direction, Vector3.Up); }
		}
		public Matrix 		Projection 
		{ 
			get { return m_mProj; } 
		}

		public Vector3 Position
		{
			get { return m_Position; }
			set { m_Position = value; }
		}
		public Vector3 Direction
		{
			get { return m_Direction; }
			set { m_Direction = value; }
		}
		public Color DiffuseColor
		{
			get { return m_DiffuseColor; }
			set { m_DiffuseColor = value; }
		}
		public Color AmbientColor
		{
			get { return m_AmbientColor; }
			set { m_AmbientColor = value; }
		}
		public Vector4 Attenuation
		{
			get { return m_Attenuation; }
			set { m_Attenuation = value; }
		}
		public float CosHalfPhi
		{
			get { return m_cosHalfPhi; }
			set { m_cosHalfPhi = value; }
		}
		public float CosHalfTheta
		{
			get { return m_cosHalfTheta; }
			set { m_cosHalfTheta = value; }
		}

		public Light(Vector3 vPosition, Vector3 vDirection, Color DiffuseColor, Color AmbientColor)
		{
			m_Position = new Vector3(-10.0f, 2.0f, 0.0f);
			m_Direction = new Vector3(1.0f, 0.0f, 0.0f);
			m_DiffuseColor = new Color(0.9f);
			m_AmbientColor= new Color(0.1f);
			m_Attenuation= new Vector4(0, 0, 0.4f, 0);

			m_cosHalfPhi = 0.4f;
			m_cosHalfTheta = 0.9f;
		}

		public virtual void SetProjParams(float fFov, float fAspect, float fNearPlane, float fFarPlane)
		{
			m_fFOV = fFov;
			m_fAspect = fAspect;
			m_fNearPlane = fNearPlane;
			m_fFarPlane = fFarPlane;
			m_mProj = Matrix.CreateProjection(m_fFOV, m_fAspect, m_fNearPlane, m_fFarPlane);
		}


	}

}


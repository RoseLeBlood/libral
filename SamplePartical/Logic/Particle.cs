//
//  Particle.cs
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

namespace SamplePartical
{
	public class Particle
	{
		Vector3   	m_Position; // Center point of particle
		Vector3   	m_Velocity; // Current particle velocity
		Color   	m_Color;    // Particle color
		float       m_fRotate;  // Rotate the particle the center
		float       m_fSize;    // Size of the particle
		float       m_fAge;
		float       m_fLifeTime;

		public Vector3 Position
		{
			get { return m_Position; }
			set { m_Position = value; }
		}
		public Vector3 Velocity
		{
			get { return m_Velocity; }
			set { m_Velocity = value; }
		}
		public Color Color
		{
			get { return m_Color; }
			set { m_Color = value; }
		}
		public float Rotate
		{
			get { return m_fRotate; }
			set { m_fRotate = value; }
		}
		public float Size
		{
			get { return m_fSize; }
			set { m_fSize = value; }
		}
		public float Age
		{
			get { return m_fAge; }
			set { m_fAge = value; }
		}
		public float LifeTime
		{
			get { return m_fLifeTime; }
			set { m_fLifeTime = value; }
		}

		public Particle()
		{
			m_Position = Vector3.Zero;
			m_Velocity = Vector3.Zero;
			m_Color = Colors.Black;
			m_fRotate = 0.0f;
			m_fAge = 0.0f;
			m_fLifeTime = 0.0f;
		}
	}
}


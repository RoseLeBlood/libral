//
//  Physik.cs
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
	public class Physik //: IEquatable<Physik>
	{
		private Matrix					m_Translation;
		private Vector3					m_Velocity;
		private Vector3					m_Accelerate;

		private Matrix					m_Rotation;
		private Vector3					m_AngularVelocity;
		private Vector3					m_AngularAccelerate;

		private Matrix					m_Scaling;
		private float					m_Mass;

		private IPhysikRenderObject		m_pRender;

		public Vector3	Accelerate 
		{
			get { return m_Accelerate; }
			set 
			{ 
				//D3DXVec3TransformNormal((D3DXVECTOR3*)&m_Accelerate, (D3DXVECTOR3*)&value, (D3DXMATRIX*)m_pRender->GetWorldMatrix());
				m_Accelerate = Vector3.TransformNormal(value, m_pRender.WorldMatrix);
			}
		}
		public Vector3	 AngAccel
		{
			get { return m_AngularAccelerate; }
			set
			{
				//D3DXVec3TransformNormal((D3DXVECTOR3*)&m_AngularAccelerate, (D3DXVECTOR3*)&value, (D3DXMATRIX*)m_pRender->GetWorldMatrix());
				m_Accelerate = Vector3.TransformNormal(value, m_pRender.WorldMatrix);
			}
		}
		public Vector3	Position
		{
			get
			{
				Vector3 pos1 = Vector3.Zero;
				return Vector3.TransformCoord(pos1, m_Translation);
			}
		}
		public Vector3 Velocity
		{
			get { return m_Velocity; }
			set { m_Velocity = value; }
		}
		public float Mass
		{
			get { return m_Mass; }
		}
		public BoundingSphere Sphere
		{
			get { return new BoundingSphere(Position, m_pRender.BoundingSphereRadius); }
		}
		public Physik(IPhysikRenderObject rand)
		{
			m_Velocity = Vector3.Zero;
			m_Accelerate = Vector3.Zero;
			m_AngularAccelerate = Vector3.Zero;
			m_AngularVelocity = Vector3.Zero;

			m_pRender = rand;

			m_Translation = Matrix.Identity;
			m_Rotation = Matrix.Identity;

			m_Scaling = Matrix.CreateScale(new Vector3(m_pRender.WorldMatrix.M11, 
													   m_pRender.WorldMatrix.M22,
													   m_pRender.WorldMatrix.M33));

			m_Mass = 1;

		}
		public Physik(IPhysikRenderObject rend, Vector3 Position,
			Vector3 Velocity, Vector3 Accelerator, float mass = 1.0f)
		{
			m_Velocity = Position;
			m_Accelerate = Velocity;
			m_AngularAccelerate = Accelerator;
			m_AngularVelocity = Vector3.Zero;

			m_pRender = rend;

			m_Translation = Matrix.CreateTranslation(Position);
			m_Rotation = Matrix.Identity;

			m_Scaling = Matrix.CreateScale(new Vector3(m_pRender.WorldMatrix.M11, 
				m_pRender.WorldMatrix.M22,
				m_pRender.WorldMatrix.M33));

			m_Mass = mass;
		}

		public virtual void Update(float fTime, float fRunTime)
		{
			Vector3 translation = Vector3.Scale(m_Velocity, fTime);
			float angle = fTime * m_AngularAccelerate.LengthSquared();

			Vector3 axis = Vector3.Normalize(m_AngularVelocity);

			if (m_pRender != null)
			{
				Matrix mr, mt, mWorld;

				mr = Matrix.CreateFromAxisAngle(axis, angle);
				m_Rotation *= mr;

				mt = Matrix.CreateTranslation(translation);
				m_Translation *= mt;

				Matrix m;
				m = m_Scaling * m_Rotation;
				mWorld = m * m_Translation;

				m_pRender.WorldMatrix = mWorld;
			}
			m_Velocity += fTime * m_Accelerate;
			m_AngularVelocity += fTime * m_AngularAccelerate;
		}
		public virtual void StopMotion()
		{
			m_Accelerate = Vector3.Zero;
			m_AngularAccelerate = Vector3.Zero;
			m_Velocity = Vector3.Zero;
			m_AngularVelocity = Vector3.Zero;
		}


		public static bool Collide(Physik p1, Physik p2)
		{
			Vector3 dist = p1.Position - p2.Position;

			if (dist.Length() <= p1.m_pRender.BoundingSphereRadius + p2.m_pRender.BoundingSphereRadius)
			{
				Vector3 v1 = p1.Velocity;
				Vector3 v2 = p2.Velocity;

				float m1 = p1.Mass;
				float m2 = p2.Mass;

				Vector3 v1New, v2New, a;

				a = Vector3.Normalize(dist);

				Vector3 n = Vector3.Cross(p1.Velocity, p2.Velocity);

				if (n.X == n.Y && n.X == n.Z && n.X == 0.0f) // Eindimensional
				{
					v1New = ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
					v2New = ((m2 - m1) * v2 + 2 * m1 * v1) / (m1 + m2);
				}
				else
				{
					n = Vector3.Normalize(n);

					Vector3 b = Vector3.Cross(n, a);

					float ab = Vector3.Dot(a, b);
					float v11 = (Vector3.Dot(v1, a) - Vector3.Dot(v1, b) * ab) /
					              (1 - ab * ab);
					float v12 = (Vector3.Dot(v1, b) - Vector3.Dot(v1, a) * ab) /
					              (1 - ab * ab);

					float v21 = (Vector3.Dot(v2, a) - Vector3.Dot(v2, b) * ab) /
					              (1 - ab * ab);
					float v22 = (Vector3.Dot(v2, b) - Vector3.Dot(v2, a) * ab) /
					              (1 - ab * ab);

					v1New = a * ((m1 - m2) * v11 + 2 * m2 * v21) / (m1 + m2) + b * v12;
					v2New = a * ((m2 - m1) * v21 + 2 * m1 * v11) / (m1 + m2) + b * v22;
				}

				p1.Velocity = v1New;
				p2.Velocity = v2New;

				return true;
			}
			return false;
		}
	}
}


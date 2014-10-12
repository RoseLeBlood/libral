//
//  Plane.cs
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
	public enum PlaneIntersection
	{
		Intersecting,
		Back,
		Front,
	}
	public class Plane : IEquatable<Plane>
	{
		private float m_fD;
		private Vector3 m_vNormal;

		public float D { get { return m_fD; } set { m_fD = value; } }
		public Vector3 Normal { get { return m_vNormal; } set { m_vNormal = value; } }

		public Plane()
			: this(0,0,0,0)
		{

		}
		public Plane (Vector4 value)
			: this(value.X, value.Y, value.Z, value.W)
		{
		}


		public Plane (Vector3 normal, float d)
			: this(normal.X, normal.Y, normal.Z, d)
		{
		}
		public Plane(Vector3 p, Vector3 n)
			: this(n, -n.X * p.X - n.Y * p.Y - n.Z * p.Z)
		{
			//raPlane(n, -n.x * p.x - n.y * p.y - n.z * p.z)
		}
		public Plane (float a, float b, float c, float d)
		{
			Normal = new Vector3 (a, b, c);
			D = d;
		}
		public Plane (Vector3 point1, Vector3 point2, Vector3 point3)
		{
			Vector3 a = Vector3.Subtract (point2, point1);
			Vector3 b = Vector3.Subtract (point3, point1);

			Vector3 normal = Vector3.Normalize(Vector3.Cross(a, b));
			float d = Vector3.Dot (normal, point1);

			Normal = normal;
			D = -d;
		}
		public void Normalize ()
		{
			//const float fLength = raVector4Lenght(s0); return raPlane(s0 / fLength);
			var s0 = new Vector4(Normal.X, Normal.Y, Normal.Z, D);
			var s1 = new Plane(s0 / s0.Length());
			this.D = s1.D;
			this.Normal = s1.Normal;
		}
		public float Dot (Vector4 value)
		{
			return (Normal.X * value.X) + (Normal.Y * value.Y) + (Normal.Z * value.Z) + (D * value.W);
		}
		public float DotCoordinate (Vector3 value)
		{
			return (Normal.X * value.X) + (Normal.Y * value.Y) + (Normal.Z * value.Z) + D;
		}
		public float DotNormal (Vector3 value)
		{
			return (Normal.X * value.X) + (Normal.Y * value.Y) + (Normal.Z * value.Z);
		}

		public static Plane Normalize (Plane value)
		{
			var s0 = new Vector4(value.Normal.X, value.Normal.Y, value.Normal.Z, value.D);
			var s1 = s0 / s0.Length();

			return new Plane(s1);
		}
		public bool Equals (Plane other)
		{
			return other == this;
		}

		public override bool Equals (object obj)
		{
			return obj is Plane && ((Plane)obj) == this;
		}

		public override int GetHashCode ()
		{
			return Normal.GetHashCode () ^ D.GetHashCode ();
		}

		public static bool operator == (Plane a, Plane b)
		{
			return a.D == b.D && a.Normal == b.Normal;
		}

		public static bool operator != (Plane a, Plane b)
		{
			return a.D != b.D || a.Normal != b.Normal;
		}
		public override string ToString ()
		{
			return string.Format ("Plane: {0} {1}", Normal, D);
		}
	}
}


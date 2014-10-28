//
//  BoundingFrustum.cs
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
	[Serializable]
	public class BoundingSphere : IEquatable<BoundingSphere>
	{
		private Vector3 	m_vCenter;
		private float 		m_fRadius;

		public Vector3 Center { get { return m_vCenter; } set { m_vCenter = value; } }
		public float Radius { get { return m_fRadius; } set { m_fRadius = value; }}

		public BoundingSphere (Vector3 center, float radius)
		{
			if (radius < 0)
				throw new ArgumentException ("Radius cannot be less than zero", "radius");

			m_vCenter = center;
			m_fRadius = radius;
		}
		public BoundingSphere (BoundingBox box)
		{
			Center = Vector3.Lerp (box.Min, box.Max, 0.5f);
			Radius = Vector3.Distance (box.Min, box.Max) * 0.5f;
		}
		public bool Intersects (BoundingBox box)
		{
			return Contains (box) == BoundingContains.Intersects;
		}
		public bool Intersects (BoundingSphere sphere)
		{
			return Contains (sphere) == BoundingContains.Intersects;
		}
		public PlaneIntersection Intersects (Plane plane)
		{
			float distance = Vector3.Dot(plane.Normal, Center);
			distance += plane.D;

			if (distance > Radius)
			{
				return PlaneIntersection.Front;
			}

			if (distance < -Radius)
			{
				return  PlaneIntersection.Back;
			}

			return PlaneIntersection.Intersecting;
		}
		public BoundingContains Contains (BoundingBox box)
		{
			if (!box.Intersects(this))
			{
				return BoundingContains.Disjoint;
			}

			float radiusSq = Radius * Radius;
			float cx = Center.X;
			float cy = Center.Y;
			float cz = Center.Z;

			if (new Vector3(cx - box.Min.X, cy - box.Max.Y, cz - box.Max.Z).LengthSquared() > radiusSq ||
			    new Vector3(cx - box.Max.X, cy - box.Max.Y, cz - box.Max.Z).LengthSquared() > radiusSq ||
			    new Vector3(cx - box.Max.X, cy - box.Min.Y, cz - box.Max.Z).LengthSquared() > radiusSq ||
			    new Vector3(cx - box.Min.X, cy - box.Min.Y, cz - box.Max.Z).LengthSquared() > radiusSq ||
			    new Vector3(cx - box.Min.X, cy - box.Max.Y, cz - box.Min.Z).LengthSquared() > radiusSq ||
			    new Vector3(cx - box.Max.X, cy - box.Max.Y, cz - box.Min.Z).LengthSquared() > radiusSq ||
			    new Vector3(cx - box.Max.X, cy - box.Min.Y, cz - box.Min.Z).LengthSquared() > radiusSq ||
			    new Vector3(cx - box.Min.X, cy - box.Min.Y, cz - box.Min.Z).LengthSquared() > radiusSq)
			{
				return BoundingContains.Intersects;
			}

			return BoundingContains.Contains;
		}
		public BoundingContains Contains (BoundingSphere sphere)
		{
			float dist = Vector3.Distance(Center, sphere.Center);
			float sphereRadius = sphere.Radius;

			if (Radius + sphereRadius < dist)
			{
				return BoundingContains.Disjoint;
			}

			if (Radius - sphereRadius < dist)
			{
				return  BoundingContains.Intersects;
			}

			return BoundingContains.Contains;
		}
		public BoundingContains Contains (Vector3 point)
		{
			if (Vector3.DistanceSquared(point, Center) >= Radius * Radius)
			{
				return BoundingContains.Disjoint;
			}

			return BoundingContains.Contains;
		}
		public bool Equals (BoundingSphere other)
		{
			return other == this;
		}

		public override bool Equals (object obj)
		{
			return obj is BoundingSphere && ((BoundingSphere)obj) == this;
		}

		public override int GetHashCode ()
		{
			return Radius.GetHashCode () ^ Center.GetHashCode ();
		}

		public static bool operator == (BoundingSphere a, BoundingSphere b)
		{
			return a.Radius == b.Radius && a.Center == b.Center;
		}

		public static bool operator != (BoundingSphere a, BoundingSphere b)
		{
			return a.Radius != b.Radius || a.Center != b.Center;
		}
		public override string ToString ()
		{
			return string.Format ("Sphere {0} {1}", Center, Radius);
		}
	}
}


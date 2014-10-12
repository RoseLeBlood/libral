//
//  BoundingBox.cs
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
using System.Collections.Generic;

namespace System.Common
{
	public enum BoundingContains
	{
		Disjoint,
		Contains,
		Intersects
	}

	[Serializable]
	public class BoundingBox : IEquatable<BoundingBox>
	{
		public const int CornerCount = 8;

		private Vector3 m_vMin;
		private Vector3 m_vMax;

		public Vector3 Min { get { return m_vMin; } set { m_vMin = value; } }
		public Vector3 Max { get { return m_vMax; } set { m_vMax = value; } }

		public BoundingBox (Vector3 min, Vector3 max)
		{
			Min = min;
			Max = max;
		}
		public BoundingBox(BoundingSphere sphere)
		{
			Min = new Vector3 (sphere.Center.X - sphere.Radius, sphere.Center.Y - sphere.Radius,
				sphere.Center.Z - sphere.Radius);
			Max = new Vector3 (sphere.Center.X + sphere.Radius, sphere.Center.Y + sphere.Radius,
				sphere.Center.Z + sphere.Radius);
		}
		public BoundingBox (IEnumerable<Vector3> points)
		{
			if (points == null)
				throw new ArgumentNullException("points");

			bool hasPoints = false;
			m_vMin = new Vector3(float.MaxValue);
			m_vMax = new Vector3(float.MinValue);

			foreach (Vector3 point in points)
			{
				Vector3 pt = point;

				Vector3.Min(ref m_vMin, ref pt, out m_vMin);
				Vector3.Max(ref m_vMax, ref pt, out m_vMax);

				hasPoints = true;
			}

			if (!hasPoints)
				throw new ArgumentException("No points were given", "points");
		}
		public BoundingBox (BoundingBox original, BoundingBox additional)
		{
			Min = Vector3.Min(original.Min, additional.Min);
			Max = Vector3.Max(original.Max, additional.Max);
		}
		public BoundingContains Contains (BoundingBox box)
		{
			if ((Max.X < box.Min.X || Min.X > box.Max.X) ||
				(Max.Y < box.Min.Y || Min.Y > box.Max.Y) ||
				(Max.Z < box.Min.Z || Min.Z > box.Max.Z)) 
				{
					return BoundingContains.Disjoint;
				}

			if ((Min.X <= box.Min.X && Max.X >= box.Max.X) &&
				(Min.Y <= box.Min.Y && Max.Y >= box.Max.Y) &&
				(Min.Z <= box.Min.Z && Max.Z >= box.Max.Z)) 
				{
					return BoundingContains.Contains;
				}

			return BoundingContains.Intersects;
		}
		public BoundingContains Contains (Vector3 point)
		{
			if ((Min.X <= point.X && Max.X >= point.X) &&
			    (Min.Y <= point.Y && Max.Y >= point.Y) &&
			    (Min.Z <= point.Z && Max.Z >= point.Z))
			{
				return BoundingContains.Contains;
			}

			return BoundingContains.Disjoint;
		}
		public BoundingContains Contains (BoundingSphere sphere)
		{
			Vector3 center = sphere.Center;
			Vector3 point = Vector3.Clamp(center, Min, Max);
			float dist = Vector3.DistanceSquared(center, point);

			float radius = sphere.Radius;

			if (dist > radius)
			{
				return BoundingContains.Disjoint;
			}

			if (Min.X + radius <= center.X && Max.X - radius >= center.X && Max.X - Min.X > radius &&
			    Min.Y + radius <= center.Y && Max.Y - radius >= center.Y && Max.Y - Min.Y > radius &&
			    Min.Z + radius <= center.Z && Max.Z - radius >= center.Z && Max.X - Min.X > radius)
			{
				return BoundingContains.Contains;
			}

			return BoundingContains.Intersects;
		}
		public Vector3[] GetCorners ()
		{
			var corners = new Vector3 [CornerCount];

			corners[0] = new Vector3 (Min.X, Max.Y, Max.Z);
			corners[1] = new Vector3 (Max.X, Max.Y, Max.Z);
			corners[2] = new Vector3 (Max.X, Min.Y, Max.Z);
			corners[3] = new Vector3 (Min.X, Min.Y, Max.Z);
			corners[4] = new Vector3 (Min.X, Max.Y, Min.Z);
			corners[5] = new Vector3 (Max.X, Max.Y, Min.Z);
			corners[6] = new Vector3 (Max.X, Min.Y, Min.Z);
			corners[7] = new Vector3 (Min.X, Min.Y, Min.Z);

			return corners;
		}
		public bool Intersects (BoundingBox box)
		{
			return Contains (box) == BoundingContains.Intersects;
		}
		public bool Intersects (BoundingSphere sphere)
		{
			return Contains (sphere) == BoundingContains.Intersects;
		}

		public bool Equals (BoundingBox other)
		{
			return other == this;
		}

		public override bool Equals (object obj)
		{
			return obj is BoundingBox && ((BoundingBox)obj) == this;
		}

		public override int GetHashCode ()
		{
			return Min.GetHashCode () ^ Max.GetHashCode ();
		}

		public static bool operator == (BoundingBox a, BoundingBox b)
		{
			return a.Min == b.Min && a.Max == b.Max;
		}

		public static bool operator != (BoundingBox a, BoundingBox b)
		{
			return a.Min != b.Min || a.Max != b.Max;
		}

		public override string ToString ()
		{
			return string.Format ("{Min:{0} Max:{1}}", Min, Max);
		}
	}
}


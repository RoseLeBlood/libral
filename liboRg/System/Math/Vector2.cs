//
//  Vector2.cs
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
using Mono.Simd;

namespace System.Common
{
	[Serializable]
	public class Vector2 : IEquatable<Vector2>
	{
		private Vector4f m_fVector4;

		public float X { get { return m_fVector4.X; } set { m_fVector4.X = value; } }
		public float Y { get { return m_fVector4.Y; } set { m_fVector4.Y = value; }}

		public static Vector2 UnitX 
		{
			get { return new Vector2 (1f, 0f); }
		}

		public static Vector2 UnitY 
		{
			get { return new Vector2 (0f, 1f); }
		}

		public static Vector2 One 
		{
			get { return new Vector2 (1f); }
		}

		public static Vector2 Zero 
		{
			get { return new Vector2 (0f); }
		}

		public Vector2 ()
			: this(0f, 0f)
		{
		}
		public Vector2 (float value)
			: this(value, value)
		{
		}
		public Vector2 (float[] value)
			: this(value[0], value[1])
		{
		}
		public Vector2 (float x, float y)
		{
			m_fVector4.X =  x;
			m_fVector4.Y = y;
		}
		public Vector2 (Vector4f fVector4)
		{
			m_fVector4 = fVector4;
		}

		public void Normalize ()
		{
			var x = this;
			Normalize (ref x, out x);
			X = x.X;
			Y = x.Y;
		}
		[Acceleration(AccelMode.SSE2)]
		public float Length ()
		{
			return (float) System.Math.Sqrt (LengthSquared ());
		}
		[Acceleration(AccelMode.SSE2)]
		public float LengthSquared ()
		{
			return X * X + Y * Y;
		}
		[Acceleration(AccelMode.SSE2)]
		public bool Equals (Vector2 other)
		{
			return X == other.X && Y == other.Y;
		}
		[Acceleration(AccelMode.SSE2)]
		public override bool Equals (object obj)
		{
			return obj is Vector2 && ((Vector2)obj) == this;
		}
		public override string ToString ()
		{
			return string.Format ("{0} {1}", X, Y);
		}
		public override int GetHashCode ()
		{
			unsafe 
			{
				Vector4f f = m_fVector4;
				Vector4i i = *((Vector4i*)&f);
				i = i ^ i.Shuffle (ShuffleSel.Swap);
				i = i ^ i.Shuffle (ShuffleSel.RotateLeft);
				return i.X;
			}
		}

		public static bool operator == (Vector2 a, Vector2 b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator != (Vector2 a, Vector2 b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static Vector2 Add (Vector2 value1, Vector2 value2)
		{
			return new Vector2(value1.X + value2.X,
				value1.Y + value2.Y);
		}

		public static void Add (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result = new Vector2(value1.X + value2.X,
								 value1.Y + value2.Y);
		}

		public static Vector2 Multiply (Vector2 value1, float scaleFactor)
		{
			return new Vector2 (value1.m_fVector4 / (new Vector4f (scaleFactor)) );
		}

		public static void Multiply (ref Vector2 value1, float scaleFactor, out Vector2 result)
		{
			result = new Vector2 (value1.m_fVector4 / (new Vector4f (scaleFactor)) );
		}

		public static Vector2 Multiply (Vector2 value1, Vector2 value2)
		{
			return new Vector2(value1.m_fVector4 * value2.m_fVector4);
		}

		public static void Multiply (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result = new Vector2(value1.m_fVector4 * value2.m_fVector4);
		}

		public static Vector2 Negate (Vector2 value)
		{
			Negate (ref value, out value); return value;
		}

		public static void Negate (ref Vector2 value, out Vector2 result)
		{
			result = new Vector2(-value.X, -value.Y);
		}

		public static Vector2 Divide (Vector2 value1, float divider)
		{
			return new Vector2 (value1.m_fVector4 / (new Vector4f (divider)) );
		}

		public static void Divide (ref Vector2 value1, float divider, out Vector2 result)
		{
			result = new Vector2 (value1.m_fVector4 / (new Vector4f (divider)) );
		}

		public static Vector2 Divide (Vector2 value1, Vector2 value2)
		{
			return new Vector2 (value1.m_fVector4 / value2.m_fVector4);
		}

		public static void Divide (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result = new Vector2(value1.m_fVector4 / value2.m_fVector4);
		}

		public static Vector2 Subtract (Vector2 value1, Vector2 value2)
		{
			Subtract (ref value1, ref value2, out value1); return value1;
		}

		public static void Subtract (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result = new Vector2(value1.X - value2.X, value1.Y - value2.Y);
		}
		public static Vector2 operator + (Vector2 value1, Vector2 value2)
		{
			return new Vector2 ( value1.m_fVector4  + value2.m_fVector4);
		}

		public static Vector2 operator / (Vector2 value, float divider)
		{
			return new Vector2 ( value.m_fVector4 / new Vector4f (divider));
		}

		public static Vector2 operator / (Vector2 value1, Vector2 value2)
		{
			return new Vector2 ( value1.m_fVector4  / value2.m_fVector4);
		}

		public static Vector2 operator * (Vector2 value1, Vector2 value2)
		{
			return new Vector2 ( value1.m_fVector4 * value2.m_fVector4);
		}

		public static Vector2 operator * (Vector2 value, float scaleFactor)
		{
			return new Vector2 ( value.m_fVector4 * new Vector4f (scaleFactor));	
		}

		public static Vector2 operator * (float scaleFactor, Vector2 value)
		{
			return new Vector2 ( value.m_fVector4 * new Vector4f (scaleFactor));
		}

		public static Vector2 operator - (Vector2 value1, Vector2 value2)
		{
			return new Vector2 (value1.m_fVector4 - value1.m_fVector4);
		}

		public static Vector2 operator - (Vector2 value)
		{
			return new Vector2 (- value.X, - value.Y);
		}


		public static Vector2 Lerp (Vector2 value1, Vector2 value2, float amount)
		{
			Lerp (ref value1, ref value2, amount, out value1);
			return value1;
		}

		public static void Lerp (ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
		{
			Subtract (ref value2, ref value1, out result);
			Multiply (ref result, amount, out result);
			Add (ref value1, ref result, out result);
		}

		public static Vector2 SmoothStep (Vector2 value1, Vector2 value2, float amount)
		{
			SmoothStep (ref value1, ref value2, amount, out value1);
			return value1;
		}

		public static void SmoothStep (ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
		{
			float scale = (amount * amount * (3 - 2 * amount));
			Subtract (ref value2, ref value1, out result);
			Multiply (ref result, scale, out result);
			Add (ref value1, ref result, out result);
		}
	
		public static void Clamp (ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
		{
			var v4 = VectorOperations.Min (VectorOperations.Max (
				new Vector4f (value1.X, value1.Y, 0f, 0f),
				new Vector4f (min.X, min.Y, 0f, 0f)),
				new Vector4f (max.X, max.Y, 0f, 0f));
			result = new Vector2 (v4.X, v4.Y);
		}

		public static float Distance (Vector2 value1, Vector2 value2)
		{
			float result;
			Distance (ref value1, ref value2, out result);
			return result;
		}

		public static void Distance (ref Vector2 value1, ref Vector2 value2, out float result)
		{
			Vector4f r0 = value2.m_fVector4 - value1.m_fVector4;
			r0 = r0 * r0;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			result = r0.Sqrt ().X;
		}

		public static float DistanceSquared (Vector2 value1, Vector2 value2)
		{
			float result;
			DistanceSquared (ref value1, ref value2, out result);
			return result;
		}

		public static void DistanceSquared (ref Vector2 value1, ref Vector2 value2, out float result)
		{
			Vector2 val;
			Subtract (ref value1, ref value2, out val);
			result = val.LengthSquared ();
		}

		public static float Dot (Vector2 value1, Vector2 value2)
		{
			float result;
			Dot (ref value1, ref value2, out result);
			return result;
		}

		public static void Dot (ref Vector2 value1, ref Vector2 value2, out float result)
		{
			result = value1.X * value2.X + value1.Y * value2.Y;
		}



		public static Vector2 Max (Vector2 value1, Vector2 value2)
		{
			Max (ref value1, ref value2, out value1);
			return value1;
		}

		public static void Max (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result = new Vector2( VectorOperations.Max (value1.m_fVector4, value2.m_fVector4));
		}

		public static Vector2 Min (Vector2 value1, Vector2 value2)
		{
			Min (ref value1, ref value2, out value1);
			return value1;
		}

		public static void Min (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result = new Vector2( VectorOperations.Min (value1.m_fVector4, value2.m_fVector4));
		}
		public static Vector2 Normalize (Vector2 value)
		{
			value.Normalize ();
			return value;
		}
		public static void Normalize (ref Vector2 value, out Vector2 result)
		{
			Vector4f v4 = value.m_fVector4;
			Vector4f r0 = v4 * v4;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			v4 = v4 / r0.Sqrt ();

			result = new Vector2(v4);
		}
		public static Vector2 Reflect (Vector2 vector, Vector2 normal)
		{
			Vector2 result;
			Reflect (ref vector, ref normal, out result);
			return result;
		}

		public static void Reflect (ref Vector2 vector, ref Vector2 normal, out Vector2 result)
		{
			float d2 = (float) System.Math.Sqrt (normal.X * vector.X + normal.Y * vector.Y);
			d2 = d2 + d2;
			var x = d2 * normal.m_fVector4 - vector.m_fVector4;
			result = new Vector2(x);
		}
		public static Vector2 Transform( Vector2 vector, Matrix transform )
		{
			Vector2 result = new Vector2();

			result.X = (vector.X * transform.M11) + (vector.Y * transform.M21) + transform.M41;
			result.Y = (vector.X * transform.M12) + (vector.Y * transform.M22) + transform.M42;

			return result;
		}
		public static Vector2 Transform( Vector2 value, Quaternion rotation )
		{
			Vector2 vector = new Vector2();
			float x = rotation.X + rotation.X;
			float y = rotation.Y + rotation.Y;
			float z = rotation.Z + rotation.Z;
			float wz = rotation.W * z;
			float xx = rotation.X * x;
			float xy = rotation.X * y;
			float yy = rotation.Y * y;
			float zz = rotation.Z * z;

			vector.X = ((value.X * ((1.0f - yy) - zz)) + (value.Y * (xy - wz)));
			vector.Y = ((value.X * (xy + wz)) + (value.Y * ((1.0f - xx) - zz)));

			return vector;
		}
		public static Vector2 TransformCoordinate( Vector2 coord, Matrix transform )
		{
			Vector4 vector = new Vector4();

			vector.X = (coord.X * transform.M11) + (coord.Y * transform.M21) + transform.M41;
			vector.Y = (coord.X * transform.M12) + (coord.Y * transform.M22) + transform.M42;
			vector.Z = (coord.X * transform.M13) + (coord.Y * transform.M23) + transform.M43;
			vector.W = 1 / ((coord.X * transform.M14) + (coord.Y * transform.M24) + transform.M44);

			return new Vector2( vector.X * vector.W, vector.Y * vector.W );
		}
		public static Vector2 TransformNormal( Vector2 normal, Matrix transform )
		{
			Vector2 vector = new Vector2();

			vector.X = (normal.X * transform.M11) + (normal.Y * transform.M21);
			vector.Y = (normal.X * transform.M12) + (normal.Y * transform.M22);

			return vector;
		}

		public float[] ToArray()
		{
			return new float[]
			{
				X, Y
			};
		}
	}
}


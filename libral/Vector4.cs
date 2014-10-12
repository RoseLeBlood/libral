//
//  Vector4.cs
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
	public class Vector4 :  IEquatable<Vector4>
	{
		internal Vector4f m_fVector4;
		public float X { get { return m_fVector4.X; } set { m_fVector4.X = value; } }
		public float Y { get { return m_fVector4.Y; } set { m_fVector4.Y = value; } }
		public float Z { get { return m_fVector4.Z; } set { m_fVector4.Z = value; } }
		public float W { get { return m_fVector4.W; } set { m_fVector4.W = value; } }

		public static Vector4 UnitX 
		{
			get { return new Vector4 (1f, 0f, 0f, 0f); }
		}

		public static Vector4 UnitY 
		{
			get { return new Vector4 (0f, 1f, 0f, 0f); }
		}

		public static Vector4 UnitZ 
		{
			get { return new Vector4 (0f, 0f, 1f, 0f); }
		}

		public static Vector4 UnitW 
		{
			get { return new Vector4 (0f, 0f, 0f, 1f); }
		}

		public static Vector4 One
		{
			get { return new Vector4 (1f); }
		}

		public static Vector4 Zero 
		{
			get { return new Vector4 (0f); }
		}

		public Vector4()
			: this(0.0f)
		{

		}
		public Vector4 (float value)
			: this(new Vector4f (value))
		{
		}
		public Vector4 (Vector4f fVector4) 
		{ 
			m_fVector4 = fVector4; 
		}
		public Vector4 (float x, float y, float z, float w)
		{
			m_fVector4 = new Vector4f (x, y, z, w);
		}
		public float Length ()
		{
			Vector4f r0 = m_fVector4;
			r0 = r0 * r0;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			return r0.Sqrt ().X;
		}
		public float LengthSquared ()
		{
			Vector4f r0 = m_fVector4;
			r0 = r0 * r0;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			return r0.X;
		}
		public void Normalize ()
		{
			Vector4 val = Normalize (this);
			m_fVector4 = val.m_fVector4;
		}
		public override string ToString ()
		{
			return string.Format ("{0} {1} {2} {3}", X, Y, Z, W);
		}
		public bool Equals (Vector4 other)
		{
			return m_fVector4 == other.m_fVector4;
		}
		public override bool Equals (object obj)
		{
			return obj is Vector4 && ((Vector4)obj) == this;
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

		public Vector4 Min(Vector4 val2)
		{
			return Vector4.Min(this, val2);
		}
		public static Vector4 CatmullRom (Vector4 value1, Vector4 value2, Vector4 value3, Vector4 value4, float amount)
		{
			return new Vector4 ( 
				MathUtil.CatmullRom (value1.X, value2.X, value3.X, value4.X, amount),
				MathUtil.CatmullRom (value1.Y, value2.Y, value3.Y, value4.Y, amount),
				MathUtil.CatmullRom (value1.Z, value2.Z, value3.Z, value4.Z, amount),
				MathUtil.CatmullRom (value1.W, value2.W, value3.W, value4.W, amount));;
		}
		public static Vector4 Hermite (Vector4 value1, Vector4 tangent1, Vector4 value2, Vector4 tangent2, float amount)
		{
			var s = new Vector4f (amount);
			var s2 = s * s;
			var s3 = s2 * s;
			var c1 = new Vector4f (1f);
			var c2 = new Vector4f (2f);
			var m2 = new Vector4f (-2f);
			var c3 = new Vector4f (3f);

			var h1 = c2 * s3 - c3 * s2 + c1;
			var h2 = m2 * s3 + c3 * s2;
			var h3 = s3 - 2 * s2 + s;
			var h4 = s3 - s2;

			return new Vector4( h1 * value1.m_fVector4 + h2 * value2.m_fVector4 + h3 * 
				tangent1.m_fVector4 + h4 * tangent2.m_fVector4);
		}
		public static Vector4 Lerp (Vector4 value1, Vector4 value2, float amount)
		{
			return new Vector4( value1.m_fVector4 + (value2.m_fVector4 - value1.m_fVector4) * amount); ;
		}
		public static Vector4 SmoothStep (Vector4 value1, Vector4 value2, float amount)
		{
			float scale = (amount * amount * (3 - 2 * amount));
			return new Vector4( value1.m_fVector4 + (value2.m_fVector4 - value1.m_fVector4) * scale); 
		}
		public static Vector4 Barycentric (Vector4 value1, Vector4 value2, Vector4 value3, float amount1, float amount2)
		{
			return new Vector4 (
				MathUtil.Barycentric (value1.X, value2.X, value3.X, amount1, amount2),
				MathUtil.Barycentric (value1.Y, value2.Y, value3.Y, amount1, amount2),
				MathUtil.Barycentric (value1.Z, value2.Z, value3.Z, amount1, amount2),
				MathUtil.Barycentric (value1.W, value2.W, value3.Z, amount1, amount2));
		}
		public static Vector4 Clamp (Vector4 value1, Vector4 min, Vector4 max)
		{
			return new Vector4()
				{
					m_fVector4 = new Vector4f(
						Math.Min(Math.Max(value1.X, min.X), max.X),
						Math.Min(Math.Max(value1.Y, min.Y), max.Y),
						Math.Min(Math.Max(value1.Z, min.Z), max.Z),
						Math.Min(Math.Max(value1.W, min.W), max.W))
				};
		}
		public static float Distance (Vector4 value1, Vector4 value2)
		{
			Vector4f r0 = value2.m_fVector4 - value1.m_fVector4;
			r0 = r0 * r0;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			return r0.Sqrt ().X;
		}	
		public static float DistanceSquared (Vector4 value1, Vector4 value2)
		{
			Vector4f r0 = value2.m_fVector4 - value1.m_fVector4;
			r0 = r0 * r0;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			return r0.X ;
		}
		public static float Dot (Vector4 vector1, Vector4 vector2)
		{
			Vector4f r0 = vector2.m_fVector4 * vector1.m_fVector4;
			Vector4f r1 = r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r1;
			r1 = r0.Shuffle (ShuffleSel.RotateLeft);
			r0 = r0 + r1;
			return r0.Sqrt ().X;
		}

		public static Vector4 operator + (Vector4 value1, Vector4 value2)
		{
			return new Vector4 (value1.m_fVector4 + value2.m_fVector4);
		}
		public static Vector4 operator + (Vector4 value1, float value2)
		{
			return new Vector4 (value1.m_fVector4 + new Vector4f(value2));
		}	
		public static Vector4 operator - (Vector4 value1, Vector4 value2)
		{
			return new Vector4 (value1.m_fVector4 - value2.m_fVector4);
		}
		public static Vector4 operator - (Vector4 value)
		{
			return new Vector4 (value.m_fVector4 ^ new Vector4f (-0.0f));
		}
		public static Vector4 operator / (Vector4 value, float divider)
		{
			return new Vector4 (value.m_fVector4 / new Vector4f (divider));
		}
		public static Vector4 operator / (float divider, Vector4 value)
		{
			return new Vector4 (new Vector4f (divider) / value.m_fVector4);
		}
		public static Vector4 operator / (Vector4 value1, Vector4 value2)
		{
			return new Vector4 (value1.m_fVector4 / value2.m_fVector4);
		}
		public static Vector4 operator * (Vector4 value1, Vector4 value2)
		{
			return new Vector4 (value1.m_fVector4 / value2.m_fVector4);
		}
		public static Vector4 operator * (Vector4 value, float divider)
		{
			return new Vector4 (value.m_fVector4 * new Vector4f (divider));
		}
		public static Vector4 operator * (float scaleFactor, Vector4 value1)
		{
			return new Vector4 (value1.m_fVector4 * scaleFactor);
		}
		public static bool operator == (Vector4 a, Vector4 b)
		{
			return a.m_fVector4 == b.m_fVector4;
		}
		public static bool operator != (Vector4 a, Vector4 b)
		{
			return a.m_fVector4 != b.m_fVector4;
		}

		public static Vector4 Add (Vector4 value1, Vector4 value2)
		{
			return new Vector4 (value1.m_fVector4 + value2.m_fVector4);
		}

		public static Vector4 Subtract (Vector4 value1, Vector4 value2)
		{
			return new Vector4 (value1.m_fVector4 - value2.m_fVector4);
		}

		public static Vector4 Divide (Vector4 value1, float value2)
		{
			return new Vector4 (value1.m_fVector4 / new Vector4f (value2));
		}

		public static Vector4 Divide (Vector4 value1, Vector4 value2)
		{
			return new Vector4(value1.m_fVector4 / value2.m_fVector4);	
		}

		public static Vector4 Multiply (Vector4 value1, float scaleFactor)
		{
			return new Vector4 (value1.m_fVector4 * new Vector4f (scaleFactor));
		}

		public static Vector4 Multiply (Vector4 value1, Vector4 value2)
		{
			return new Vector4(value1.m_fVector4 * value2.m_fVector4);
		}

		public static Vector4 Negate (Vector4 value)
		{
			return new Vector4 (value.m_fVector4 ^ new Vector4f (-0.0f));
		}
		public static Vector4 Max (Vector4 value1, Vector4 value2)
		{
			return new Vector4(value1.m_fVector4.Max(value2.m_fVector4));;
		}
		public static Vector4 Min (Vector4 value1, Vector4 value2)
		{
			return new Vector4(value1.m_fVector4.Min(value2.m_fVector4));;
		}
		public static Vector4 Transform( Vector4 vector, Matrix transform )
		{
			Vector4 result = new Vector4();

			result.X = (vector.X * transform.M11) + (vector.Y * transform.M21) + (vector.Z * transform.M31) + (vector.W * transform.M41);
			result.Y = (vector.X * transform.M12) + (vector.Y * transform.M22) + (vector.Z * transform.M32) + (vector.W * transform.M42);
			result.Z = (vector.X * transform.M13) + (vector.Y * transform.M23) + (vector.Z * transform.M33) + (vector.W * transform.M43);
			result.W = (vector.X * transform.M14) + (vector.Y * transform.M24) + (vector.Z * transform.M34) + (vector.W * transform.M44);

			return result;
		}
		public static Vector4 Transform( Vector4 value, Quaternion rotation )
		{
			Vector4 vector = new Vector4();
			float x = rotation.X + rotation.X;
			float y = rotation.Y + rotation.Y;
			float z = rotation.Z + rotation.Z;
			float wx = rotation.W * x;
			float wy = rotation.W * y;
			float wz = rotation.W * z;
			float xx = rotation.X * x;
			float xy = rotation.X * y;
			float xz = rotation.X * z;
			float yy = rotation.Y * y;
			float yz = rotation.Y * z;
			float zz = rotation.Z * z;

			vector.X = ((value.X * ((1.0f - yy) - zz)) + (value.Y * (xy - wz))) + (value.Z * (xz + wy));
			vector.Y = ((value.X * (xy + wz)) + (value.Y * ((1.0f - xx) - zz))) + (value.Z * (yz - wx));
			vector.Z = ((value.X * (xz - wy)) + (value.Y * (yz + wx))) + (value.Z * ((1.0f - xx) - yy));
			vector.W = value.W;

			return vector;
		}
		public static Vector4 Normalize (Vector4 value)
		{
			Vector4f r0 = value.m_fVector4;
			r0 = r0 * r0;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			return new Vector4( value.m_fVector4 / r0.Sqrt ());
		}
	}
}


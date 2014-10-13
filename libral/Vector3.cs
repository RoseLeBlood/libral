//
//  Vector3.cs
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
	public class Vector3 :  IEquatable<Vector3>
	{
		private Vector4f m_fVector4;

		public float X { get { return m_fVector4.X; } set { m_fVector4.X = value; } }
		public float Y { get { return m_fVector4.Y; } set { m_fVector4.Y = value; } }
		public float Z { get { return m_fVector4.Z; } set { m_fVector4.Z = value; } }

		public static Vector3 Right 
		{
			get { return new Vector3 (1f, 0f, 0f); }
		}

		public static Vector3 Left 
		{
			get { return new Vector3 (-1f, 0f, 0f); }
		}

		public static Vector3 Up 
		{
			get { return new Vector3 (0f, 1f, 0f); }
		}

		public static Vector3 Down 
		{
			get { return new Vector3 (0f, -1f, 0f); }
		}

		public static Vector3 Backward 
		{
			get { return new Vector3 (0f, 0f, 1f); }
		}

		public static Vector3 Forward 
		{
			get { return new Vector3 (0f, 0f, -1f); }
		}

		public static Vector3 UnitX 
		{
			get { return new Vector3 (1f, 0f, 0f); }
		}

		public static Vector3 UnitY 
		{
			get { return new Vector3 (0f, 1f, 0f); }
		}

		public static Vector3 UnitZ 
		{
			get { return new Vector3 (0f, 0f, 1f); }
		}

		public static Vector3 One 
		{
			get { return new Vector3 (1f); }
		}

		public static Vector3 Zero 
		{
			get { return new Vector3 (0f); }
		}

		public Vector3()
		{
		}
		public Vector3(float xyz)
			: this(xyz, xyz, xyz)
		{
		}
		public Vector3(float[] xyz)
			: this(xyz[0], xyz[1], xyz[2])
		{
		}
		public Vector3 (float x, float y, float z)
		{
			m_fVector4 = new Vector4f(x, y, z, 0f);
		}
		public Vector3 (Vector4f pVector)
		{ 
			this.m_fVector4 = pVector; 
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
			Vector3 vec = this;
			Normalize (ref vec, out vec);
			X = vec.X;
			Y = vec.Y;
			Z = vec.Z;
		}
		public bool Equals (Vector3 other)
		{
			return m_fVector4 == other.m_fVector4;
		}
		public override bool Equals (object obj)
		{
			return obj is Vector3 && ((Vector3)obj) == this;
		}
		public static Vector3 CatmullRom (Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, float amount)
		{
			CatmullRom (ref value1, ref value2, ref value3, ref value4, amount, out value1);
			return value1;
		}

		public static void CatmullRom (ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4,
			float amount, out Vector3 result)
		{
			result = new Vector3 (new Vector4f(
				MathUtil.CatmullRom (value1.X, value2.X, value3.X, value4.X, amount),
				MathUtil.CatmullRom (value1.Y, value2.Y, value3.Y, value4.Y, amount),
				MathUtil.CatmullRom (value1.Z, value2.Z, value3.Z, value4.Z, amount),
				0));
		}
		public static Vector3 Hermite (Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, float amount)
		{
			Hermite (ref value1, ref tangent1, ref value2, ref tangent2, amount, out value1);
			return value1;
		}

		public static void Hermite (ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2,
			float amount, out Vector3 result)
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

			result = new Vector3(h1 * value1.m_fVector4 + h2 * value2.m_fVector4 + h3 * tangent1.m_fVector4 + h4 * tangent2.m_fVector4);
		}
		public static Vector3 Lerp (Vector3 value1, Vector3 value2, float amount)
		{
			Lerp (ref value1, ref value2, amount, out value1);
			return value1;
		}

		public static void Lerp (ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
		{
			result = new Vector3(value1.m_fVector4 + (value2.m_fVector4 - value1.m_fVector4) * amount); 
		}

		public static Vector3 SmoothStep (Vector3 value1, Vector3 value2, float amount)
		{
			SmoothStep (ref value1, ref value2, amount, out value1);
			return value1;
		}

		public static void SmoothStep (ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
		{
			float scale = (amount * amount * (3 - 2 * amount));	
			result = new Vector3(value1.m_fVector4 + (value2.m_fVector4 - value1.m_fVector4) * scale); 
		}

		public static Vector3 Barycentric (Vector3 value1, Vector3 value2, Vector3 value3, float amount1, float amount2)
		{
			Barycentric (ref value1, ref value2, ref value3, amount1, amount2, out value1);
			return value1;
		}

		public static void Barycentric (ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, float amount1,
			float amount2, out Vector3 result)
		{
			result = new Vector3 (
				MathUtil.Barycentric (value1.X, value2.X, value3.X, amount1, amount2),
				MathUtil.Barycentric (value1.Y, value2.Y, value3.Y, amount1, amount2),
				MathUtil.Barycentric (value1.Z, value2.Z, value3.Z, amount1, amount2));
		}

		public static Vector3 Clamp (Vector3 value1, Vector3 min, Vector3 max)
		{
			Clamp (ref value1, ref min, ref max, out value1);
			return value1;
		}

		public static void Clamp (ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result)
		{
			result = new Vector3(
				Math.Min(Math.Max(value1.X, min.X), max.X),
				Math.Min(Math.Max(value1.X, min.X), max.X),
				Math.Min(Math.Max(value1.X, min.X), max.X));
		}
		public static Vector3 Normalize (Vector3 value)
		{
			value.Normalize ();
			return value;
		}

		public static void Normalize (ref Vector3 value, out Vector3 result)
		{
			Vector4f r0 = value.m_fVector4;
			r0 = r0 * r0;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			result = new Vector3( value.m_fVector4 / r0.Sqrt ());
		}
		public static Vector3 Reflect (Vector3 vector, Vector3 normal)
		{
			Vector3 result;
			Reflect (ref vector, ref normal, out result);
			return result;
		}

		public static void Reflect (ref Vector3 vector, ref Vector3 normal, out Vector3 result)
		{
			Vector4f v = vector.m_fVector4, n = normal.m_fVector4;
			Vector4f r0 = v * n;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			r0 = r0.Sqrt ();
			result = new Vector3( (r0 + r0) * n - v);
		}
		public static Vector3 Cross (Vector3 vector1, Vector3 vector2)
		{
			Vector3 result;
			Cross (ref vector1, ref vector2, out result);
			return result;
		}

		public static void Cross (ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
		{
			Vector4f r1 = vector1.m_fVector4;
			Vector4f r2 = vector2.m_fVector4;
			result = new Vector3(
				r1.Shuffle (ShuffleSel.XFromY | ShuffleSel.YFromZ | ShuffleSel.ZFromX | ShuffleSel.WFromW) *
				r2.Shuffle (ShuffleSel.XFromZ | ShuffleSel.YFromX | ShuffleSel.ZFromY | ShuffleSel.WFromW) -
				r1.Shuffle (ShuffleSel.XFromZ | ShuffleSel.YFromX | ShuffleSel.ZFromY | ShuffleSel.WFromW) *
				r2.Shuffle (ShuffleSel.XFromY | ShuffleSel.YFromZ | ShuffleSel.ZFromX | ShuffleSel.WFromW));
		}

		public static float Distance (Vector3 value1, Vector3 value2)
		{
			float result;
			Distance (ref value1, ref value2, out result);
			return result;
		}

		public static void Distance (ref Vector3 value1, ref Vector3 value2, out float result)
		{
			Vector4f r0 = value2.m_fVector4 - value1.m_fVector4;
			r0 = r0 * r0;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			result = r0.Sqrt ().X;
		}

		public static float DistanceSquared (Vector3 value1, Vector3 value2)
		{
			float result;
			DistanceSquared (ref value1, ref value2, out result);
			return result;
		}

		public static void DistanceSquared (ref Vector3 value1, ref Vector3 value2, out float result)
		{
			Vector4f r0 = value2.m_fVector4 - value1.m_fVector4;
			r0 = r0 * r0;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			result = r0.X;
		}
		[Acceleration(AccelMode.SSE2)]
		public static float Dot (Vector3 vector1, Vector3 vector2)
		{
			Vector4f r0 = vector2.m_fVector4 * vector1.m_fVector4;
			r0 = r0 + r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r0.Shuffle (ShuffleSel.RotateLeft);
			return r0.Sqrt ().X;
		}
		[Acceleration(AccelMode.SSE2)]
		public static Vector3 Project( Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix worldViewProjection )
		{
			vector = TransformCoord(vector, worldViewProjection);
			return new Vector3( ( ( 1.0f + vector.X ) * 0.5f * width ) + x, ( ( 1.0f - vector.Y ) * 0.5f * height ) + y, ( vector.Z * ( maxZ - minZ ) ) + minZ );
		}
		[Acceleration(AccelMode.SSE2)]
		public static Vector3 Unproject( Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix worldViewProjection )
		{
			Vector3 v = new Vector3();
			Matrix matrix = Matrix.Invert( worldViewProjection );

			v.X = ( ( ( vector.X - x ) / width ) * 2.0f ) - 1.0f;
			v.Y = -( ( ( ( vector.Y - y ) / height ) * 2.0f ) - 1.0f );
			v.Z = ( vector.Z - minZ ) / ( maxZ - minZ );

			return TransformCoord( v, matrix);
		}
		public static Vector3 Add (Vector3 value1, Vector3 value2)
		{
			return new Vector3 (value1.m_fVector4 + value2.m_fVector4);
		}
		public static void Add (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result = new Vector3(value1.m_fVector4 + value2.m_fVector4);
		}
		public static Vector3 Subtract (Vector3 value1, Vector3 value2)
		{
			return new Vector3 (value1.m_fVector4 - value2.m_fVector4);
		}

		public static void Subtract (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result = new Vector3(value1.m_fVector4 - value2.m_fVector4);
		}
		public static Vector3 Divide (Vector3 value1, float value2)
		{
			return new Vector3 (value1.m_fVector4 / new Vector4f (value2));
		}
		public static void Divide (ref Vector3 value1, float value2, out Vector3 result)
		{
			result = new Vector3(value1.m_fVector4 / new Vector4f (value2));
		}
		public static Vector3 Divide (Vector3 value1, Vector3 value2)
		{
			return new Vector3 (value1.m_fVector4 / value2.m_fVector4);
		}

		public static void Divide (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result = new Vector3(value1.m_fVector4 / value2.m_fVector4);
		}

		public static Vector3 Multiply (Vector3 value1, float scaleFactor)
		{
			return new Vector3 (value1.m_fVector4 * new Vector4f (scaleFactor));
		}

		public static void Multiply (ref Vector3 value1, float scaleFactor, out Vector3 result)
		{
			result = new Vector3( value1.m_fVector4 * new Vector4f (scaleFactor));
		}

		public static Vector3 Multiply (Vector3 value1, Vector3 value2)
		{
			return new Vector3 (value1.m_fVector4 * value2.m_fVector4);	
		}

		public static void Multiply (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result = new Vector3( value1.m_fVector4 * value2.m_fVector4);
		}
		public static Vector3 Negate (Vector3 value)
		{
			return new Vector3 (value.m_fVector4 ^ new Vector4f (-0.0f));
		}

		public static void Negate (ref Vector3 value, out Vector3 result)
		{
			result = new Vector3 (value.m_fVector4 ^ new Vector4f (-0.0f));
		}
		public static Vector3 operator + (Vector3 value1, Vector3 value2)
		{
			return new Vector3 (value1.m_fVector4 + value2.m_fVector4);
		}

		public static Vector3 operator / (Vector3 value, float divider)
		{
			return new Vector3 (value.m_fVector4 / new Vector4f (divider));
		}

		public static Vector3 operator / (Vector3 value1, Vector3 value2)
		{
			return new Vector3 (value1.m_fVector4 / value2.m_fVector4);
		}

		public static Vector3 operator * (Vector3 value1, Vector3 value2)
		{
			return new Vector3 (value1.m_fVector4 * value2.m_fVector4);
		}

		public static Vector3 operator * (Vector3 value, float scaleFactor)
		{
			return new Vector3 (value.m_fVector4 * scaleFactor);	
		}

		public static Vector3 operator * (float scaleFactor, Vector3 value)
		{
			return new Vector3 (scaleFactor* value.m_fVector4);	
		}

		public static Vector3 operator - (Vector3 value1, Vector3 value2)
		{
			return new Vector3 (value1.m_fVector4 - value2.m_fVector4);
		}

		public static Vector3 operator - (Vector3 value)
		{
			return new Vector3 (value.m_fVector4 ^ new Vector4f (-0.0f));
		}
		public static bool operator == (Vector3 a, Vector3 b)
		{
			return a.m_fVector4 == b.m_fVector4;
		}

		public static bool operator != (Vector3 a, Vector3 b)
		{
			return a.m_fVector4 != b.m_fVector4;
		}
		public static Vector3 Max (Vector3 value1, Vector3 value2)
		{
			Max (ref value1, ref value2, out value1);
			return value1;
		}

		public static void Max (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result = new Vector3( VectorOperations.Max (value1.m_fVector4, value2.m_fVector4));
		}

		public static Vector3 Min (Vector3 value1, Vector3 value2)
		{
			Min (ref value1, ref value2, out value1);
			return value1;
		}

		public static void Min (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result = new Vector3( VectorOperations.Min (value1.m_fVector4, value2.m_fVector4));
		}

		public static Vector3 Scale(Vector3 m_Velocity, float fTime)
		{
			throw new NotImplementedException();
		}

		public static Vector3 TransformCoord(Vector3 coord, Matrix transform)
		{
			Vector4f vector = coord.m_fVector4;

			vector.X = (((coord.X * transform.M11) + (coord.Y * transform.M21)) + (coord.Z * transform.M31)) + transform.M41;
			vector.Y = (((coord.X * transform.M12) + (coord.Y * transform.M22)) + (coord.Z * transform.M32)) + transform.M42;
			vector.Z = (((coord.X * transform.M13) + (coord.Y * transform.M23)) + (coord.Z * transform.M33)) + transform.M43;
			vector.W = 1 / ((((coord.X * transform.M14) + (coord.Y * transform.M24)) + (coord.Z * transform.M34)) + transform.M44);

			return new Vector3( vector );
		}

		public static Vector3 TransformNormal(Vector3 value, Matrix m)
		{
			float x = value.X;
			float y = value.Y;
			float z = value.Z;

			return new Vector3(
				x * m.M11 + y * m.M21 + z * m.M31,
				x * m.M12 + y * m.M22 + z * m.M32,
				x * m.M13 + y * m.M23 + z * m.M33);

		}
		public static Vector3 Transform( Vector3 vector, Matrix transform )
		{
			Vector3 result = new Vector3();

			result.X = (((vector.X * transform.M11) + (vector.Y * transform.M21)) + (vector.Z * transform.M31)) + transform.M41;
			result.Y = (((vector.X * transform.M12) + (vector.Y * transform.M22)) + (vector.Z * transform.M32)) + transform.M42;
			result.Z = (((vector.X * transform.M13) + (vector.Y * transform.M23)) + (vector.Z * transform.M33)) + transform.M43;
			//result.W = (((vector.X * transform.M14) + (vector.Y * transform.M24)) + (vector.Z * transform.M34)) + transform.M44;

			return result;
		}
		public static Vector3 Transform( Vector3 value, Quaternion rotation )
		{
			Vector3 vector = new Vector3();
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
		
			return vector;
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
		public override string ToString ()
		{
			return string.Format ("{0} {1} {2}", X, Y, Z);
		}
		public float[] ToArray()
		{
			return new float[]
				{
					X, Y, Z
				};
		}
	}
}


//
//  Quaternion.cs
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
	public class Quaternion : IEquatable<Quaternion>
	{
		internal Vector4f v4;

		public float X { get { return v4.X; } set { v4.X = value; } }
		public float Y { get { return v4.Y; } set { v4.Y = value; } }
		public float Z { get { return v4.Z; } set { v4.Z = value; } }
		public float W { get { return v4.W; } set { v4.W = value; } }

		public static Quaternion Identity 
		{
			get { return new Quaternion (0, 0, 0, 1); }
		}

		public Quaternion()
		{
		}
		public Quaternion (Vector3 vectorPart, float scalarPart)
		{
			v4 = new Vector4f (vectorPart.X, vectorPart.Y, vectorPart.Z, scalarPart);
		}
		public Quaternion (float x, float y, float z, float w)
		{
			v4 = new Vector4f (x, y, z, w);
		}
		public Quaternion (Vector4f v4) 
		{ 
			this.v4 = v4; 
		}

		public void Conjugate ()
		{
			var x = Conjugate (this);
			v4 = x.v4;
		}
		public float Length ()
		{
			return (float) System.Math.Sqrt (LengthSquared ());
		}

		public float LengthSquared ()
		{
			return (X * X) + (Y * Y) + (Z * Z) + (W * W);
		}
		public bool Equals (Quaternion other)
		{
			return other == this;
		}

		public override bool Equals (object obj)
		{
			return obj is Quaternion && ((Quaternion)obj) == this;
		}

		public override int GetHashCode ()
		{
			return X.GetHashCode () ^ Y.GetHashCode () ^ Z.GetHashCode () ^ W.GetHashCode ();
		}

		public static bool operator == (Quaternion a, Quaternion b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
		}

		public static bool operator != (Quaternion a, Quaternion b)
		{
			return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;
		}
		public static Quaternion Add (Quaternion quaternion1, Quaternion quaternion2)
		{
			return new Quaternion (quaternion1.v4 + quaternion2.v4);
		}
		public static void Add (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result = new Quaternion (quaternion1.v4 + quaternion2.v4);
		}
		public static Quaternion Subtract (Quaternion quaternion1, Quaternion quaternion2)
		{
			return new Quaternion (quaternion1.v4 - quaternion2.v4);
		}
		public static void Subtract (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result = new Quaternion (quaternion1.v4 - quaternion2.v4);
		}
		public static Quaternion Multiply (Quaternion quaternion1, Quaternion quaternion2)
		{
			return new Quaternion (
				quaternion1.W * quaternion2.X + quaternion1.X * quaternion2.W + quaternion1.Y * quaternion2.Z - quaternion1.Z * quaternion2.Y,
				quaternion1.W * quaternion2.Y - quaternion1.X * quaternion2.Z + quaternion1.Y * quaternion2.W + quaternion1.Z * quaternion2.X,
				quaternion1.W * quaternion2.Z + quaternion1.X * quaternion2.Y - quaternion1.Y * quaternion2.X + quaternion1.Z * quaternion2.W,
				quaternion1.W * quaternion2.W - quaternion1.X * quaternion2.X - quaternion1.Y * quaternion2.Y - quaternion1.Z * quaternion2.Z);
		}
		public static Quaternion Multiply (Quaternion quaternion1, float scaleFactor)
		{
			return new Quaternion (quaternion1.v4 * new Vector4f (scaleFactor));
		}
		public static Quaternion Divide (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion inv = Inverse (quaternion2);
			return Multiply (quaternion1, inv);
		}
		public static Quaternion Divide (Quaternion quaternion1, float scaleFactor)
		{
			return new Quaternion (quaternion1.v4 / new Vector4f (scaleFactor));
		}
		public static Quaternion Negate (Quaternion quaternion)
		{
			return new Quaternion (quaternion.v4 ^ new Vector4f (-0.0f));
		}

		public static Quaternion operator + (Quaternion quaternion1, Quaternion quaternion2)
		{
			return new Quaternion (quaternion1.v4 + quaternion2.v4);
		}

		public static Quaternion operator / (Quaternion quaternion1, Quaternion quaternion2)
		{
			return Divide (quaternion1, quaternion2);
		}

		public static Quaternion operator / (Quaternion quaternion, float scaleFactor)
		{
			return new Quaternion (quaternion.v4 / new Vector4f (scaleFactor));
		}

		public static Quaternion operator * (Quaternion quaternion1, Quaternion quaternion2)
		{
			return new Quaternion (
				quaternion1.W * quaternion2.X + quaternion1.X * quaternion2.W + quaternion1.Y * quaternion2.Z - quaternion1.Z * quaternion2.Y,
				quaternion1.W * quaternion2.Y - quaternion1.X * quaternion2.Z + quaternion1.Y * quaternion2.W + quaternion1.Z * quaternion2.X,
				quaternion1.W * quaternion2.Z + quaternion1.X * quaternion2.Y - quaternion1.Y * quaternion2.X + quaternion1.Z * quaternion2.W,
				quaternion1.W * quaternion2.W - quaternion1.X * quaternion2.X - quaternion1.Y * quaternion2.Y - quaternion1.Z * quaternion2.Z);
		}

		public static Quaternion operator * (Quaternion quaternion, float scaleFactor)
		{
			return new Quaternion (quaternion.v4 * scaleFactor);
		}

		public static Quaternion operator - (Quaternion quaternion1, Quaternion quaternion2)
		{
			return new Quaternion (quaternion1.v4 - quaternion2.v4);
		}

		public static Quaternion operator - (Quaternion quaternion)
		{
			return new Quaternion (quaternion.v4 ^ new Vector4f (-0.0f));
		}
		public static Quaternion Concatenate (Quaternion value1, Quaternion value2)
		{
			return Multiply (value1, value2);
		}
		public static Quaternion Conjugate (Quaternion value)
		{
			return new Quaternion( new Vector4f (- value.X, - value.Y, - value.Z, value.W));
		}
		public static float Dot (Quaternion quaternion1, Quaternion quaternion2)
		{
			//NOTE: shuffle->add->shuffle->add is faster than horizontal-add->horizontal-add
			Vector4f r0 = quaternion2.v4 * quaternion1.v4;
			Vector4f r1 = r0.Shuffle (ShuffleSel.Swap);
			r0 = r0 + r1;
			r1 = r0.Shuffle (ShuffleSel.RotateLeft);
			r0 = r0 + r1;
			return r0.Sqrt ().X;
		}
		public static Quaternion Inverse (Quaternion quaternion)
		{
			Quaternion conj = new Quaternion (quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
			conj.Conjugate ();

			return  conj * (1.0f / quaternion.LengthSquared ());
		}
		public static Quaternion Lerp (Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			Quaternion q1 = Multiply (quaternion1, 1.0f - amount);
			Quaternion q2 = Multiply (quaternion2, amount);
			Quaternion q1q2 = Add (q1, q2);

			return Normalize (q1q2);
		}
		public static Quaternion Normalize (Quaternion quaternion)
		{
			return Multiply (quaternion, 1.0f / quaternion.Length ());
		}
		public static Quaternion Slerp (Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			Quaternion q3 = new Quaternion();
			float dot = Dot (quaternion1, quaternion2);

			if (dot < 0.0f)
			{
				dot = -dot;
				q3 = Negate(quaternion2);
			}
			else
			{
				q3 = quaternion2;
			}

			if (dot < 0.999999f)
			{
				float angle = (float)System.Math.Acos(dot);
				float sin1 = (float)System.Math.Sin(angle * (1.0f - amount));
				float sin2 = (float)System.Math.Sin(angle * amount);
				float sin3 = (float)System.Math.Sin(angle);

				Quaternion q1 = Multiply(quaternion1, sin1);
				Quaternion q2 = Multiply(q3, sin2);
				Quaternion q4 = Add(q1, q2);

				return Divide(q4, sin3);
			}
			else
			{
				return Lerp(quaternion1, q3, amount);
			}
		}

		public static Quaternion CreateFromAxisAngle (Vector3 axis, float angle)
		{
			Vector3 vec = axis;
			vec.Normalize ();
			float ang = angle * 0.5f;
			Vector3.Multiply (ref vec, (float) System.Math.Sin (ang), out vec);

			return new Quaternion (vec, (float) System.Math.Cos (ang));
		}
		public static Quaternion CreateFromYawPitchRoll (float yaw, float pitch, float roll)
		{
			yaw *= 0.5f;
			pitch *= 0.5f;
			roll *= 0.5f;

			float cosYaw = (float) System.Math.Cos (yaw);
			float sinYaw = (float) System.Math.Sin (yaw);
			float cosPitch = (float) System.Math.Cos (pitch);
			float sinPitch = (float) System.Math.Sin (pitch);
			float cosRoll = (float) System.Math.Cos (roll);
			float sinRoll = (float) System.Math.Sin (roll);

			float cosPitchCosYaw = cosPitch * cosYaw;
			float sinPitchSinYaw = sinPitch * sinYaw;

			float x = sinRoll * cosPitchCosYaw - cosRoll * sinPitchSinYaw;
			float y = cosRoll * sinPitch * cosYaw + sinRoll * cosPitch * sinYaw;
			float z = cosRoll * cosPitch * sinYaw - sinRoll * sinPitch * cosYaw;
			float w = cosRoll * cosPitchCosYaw + sinRoll * sinPitchSinYaw;

			return new Quaternion (x, y, z, w);
		}
		public override string ToString ()
		{
			return string.Format ("{0} {1} {2} {3}", X, Y, Z, W);
		}
	}
}


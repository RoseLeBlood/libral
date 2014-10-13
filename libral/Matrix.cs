//
//  Matrix.cs
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
	public class Matrix : IEquatable<Matrix>
	{
		internal Vector4f r1, r2, r3, r4;

		public float M11 { get { return r1.X; } set { r1.X = value; } }
		public float M12 { get { return r1.Y; } set { r1.Y = value; } }
		public float M13 { get { return r1.Z; } set { r1.Z = value; } }
		public float M14 { get { return r1.W; } set { r1.W = value; } }
		public float M21 { get { return r2.X; } set { r2.X = value; } }
		public float M22 { get { return r2.Y; } set { r2.Y = value; } }
		public float M23 { get { return r2.Z; } set { r2.Z = value; } }
		public float M24 { get { return r2.W; } set { r2.W = value; } }
		public float M31 { get { return r3.X; } set { r3.X = value; } }
		public float M32 { get { return r3.Y; } set { r3.Y = value; } }
		public float M33 { get { return r3.Z; } set { r3.Z = value; } }
		public float M34 { get { return r3.W; } set { r3.W = value; } }
		public float M41 { get { return r4.X; } set { r4.X = value; } }
		public float M42 { get { return r4.Y; } set { r4.Y = value; } }
		public float M43 { get { return r4.Z; } set { r4.Z = value; } }
		public float M44 { get { return r4.W; } set { r4.W = value; } }


		public Vector3 Right
		{
			get
			{
				return new Vector3(M11, M12, M13);
			}
			set
			{
				M11 = value.X;
				M12 = value.Y;
				M13 = value.Z;
			}
		}

		public Vector3 Up
		{
			get
			{
				return new Vector3(M21, M22, M23);
			}
			set
			{
				M21 = value.X;
				M22 = value.Y;
				M23 = value.Z;
			}
		}

		public Vector3 Backward {
			get {
				return new Vector3 (M31, M32, M33);
			}
			set {
				M31 = value.X;
				M32 = value.Y;
				M33 = value.Z;
			}
		}

		public Vector3 Left
		{
			get
			{
				return new Vector3(r1 ^ new Vector4f(-0.0f));
			}
			set
			{
				var minus = (new Vector4f(value.X, value.Y, value.Z, 0.0f) ^ new Vector4f(-0.0f));
				minus.W = M14;
				r1 = minus;
			}
		}
		public Vector3 Down
		{
			get
			{
				return new Vector3(r2 ^ new Vector4f(-0.0f));
			}
			set
			{
				var minus = (new Vector4f(value.X, value.Y, value.Z, 0.0f) ^ new Vector4f(-0.0f));
				minus.W = M24;
				r2 = minus;
			}
		}
		public Vector3 Forward
		{
			get
			{
				return new Vector3(r3 ^ new Vector4f(-0.0f));
			}
			set
			{
				var minus = (new Vector4f(value.X, value.Y, value.Z, 0.0f) ^ new Vector4f(-0.0f));
				minus.W = M34;
				r3 = minus;
			}
		}
		public Vector3 Translation
		{
			get
			{
				return new Vector3(M41, M42, M43);
			}
			set
			{
				M41 = value.X;
				M42 = value.Y;
				M43 = value.Z;
			}
		}
		public static Matrix Identity
		{
			get
			{
				return new Matrix(
					1f, 0f, 0f, 0f,
					0f, 1f, 0f, 0f,
					0f, 0f, 1f, 0f,
					0f, 0f, 0f, 1f);
			}
		}

		public Matrix()
			: this( 0f)
		{
		}

		public Matrix (Vector4f r1, Vector4f r2, Vector4f r3, Vector4f r4)
		{
			this.r1 = r1;
			this.r2 = r2;
			this.r3 = r3;
			this.r4 = r4;
		}

		public Matrix (float v)
		{
			r1 = new Vector4f (v);
			r2 = new Vector4f (v);
			r3 = new Vector4f (v);
			r4 = new Vector4f (v);
		}

		public Matrix (
			float m11, float m12, float m13, float m14,
			float m21, float m22, float m23, float m24,
			float m31, float m32, float m33, float m34,
			float m41, float m42, float m43, float m44)
		{
			r1 = new Vector4f (m11, m12, m13, m14);
			r2 = new Vector4f (m21, m22, m23, m24);
			r3 = new Vector4f (m31, m32, m33, m34);
			r4 = new Vector4f (m41, m42, m43, m44);
		}
		public float Determinant ()
		{
			return
				M11 * M22 * M33 * M44 - M11 * M22 * M34 * M43 + M11 * M23 * M34 * M42 - M11 * M23 * M32 * M44 +
				M11 * M24 * M32 * M43 - M11 * M24 * M33 * M42 - M12 * M23 * M34 * M41 + M12 * M23 * M31 * M44 -
				M12 * M24 * M31 * M43 + M12 * M24 * M33 * M41 - M12 * M21 * M33 * M44 + M12 * M21 * M34 * M43 +
				M13 * M24 * M31 * M42 - M13 * M24 * M32 * M41 + M13 * M21 * M32 * M44 - M13 * M21 * M34 * M42 +
				M13 * M22 * M34 * M41 - M13 * M22 * M31 * M44 - M14 * M21 * M32 * M43 + M14 * M21 * M33 * M42 -
				M14 * M22 * M33 * M41 + M14 * M22 * M31 * M43 - M14 * M23 * M31 * M42 + M14 * M23 * M32 * M41;
		}

		public static float Det (Matrix m)
		{
			return m.M11 * (m.M22 * m.M33 - m.M23 * m.M32) -
			m.M12 * (m.M21 * m.M33 - m.M23 * m.M31) +
			m.M13 * (m.M21 * m.M32 - m.M22 * m.M31);
		}

		public static Matrix Invert (Matrix m)
		{
			float fInvDet = Matrix.Det(m);
			if(fInvDet == 0.0f) return Identity;
			fInvDet = 1.0f / fInvDet;

			Matrix mResult = new Matrix();
			mResult.M11 =  fInvDet * (m.M22 * m.M33 - m.M23 * m.M32);
			mResult.M12 = -fInvDet * (m.M12 * m.M33 - m.M13 * m.M32);
			mResult.M13 =  fInvDet * (m.M12 * m.M23 - m.M13 * m.M22);
			mResult.M14 =  0.0f;
			mResult.M21 = -fInvDet * (m.M21 * m.M33 - m.M23 * m.M31);
			mResult.M22 =  fInvDet * (m.M11 * m.M33 - m.M13 * m.M31);
			mResult.M23 = -fInvDet * (m.M11 * m.M23 - m.M13 * m.M21);
			mResult.M24 =  0.0f;
			mResult.M31 =  fInvDet * (m.M21 * m.M32 - m.M22 * m.M31);
			mResult.M32 = -fInvDet * (m.M11 * m.M32 - m.M12 * m.M31);
			mResult.M33 =  fInvDet * (m.M11 * m.M22 - m.M12 * m.M21);
			mResult.M34 =  0.0f;
			mResult.M41 = -(m.M41 * mResult.M11 + m.M42 * mResult.M21 + m.M43 * mResult.M31);
			mResult.M42 = -(m.M41 * mResult.M12 + m.M42 * mResult.M22 + m.M43 * mResult.M32);
			mResult.M43 = -(m.M41 * mResult.M13 + m.M42 * mResult.M23 + m.M43 * mResult.M33);
			mResult.M44 =  1.0f;

			return mResult;
		}
		public bool Equals (Matrix other)
		{
			return r1 == other.r1 && r2 == other.r2 && r3 == other.r3 && r4 == other.r4; 
		}
		public override bool Equals (object obj)
		{
			return obj is Matrix && ((Matrix)obj) == this;
		}
		public override int GetHashCode ()
		{
			unsafe {
				Vector4f f = r1;
				Vector4i i = *((Vector4i*)&f);
				i = i ^ i.Shuffle(ShuffleSel.Swap);
				i = i ^ i.Shuffle(ShuffleSel.RotateLeft);
				f = r2;
				Vector4i j = *((Vector4i*)&f);
				j = j ^ j.Shuffle(ShuffleSel.Swap);
				j = j ^ j.Shuffle(ShuffleSel.RotateLeft);
				f = r3;
				Vector4i k = *((Vector4i*)&f);
				k = k ^ k.Shuffle(ShuffleSel.Swap);
				k = k ^ k.Shuffle(ShuffleSel.RotateLeft);
				f = r4;
				Vector4i l = *((Vector4i*)&f);
				l = l ^ l.Shuffle(ShuffleSel.Swap);
				l = l ^ l.Shuffle(ShuffleSel.RotateLeft);
				return (i ^ j ^ k ^ l).X;
			}
		}
		public static bool operator == (Matrix a, Matrix b)
		{
			return a.r1 == b.r1 && a.r2 == b.r2 && a.r3 == b.r3 && a.r4 == b.r4;
		}
		public static bool operator != (Matrix a, Matrix b)
		{
			return a.r1 != b.r1 || a.r2 != b.r2 || a.r3 != b.r3 || a.r4 != b.r4; 
		}

		public static Matrix CreateFromAxisAngle (Vector3 axis, float angle)
		{
			float fSin = (float)Math.Sin(-angle);
			float fCos = (float)Math.Cos(-angle);
			float fOneMinusCos = 1.0f - fCos;

			Vector3 vAxis = Vector3.Normalize(axis);

			return new Matrix((float)(vAxis.X * vAxis.X) * fOneMinusCos + fCos,
				(float)(vAxis.X * vAxis.Y) * fOneMinusCos - ((float)vAxis.Z * fSin),
				(float)(vAxis.X * vAxis.Z) * fOneMinusCos + ((float)vAxis.Y * fSin),
				0.0f,
				(float)(vAxis.Y * vAxis.X) * fOneMinusCos + ((float)vAxis.Z * fSin),
				(float)(vAxis.Y * vAxis.Y) * fOneMinusCos + fCos,
				(float)(vAxis.Y * vAxis.Z) * fOneMinusCos - ((float)vAxis.X * fSin),
				0.0f,
				(float)(vAxis.Z * vAxis.X) * fOneMinusCos - ((float)vAxis.Y * fSin),
				(float)(vAxis.Z * vAxis.Y) * fOneMinusCos + ((float)vAxis.X * fSin),
				(float)(vAxis.Z * vAxis.Z) * fOneMinusCos + fCos,
				0.0f,
				0.0f,
				0.0f,
				0.0f,
				1.0f);
		}
		public static Matrix CreateLookAt (Vector3 vPos, Vector3 vLookAt, Vector3 vUp)
		{
			Vector3 zaxis = Vector3.Normalize(vPos - vLookAt);
			Vector3 xaxis = Vector3.Normalize(Vector3.Cross(vUp, zaxis));
			Vector3 yaxis = Vector3.Cross(zaxis, xaxis);


			return new Matrix(
				xaxis.X, 					yaxis.X, 					zaxis.X, 					0,
				xaxis.Y, 					yaxis.Y, 					zaxis.Y, 					0,
				xaxis.Z, 				    yaxis.Z, 					zaxis.Z, 					0,
				-Vector3.Dot(xaxis, vPos), 	-Vector3.Dot(yaxis, vPos),  -Vector3.Dot(zaxis, vPos), 	1.0f);
		}
		public static Matrix CreateLookAtLH (Vector3 vPos, Vector3 vLookAt, Vector3 vUp)
		{
			Vector3 zaxis = Vector3.Normalize(vLookAt - vPos);
			Vector3 xaxis = Vector3.Normalize(Vector3.Cross(vUp, zaxis));
			Vector3 yaxis = Vector3.Cross(zaxis, xaxis);


			return new Matrix(
				xaxis.X, 					yaxis.X, 					zaxis.X, 					0,
				xaxis.Y, 					yaxis.Y, 					zaxis.Y, 					0,
				xaxis.Z, 				    yaxis.Z, 					zaxis.Z, 					0,
				-Vector3.Dot(xaxis, vPos), 	-Vector3.Dot(yaxis, vPos),  -Vector3.Dot(zaxis, vPos), 	1.0f);
		}
		public static Matrix CreateOrtho(float w, float h, float zn, float zf)
		{
			Matrix pout = Matrix.Identity;

			pout.M11 = 2.0f / w;
			pout.M22 = 2.0f / h;
			pout.M33 = 1.0f / (zn - zf);
			pout.M43= zn / (zn - zf);
			return pout;
		}
		public static Matrix CreateOrthoOffCenter(float l, float r, float b, float t, float zn, float zf)
		{
			Matrix pout = Matrix.Identity;
			pout.M11 = 2.0f / (r - l);
			pout.M22 = 2.0f / (t - b);
			pout.M33 = 1.0f / (zn -zf);
			pout.M41 = -1.0f -2.0f *l / (r - l);
			pout.M42 = 1.0f + 2.0f * t / (b - t);
			pout.M43 = zn / (zn -zf);
			return pout;
		}
		public static Matrix CreateOrthoLH(float w, float h, float zn, float zf)
		{
			Matrix pout = Matrix.Identity;
			pout.M11 = 2.0f / w;
			pout.M22 = 2.0f / h;
			pout.M33 = 1.0f / (zf - zn);
			pout.M43 = zn / (zn - zf);
			return pout;
		}
		public static Matrix CreateOrthoOffCenterLH(float l, float r, float b, float t, float zn, float zf)
		{
			Matrix pout = Matrix.Identity;
			pout.M11 = 2.0f / (r - l);
			pout.M22 = 2.0f / (t - b);
			pout.M33 = 1.0f / (zf -zn);
			pout.M41 = -1.0f -2.0f *l / (r - l);
			pout.M42 = 1.0f + 2.0f * t / (b - t);
			pout.M43 = zn / (zn -zf);
			return pout;
		}
		public static Matrix CreateProjection (float fFOV, float fAspect, float fNearPlane, 
			float fFarPlane)
		{
			Matrix pout = Matrix.Identity;
			pout.M11 = 1.0f / (fAspect * (float)(Math.Tan(fFOV/2.0f)) );
			pout.M22 = 1.0f / (float)(Math.Tan(fFOV/2.0f));
			pout.M33 = fFarPlane / (fNearPlane - fFarPlane);
			pout.M34 = -1.0f;
			pout.M43 = (fFarPlane * fNearPlane) / (fNearPlane - fFarPlane);
			pout.M44 = 0.0f;
			return pout;
		}
		public static Matrix CreateProjectionLH (float fFOV, float fAspect, float fNearPlane, 
			float fFarPlane)
		{
			Matrix pout = Matrix.Identity;
			pout.M11 = 1.0f / (fAspect * (float)(Math.Tan(fFOV/2.0f)) );
			pout.M22 = 1.0f / (float)(Math.Tan(fFOV/2.0f));
			pout.M33 = fFarPlane / (fFarPlane - fNearPlane);
			pout.M34 = 1.0f;
			pout.M43 = (fFarPlane * fNearPlane) / (fNearPlane - fFarPlane);
			pout.M44 = 0.0f;
			return pout;
		}
		public static Matrix CreateProjection(Size feld, float fNearPlane, float fFarPlane)
		{
			Matrix pout = Matrix.Identity;
			pout.M11 = 2.0f * fNearPlane / feld.Width;
			pout.M22 = 2.0f * fNearPlane / feld.Height;
			pout.M33 = fFarPlane / (fNearPlane - fFarPlane);
			pout.M43 = (fNearPlane * fFarPlane) / (fNearPlane - fFarPlane);
			pout.M34 = -1.0f;
			pout.M44 = 0.0f;
			return pout;
		}
		public static Matrix CreateProjectionLH(Size feld, float fNearPlane, float fFarPlane)
		{
			Matrix pout = Matrix.Identity;
			pout.M11 = 2.0f * fNearPlane / feld.Width;
			pout.M22 = 2.0f * fNearPlane / feld.Height;
			pout.M33 = fFarPlane / (fFarPlane - fNearPlane);
			pout.M43 = (fNearPlane * fFarPlane) / (fNearPlane - fFarPlane);
			pout.M34 = 1.0f;
			pout.M44 = 0.0f;
			return pout;
		}
		public static Matrix CreatePerspectiveOffCenterLH(float l, float r, float b, float t, float fNearPlane, float fFarPlane)
		{
			Matrix pout = Matrix.Identity;
			pout.M11 = 2.0f * fNearPlane / (r - l);
			pout.M22 = -2.0f * fNearPlane / (b - t);
			pout.M31 = -1.0f - 2.0f * l / (r - l);
			pout.M32 = 1.0f + 2.0f * t / (b - t);
			pout.M33 = - fFarPlane / (fNearPlane - fFarPlane);
			pout.M43 = (fNearPlane * fFarPlane) / (fNearPlane -fFarPlane);
			pout.M34 = 1.0f;
			pout.M44 = 0.0f;
			return pout;
		}

		public static Matrix CreatePerspectiveOffCenter(float l, float r, float b, float t, float fNearPlane, float fFarPlane)
		{
			Matrix pout = Matrix.Identity;
			pout.M11 = 2.0f * fNearPlane / (r - l);
			pout.M22 = -2.0f * fNearPlane / (b - t);
			pout.M31 = 1.0f + 2.0f * l / (r - l);
			pout.M32 = -1.0f -2.0f * t / (b - t);
			pout.M33 = fFarPlane / (fNearPlane - fFarPlane);
			pout.M43 = (fNearPlane * fFarPlane) / (fNearPlane -fFarPlane);
			pout.M34 = -1.0f;
			pout.M44 = 0.0f;
			return pout;
		}
		public static Matrix CreateBillboard( Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3 cameraForwardVector )
		{
			Matrix result = new Matrix();
			Vector3 difference = objectPosition - cameraPosition;
			Vector3 crossed;
			Vector3 final;

			float lengthSq = difference.LengthSquared();
			if (lengthSq < 0.0001f)
				difference = -cameraForwardVector;
			else
				difference = new Vector3((float)( 1.0f / Math.Sqrt( lengthSq ) ));

			crossed = Vector3.Cross( cameraUpVector, difference );
			crossed.Normalize();
			final = Vector3.Cross( difference, crossed );

			result.M11 = crossed.X;
			result.M12 = crossed.Y;
			result.M13 = crossed.Z;
			result.M14 = 0.0f;
			result.M21 = final.X;
			result.M22 = final.Y;
			result.M23 = final.Z;
			result.M24 = 0.0f;
			result.M31 = difference.X;
			result.M32 = difference.Y;
			result.M33 = difference.Z;
			result.M34 = 0.0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1.0f;

			return result;
		}
		public static Matrix CreateRotationX (float radians)
		{
			float cos = (float) System.Math.Cos (radians);
			float sin = (float) System.Math.Sin (radians);

			return new Matrix()
			{
				M11 = 1.0f,
				M22 = cos,
				M23 = sin,
				M32 = -sin,
				M33 = cos,
				M44 = 1.0f,
			};
		}
		public static Matrix CreateRotationY (float radians)
		{
			float cos = (float) System.Math.Cos (radians);
			float sin = (float) System.Math.Sin (radians);

			return new Matrix()
			{
				M11 = cos,
				M13 = -sin,
				M22 = 1.0f,
				M31 = sin,
				M33 = cos,
				M44 = 1.0f,
			};
		}
		public static Matrix CreateRotationZ (float radians)
		{
			float cos = (float)System.Math.Cos(radians);
			float sin = (float)System.Math.Sin(radians);

			return new Matrix()
			{
				M11 = cos,
				M12 = sin,
				M21 = -sin,
				M22 = cos,
				M33 = 1.0f,
				M44 = 1.0f,
			};
		}
		public static Matrix CreateRotation (Quaternion quaternion)
		{
			float xx = quaternion.X * quaternion.X;
			float yy = quaternion.Y * quaternion.Y;
			float zz = quaternion.Z * quaternion.Z;
			float xy = quaternion.X * quaternion.Y;
			float zw = quaternion.Z * quaternion.W;
			float zx = quaternion.Z * quaternion.X;
			float yw = quaternion.Y * quaternion.W;
			float yz = quaternion.Y * quaternion.Z;
			float xw = quaternion.X * quaternion.W;

			return new Matrix()
			{
				r1 = new Vector4f(
					1.0f - (2.0f * (yy + zz)),
					2.0f * (xy + zw),
					2.0f * (zx - yw),
					0.0f),
				r2 = new Vector4f(
					2.0f * (xy - zw),
					1.0f - (2.0f * (zz + xx)),
					2.0f * (yz + xw),
					0.0f),
				r3 = new Vector4f(
					2.0f * (zx + yw),
					2.0f * (yz - xw),
					1.0f - (2.0f * (yy + xx)),
					0.0f),	
				r4 = new Vector4f(
					0.0f,
					0.0f,
					0.0f,
					1.0f),
			};
		}
		public static Matrix CreateFromYawPitchRoll( float yaw, float pitch, float roll )
		{
			Quaternion quaternion = Quaternion.CreateFromYawPitchRoll( yaw, pitch, roll );
			return Matrix.CreateRotation(quaternion);
		}
		public static Matrix CreateRotation(float x, float y, float z)
		{
			return CreateRotationZ(z) * CreateRotationX(x) * CreateRotationY(y);
		}
		public static Matrix CreateScale (Vector3 scales)
		{
			return new Matrix()
			{
				M11 = scales.X,
				M22 = scales.Y,
				M33 = scales.Z,
				M44 = 1.0f,
			};
		}
		public static Matrix CreateTranslation (Vector3 position)
		{
			return new Matrix()
			{
				M11 = 1.0f,
				M22 = 1.0f,
				M33 = 1.0f,
				M41 = position.X,
				M42 = position.Y,
				M43 = position.Z,
				M44 = 1.0f,
			};
		}
		public static Matrix CreateWorld (ref Vector3 position, ref Vector3 forward, ref Vector3 up)
		{
			Vector3 x = Vector3.Normalize( Vector3.Cross (forward, up));
			Vector3 y = Vector3.Normalize( Vector3.Cross (x, forward));
			Vector3 z = Vector3.Normalize( Vector3.Normalize (forward));

			return new Matrix()
			{
				Right = x,
				Up = y,
				Forward = z,
				Translation = position,
				M44 = 1.0f,
			};
		}
		public static Matrix Transpose (Matrix matrix)
		{
			Vector4f xmm0 = matrix.r1, xmm1 = matrix.r2, xmm2 = matrix.r3, xmm3 = matrix.r4;
			Vector4f xmm4 = xmm0;
			xmm0 = VectorOperations.InterleaveLow  (xmm0, xmm2);
			xmm4 = VectorOperations.InterleaveHigh (xmm4, xmm2);
			xmm2 = xmm1;
			xmm1 = VectorOperations.InterleaveLow  (xmm1, xmm3);
			xmm2 = VectorOperations.InterleaveHigh (xmm2, xmm3);
			xmm3 = xmm0;
			xmm0 = VectorOperations.InterleaveLow  (xmm0, xmm1);
			xmm3 = VectorOperations.InterleaveHigh (xmm3, xmm1);
			xmm1 = xmm4;
			xmm1 = VectorOperations.InterleaveLow  (xmm1, xmm2);
			xmm4 = VectorOperations.InterleaveHigh (xmm4, xmm2);

			return new Matrix (xmm0, xmm3, xmm1, xmm4);
		}



		public static Matrix Transformation(Vector3 pscalingcenter, Quaternion pscalingrotation, 
			Vector3 pscaling, Vector3 protationcenter, Quaternion protation, Vector3 ptranslation)
		{
			Matrix m1, m2, m3, m4, m5, m6, m7, p1, p2, p3, p4, p5;
			Quaternion prc = ( protationcenter == null ? new Quaternion(Vector3.Zero, 1.0f) : 
				new Quaternion(protationcenter, 1.0f));

			Vector3 psc = ( pscalingcenter == null ? Vector3.Zero : pscalingcenter);
			Vector3 pt = ( ptranslation == null ? Vector3.Zero : ptranslation);

			m1 = Matrix.CreateTranslation(psc);


			if (pscalingrotation == null)
			{
				m2 = m4 = Matrix.Identity;
			}
			else
			{
				m4 = Matrix.CreateRotation(pscalingrotation);
				m2 = Matrix.Invert(m4);
			}

			m3 =  (pscaling == null ? Matrix.Identity : Matrix.CreateScale(pscaling));
			m6 =  (protation == null ? Matrix.Identity : Matrix.CreateRotation(protation));

			m5 = Matrix.CreateTranslation(new Vector3(psc.X - prc.X, psc.Y - prc.Y, psc.Z - prc.Z));
			m7 = Matrix.CreateTranslation(new Vector3(prc.X + pt.X, prc.Y + pt.Y, prc.Z + pt.Z));
			p1 = Matrix.Multiply(m1, m2);
			p2 = Matrix.Multiply(p1, m3);
			p3 = Matrix.Multiply(p2, m4);
			p4 = Matrix.Multiply(p3, m5);
			p5 = Matrix.Multiply(p4, m6);
			return Matrix.Multiply(p5, m7);
		}
		public static Matrix Add (Matrix matrix1, Matrix matrix2)
		{
			return new Matrix()
			{
				r1 = matrix1.r1 + matrix2.r1,
				r2 = matrix1.r2 + matrix2.r2,
				r3 = matrix1.r3 + matrix2.r3,
				r4 = matrix1.r4 + matrix2.r4,
			};
		}
		public static Matrix Multiply (Matrix matrix1, Matrix matrix2)
		{
			Matrix result = new Matrix();
			Vector4f a1 = matrix1.r1, a2 = matrix1.r2, a3 = matrix1.r3, a4 = matrix1.r4;
			Vector4f b1 = matrix2.r1, b2 = matrix2.r2, b3 = matrix2.r3, b4 = matrix2.r4;

			Vector4f c1 = a1.Shuffle (ShuffleSel.ExpandX) * b1;
			Vector4f c2 = a2.Shuffle (ShuffleSel.ExpandX) * b2;
			Vector4f c3 = a3.Shuffle (ShuffleSel.ExpandX) * b3;
			Vector4f c4 = a4.Shuffle (ShuffleSel.ExpandX) * b4;

			c1 += a1.Shuffle (ShuffleSel.ExpandY) * b1;
			c2 += a2.Shuffle (ShuffleSel.ExpandY) * b2;
			c3 += a3.Shuffle (ShuffleSel.ExpandY) * b3;
			c4 += a4.Shuffle (ShuffleSel.ExpandY) * b4;

			c1 += a1.Shuffle (ShuffleSel.ExpandZ) * b1;
			c2 += a2.Shuffle (ShuffleSel.ExpandZ) * b2;
			c3 += a3.Shuffle (ShuffleSel.ExpandZ) * b3;
			c4 += a4.Shuffle (ShuffleSel.ExpandZ) * b4;

			c1 += a1.Shuffle (ShuffleSel.ExpandW) * b1;
			c2 += a2.Shuffle (ShuffleSel.ExpandW) * b2;
			c3 += a3.Shuffle (ShuffleSel.ExpandW) * b3;
			c4 += a4.Shuffle (ShuffleSel.ExpandW) * b4;

			result.r1 = c1;
			result.r2 = c2;
			result.r3 = c3;
			result.r4 = c4;

			return result;
		}

		public static Matrix Multiply (Matrix matrix1, float scaleFactor)
		{
			Matrix result = new Matrix();
			Vector4f scale = new Vector4f (scaleFactor);
			result.r1 = matrix1.r1 * scale;
			result.r2 = matrix1.r2 * scale;
			result.r3 = matrix1.r3 * scale;
			result.r4 = matrix1.r4 * scale;
			return result;
		}
		public static Matrix Negate (Matrix matrix)
		{
			Matrix result = new Matrix();
			Vector4f sign = new Vector4f (-0.0f);
			result.r1 = matrix.r1 ^ sign;
			result.r2 = matrix.r2 ^ sign;
			result.r3 = matrix.r3 ^ sign;
			result.r4 = matrix.r4 ^ sign;
			return result;
		}
		public static Matrix Subtract (Matrix matrix1, Matrix matrix2)
		{
			Matrix result = new Matrix();
			result.r1 = matrix1.r1 - matrix2.r1;
			result.r2 = matrix1.r2 - matrix2.r2;
			result.r3 = matrix1.r3 - matrix2.r3;
			result.r4 = matrix1.r4 - matrix2.r4;
			return result;
		}
		public static Matrix Divide (Matrix matrix1, Matrix matrix2)
		{
			Matrix result = new Matrix();

			result.r1 = matrix1.r1 * matrix2.r1;
			result.r2 = matrix1.r2 * matrix2.r2;
			result.r3 = matrix1.r3 * matrix2.r3;
			result.r4 = matrix1.r4 * matrix2.r4;

			return result;
		}
		public static Matrix Divide (Matrix matrix1, float divider)
		{
			Matrix result = new Matrix();

			Vector4f divisor = new Vector4f (divider);
			result.r1 = matrix1.r1 / divisor;
			result.r2 = matrix1.r2 / divisor;
			result.r3 = matrix1.r3 / divisor;
			result.r4 = matrix1.r4 / divisor;

			return result;
		}
		public static Matrix operator + (Matrix matrix1, Matrix matrix2)
		{
			return Add (matrix1, matrix2);
		}

		public static Matrix operator / (Matrix matrix, float divider)
		{
			return Divide (matrix, divider);
		}

		public static Matrix operator / (Matrix matrix1, Matrix matrix2)
		{
			return Divide(matrix1, matrix2);
		}
		public static Matrix operator * (Matrix matrix1, Matrix matrix2)
		{
			return Multiply (matrix1, matrix2);
		}
		public static Matrix operator * (Matrix matrix, float scaleFactor)
		{
			return Multiply (matrix, scaleFactor);
		}

		public static Matrix operator * (float scaleFactor, Matrix matrix)
		{
			return Multiply (matrix, scaleFactor);
		}

		public static Matrix operator - (Matrix matrix1, Matrix matrix2)
		{
			return Subtract (matrix1, matrix2);
		}

		public static Matrix operator - (Matrix matrix)
		{
			return Negate (matrix);
		}
		public override string ToString ()
		{
			return string.Format (
				"{0} {1} {2} {3}}} " +
				"{4} {5} {6} {7}}} " +
				"{8} {9} {10} {11}}} " +
				"{12} {13} {14} {15}}} ",
				M11, M12, M13, M14,
				M21, M22, M23, M24,
				M31, M32, M33, M34,
				M41, M42, M43, M44);
		}

		public float[] ToArray()
		{
			return new float[16]
			{
				M11, M12, M13, M14,
				M21, M22, M23, M24,
				M31, M32, M33, M34,
				M41, M42, M43, M44
			};
		}
	}
}


//
//  MathUtil.cs
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
	internal class MathUtil
	{
		public static float E       { get { return (float) System.Math.E; } }
		public static float Log10E  { get { return (float) 0.4342944819032f; } }
		public static float Log2E   { get { return (float) 1.442695040888f; } }
		public static float Pi      { get { return (float) System.Math.PI; } }
		public static float PiOver2 { get { return (float) (System.Math.PI / 2.0); } }
		public static float PiOver4 { get { return (float) (System.Math.PI / 4.0); } }
		public static float TwoPi   { get { return (float) (System.Math.PI * 2.0); } }

		public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
		{
			return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
		}

		public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
		{
			float amountSq = amount * amount;
			float amountCube = amountSq * amount;
			return ((2.0f * value2 +
				(-value1 + value3) * amount +
				(2.0f * value1 - 5.0f * value2 + 4.0f * value3 - value4) * amountSq +
				(3.0f * value2 - 3.0f * value3 - value1 + value4) * amountCube) * 0.5f);
		}
		public static float Clamp (float value, float min, float max)
		{
			return System.Math.Min (System.Math.Max (min, value), max);
		}

		public static float Distance (float value1, float value2)
		{
			return System.Math.Abs (value1 - value2);
		}

		public static float Hermite (float value1, float tangent1, float value2, float tangent2, float amount)
		{
			float s = amount;
			float s2 = s * s;
			float s3 = s2 * s;
			float h1 =  2 * s3 - 3 * s2 + 1;
			float h2 = -2 * s3 + 3 * s2;
			float h3 =      s3 - 2 * s2 + s;
			float h4 =      s3 -     s2;
			return value1 * h1 + value2 * h2 + tangent1 * h3 + tangent2 * h4;
		}

		public static float Lerp (float value1, float value2, float amount)
		{
			return value1 + (value2 - value1) * amount;
		}

		public static float Max (float value1, float value2)
		{
			return System.Math.Max (value1, value2);
		}

		public static float Min (float value1, float value2)
		{
			return System.Math.Min (value1, value2);
		}

		public static float SmoothStep (float value1, float value2, float amount)
		{
			//FIXME: check this
			//the function is Smoothstep (http://en.wikipedia.org/wiki/Smoothstep) but the usage has been altered
			// to be similar to Lerp
			amount = amount * amount * (3f - 2f * amount);
			return value1 + (value2 - value1) * amount;
		}

		public static float ToDegrees (float radians)
		{
			return radians * (180f / Pi);
		}

		public static float ToRadians (float degrees)
		{
			return degrees * (Pi / 180f);
		}

		public static float WrapAngle (float angle)
		{
			angle = angle % TwoPi;
			if (angle > Pi)
				return angle - TwoPi;
			if (angle < -Pi)
				return angle + TwoPi;
			return angle;
		}
	}
}


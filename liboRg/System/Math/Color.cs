//
//  MyClass.cs
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
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Mono.Simd;

namespace System.Common
{
	[StructLayout(LayoutKind.Explicit, Pack = 0, Size = 16)]
	public struct Color : IFormattable, IEquatable<Color>
	{
		[ FieldOffset(0) ]
		internal float m_fRed;
		[ FieldOffset(4) ]
		internal float m_fGreen;
		[ FieldOffset(8) ]
		internal float m_fBlue;
		[ FieldOffset(12) ]
		internal float m_fAlpha;


		public float Red { get { return m_fRed; } set { m_fRed = value; } }
		public float Green { get { return m_fGreen; } set { m_fGreen = value; } }
		public float Blue { get { return m_fBlue; } set { m_fBlue = value; } }
		public float Alpha { get { return m_fAlpha; } set { m_fAlpha = value; } }

		public byte R { get { return FloatColorToByte(Red ); } set { m_fRed = ByteColorToFloat(value); } }
		public byte G { get { return FloatColorToByte(Green ); } set { m_fGreen = ByteColorToFloat(value); } }
		public byte B { get { return FloatColorToByte(Blue); } set { m_fBlue = ByteColorToFloat(value); } }
		public byte A { get { return FloatColorToByte(Alpha ); } set { m_fAlpha = ByteColorToFloat(value); } }

		[System.Runtime.CompilerServices.IndexerName ("Component")]
		public unsafe float this [int index]
		{
			get
			{
				if ((index | 0x3) != 0x3) //index < 0 || index > 3
					throw new ArgumentOutOfRangeException("index");
				fixed (float *v = &m_fRed)
				{
					return *(v + index);
				}
			}
			set
			{
				if ((index | 0x3) != 0x3) //index < 0 || index > 3
					throw new ArgumentOutOfRangeException("index");
				fixed (float *v = &m_fRed)
				{
					*(v + index) = value;
				}
			}
		}

		public Color(float red, float green, float blue) 
			: this(red, green, blue, 1.0f)
		{

		}
		public Color(float f) 
			: this(f, f, f, f)
		{

		}
		public Color(float[] com) 
			: this(com[0], com[1], com[2], com[3])	
		{

		}
		public Color(byte red, byte green, byte blue) 
			: this(red, green, blue, 255)
		{
		}
		public Color(byte[] com)
			: this(com[0], com[1], com[2], com[3])	
		{
		}
		public Color(byte i) 
			: this(i,i,i,i)
		{
		}
		public Color(float red, float green, float blue, float alpha)		
		{
			m_fRed = red;
			m_fGreen = green;
			m_fBlue = blue;
			m_fAlpha = alpha;
		}
		public Color(byte red, byte green, byte blue, byte alpha) 				
		{
			m_fRed = m_fGreen = m_fBlue = m_fAlpha = 0.0f;
			m_fRed = ByteColorToFloat(red);
			m_fGreen = ByteColorToFloat(green);
			m_fBlue = ByteColorToFloat(blue);
			m_fAlpha = ByteColorToFloat(alpha);

		}	
		public Color(uint rgba)
		{
			byte[] values = BitConverter.GetBytes(rgba);
			if (!BitConverter.IsLittleEndian) Array.Reverse(values);
			m_fRed = m_fGreen = m_fBlue = m_fAlpha = 0.0f;
			m_fRed = ByteColorToFloat(values[3]);
			m_fGreen = ByteColorToFloat(values[2]);
			m_fBlue = ByteColorToFloat(values[1]);
			m_fAlpha = ByteColorToFloat(values[0]);
		}
		public uint ToRgba()
		{
			return (uint)((R << 24) | (G << 16) | (B << 8)  | (A << 0));
		}
		public  Color(string hexstring)
		{
			var cal = Colors.FromString(hexstring);
			m_fRed = cal.R;
			m_fGreen = cal.G;
			m_fBlue = cal.B;
			m_fAlpha = cal.A;
		}
		public override bool Equals (object obj)
		{
			if (!(obj is Color))
				{
					return false;
				}
			Color right = (Color)obj;
			return this == right;
		}
		[Acceleration (AccelMode.SSE2)]
		public override int GetHashCode()
		{
			unsafe 
			{
				Vector4f f = new Vector4f(R,G,B,A);
				Vector4i i = *((Vector4i*)&f);
				i = i ^ i.Shuffle (ShuffleSel.Swap);
				i = i ^ i.Shuffle (ShuffleSel.RotateLeft);
				return i.X;
			}
		}
		public override string ToString()
		{
			return ToString(null, null);
		}
		public string ToString(string format, IFormatProvider provider)
		{
			StringBuilder sb = new StringBuilder();

			if (format == null)
			{
				sb.AppendFormat(provider, "#{0:X2}", this.R);
				sb.AppendFormat(provider, "{0:X2}", this.G);
				sb.AppendFormat(provider, "{0:X2}", this.B);
				sb.AppendFormat(provider, "{0:X2}", this.A);
			}
			else
			{
				sb.AppendFormat(provider,
					"sc#{1:" + format + "}{0} {2:" + format + "}{0} {3:" + format + "}{0} {4:" + format + "}",
					".", Red, Green,
					Blue, Alpha);
			}

			return sb.ToString();
		}

		#region IEquatable implementation


		public bool Equals(Color color)
		{
			return (this == color);
		}


		#endregion

		public void Interpolate(Color c2, float p)
		{
			Color n = Interpolate(this, c2, p);
			this.Red = n.Red;
			this.Blue = n.Blue;
			this.Green = n.Green;
			this.Alpha = n.Alpha;
		}
		public void Negate()											
		{
			Color n = Negate(this);
			this.Red = n.Red;
			this.Blue = n.Blue;
			this.Green = n.Green;
			this.Alpha = n.Alpha;
		}
		public float Brightness()										
		{
			return Brightness(this);
		}
		public void	Min(Color c1)						
		{
			Color n = Min(this, c1);
			this.Red = n.Red;
			this.Blue = n.Blue;
			this.Green = n.Green;
			this.Alpha = n.Alpha;
		}
		public void	Max(Color c1)						
		{
			Color n = Max(this, c1);
			this.Red = n.Red;
			this.Blue = n.Blue;
			this.Green = n.Green;
			this.Alpha = n.Alpha;
		}
			
		public static implicit operator Color(string hex) 
		{
			return new Color(hex);  
		}
		[Acceleration (AccelMode.SSE3)]
		public static Color Contrast(Color c, float s)
		{
			return new Color()
			{
				Red = 0.5f + s * (c.Red - 0.5f),
				Green = 0.5f + s * (c.Green - 0.5f),
				Blue = 0.5f + s * (c.Blue - 0.5f),
				Alpha = c.Alpha,
			};
		}
		[Acceleration (AccelMode.SSE3)]
		public static Color	Negate(Color c)											
		{
			return new Color(1.0f - c.Red, 1.0f - c.Green, 1.0f - c.Blue, 1.0f - c.Alpha);
		}
		[Acceleration (AccelMode.SSE3)]
		public static float	Brightness(Color c)										
		{
			return c.Red * 0.299f + c.Green * 0.587f + c.Blue * 0.114f;
		}
		[Acceleration (AccelMode.SSE3)]
		public static Color	Interpolate(Color c1, Color c2, float p)	
		{
			return c1 + p * (c2 - c1);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color	Min(Color c1, Color c2)						
		{
			return new Color(Math.Min(c1.Red, c2.Red), 
				Math.Min(c1.Green, c2.Green), 
				Math.Min(c1.Blue, c2.Blue), 
				Math.Min(c1.Alpha, c2.Alpha));
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color	Max(Color c1, Color c2)						
		{
			return new Color(Math.Max(c1.Red, c2.Red), 
				Math.Max(c1.Green, c2.Green), 
				Math.Max(c1.Blue, c2.Blue), 
				Math.Max(c1.Alpha, c2.Alpha));
		}

		[Acceleration (AccelMode.SSE2)]
		public static bool operator == (Color left, Color right) 
		{
			bool ret = false;
			try
			{
				ret = left.Red == right.Red && left.Green == right.Green && left.Blue == right.Blue 
					&& left.Alpha == right.Alpha;
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		[Acceleration (AccelMode.SSE2)]
		public static bool operator != (Color left, Color right) 
		{
			return !(left == right);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator + ( Color a, Color b)
		{
			return new Color(a.Red + b.Red, a.Green + b.Green, a.Blue + b.Blue, a.Alpha + b.Alpha);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator - ( Color a, Color b)
		{
			return new Color(a.Red - b.Red, a.Green - b.Green, a.Blue - b.Blue, a.Alpha - b.Alpha);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator * ( Color a, Color b)
		{
			return new Color(a.Red * b.Red, a.Green * b.Green, a.Blue * b.Blue, a.Alpha * b.Alpha);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator / ( Color a, Color b)
		{
			return new Color(a.Red / b.Red, a.Green / b.Green, a.Blue / b.Blue, a.Alpha / b.Alpha);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator + ( Color a, float b)
		{
			return new Color(a.Red + b, a.Green + b, a.Blue + b, a.Alpha + b);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator - ( Color a, float b)
		{
			return new Color(a.Red - b, a.Green - b, a.Blue - b, a.Alpha - b);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator * ( Color a, float b)
		{
			return new Color(a.Red * b, a.Green * b, a.Blue * b, a.Alpha * b);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator / ( Color a, float b)
		{
			return new Color(a.Red / b, a.Green / b, a.Blue / b, a.Alpha / b);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator + ( float a, Color b)
		{
			return new Color(a + b.Red, a + b.Green, a + b.Blue, a + b.Alpha);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator - ( float a, Color b)
		{
			return new Color(a - b.Red, a - b.Green, a - b.Blue, a - b.Alpha);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator * ( float a, Color b)
		{
			return new Color(a * b.Red, a * b.Green, a * b.Blue, a * b.Alpha);
		}
		[Acceleration (AccelMode.SSE1)]
		public static Color operator / ( float a, Color b)
		{
			return new Color(a / b.Red, a / b.Green, a / b.Blue, a / b.Alpha);
		}

		public byte[] ToByteRgba()
		{
			uint intValue = ToRgba();

			byte[] intBytes = BitConverter.GetBytes(intValue);
			if (BitConverter.IsLittleEndian)
			    Array.Reverse(intBytes);
			return intBytes;
		}
		[Acceleration (AccelMode.SSE2)]
		private float ByteColorToFloat(byte val)
		{
			float f = ((float)val / 255.0f);
			if (!(f > 0.0))
				{
					return (0.0f);
				}
			else if (f <= 0.04045)
				{
					return (f / 12.92f);
				}
			else if (f < 1.0f)
				{
					return (float)Math.Pow(((double)f + 0.055) / 1.055, 2.4);
				}
			else
				{
					return (1.0f);
				}
		}
		[Acceleration (AccelMode.SSE2)]
		private static byte FloatColorToByte(float val)
		{
			if (!(val > 0.0))
			{
				return (0);
			}
			else if (val <= 0.0031308)
			{
				return ((byte)((255.0f * val * 12.92f) + 0.5f));
			}
			else if (val < 1.0)
			{
				return ((byte)((255.0f * ((1.055f * (float)Math.Pow((double)val, (1.0 / 2.4))) - 0.055f)) + 0.5f));
			}
			else
			{
				return (255);
			}
		}

		public float[] ToArray()
		{
			return new float[]
			{
				R, G, B, A
			};
		}
	}
}


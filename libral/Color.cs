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

namespace System.Common
{
	[Serializable]
	public class Color : IFormattable, IEquatable<Color>
	{
		private float[] m_colors = new float[4] { 0, 0, 0, 0 };

		[XmlIgnore]
		public float Red { get { return m_colors[0]; } set { m_colors[0] = value; } }
		[XmlIgnore]
		public float Green { get { return m_colors[1]; } set { m_colors[1] = value; } }
		[XmlIgnore]
		public float Blue { get { return m_colors[2]; } set { m_colors[2] = value; } }
		[XmlIgnore]
		public float Alpha { get { return m_colors[3]; } set { m_colors[3] = value; } }
		[XmlIgnore]
		public float[] fRGBA { get { return m_colors; } set { m_colors = value; } }

		[XmlElement("Red")]
		public byte R { get { return FloatColorToByte(Red ); } set { Red = ByteColorToFloat(value); } }
		[XmlElement("Green")]
		public byte G { get { return FloatColorToByte(Green ); } set { Green = ByteColorToFloat(value); } }
		[XmlElement("Blue")]
		public byte B { get { return FloatColorToByte(Blue); } set { Blue = ByteColorToFloat(value); } }
		[XmlElement("Alpha")]
		public byte A { get { return FloatColorToByte(Alpha ); } set { Alpha = ByteColorToFloat(value); } }

		public Color()
			: this(0,0,0, 1.0f)
		{

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
			Red = red;
			Green = green;
			Blue = blue;
			Alpha = alpha;
		}
		public Color(byte red, byte green, byte blue, byte alpha) 				
		{
			R = red;
			G = green;
			B = blue;
			A = alpha;
		}	
		public Color(string hexstring)
		{
			var cal = Colors.FromString(hexstring);
			R = cal.R;
			G = cal.G;
			B = cal.B;
			A = cal.A;
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
		public override int GetHashCode()
		{
			return base.GetHashCode();
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


		public static Color	Negate(Color c)											
		{
			return new Color(1.0f - c.Red, 1.0f - c.Green, 1.0f - c.Blue, 1.0f - c.Alpha);
		}
		public static float	Brightness(Color c)										
		{
			return c.Red * 0.299f + c.Green * 0.587f + c.Blue * 0.114f;
		}
		public static Color	Interpolate(Color c1, Color c2, float p)	
		{
			return c1 + p * (c2 - c1);
		}

		public static Color	Min(Color c1, Color c2)						
		{
			return new Color(Math.Min(c1.Red, c2.Red), 
				Math.Min(c1.Green, c2.Green), 
				Math.Min(c1.Blue, c2.Blue), 
				Math.Min(c1.Alpha, c2.Alpha));
		}
		public static Color	Max(Color c1, Color c2)						
		{
			return new Color(Math.Max(c1.Red, c2.Red), 
				Math.Max(c1.Green, c2.Green), 
				Math.Max(c1.Blue, c2.Blue), 
				Math.Max(c1.Alpha, c2.Alpha));
		}


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

		public static bool operator != (Color left, Color right) 
		{
			return !(left == right);
		}
		public static Color operator + ( Color a, Color b)
		{
			return new Color(a.Red + b.Red, a.Green + b.Green, a.Blue + b.Blue, a.Alpha + b.Alpha);
		}
		public static Color operator - ( Color a, Color b)
		{
			return new Color(a.Red - b.Red, a.Green - b.Green, a.Blue - b.Blue, a.Alpha - b.Alpha);
		}
		public static Color operator * ( Color a, Color b)
		{
			return new Color(a.Red * b.Red, a.Green * b.Green, a.Blue * b.Blue, a.Alpha * b.Alpha);
		}
		public static Color operator / ( Color a, Color b)
		{
			return new Color(a.Red / b.Red, a.Green / b.Green, a.Blue / b.Blue, a.Alpha / b.Alpha);
		}

		public static Color operator + ( Color a, float b)
		{
			return new Color(a.Red + b, a.Green + b, a.Blue + b, a.Alpha + b);
		}
		public static Color operator - ( Color a, float b)
		{
			return new Color(a.Red - b, a.Green - b, a.Blue - b, a.Alpha - b);
		}
		public static Color operator * ( Color a, float b)
		{
			return new Color(a.Red * b, a.Green * b, a.Blue * b, a.Alpha * b);
		}
		public static Color operator / ( Color a, float b)
		{
			return new Color(a.Red / b, a.Green / b, a.Blue / b, a.Alpha / b);
		}

		public static Color operator + ( float a, Color b)
		{
			return new Color(a + b.Red, a + b.Green, a + b.Blue, a + b.Alpha);
		}
		public static Color operator - ( float a, Color b)
		{
			return new Color(a - b.Red, a - b.Green, a - b.Blue, a - b.Alpha);
		}
		public static Color operator * ( float a, Color b)
		{
			return new Color(a * b.Red, a * b.Green, a * b.Blue, a * b.Alpha);
		}
		public static Color operator / ( float a, Color b)
		{
			return new Color(a / b.Red, a / b.Green, a / b.Blue, a / b.Alpha);
		}

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
	}
}


//
//  Colors.cs
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
using System.Globalization;

namespace System.Common
{
	public class Colors
	{
		public static Color AliceBlue {  get { return  FromUInt32(0xF0F8FFFF); } }
		public static Color AntiqueWhite {  get { return  FromUInt32(0xFAEBD7FF); } }
		public static Color Aqua {  get { return  FromUInt32(0x00FFFFFF); } }
		public static Color Aquamarine {  get { return  FromUInt32(0x7FFFD4FF); } }
		public static Color Azure {  get { return  FromUInt32(0xF0FFFFFF); } }
		public static Color Beige {  get { return  FromUInt32(0xF5F5DCFF); } }
		public static Color Bisque {  get { return  FromUInt32(0xFFE4C4FF); } }
		public static Color Black {  get { return  FromUInt32(0x000000FF); } }
		public static Color BlanchedAlmond {  get { return  FromUInt32(0xFFEBCDFF); } }
		public static Color Blue {  get { return  FromUInt32(0x0000FFFF); } }
		public static Color BlueViolet {  get { return  FromUInt32(0x8A2BE2FF); } }
		public static Color Brown {  get { return  FromUInt32(0xA52A2AFF); } }
		public static Color BurlyWood {  get { return  FromUInt32(0xDEB887FF); } }
		public static Color CadetBlue {  get { return  FromUInt32(0x5F9EA0FF); } }
		public static Color Chartreuse {  get { return  FromUInt32(0x7FFF00FF); } }
		public static Color Chocolate {  get { return  FromUInt32(0xD2691EFF); } }
		public static Color Coral {  get { return  FromUInt32(0xFF7F50FF); } }
		public static Color CornflowerBlue {  get { return  FromUInt32(0x6495EDFF); } }
		public static Color Cornsilk {  get { return  FromUInt32(0xFFF8DCFF); } }
		public static Color Crimson {  get { return  FromUInt32(0xDC143CFF); } }
		public static Color Cyan {  get { return  FromUInt32(0x00FFFFFF); } }
		public static Color DarkBlue {  get { return  FromUInt32(0x00008BFF); } }
		public static Color DarkCyan {  get { return  FromUInt32(0x008B8BFF); } }
		public static Color DarkGoldenrod {  get { return  FromUInt32(0xB8860BFF); } }
		public static Color DarkGray {  get { return  FromUInt32(0xA9A9A9FF); } }
		public static Color DarkGreen {  get { return  FromUInt32(0x006400FF); } }
		public static Color DarkKhaki {  get { return  FromUInt32(0xBDB76BFF); } }
		public static Color DarkMagenta {  get { return  FromUInt32(0x8B008BFF); } }
		public static Color DarkOliveGreen {  get { return  FromUInt32(0x556B2FFF); } }
		public static Color DarkOrange {  get { return  FromUInt32(0xFF8C00FF); } }
		public static Color DarkOrchid {  get { return  FromUInt32(0x9932CCFF); } }
		public static Color DarkRed {  get { return  FromUInt32(0x8B0000FF); } }
		public static Color DarkSalmon {  get { return  FromUInt32(0xE9967AFF); } }
		public static Color DarkSeaGreen {  get { return  FromUInt32(0x8FBC8FFF); } }
		public static Color DarkSlateBlue {  get { return  FromUInt32(0x8FBC8FFF); } }
		public static Color DarkSlateGray {  get { return  FromUInt32(0x2F4F4FFF); } }
		public static Color DarkTurquoise {  get { return  FromUInt32(0x00CED1FF); } }
		public static Color DarkViolet {  get { return  FromUInt32(0x9400D3FF); } }
		public static Color DeepPink {  get { return  FromUInt32(0xFF1493FF); } }
		public static Color DeepSkyBlue {  get { return  FromUInt32(0x00BFFFFF); } }
		public static Color DimGray {  get { return  FromUInt32(0x696969FF); } }
		public static Color DodgerBlue {  get { return  FromUInt32(0x1E90FFFF); } }
		public static Color FireBrick {  get { return  FromUInt32(0xB22222FF); } }
		public static Color FloralWhite {  get { return  FromUInt32(0xFFFAF0FF); } }
		public static Color ForestGreen {  get { return  FromUInt32(0x228B22FF); } }
		public static Color Fuchsia {  get { return  FromUInt32(0xFF00FFFF); } }
		public static Color Gainsboro {  get { return  FromUInt32(0xDCDCDCFF); } }
		public static Color GhostWhite {  get { return  FromUInt32(0xF8F8FFFF); } }
		public static Color Gold {  get { return  FromUInt32(0xFFD700FF); } }
		public static Color Goldenrod {  get { return  FromUInt32(0xDAA520FF); } }
		public static Color Gray {  get { return  FromUInt32(0x808080FF); } }
		public static Color Green {  get { return  FromUInt32(0x008000FF); } }
		public static Color GreenYellow {  get { return  FromUInt32(0xADFF2FFF); } }
		public static Color Honeydew {  get { return  FromUInt32(0xF0FFF0FF); } }
		public static Color IndianRed {  get { return  FromUInt32(0xCD5C5CFF); } }
		public static Color Indigo {  get { return  FromUInt32(0x4B0082FF); } }
		public static Color Ivory {  get { return  FromUInt32(0xFFFFF0FF); } }
		public static Color Khaki {  get { return  FromUInt32(0xF0E68CFF); } }
		public static Color Lavender {  get { return  FromUInt32(0xE6E6FAFF); } }
		public static Color LavenderBlush {  get { return  FromUInt32(0xFFF0F5FF); } }
		public static Color LawnGreen {  get { return  FromUInt32(0x7CFC00FF); } }
		public static Color LemonChiffon {  get { return  FromUInt32(0xFFFACDFF); } }
		public static Color LightBlue {  get { return  FromUInt32(0xADD8E6FF); } }
		public static Color LightCoral {  get { return  FromUInt32(0xF08080FF); } }
		public static Color LightCyan {  get { return  FromUInt32(0xE0FFFFFF); } }
		public static Color LightGoldenrodYellow {  get { return  FromUInt32(0xFAFAD2FF); } }
		public static Color LightGreen {  get { return  FromUInt32(0x90EE90FF); } }
		public static Color LightGrey {  get { return  FromUInt32(0xD3D3D3FF); } }
		public static Color LightPink {  get { return  FromUInt32(0x228B22FF); } }
		public static Color LightSalmon {  get { return  FromUInt32(0xFFA07AFF); } }
		public static Color LightSeaGreen {  get { return  FromUInt32(0x20B2AAFF); } }
		public static Color LightSkyBlue {  get { return  FromUInt32(0x87CEFAFF); } }
		public static Color LightSlateGray {  get { return  FromUInt32(0x778899FF); } }
		public static Color LightSteelBlue {  get { return  FromUInt32(0xB0C4DEFF); } }
		public static Color LightYellow {  get { return  FromUInt32(0xFFFFE0FF); } }
		public static Color Lime {  get { return  FromUInt32(0x00FF00FF); } }
		public static Color LimeGreen {  get { return  FromUInt32(0x32CD32FF); } }
		public static Color Linen {  get { return  FromUInt32(0xFAF0E6FF); } }
		public static Color Magenta {  get { return  FromUInt32(0xFF00FFFF); } }
		public static Color Maroon {  get { return  FromUInt32(0x800000FF); } }
		public static Color MediumAquamarine {  get { return  FromUInt32(0x66CDAAFF); } }
		public static Color MediumBlue {  get { return  FromUInt32(0x0000CDFF); } }
		public static Color MediumOrchid {  get { return  FromUInt32(0xBA55D3FF); } }
		public static Color MediumPurple {  get { return  FromUInt32(0x9370DBFF); } }
		public static Color MediumSeaGreen {  get { return  FromUInt32(0x3CB371FF); } }
		public static Color MediumSlateBlue {  get { return  FromUInt32(0x7B68EEFF); } }
		public static Color MediumSpringGreen {  get { return  FromUInt32(0x00FA9AFF); } }
		public static Color MediumTurquoise {  get { return  FromUInt32(0x48D1CCFF); } }
		public static Color MediumVioletRed {  get { return  FromUInt32(0xC71585FF); } }
		public static Color MidnightBlue {  get { return  FromUInt32(0x191970FF); } }
		public static Color MintCream {  get { return  FromUInt32(0xF5FFFAFF); } }
		public static Color MistyRose {  get { return  FromUInt32(0xFFE4E1FF); } }
		public static Color Moccasin {  get { return  FromUInt32(0xFFE4B5FF); } }
		public static Color NavajoWhite {  get { return  FromUInt32(0xFFDEADFF); } }
		public static Color Navy {  get { return  FromUInt32(0x000080FF); } }
		public static Color OldLace {  get { return  FromUInt32(0xFDF5E6FF); } }
		public static Color Olive {  get { return  FromUInt32(0x808000FF); } }
		public static Color OliveDrab {  get { return  FromUInt32(0x6B8E23FF); } }
		public static Color Orange {  get { return  FromUInt32(0xFFA500FF); } }
		public static Color OrangeRed {  get { return  FromUInt32(0xFF4500FF); } }
		public static Color Orchid {  get { return  FromUInt32(0xDA70D6FF); } }
		public static Color PaleGoldenrod {  get { return  FromUInt32(0xEEE8AAFF); } }
		public static Color PaleGreen {  get { return  FromUInt32(0x98FB98FF); } }
		public static Color PaleTurquoise {  get { return  FromUInt32(0xAFEEEEFF); } }
		public static Color PaleVioletRed {  get { return  FromUInt32(0xDB7093FF); } }
		public static Color PapayaWhip {  get { return  FromUInt32(0xFFEFD5FF); } }
		public static Color PeachPuff {  get { return  FromUInt32(0xFFDAB9FF); } }
		public static Color Peru {  get { return  FromUInt32(0xCD853FFF); } }
		public static Color Pink {  get { return  FromUInt32(0xFFC0CBFF); } }
		public static Color Plum {  get { return  FromUInt32(0xDDA0DDFF); } }
		public static Color PowderBlue {  get { return  FromUInt32(0xB0E0E6FF); } }
		public static Color Purple {  get { return  FromUInt32(0x800080FF); } }
		public static Color Red {  get { return  FromUInt32(0xFF0000FF); } }
		public static Color RosyBrown {  get { return  FromUInt32(0xBC8F8FFF); } }
		public static Color RoyalBlue {  get { return  FromUInt32(0x4169E1FF); } }
		public static Color SaddleBrown {  get { return  FromUInt32(0x8B4513FF); } }
		public static Color Salmon {  get { return  FromUInt32(0xFA8072FF); } }
		public static Color SandyBrown {  get { return  FromUInt32(0xF4A460FF); } }
		public static Color SeaGreen {  get { return  FromUInt32(0x2E8B57FF); } }
		public static Color Seashell {  get { return  FromUInt32(0xFFF5EEFF); } }
		public static Color Sienna {  get { return  FromUInt32(0xA0522DFF); } }
		public static Color Silver {  get { return  FromUInt32(0xC0C0C0FF); } }
		public static Color SkyBlue {  get { return  FromUInt32(0x87CEEBFF); } }
		public static Color SlateBlue {  get { return  FromUInt32(0x6A5ACDFF); } }
		public static Color SlateGray {  get { return  FromUInt32(0x708090FF); } }
		public static Color Snow {  get { return  FromUInt32(0xFFFAFAFF); } }
		public static Color SpringGreen {  get { return  FromUInt32(0x00FF7FFF); } }
		public static Color SteelBlue {  get { return  FromUInt32(0x4682B4FF); } }
		public static Color Tan {  get { return  FromUInt32(0xD2B48CFF); } }
		public static Color Teal {  get { return  FromUInt32(0x008080FF); } }
		public static Color Thistle {  get { return  FromUInt32(0xD8BFD8FF); } }
		public static Color Tomato {  get { return  FromUInt32(0xFF6347FF); } }
		public static Color Turquoise {  get { return  FromUInt32(0x40E0D0FF); } }
		public static Color Violet {  get { return  FromUInt32(0xEE82EEFF); } }
		public static Color Wheat {  get { return  FromUInt32(0xF5DEB3FF); } }
		public static Color White {  get { return  FromUInt32(0xFFFFFFFF); } }
		public static Color WhiteSmoke {  get { return  FromUInt32(0xF5F5F5FF); } }
		public static Color Yellow {  get { return  FromUInt32(0xFFFF00FF); } }
		public static Color YellowGreen {  get { return  FromUInt32(0x9ACD32FF); } }


		public static Color FromUInt32(uint rgba)
		{
			Color c1 = new Color();

			c1.R = (byte)((rgba & 0xff000000) >> 24);
			c1.G = (byte)((rgba & 0x00ff0000) >> 16);
			c1.B = (byte)((rgba & 0x0000ff00) >> 8);
			c1.A = (byte)(rgba & 0x000000ff);

			return c1;
		}
		public static Color FromString(string hexColor)
		{
			//Remove # if present
			if (hexColor.IndexOf('#') != -1)
				hexColor = hexColor.Replace("#", "");

			if (hexColor.Length == 8)
				{
					return new Color(
						//#RRGGBBAA
						byte.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier),
						byte.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier),
						byte.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier),
						byte.Parse(hexColor.Substring(6, 2), NumberStyles.AllowHexSpecifier));
				}
			else if (hexColor.Length == 6)
				{
					return new Color(
						//#RRGGBBAA
						byte.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier),
						byte.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier),
						byte.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier),
						255);
				}
			else if (hexColor.Length == 4)
				{
					return new Color(
						byte.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier),
						byte.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier),
						byte.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier),
						byte.Parse(hexColor[3].ToString() + hexColor[3].ToString(), NumberStyles.AllowHexSpecifier));
				}
			else if (hexColor.Length == 3)
				{
					return new Color(
						byte.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier),
						byte.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier),
						byte.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier),
						255);

				}
			return Colors.Black;
		}
		public static Color FromYUV(float y, float u, float v)
		{
			float r = 1.164f * (y - 16) + 1.596f*(v - 128);
			float g = 1.164f * (y - 16) - 0.813f*(v - 128) - 0.391f*(u - 128);
			float b = 1.164f * (y - 16) + 2.018f*(u - 128);

			return new Color(r, g, b);
		}
		public static Color FromHSV(float h, float s, float v)
		{
			float i;
			float f, p, q, t;
			if (s == 0)
				{
					// achromatic (grey)
					return new Color(v, v, v);
				}
			h /= 60;			// sector 0 to 5
			i = (float)Math.Floor(h);
			f = h - i;			// factorial part of h
			p = v * ( 1 - s );
			q = v * ( 1 - s * f );
			t = v * ( 1 - s * ( 1 - f ) );

			if(i == 0)
				return new Color(v, t, p);
			if( i == 1.0f)
				return new Color(q, v, p);
			if(i == 2.0f)
				return new Color(p, v, t);
			if(i == 3.0f)
				return new Color(p, q, v);
			if(i == 4.0f)
				return new Color(t, p, v);

			return new Color(v, p, q);
		}
		public static Color FromCMY(float c, float m, float y)
		{
			return new Color( 1.0f - c, 1.0f - m, 1.0f - y);
		}
	}
}


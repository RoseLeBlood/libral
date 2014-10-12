//
//  Rectangle.cs
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
	public class Rectangle : IEquatable<Rectangle>
	{
		private Vector4i	m_iRect;

		public static Rectangle Empty 
		{
			get { return new Rectangle (0, 0, 0, 0); }
		}

		public int		X
		{
			get { return m_iRect.X; }
			set { m_iRect.X = value; }
		}
		public int Y
		{
			get { return m_iRect.Y; }
			set { m_iRect.Y = value; }
		}

		public int Width
		{
			get { return m_iRect.Z; }
			set { m_iRect.Z = value; }
		}
		public int Height
		{
			get { return m_iRect.W; }
			set { m_iRect.W = value; }
		}
		public int Top
		{
			get { return Y;	}
		}
		public int Left
		{
			get { return X;	}
		}
		public int Bottom
		{
			get { return Y + Height;	}
		}
		public int Right
		{
			get { return X + Width;	}
		}
		public Point Location
		{
			get { return new Point (X, Y);	}
			set { X = value.X; Y = value.Y; }
		}
		public Point Center 
		{
			get { return new Point (X + Width / 2, Y + Height / 2); }
		}
		public bool IsEmpty 
		{
			get { return X == 0 && Y == 0; }
		}

		public Size Size
		{
			get { return new Size (Width, Height);	}
		}
		public Rectangle()
			: this(0,0,1,1)
		{
		}
		public Rectangle(int x, int y, int width, int height)
		{
			m_iRect = new Vector4i();
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}
		public Rectangle(int[] rect)
		{
			m_iRect = new Vector4i();
			X = rect[0];
			Y = rect[1];
			Width = rect[2];
			Height = rect[3];
		}
		public Rectangle(int x, int y, Size size)
		{
			m_iRect = new Vector4i();
			X = x;
			Y = y;
			Width = size.Width;
			Height = size.Height;
		}
		public Rectangle(string rectstring)
		{
			string[] rect = rectstring.Split(',');
			int x = 0, y=0, w=0, h=0;
			if (rect.Length == 4)
			{

				int.TryParse(rect[0], out y); // Top
				int.TryParse(rect[1], out x); // Left
				int.TryParse(rect[2], out w); 
				int.TryParse(rect[3], out h); 

			}
			else if (rect.Length == 2)
			{

				int.TryParse(rect[0], out x); // Top
				int.TryParse(rect[0], out y); // Left
				int.TryParse(rect[1], out w); 
				int.TryParse(rect[1], out h); 

			}
			m_iRect = new Vector4i(x, y, w, h);
		}
		public Rectangle Inflate (int leftRight, int topBottom)
		{
			m_iRect.X -= leftRight;
			m_iRect.Z += leftRight * 2;
			m_iRect.Y -= topBottom;
			m_iRect.W += topBottom * 2;

			return this;
		}
		public bool Contains (int x, int y)
		{
			return Contains (new Point (x, y));
		}
		public bool Contains (Point value)
		{
			bool result;
			Contains (ref value, out result);
			return result;
		}
		public void Contains (ref Point value, out bool result)
		{
			result = value.X >= Left && value.X <= Right && value.Y >= Top && value.Y <= Bottom;  
		}
		public bool Contains (Rectangle value)
		{
			bool result;
			Contains (ref value, out result);
			return result;
		}
		public void Contains (ref Rectangle value, out bool result)
		{
			result = value.Left >= Left && value.Right <= Right && value.Top >= Top && value.Bottom <= Bottom;
		}
		public static Rectangle Intersect (Rectangle value1, Rectangle value2)
		{
			Rectangle result;
			Intersect (ref value1, ref value2, out result);
			return result;
		}
		public static void Intersect (ref Rectangle value1, ref Rectangle value2, out Rectangle result)
		{
			int x = System.Math.Max (value1.X, value2.X);
			int y = System.Math.Max (value1.Y, value2.Y);
			int w = System.Math.Min (value1.Right, value2.Right) - x;
			int h = System.Math.Min (value1.Bottom, value2.Bottom) - y;
			if (w <= 0 || h <= 0)
				result = Rectangle.Empty;
			else
				result = new Rectangle (x, y, w, h);
		}
		public bool Intersects (Rectangle value)
		{
			bool result;
			Intersects (ref value, out result);
			return result;
		}

		public void Intersects (ref Rectangle value, out bool result)
		{
			int w = System.Math.Min (value.Right, Right) - System.Math.Max (value.X, X);
			int h = System.Math.Min (value.Bottom, Bottom) - System.Math.Max (value.Y, Y);
			result = w > 0 && h > 0;
		}
		public void Offset (int offsetX, int offsetY)
		{
			X += offsetX;
			Y += offsetY;
		}

		public void Offset (Point amount)
		{
			X += amount.X;
			Y += amount.Y;
		}

		public static Rectangle Union (Rectangle value1, Rectangle value2)
		{
			Rectangle result;
			Union (ref value1, ref value2, out result);
			return result;
		}

		public static void Union (ref Rectangle value1, ref Rectangle value2, out Rectangle result)
		{
			int x = System.Math.Min (value1.X, value2.X);
			int y = System.Math.Min (value1.Y, value2.Y);
			int w = System.Math.Max (value1.Right, value2.Right) - x;
			int h = System.Math.Max (value1.Bottom, value2.Bottom) - y;
			result = new Rectangle (x, y, w, h);
		}

		public override string ToString()
		{
			return string.Format("{0},{1},{2},{3}", 
				Top, Left, Width, Height);
		}

		public bool Equals (Rectangle other)
		{
			return other == this;
		}

		public override bool Equals (object obj)
		{
			return obj is Rectangle && ((Rectangle)obj) == this;
		}

		public override int GetHashCode ()
		{
			return X ^ Y ^ Width ^ Height;
		}

		public static bool operator == (Rectangle a, Rectangle b)
		{
			return a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
		}

		public static bool operator != (Rectangle a, Rectangle b)
		{
			return a.X != b.X || a.Y != b.Y || a.Width != b.Width || a.Height != b.Height;
		}
		public static implicit operator Rectangle(string strRect) 
		{
			Rectangle d = new Rectangle(strRect);  
			return d;
		}
		public static implicit operator Rectangle(Size sSize) 
		{
			Rectangle d = new Rectangle(0,0,sSize.Width, sSize.Height);  
			return d;
		}
	}
}


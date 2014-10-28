//
//  Point.cs
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
	public class Point //: IEquatable<Point>
	{
		private int m_iX;
		private int m_iY;

		public static Point Zero 
		{
			get { return new Point (0, 0); }
		}

		public int X
		{
			get { return m_iX; }
			set { m_iX = value; }
		}
		public int Y
		{
			get { return m_iY; }
			set { m_iY = value; }
		}

		public Point()
			: this(0,0)
		{
		}
		public Point(int xy)
			: this(xy,xy)
		{
		}
		public Point(int[] xy)
			: this(xy[0],xy[1])
		{
		}
		public Point(int iX, int iY)
		{
			m_iX = iX;
			m_iY = iY;
		}
		public Point(string rectstring)
		{
			string[] rect = rectstring.Split(',');
			if (rect.Length == 1)
				{
					int.TryParse(rect[0], out m_iX); 
					int.TryParse(rect[0], out m_iY); 
				}
			else if (rect.Length == 2)
				{
					int.TryParse(rect[0], out m_iX);
					int.TryParse(rect[1], out m_iY); 
				}
		}
		public bool Equals (Point other)
		{
			return other == this;
		}
		public override bool Equals (object obj)
		{
			return obj is Point && ((Point)obj) == this;
		}

		public override int GetHashCode ()
		{
			return X.GetHashCode () ^ Y.GetHashCode ();
		}

		public static bool operator == (Point a, Point b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator != (Point a, Point b)
		{
			return a.X != b.X || a.Y != b.Y;
		}
		public override string ToString ()
		{
			return string.Format ("{0} {1}", X, Y);
		}
		public static implicit operator Point(string strPoint) 
		{
			return new Point(strPoint);  
		}
	}
}


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

namespace libral
{
	[Serializable]
	public class Rectangle
	{
		private int 	m_iX;
		private int 	m_iY;
		private int 	m_iWidth;
		private int 	m_iHeight;

		public int		X
		{
			get { return m_iX; }
			set { m_iX = value; }
		}
		public int Y
		{
			get { return m_iY; }
			set { m_iY = value; }
		}

		public int Width
		{
			get { return m_iWidth; }
			set { m_iWidth = value; }
		}
		public int Height
		{
			get { return m_iHeight; }
			set { m_iHeight = value; }
		}
		public int Top
		{
			get { return m_iY;	}
		}
		public int Left
		{
			get { return m_iX;	}
		}
		public int Bottom
		{
			get { return m_iY + m_iHeight;	}
		}
		public int Right
		{
			get { return m_iX + m_iWidth;	}
		}
		public Point Position
		{
			get { return new Point (m_iY, m_iX);	}
		}
		public Size Size
		{
			get { return new Size (m_iWidth, m_iHeight);	}
		}
		public Rectangle()
			: this(0,0,1,1)
		{
		}
		public Rectangle(int x, int y, int width, int height)
		{
			m_iX = x;
			m_iY = y;
			m_iWidth = width;
			m_iHeight = height;
		}
		public Rectangle(string rectstring)
		{
			string[] rect = rectstring.Split(',');
			if (rect.Length == 4)
			{
				var _bottom = 0;
				var _right = 0;
				int.TryParse(rect[0], out m_iY); // Top
				int.TryParse(rect[1], out m_iX); // Left
				int.TryParse(rect[2], out _bottom); // Bottom = m_iY + m_iHeight; 
				int.TryParse(rect[3], out _right); // Right = m_iX + m_iWidth;

				m_iHeight = _bottom - m_iY;
				m_iWidth = _right - m_iX;
			}
			else if (rect.Length == 2)
			{
				var _bottom = 0;
				var _right = 0;
				int.TryParse(rect[0], out m_iY); // Top
				int.TryParse(rect[0], out m_iX); // Left
				int.TryParse(rect[1], out _bottom); // Bottom = m_iY + m_iHeight; 
				int.TryParse(rect[1], out _right); // Right = m_iX + m_iWidth;

				m_iHeight = _bottom - m_iY;
				m_iWidth = _right - m_iX;
			}
			Console.WriteLine(this.ToString());
		}
		public Rectangle Inflate (int leftRight, int topBottom)
		{
			m_iX -= leftRight;
			m_iWidth += leftRight * 2;
			m_iY -= topBottom;
			m_iHeight += topBottom * 2;

			return this;
		}
		public override string ToString()
		{
			return string.Format("{0},{1},{2},{3}", 
				Top, Left, Bottom, Right);
		}
	}
}


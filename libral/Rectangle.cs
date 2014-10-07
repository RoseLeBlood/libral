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
		public Rectangle(int[] rect)
		{
			m_iX = rect[0];
			m_iY = rect[1];
			m_iWidth = rect[2];
			m_iHeight = rect[3];
		}
		public Rectangle(int x, int y, Size size)
		{
			m_iX = x;
			m_iY = y;
			m_iWidth = size.Width;
			m_iHeight = size.Height;
		}
		public Rectangle(string rectstring)
		{
			string[] rect = rectstring.Split(',');
			if (rect.Length == 4)
			{

				int.TryParse(rect[0], out m_iY); // Top
				int.TryParse(rect[1], out m_iX); // Left
				int.TryParse(rect[2], out m_iWidth); 
				int.TryParse(rect[3], out m_iHeight); 

			}
			else if (rect.Length == 2)
			{

				int.TryParse(rect[0], out m_iY); // Top
				int.TryParse(rect[0], out m_iX); // Left
				int.TryParse(rect[1], out m_iWidth); 
				int.TryParse(rect[1], out m_iHeight); 

			}
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
				Top, Left, Width, Height);
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


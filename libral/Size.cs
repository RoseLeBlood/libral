//
//  Size.cs
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
	[Serializable]
	public class Size
	{
		private int m_iWidth;
		private int m_iHeight;

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

		public Size() : this(0,0)
		{
		}
		public Size(int iWh) : this(iWh, iWh)
		{
		}
		public Size(int iWidth, int iHeight)
		{
			m_iWidth = iWidth;
			m_iHeight = iHeight;
		}
		public Size(string sizestring)
		{
			string[] rect = sizestring.Split('x');
			if (rect.Length == 2)
			{
				int.TryParse(rect[0], out m_iWidth); // Left
				int.TryParse(rect[1], out m_iHeight); 
			}
		}
		public override string ToString()
		{
			return string.Format("{0}x{1}", 
				Width, Height);
		}


		public static implicit operator Size(string strSize) 
		{
			Size d = new Size(strSize);  
			return d;
		}
	}
}


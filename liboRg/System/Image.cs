//
//  Image.cs
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
using System.Common;
using System.Runtime.InteropServices;
using System.IO;

namespace System
{
	public class Image : ImageHandle
	{
		private Color[] m_clImage;
		private Size m_sSize;

		public Color[] ImageData { get { return m_clImage; } }
		public Size Size { get { return m_sSize; } }

		public Image(string strName)
			: this(strName, 32,32,Colors.Navy)
		{

		}
		public Image(string strName, ushort sWidth, ushort sHeight)
			: this(strName, sWidth, sHeight,Colors.Navy)
		{

		}
		public Image(string strName, ushort sWidth, ushort sHeight, Stream stream)
			: base(strName)
		{
			m_clImage = new Color[ sWidth * sHeight ];
			var x = Math.Min(m_clImage.Length*4, stream.Length);

			for (uint i = 0; i < x; i++)
			{
				byte[] colorBlock = new byte[4];
				if (stream.Read(colorBlock, 0, 4) == 4)
					m_clImage[i] = new Color(colorBlock[0], colorBlock[1], colorBlock[2], colorBlock[3]);
			}
		}
		public Image(string strName, ushort sWidth, ushort sHeight, Color clBackground )
			: base(strName)
		{
			m_clImage = new Color[ sWidth * sHeight ];

			for ( uint i = 0; i < m_clImage.Length; i++ )
				m_clImage[i] = clBackground;

			m_sSize = new Size(sWidth, sHeight);
		}
		public Color GetPixel(uint x, uint y)
		{
			if ( x >= m_sSize.Width || y >= m_sSize.Height ) return new Color();
			return m_clImage[ x + y * m_sSize.Width ];
		}
		public void SetPixel(uint x, uint y, Color color)
		{
			if (x >= m_sSize.Width || y >= m_sSize.Height)
				return;
			m_clImage[x + y * m_sSize.Width] = color;
		}
		public byte[] ToByteArray()
		{
			MemoryStream stream = new MemoryStream();

			for (int i = 0; i < m_clImage.Length; i++)
			{
				byte[] data = m_clImage[i].ToByteRgba();
				stream.Write(data, 0, data.Length);
			}
			return stream.ToArray();
		}

	}
}


//
//  VertexDataBuffer.cs
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
using System.IO;

namespace liboRg.Framework
{
	public class VertexDataBuffer
	{
		private MemoryStream m_stream = new MemoryStream();


		public void Floats( float[] v ) 
		{ 
			var byteArray = new byte[v.Length * 4];
			Buffer.BlockCopy(v, 0, byteArray, 0, byteArray.Length);

			Bytes(byteArray);
		}
		public void Int8( sbyte v ) 
		{ 
			byte[] pBytes = BitConverter.GetBytes(v);
			if (BitConverter.IsLittleEndian) Array.Reverse(pBytes);
			Bytes(pBytes);
		}
		public void Int16( Int16 v ) 
		{ 
			byte[] pBytes = BitConverter.GetBytes(v);
			if (BitConverter.IsLittleEndian) Array.Reverse(pBytes);
			Bytes(pBytes);
		}
		public void Int32( Int32 v ) 
		{ 
			byte[] pBytes = BitConverter.GetBytes(v);
			if (BitConverter.IsLittleEndian)
				Array.Reverse(pBytes);

			Bytes(pBytes);
		}
		public void Uint8( byte v ) 
		{ 
			byte[] pBytes = BitConverter.GetBytes(v);
			if (BitConverter.IsLittleEndian)
				Array.Reverse(pBytes);

			Bytes(pBytes);
		}
		public void Uint16( UInt16 v ) 
		{ 
			byte[] pBytes = BitConverter.GetBytes(v);
			if (BitConverter.IsLittleEndian)
				Array.Reverse(pBytes);

			Bytes(pBytes);
		}
		public void Uint32( UInt32 v ) 
		{ 
			byte[] pBytes = BitConverter.GetBytes(v);
			if (BitConverter.IsLittleEndian)
				Array.Reverse(pBytes);

			Bytes(pBytes); 
		}
		public void Float( float v ) 		{ Floats(new float[] { v }); }
		public void Vector2( Vector2 v ) 	{ Floats(v.ToArray()); }
		public void Vector3( Vector3 v ) 	{ Floats(v.ToArray()); }
		public void Vector4( Vector4 v ) 	{ Floats(v.ToArray()); }
		public void Matrix( Matrix m ) 		{ Floats(m.ToArray()); }
		public void Color( Color v )  		{ Floats(v.ToArray()); }

		public byte[] ToArray()  			{ return m_stream.ToArray(); }
		public long Length() 				{ return m_stream.Length; }

		private void Bytes( byte[] bytes ) 	{ m_stream.Write(bytes, 0, bytes.Length); }
	}
}


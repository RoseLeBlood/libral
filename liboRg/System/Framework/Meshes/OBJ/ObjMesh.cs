//
//  ObjMesh.cs
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
using System.Collections.Generic;
using System.Common;

namespace System.Framework.Meshes
{
	internal enum mode
	{
		MODE_NONE,
		MODE_UNKNOWN,
		MODE_VERTEX,
		MODE_NORMAL,
		MODE_TEXTURE,
		MODE_FACE
	}
	internal struct face
	{
		public int v1, v2, v3;
		public int t1, t2, t3;
		public int n1, n2, n3;
	}

	public unsafe class ObjMesh : Mesh
	{
		public unsafe ObjMesh(string strPath)
			: base(strPath)
		{
			int approxMem = 295 + m_arData.Length / 1024 * 11;
			List<Vector3> vectors = new List<Vector3>(approxMem);
			List<Vector3> normals = new List<Vector3>(approxMem);
			List<Vector2> texcoords = new List<Vector2>(approxMem);

			bool inComment = false;
			mode mode = mode.MODE_NONE;
			Vector3 v3 = new Vector3();
			face f = new face();

			fixed(byte *buf = m_arData)
			{
				for (int i = 0; i < m_arData.Length; i++)
				{
					if (buf[i] == '#')
					{
						inComment = true;
						continue;
					}
					else if (inComment && buf[i] != '\n')
					{
						continue;
					}
					else if (inComment && buf[i] == '\n')
					{
						inComment = false;
						continue;
					}

					if (mode == mode.MODE_NONE && !IS_SPACING(m_arData[i]))
					{
						if (m_arData[i] == 'f')
							mode = mode.MODE_FACE;
						else if (buf[i] == 'v')
						{
							if (IS_SPACING(buf[i + 1]))
								mode = mode.MODE_VERTEX;
							else if (buf[i + 1] == 'n')
								mode = mode.MODE_NORMAL;
							else if (buf[i + 1] == 't')
								mode = mode.MODE_TEXTURE;
							i++;
						}
						else
							mode = mode.MODE_UNKNOWN;
					}
					else if (mode != mode.MODE_NONE)
					{
						switch (mode)
						{
							case mode.MODE_UNKNOWN:
								if (buf[i] == '\n')
									mode = mode.MODE_NONE;
								break;
							case mode.MODE_VERTEX:
								i += readVector(&buf[i], out v3);
								vectors.Add(v3);
								mode = mode.MODE_NONE;
								break;
							case mode.MODE_NORMAL:
								
								i += readVector(&buf[i], out v3);
								normals.Add(v3);
								mode = mode.MODE_NONE;
								break;
							case mode.MODE_TEXTURE:
								i += readVector(&buf[i], out v3);
								texcoords.Add(new Vector2(v3.X, v3.Y));
								mode = mode.MODE_NONE;
								break;
							case mode.MODE_FACE:
								i += readFace(&buf[i], out f);
								base.m_lstVertices.Add(new Vector3(f.v1, f.v2, f.v3),
									new Vector2(f.t1, f.t2),
									new Vector3(f.n1, f.n2, f.n3));

								mode = mode.MODE_NONE;
								break;
						}
					}
				}
			}

		}
		bool IS_SPACING(byte c)
		{
			return c == ' ' || c == '\n' || c == '\t';
		}


		unsafe int readVector(byte* buf, out Vector3 v3)
		{
			int i = 0;
			float x = 0, y = 0, z = 0;

			while ( IS_SPACING( *buf ) ) { buf++; i++; };
			buf += readFloat( buf, out x );
			while ( IS_SPACING( *buf ) ) { buf++; i++; }
			buf += readFloat( buf, out y );
			while ( IS_SPACING( *buf ) ) { buf++; i++; }
			buf += readFloat( buf, out z );

			v3 = new Vector3(x,y,z);
			return i;
		}

		int readInt(byte* buf, out int frac)
		{
			// Determine length and sign of number
			int l = 0;
			bool neg = false;
			if (buf[0] == '-')
			{
				neg = true;
				buf++;
			}
			while (buf[l] >= '0' && buf[l] <= '9')
				l++;
			// Multiply each digit by the appropriate power of 10
			int n = 0;
			int p = 1;
			for (int i = l - 1; i >= 0; i--)
			{
				n += (buf[i] - '0') * p;
				p *= 10;
			}
			// Handle sign
			if (neg)
			{
				frac = -n;
				l++;
			}
			else
				frac = n;
			return l;
		}
	
		int readFloat(byte* buf, out float x)
		{
			// Read natural part
			bool neg = buf[0] == '-';
			int nat, frac;
			int ln = readInt( buf, out nat );

			// Read fractional part
			buf += ln + 1;
			int lf = readInt( buf, out frac );
			int p = 1;
			for ( int i = 0; i < lf; i++ ) p *= 10;

			// Handle sign
			if ( neg )
				x = nat - frac / (float)p;
			else
				x = nat + frac / (float)p;
			return ln + lf + 1;
		}
		int readFace( byte* buf, out face fe )
		{
			int i = 0;
			fe = new face();
			while ( IS_SPACING( *buf ) ) { buf++; i++; };
			buf += readInt( buf, out fe.v1 ) + 1;
			buf += readInt( buf, out fe.t1 ) + 1;
			buf += readInt( buf, out fe.n1 ) + 1;
			while ( IS_SPACING( *buf ) ) { buf++; i++; }
			buf += readInt( buf, out fe.v2 ) + 1;
			buf += readInt( buf, out fe.t2 ) + 1;
			buf += readInt( buf, out fe.n2 ) + 1;
			while ( IS_SPACING( *buf ) ) { buf++; i++; }
			buf += readInt( buf, out fe.v3 ) + 1;
			buf += readInt( buf, out fe.t3 ) + 1;
			buf += readInt( buf, out fe.n3 ) + 1;
			return i;
		}
	}
}


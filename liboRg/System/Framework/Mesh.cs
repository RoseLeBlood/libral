//
//  VertexBuffer.cs
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
using System.IO;
using System.Common;
using System.Runtime.InteropServices;
using System.API.OpenGL;
using System.Collections.Generic;

namespace System.Framework
{

	public class Mesh : OpenGLHandle
	{
		protected MeshVertex		m_lstVertices;
		protected Byte[]			m_arData;

		public MeshVertex Vertices
		{
			get { return m_lstVertices; }
		}

		public Mesh(string filename)
			: base("Mesh_" + System.IO.Path.GetFileName(filename))
		{
			var t = Application.Current.GetHandle<Mesh>(this.Name);
			if (t != null)
			{
				m_lstVertices = t.m_lstVertices;
				m_arData = t.m_arData;
				return;
			}
			if (!File.Exists(filename))
			{

			}
			m_arData = File.ReadAllBytes(filename);
			m_lstVertices = new MeshVertex(this);
			Register(true);
		}
	}
}


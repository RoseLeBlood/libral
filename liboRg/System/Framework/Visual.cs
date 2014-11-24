//
//  Visual.cs
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

namespace System.Framework
{
	public enum VisualFillMode
	{
		Solid,
		Wired
	}
	public class Visual : Render, IMesh
	{
		private VisualFillMode m_eVisualMode;

		public VisualFillMode VisualMode
		{
			get { return m_eVisualMode; }
		}

		public Visual( Game pGame, Matrix mWorld, string strName, bool bAlpha = false, bool bAdditive = false,
			bool bDepthDisable = false, VisualFillMode fillMode = VisualFillMode.Solid)
			: base(pGame, mWorld, strName)
		{
			m_eVisualMode = fillMode;

		}



		#region IMesh implementation

		public bool CreateMesh()
		{
			throw new NotImplementedException();
		}

		public bool DestroyMesh()
		{
			throw new NotImplementedException();
		}

		public bool RenderMesh()
		{
			throw new NotImplementedException();
		}

		public void CreateEffectVariables()
		{
			throw new NotImplementedException();
		}

		public void DestroyEffectVariables()
		{
			throw new NotImplementedException();
		}

		public void SetupEffectVariables(System.Common.Matrix pView, System.Common.Matrix pProj, System.Common.Light[] pLight)
		{
			throw new NotImplementedException();
		}

		public void CreateMaterials()
		{
			throw new NotImplementedException();
		}

		public void DestroyMaterials()
		{
			throw new NotImplementedException();
		}

		public uint GetNumMaterials()
		{
			throw new NotImplementedException();
		}

		public Material[] GetMaterial(uint n)
		{
			throw new NotImplementedException();
		}

		public VertexBuffer VertexBuffer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IndexBuffer IndexBuffer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public uint StrideSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		#endregion


	}
}


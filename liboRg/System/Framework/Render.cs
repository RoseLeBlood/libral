//
//  Render.cs
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
	public class Render : Handle
	{
		protected Game		m_pGame;
		protected Matrix	m_World;

		public virtual bool Visibale { get; set; }
		public virtual bool Selected  { get; set; }
		public virtual Matrix WorldMatrix { get { return m_World; } set { m_World = value; } }
	
		public Render(Game pGame, Matrix mWorld, string strName = "Render")
			: base(strName)
		{
			m_pGame = pGame;
			m_World = mWorld;
		}
		public virtual bool Create() 
		{ 
			return false; 
		}
		public virtual void Destroy() 
		{ 
		}
		public virtual bool Draw(uint drawOrder, Matrix pView, Matrix pProj, Light[] pLights)
		{
			return true; 
		}

		public virtual bool Update(float fTime, float fRunTime)
		{ 
			return true; 
		}
			
		public virtual float GetBoundingsphereRadius()		
		{ 
			return 1.0f; 
		}
		public virtual bool Intersects(Vector3 pRayPos,
			Vector3 pRayDir, float pDist) 
		{ 
			return false; 
		}

	}
}


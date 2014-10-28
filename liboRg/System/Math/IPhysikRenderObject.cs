//
//  IPhysikRenderObject.cs
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
	public interface IPhysikRenderObject
	{
		bool Visible { get; set; }
		bool Selected { get; set; }

		float BoundingSphereRadius { get; set; }

		Matrix WorldMatrix { get; set; }

		bool Create();
		void Destroy();

		bool Render(uint drawOrder, Matrix pView, Matrix pProj, Light pLight);
		bool Update(float fTime, float fRunTime);

		bool Intersects(Vector3 pRayPos, Vector3 pRayDir, float fDist );

	}
}


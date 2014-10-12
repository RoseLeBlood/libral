//
//  Ray.cs
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
	public struct Ray : IEquatable<Ray>
	{
		private Vector3 m_vPosition;
		private Vector3	m_vDirection;

		public Vector3 Position { get { return m_vPosition; } set { m_vPosition = value; } } 
		public Vector3 Direction { get { return m_vDirection; } set { m_vDirection= value; } }  

		public Ray (Vector3 position, Vector3 direction)
		{
			m_vPosition = position;
			m_vDirection = direction;
		}
			
		public bool Equals (Ray other)
		{
			return other == this;
		}

		public override bool Equals (object obj)
		{
			return obj is Ray && ((Ray)obj) == this;
		}

		public override int GetHashCode ()
		{
			return Position.GetHashCode () ^ Direction.GetHashCode ();
		}

		public static bool operator == (Ray a, Ray b)
		{
			return a.Position == b.Position && a.Direction == b.Direction;
		}

		public static bool operator != (Ray a, Ray b)
		{
			return a.Position != b.Position || a.Direction != b.Direction;
		}


		public override string ToString ()
		{
			return string.Format ("Ray: Position:{0} Direction:{1}", Position, Direction);

		}
	}
}


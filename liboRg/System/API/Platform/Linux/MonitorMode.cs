//
//  MonitorMode.cs
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

namespace System.API.Platform.Linux
{
	[Serializable]
	public class MonitorMode
	{
		private Size m_sSize;
		private double  m_iRate;

		public Size Size { get { return m_sSize; } }
		public double Rate { get { return m_iRate; } }


		public MonitorMode(Size size, int iRate)
		{
			m_sSize = size;
			m_iRate = iRate;
		}
		internal MonitorMode(Size size, string ratestring)
		{
			m_iRate = double.Parse(ratestring.Replace("*", "").Replace("+", "").Replace(".", ","));
			m_sSize = size;
		}
		public override string ToString()
		{
			return string.Format("{0}@{1}", Size, Rate);
		}
	}
}


//
//  Program.cs
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
using System.API.OpenCL;

namespace SampleOpenCL
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init("");
			Platforms platforms = new Platforms();
			foreach (var platform in platforms)
			{
				Console.WriteLine(platform);
				foreach (var device in platform.Devices)
				{
					Console.WriteLine("\t" + device.ToString().Replace("\n", "\n\t"));
				}
			}
		}
	}
}

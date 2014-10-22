//
//  Utils.cs
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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection.Emit;

namespace X11
{
	public static class Utils
	{
		public static string CallProgram(string prg, string args)
		{
			Process compiler = new Process();
			compiler.StartInfo.FileName = prg;
			compiler.StartInfo.Arguments = args;
			compiler.StartInfo.UseShellExecute = false;
			compiler.StartInfo.RedirectStandardOutput = true;
			compiler.Start();
			string ldsoutput = compiler.StandardOutput.ReadToEnd();
			compiler.WaitForExit();

			return ldsoutput;
		}

		/// <summary>
		/// Function: Strchr
		/// Returns the first occurrence of Character to be located 
		/// in string or null otherwise
		/// </summary>
		/// <param name="originalString"></param>
		/// <param name="charToSearch"></param>
		/// <returns></returns>
		public static int? strchr(string originalString,char charToSearch)
		{
			int? found = originalString.IndexOf(charToSearch);
			return found > -1 ? found : null;
		}
	}
}


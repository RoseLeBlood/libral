//
//  Kernel.cs
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
using System.Runtime.InteropServices;

namespace System.API.OpenCL
{
	public class Kernel : OpenCLHandle
	{
		protected Program m_pProgram;
		private readonly int intPtrSize;

		public Kernel(string strProgramName, Program pProgram )
			: base(pProgram.Name + "_" + strProgramName)
		{
			intPtrSize = Marshal.SizeOf(typeof(IntPtr));

			m_pProgram = pProgram;
			m_pHandle  = cl.clCreateKernel(m_pProgram.RawHandle, strProgramName, out m_iErrorCode);

			Register(true);
		}

		public void SetArgumente(int iIndex, Buffer buffer)
		{
			cl.clSetKernelArg(RawHandle, (uint)iIndex, new IntPtr(intPtrSize), buffer.RawHandle);
		}
	}
}


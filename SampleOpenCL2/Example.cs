//
//  Example.cs
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
using System.Runtime.InteropServices;
using System.API.OpenCL;
using System.Framework.OpenCL;
using Microsoft.Build.Framework;

namespace SampleOpenCL2
{
  public class Example
  {
		const int WORKGROUP_SIZE = 100;
		CommandQueue m_pCommandQueue;

		System.API.OpenCL.Program	 m_pProgram;
		System.API.OpenCL.Buffer	m_a, m_b, m_c;
		System.API.OpenCL.Kernel	m_pKernel;

		public Example(string path)
		{
			OpenCLEnvironment.SetupSingleDevice("Advanced Micro Devices", OpenCLDeviceTyp.Cpu);
			m_pCommandQueue = OpenCLEnvironment.CreateCommandQueue();
			m_pProgram = OpenCLEnvironment.CreateProgramFromSource(System.IO.File.ReadAllText(path), "testProgramm");
			m_pProgram.Build(0, null);
			m_pKernel = m_pProgram.CreateKernel("add");
		}

   		 public void popCorn()
		{
			float[] a = new float[WORKGROUP_SIZE];
			float[] b = new float[WORKGROUP_SIZE];

			for (int i = 0; i < WORKGROUP_SIZE; i++)
			{
				a[i] = 1.0f * i;
				b[i] = 1.0f * i;
			}
			m_a = OpenCLEnvironment.CreateBuffer("bufferA", BufferFlags.ReadOnly | BufferFlags.CopyHostPtr, (sizeof(float) * WORKGROUP_SIZE), a);
			m_b = OpenCLEnvironment.CreateBuffer("bufferB", BufferFlags.ReadOnly, (sizeof(float) * WORKGROUP_SIZE));
			m_c = OpenCLEnvironment.CreateBuffer("bufferC", BufferFlags.WriteOnly, (sizeof(float) * WORKGROUP_SIZE));

			Events ev = m_pCommandQueue.EnqueueWriteBuffer(m_b, true, 0, (sizeof(float) * WORKGROUP_SIZE), b);
			ev.ReleaseEvents();

			m_pKernel.SetArgumente(0, m_a);
			m_pKernel.SetArgumente(1, m_b);
			m_pKernel.SetArgumente(2, m_c);

			m_pCommandQueue.Finish();
		}
    public void runKernel()
		{
			IntPtr ev;
			cl.clEnqueueNDRangeKernel(m_pCommandQueue[0], m_pKernel.RawHandle, 1, null, new IntPtr[]{ (IntPtr)WORKGROUP_SIZE }, null, 0, null, out ev);
			cl.clReleaseEvent(ev);

			m_pCommandQueue.Finish();

			float[] cl_done = new float[10];

			cl.clEnqueueReadBuffer(m_pCommandQueue[0], m_c, true, (IntPtr)0, (IntPtr)(sizeof(float) * 10), 
				cl_done, 0, null, out ev);

			cl.clReleaseEvent(ev);

			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine("c_done[{0}] = {1}", i, cl_done[i]);
			}
		}
	}
}


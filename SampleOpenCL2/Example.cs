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
using liboRg.OpenCL;
using System.Runtime.InteropServices;

namespace SampleOpenCL2
{
  public class Example
  {
    clDevice	m_pDevice;
    clContext 	m_pContext;

    IntPtr	m_pCommandQueue;
    clProgram	m_pProgram;

    IntPtr	m_a, m_b, m_c;
    IntPtr	m_pKernel;

    int[] 	workGroupSize = new int[1];

    public Example()
    {
      clPlatforms platforms = new clPlatforms();
      foreach (var platform in platforms)
      {
	if (platform.HaveDevices)
	{
	  m_pDevice = platform.Devices[0];
	  break;
	}
      }

      if (m_pDevice == null)
	      throw new System.Exception("No OpenCL Device found ");

      m_pContext = m_pDevice.CreateContext();

      uint errorCode = 0;
      m_pCommandQueue = cl.clCreateCommandQueue(m_pContext.RawHandle, m_pDevice.RawHandle, 0, out errorCode);
    }
    public void LoadProgram(string path)
    {
      m_pProgram =  m_pContext.CreateProgramFromSource(System.IO.File.ReadAllText(path),"testProgramm");
      m_pProgram.Build(m_pDevice, null);
    }
    public void popCorn()
    {
      uint errorCode = 0;
      m_pKernel = cl.clCreateKernel(m_pProgram.RawHandle, "add", out errorCode);

      float[] a = new float[10];
      float[] b = new float[10];

      for (int i = 0; i < 10; i++)
      {
	a[i] = 1.0f * i;
	b[i] = 1.0f * i;
      }
		      
      using (var xa = a.Pin())
      {
	m_a = cl.clCreateBuffer(m_pContext.RawHandle, (uint)(CL.MEM_READ_ONLY | CL.MEM_COPY_HOST_PTR),
		(IntPtr)(sizeof(float) * 10), xa, out errorCode);

	m_b = cl.clCreateBuffer(m_pContext.RawHandle, (uint)(CL.MEM_READ_ONLY),
		(IntPtr)(sizeof(float) * 10), IntPtr.Zero, out errorCode);
	m_c = cl.clCreateBuffer(m_pContext.RawHandle, (uint)(CL.MEM_WRITE_ONLY),
		(IntPtr)(sizeof(float) * 10), IntPtr.Zero, out errorCode);
      }
      IntPtr ev;

      using (var xb = b.Pin())
      {
	cl.clEnqueueWriteBuffer(m_pCommandQueue, m_b, true, (IntPtr)0, (IntPtr)(sizeof(float) * 10),
		xb, 0, null, out ev);
      }
      cl.clReleaseEvent(ev);

      int intPtrSize = 0;
      intPtrSize = Marshal.SizeOf(typeof(IntPtr));

      CL err  = (CL)cl.clSetKernelArg(m_pKernel, 0, new IntPtr(intPtrSize), m_a);
	err  = (CL)cl.clSetKernelArg(m_pKernel, 1, new IntPtr(intPtrSize), m_b);
	err  = (CL)cl.clSetKernelArg(m_pKernel, 2, new IntPtr(intPtrSize), m_c);

      cl.clFinish(m_pCommandQueue);
      workGroupSize[0] = 10;
    }
    public void runKernel()
    {
      IntPtr ev;
      cl.clEnqueueNDRangeKernel(m_pCommandQueue, m_pKernel, 1, null, new IntPtr[]{ (IntPtr)workGroupSize[0] } , null, 0, null, out ev);
      cl.clReleaseEvent(ev);

      cl.clFinish(m_pCommandQueue);

      float[] cl_done = new float[10];

      cl.clEnqueueReadBuffer(m_pCommandQueue, m_c, true, (IntPtr)0, (IntPtr)(sizeof(float) * 10), 
	      cl_done, 0, null, out ev);

      cl.clReleaseEvent(ev);

      for (int i = 0; i < 10; i++)
      {
	Console.WriteLine("c_done[{0}] = {1}", i, cl_done[i]);
      }
    }
  }
}


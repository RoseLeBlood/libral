//
//  OpenCLSytem.cs
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
using CL = System.API.OpenCL;
using System.Common;
using System.Framework.OpenCL;

namespace SamplePartical
{
	public class OpenCLSytem
	{
		//std::vector<cl::Memory> cl_vbos; //0: position vbo, 1: color vbo
		CL.Buffer cl_velocities; 	//particle velocities
		CL.Buffer cl_pos_gen; 		//want to have the start points for reseting particles
		CL.Buffer cl_vel_gen; 		//want to have the start velocities for reseting particles
		int p_vbo; 					//position vbo
		int c_vbo; 					//colors vbo
		int num; 					//the number of particles
		uint array_size; 			//the size of our arrays num * sizeof(Vec4)

		int deviceUsed;
		CL.Devices devices;
		CL.Context context;
		CL.CommandQueue m_pCommandQueue;
		CL.Program m_pProgram;
		CL.Kernel m_pKernel;

		CL.Event xevent;

		public OpenCLSytem(string kernel_source)
		{
			OpenCLEnvironment.SetupSingleDevice("*A*", CL.OpenCLDeviceTyp.Cpu);
			m_pCommandQueue = OpenCLEnvironment.CreateCommandQueue();
			m_pProgram = OpenCLEnvironment.CreateProgramFromSource(kernel_source, "testProgramm");
			m_pProgram.Build(0, null);
			m_pKernel = m_pProgram.CreateKernel("add");
		}

		public void loadProgram(string kernel_source)
		{

		}
		//setup the data for the kernel
		public void loadData(Vector4[] pos, Vector4[]  vel, Color[]  color)
		{

		}
		//these are implemented in part1.cpp (in the future we will make these more general)
		public void popCorn()
		{

		}
		//execute the kernel
		public void runKernel()
		{

		}
	}
}


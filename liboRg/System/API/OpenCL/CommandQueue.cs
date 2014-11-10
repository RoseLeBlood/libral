//
//  CommandQueue.cs
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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.API.Platform.Linux;

namespace System.API.OpenCL
{
	public class CommandQueue : UnmanagedHandles
	{
		public CommandQueue(string strName)
			: base(strName)
		{
			Register(true);
		}

		#region EnqueueWriteBuffer

	
		public Events EnqueueWriteBuffer(System.API.OpenCL.Buffer cl_buffer, bool blockingWrite,
			int offsetInBytes, int lengthInBytes, Object data, uint numEventsInWaitList = 0,
			IntPtr[] eventWaitList = null)
		{
			IntPtr cl_event = IntPtr.Zero;
			Event[] _event = new Event[Count];

			for (int i = 0; i < Count; i++)
			{
				using (var xb = data.Pin())
				{
							cl.clEnqueueWriteBuffer(this[i], cl_buffer.RawHandle, 
						blockingWrite, (IntPtr)offsetInBytes, (IntPtr)lengthInBytes, xb, numEventsInWaitList,
						eventWaitList, out cl_event);
					_event[i] = new Event(m_strName, cl_event);
				}
			}
			return new Events(m_strName, _event);
		}


		#endregion

		public void Finish()
		{
			for (int i = 0; i < Count; i++)
				{
					cl.clFinish(this[i]);
				}
		}
	}
}


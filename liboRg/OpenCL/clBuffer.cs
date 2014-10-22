//
//  clBuffer.cs
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

namespace liboRg.OpenCL
{
	[Flags()]
	public enum BufferFlags : uint
	{
		ReadWrite = CL.MEM_READ_WRITE,
		WriteOnly = CL.MEM_WRITE_ONLY,
		ReadOnly = CL.MEM_READ_ONLY,
		UseHostPtr = CL.MEM_USE_HOST_PTR,
		AllocHostPtr = CL.MEM_ALLOC_HOST_PTR,
		CopyHostPtr = CL.MEM_COPY_HOST_PTR,
		Reserved = (1 << 6),

		HostWriteOnly = CL.MEM_HOST_WRITE_ONLY,
		HostReadOnly = CL.MEM_HOST_READ_ONLY,
		HostNoAccess = CL.MEM_HOST_NO_ACCESS,

	}
}


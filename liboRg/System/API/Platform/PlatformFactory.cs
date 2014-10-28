//
//  PlatformFactory.cs
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
using liboRg.Context;
using System.Runtime.InteropServices;
using System.API.OpenCL;

#if LINUX
using System.API.Platform.Linux.Widgets;
using System.API.Platform.Linux;
#endif

namespace System.API.Platform
{
	public static class PlatformFactory
	{
		internal static IGLNativeContext GetContext(IWindow handle, GameContextConfig pConfig)
		{
			#if LINUX
			return new glxNativeContext(handle, pConfig);
			#endif
		}
		internal static System.Delegate GetProcAdress<T>(string name)
		{
			#if LINUX
			var ptr = glxNativeContext.glXGetProcAddressARB(name);
			return (ptr == IntPtr.Zero ? null : Marshal.GetDelegateForFunctionPointer(ptr, typeof(T)));
			#endif
		}
		internal static System.Delegate GetProcAdressOpenCL<T>(string name)
		{
			var ptr = cl.clGetExtensionFunctionAddress(name);
			return (ptr == IntPtr.Zero ? null : Marshal.GetDelegateForFunctionPointer(ptr, typeof(T)));
		}
	}
}


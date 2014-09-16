//
//  Color.cs
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

namespace X11.Widgets
{
	public class NULLXOpenDisplayException : Exception
	{
		public NULLXOpenDisplayException(string File, int iLine, string Function)
			: base(File, iLine, Function)
		{
		}
	}
	public class GetenvXOpenDisplayException : Exception
	{
		public GetenvXOpenDisplayException(string File, int iLine, string Function)
			: base(File, iLine, Function)
		{
		}
	}
	public class OpenDisplayException :Exception
	{
		public OpenDisplayException(string File, int iLine, string Function)
			: base(File, iLine, Function)
		{
		}
	}
	public class NULLPtrConnectionException :Exception
	{
		public NULLPtrConnectionException(string File, int iLine, string Function)
			: base(File, iLine, Function)
		{
		}
	}

	public class Display : XHandle
	{
		private Screen	m_pScreen;

		public virtual int ScreensCount  { get { return m_iScreensCount; } }

		public virtual string ServerName { get { return X11._internal.Lib.XDisplayString(m_pHandle); } }
		public virtual string ServerVendorName { get { return X11._internal.Lib.XServerVendor(m_pHandle); } }
		public virtual int ServerVendorRelease { get { return (int)X11._internal.Lib.XVendorRelease(m_pHandle); } }
		public virtual int ProtocolVersion { get { return (int)X11._internal.Lib.XProtocolVersion(m_pHandle); } }
		public virtual int ProtocolRevision { get { return (int)X11._internal.Lib.XProtocolRevision(m_pHandle); } }
		public virtual int ConnectionNumber { get { return (int)X11._internal.Lib.XConnectionNumber(m_pHandle); } } 

		public Screen Screen { get { return m_pScreen; } }


		public Display()
			: base(System.Environment.GetEnvironmentVariable("DISPLAY"))
		{
			m_pHandle = X11._internal.Lib.XOpenDisplay("");
			if (m_pHandle == IntPtr.Zero)
			{
				throw new NULLXOpenDisplayException("ServerConnection.cs", 78, "Display::Display()");
			}
			m_iScreensCount = X11._internal.Lib.XScreenCount(m_pHandle);
			m_pScreen = new Screen(X11._internal.Lib.XDefaultScreenOfDisplay(m_pHandle), this);
			Register();

			#if DEBUG
			Console.WriteLine("ServerConnection::ServerConnection Open Display {0}", ServerName);
			Console.WriteLine("Server: " + ServerVendorName + " " + ServerVendorRelease);
			Console.WriteLine("Protocol: " + ProtocolVersion + "-" + ProtocolRevision);
			Console.WriteLine("ConnectionNumber: " + ConnectionNumber);
			#endif
		}
		public Display(string DisplayName)
			: base(DisplayName)
		{
			m_pHandle = X11._internal.Lib.XOpenDisplay(DisplayName);
			if (m_pHandle == IntPtr.Zero)
			{
					throw new NULLXOpenDisplayException("ServerConnection.cs", 86, "Display::Display(string DisplayName)");
			}
			m_iScreensCount = X11._internal.Lib.XScreenCount(RawHandle);
			m_pScreen = new Screen(X11._internal.Lib.XDefaultScreenOfDisplay(m_pHandle), this);
			Register();

			#if DEBUG
			Console.WriteLine("Display: Open {0}", ServerName);
			Console.WriteLine("Server: " + ServerVendorName + " " + ServerVendorRelease);
			Console.WriteLine("Protocol: " + ProtocolVersion + "-" + ProtocolRevision);
			Console.WriteLine("ConnectionNumber: " + ConnectionNumber);
			#endif
		}
		internal Display(IntPtr pDisplay)
		{
			m_pHandle = pDisplay;
			if (m_pHandle == IntPtr.Zero)
			{
				throw new NULLXOpenDisplayException("ServerConnection.cs", 106, "Display::Display(IntPtr)");
			}
			m_iScreensCount = X11._internal.Lib.XScreenCount(RawHandle);
		}
		public virtual bool IsScreenNumberValid( int iScreenNumber)
		{
			if (0 == m_iScreensCount)
			{
				return false;
			}
			if (0 > iScreenNumber)
			{
				return false;
			}
			if (iScreenNumber >= m_iScreensCount)
			{
				return false;
			}
			return true;
		}
		public int Sync (bool discard)
		{
			return X11._internal.Lib.XSync(m_pHandle, Convert.ToInt32(discard));
		}
		public int Grab()
		{
			return X11._internal.Lib.XUngrabServer(m_pHandle);
		}
		public int UnGrab()
		{
			return X11._internal.Lib.XGrabServer(m_pHandle);
		}
		public void Close()
		{
			X11._internal.Lib.XCloseDisplay(RawHandle);
		}
		
		protected int m_iScreensCount;
		protected int m_iDefaultScreenNumber;
	}


	
}


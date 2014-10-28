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

namespace System.API.Platform.Linux.Widgets
{
	public class ScreenNumberOutOfBoundsException : Exception
	{
		public ScreenNumberOutOfBoundsException(string File, int iLine, string Function)
			: base(File, iLine, Function)
		{
		}
	};
	public class NULLPtrScreenException : Exception 
	{

		public NULLPtrScreenException(string File, int iLine, string Function)
			: base(File, iLine, Function)
		{
		}
	};

	public class Screen : XHandle, IScreen
	{
		public int 		ScreenNumber { get { return m_iScreenNumber; } }
		public int 		Width { get { return m_iScreenWidth; } }
		public int 		Height { get { return m_iScreenHeight; } }

		public int 		DefaultColormap
		{
			get
			{
				return Lib.XDefaultColormapOfScreen (m_pHandle);
			}
		}
		public Screen(string name, string display) : this(0, name,Application.Current.GetHandle<Display>(display))
		{
		}

		public Screen(string name, Display display) : this(0, name,display)
		{
		}
		public Screen(int iScreenNumber, string name, Display display)
			: base(name + iScreenNumber)
		{
			if (display == null)
				throw new NULLPtrConnectionException("Screen.cs", 56, "Screen::Screen()");

			if (!display.IsScreenNumberValid(iScreenNumber))
			{
					throw new ScreenNumberOutOfBoundsException("Screen.cs", 60, "Screen::Screen(int iScreenNumber)");
			}
			m_pHandle = Lib.XScreenOfDisplay(display.RawHandle, 
														  (TInt)iScreenNumber);

			m_iScreenNumber = iScreenNumber;
			m_iScreenWidth = (int)Lib.XDisplayWidth(display.RawHandle, 
				(TInt)m_iScreenNumber);
			m_iScreenHeight = (int)Lib.XDisplayHeight(display.RawHandle, 
				(TInt)m_iScreenNumber);

			Register();

			#if DEBUG
			Console.WriteLine("Open Screen: {0}\n\tSize: {1}x{2}", ScreenNumber, Width, Height);
			#endif
		}
		public Screen(IntPtr screen, Display display) 
			: base("SCREEN")
		{
			if (screen == IntPtr.Zero)
				throw new NULLPtrConnectionException("Screen.cs", 96, "Screen::Screen(IntPtr )");

			m_pHandle = screen;
			m_iScreenNumber = Lib.XScreenNumberOfScreen(m_pHandle);

			m_iScreenWidth = (int)Lib.XDisplayWidth(display.RawHandle, 
				(TInt)m_iScreenNumber);
			m_iScreenHeight = (int)Lib.XDisplayHeight(display.RawHandle, 
				(TInt)m_iScreenNumber);

			m_strName = m_iScreenNumber.ToString();

			Register(true);
		}
		protected int m_iScreenNumber;
		protected int m_iScreenWidth;
		protected int m_iScreenHeight;
	}


	
}


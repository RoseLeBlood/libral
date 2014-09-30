//
//  Pixmap.cs
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
using X11._internal;

namespace X11.Widgets
{
	class NULLPtrFilePathException : Exception
	{
		public NULLPtrFilePathException(string File, int iLine, string Function)
			: base(File, iLine, Function)
		{
		}
	};

	class XpmReadFileToPixmapException : Exception 
	{
		public XpmReadFileToPixmapException(string File, int iLine, string Function)
			: base(File, iLine, Function)
		{
		}
	};
	public class Pixmap : XHandle
	{
		public virtual string Path 	 { get { return m_PixmapPath; } }
		public virtual TSize Size 	 { get { return m_Size; } }
		public virtual int Mask 	 { get { return m_Mask; } }

		internal Pixmap(string PixmapPath, string name, string displayName, string screenName)
			: base(name)
		{
			Display display = Application.Current.GetHandle<Display>(displayName);
			Screen screen = Application.Current.GetHandle<Screen>(screenName);

			if (display == null)
				throw new NULLPtrConnectionException("Pixmap.cs", 54, "Pixmap::Pixmap()");
			if (screen == null)
				throw new NULLPtrScreenException("Pixmap.cs", 56, "Pixmap::Pixmap()");
			if (string.Empty == PixmapPath)
				throw new NULLPtrFilePathException("Pixmap.cs", 58, "Pixmap::Pixmap()");

			Xpm.XpmAttributes xpma = new Xpm.XpmAttributes();
			m_pDisplay = display;
			xpma.valuemask = 0;
			IntPtr pxmap;

			if (Xpm.XpmReadFileToPixmap(m_pDisplay.RawHandle,
				Lib.XRootWindow(m_pDisplay.RawHandle, (TInt)screen.ScreenNumber),
				PixmapPath, out pxmap, out m_Mask, ref xpma) != 0)
			{
					throw new XpmReadFileToPixmapException("Pixmap.cs", 66, "Pixmap::Pixmap()");
			}
			m_PixmapPath =  PixmapPath;
			m_Size.Height = xpma.height;
			m_Size.Width = xpma.width;
			m_pHandle = pxmap;
			Xpm.XpmFreeAttributes(ref xpma);

			Register();
		}
		protected override void CleanUpUnManagedResources()
		{
			X11._internal.Lib.XFreePixmap(m_pDisplay.RawHandle, m_pHandle);
			m_pHandle = IntPtr.Zero;
		}

		protected Display m_pDisplay;
		protected int m_Mask;
		protected string m_PixmapPath;
		protected TSize m_Size;
	}
}


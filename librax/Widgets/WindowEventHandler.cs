//
//  WindowEventHandler.cs
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
using System.Reflection;

namespace X11.Widgets
{
	public class WindowEventHandler : Handle, IEventHandler
	{
		public static WindowEventHandler BaseEvent = new WindowEventHandler(); 

		public WindowEventHandler() : base("WindowEventHandler")
		{
		}
		public virtual bool WndProc(XEvent xevent, BaseWindow wnd)
		{
			if (xevent.type == XEventName.ClientMessage)
			{
					IntPtr protocolsAtom = Lib.XInternAtom(wnd.Display.RawHandle, "WM_PROTOCOLS", false);
					IntPtr deleteWindowAtom = Lib.XInternAtom(wnd.Display.RawHandle, "WM_DELETE_WINDOW", false);

				if (xevent.ClientMessageEvent.message_type == protocolsAtom &&
				    xevent.ClientMessageEvent.ptr1 == deleteWindowAtom)
				{
					CallHandler(xevent.type.ToString(), new XEventArgs(xevent), wnd);
					return false;
				}
			}
			else if (xevent.type == XEventName.FocusOut)
			{
					//m_bHaveFocus = false;
			}
			else if (xevent.type == XEventName.FocusIn)
			{
					//m_bHaveFocus = true;
			}
			else if (xevent.type == XEventName.ConfigureNotify)
			{
				if (wnd.Rectangle.Width != (int)xevent.ConfigureEvent.width ||
				    wnd.Rectangle.Height != (int)xevent.ConfigureEvent.height)
				{
					wnd.Rectangle.Width = (int)xevent.ConfigureEvent.width;
					wnd.Rectangle.Height = (int)xevent.ConfigureEvent.height;
					
					return CallHandler("Resize", new XEventArgs(xevent), wnd);
				}
				else if ((int)xevent.ConfigureEvent.x != wnd.Rectangle.X || 
						(int)xevent.ConfigureEvent.y != wnd.Rectangle.Y)
				{
					wnd.Rectangle.X = (int)xevent.ConfigureEvent.x;
					wnd.Rectangle.Y = (int)xevent.ConfigureEvent.y;

					return CallHandler("Move", new XEventArgs(xevent), wnd);
				}

			}
			return CallHandler(xevent.type.ToString(), new XEventArgs(xevent), wnd);
		}
		public virtual bool CallHandler(string eventName, XEventArgs args, BaseWindow wnd)
		{
			if (wnd.CallHandlers.ContainsKey(eventName))
			{
				Type calcType = wnd.GetType();
				return (bool)calcType.InvokeMember(wnd.CallHandlers[eventName],
					BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
						null, wnd, new object[] { wnd, args });
			}

			return true;
		}
	}
}


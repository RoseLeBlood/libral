//
//  BaseGameWindow.cs
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
using X11.Widgets;
using System.Common;
using X11;

namespace liboRg
{
	public class BaseGameWindow : SimpleWindow
	{
		public BaseGameWindow(string strDisplay, Game pGame)
			: base(strDisplay, "GameWindow", Colors.SteelBlue, new 	libral.Rectangle(0,0,320,320), 
				new GameWindowHandler(), "Game Window")
		{
			Namespace = "test";
			ClassName = "TestWindow";
			Created = "BaseGameWindow_Created";
			KeyPress = "BaseGameWindow_KeyPressed";
			Resize = "BaseGameWindow_Resize";
			UserEvents = "BaseGameWindow_UserEvents";
		}


		protected bool BaseGameWindow_UserEvents(Object sender, XEventArgs args)
		{
			// Game Draw logic
			return true;
		}





		protected bool BaseGameWindow_Created(Object sender, XEventArgs args)
		{
			return true;
		}
		protected bool BaseGameWindow_KeyPressed(Object sender, XEventArgs args)
		{
			if (((Keys)(args.Event.KeyEvent.keycode)) == Keys.Escape)
			{
				Console.WriteLine("Naja das war der Escape cheat bye bye ...");
				return false;
			}
			return true;
		}
		protected bool BaseGameWindow_Resize(Object sender, XEventArgs args)
		{
			return true;
		}
	}
}


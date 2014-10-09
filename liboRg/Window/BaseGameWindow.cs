﻿//
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
using System.Common;
using X11;
using X11.Widgets;
using X11.Widgets.Event;
using X11._internal;
using liboRg.Context;
using liboRg.Input;

namespace liboRg.Window
{
	public class BaseGameWindow : SimpleWindow
	{
		private Game m_pGame;
		private GameContext m_pIContext;

		public GameContext Context
		{
			get
			{
				return m_pIContext;
			}
		}

		public BaseGameWindow(Game pGame, string title, WindowStyle style )
			: base(pGame.m_strDisplay, "GameWindow", Colors.SteelBlue, pGame.ContextConfig.Resolution.Size, 
				title)
		{
			m_pIContext = null;
			m_pGame = pGame;
			Namespace = "liboRg";
			ClassName = "BaseGameWindow";

			if (style == (style & WindowStyle.NoResize))
				Resizeable = false;
			else if(style == (style & WindowStyle.Resize))
				Resizeable = true;

			this.EventMask = EventMask.FocusChangeMask | EventMask.ButtonPressMask | EventMask.ButtonReleaseMask |
				EventMask.ButtonMotionMask | EventMask.PointerMotionMask | EventMask.KeyPressMask | 
				EventMask.KeyReleaseMask | EventMask.StructureNotifyMask | EventMask.EnterWindowMask | 
				EventMask.LeaveWindowMask | EventMask.All;

			this.Created += BaseGameWindow_Created;
			KeyPress += BaseGameWindow_KeyPressed;
			KeyRelease += BaseGameWindow_KeyRelease;
			Resize += BaseGameWindow_Resize;
			UserEvent += BaseGameWindow_UserEvents;
			Move += BaseGameWindow_Move;
		}


		protected void BaseGameWindow_UserEvents(Object sender, XEventArgs args)
		{
			m_pGame.drawing();
		}

		protected void BaseGameWindow_Created(Object sender, XEventArgs args)
		{
			m_pIContext = new X11Context(this, m_pGame.ContextConfig);

			m_pGame.Create();
		}
		protected void BaseGameWindow_KeyPressed(Object sender, XKeyEventArgs args)
		{
			m_pGame.InternalKeyPress(args.Key);
		}
		protected void BaseGameWindow_KeyRelease(Object sender, XKeyEventArgs args)
		{
			m_pGame.InternalKeyRelease(args.Key);
		}
		protected void BaseGameWindow_Resize(Object sender, XEventArgs args)
		{
			m_pGame.OnResize(this.Rectangle);
		}
		protected void BaseGameWindow_Move(Object sender, XEventArgs args)
		{
			m_pGame.OnMove(this.Rectangle);
		}
	}
}


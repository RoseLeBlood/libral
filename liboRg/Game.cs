//
//  Game.cs
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
using libral;
using X11.Widgets;
using liboRg.Input;
using liboRg.Window;


namespace liboRg
{
	public class Game
	{
		private BaseGameWindow m_pGameWindow;
		private Keyboard	   m_pKeyboard;

		internal string		   m_strDisplay;

		public Display		   Display { get { return m_pGameWindow.Display; } }
		public Rectangle	   Bounds { get { return m_pGameWindow.Rectangle; } }
		public BaseGameWindow  Window { get { return m_pGameWindow; } }


		public Game(string strDisplay, GameResolution pResolution, string title, WindowStyle style)
		{
			m_strDisplay = strDisplay;
			m_pGameWindow = new BaseGameWindow(this, pResolution, title, style );
			m_pKeyboard = new Keyboard();

		}

		public void Init()
		{
			m_pGameWindow.Create();
		}

		public virtual void Create()
		{

		}
		public virtual void Destroy()
		{

		}
		protected virtual bool Move(InputState keyState)
		{
			if (keyState[X11.Keys.Escape])
				Application.Current.Exit();

			return true;
		}
		protected virtual bool Draw()
		{
			return true;
		}
		public virtual void OnResize(Rectangle newSize)
		{

		}

		public void OnMove(Rectangle position)
		{

		}

		internal bool drawing()
		{
			if (Move( (InputState)m_pKeyboard.GetState() ) == true)
				Draw();

			// SwappBuffer
			return true;
		}

		internal void InternalKeyPress(uint keycode)
		{
			m_pKeyboard.SetPress((X11.Keys)keycode);
		}
		internal void InternalKeyRelease(uint keycode)
		{
			m_pKeyboard.SetRelease((X11.Keys)keycode);
		}
	}
}


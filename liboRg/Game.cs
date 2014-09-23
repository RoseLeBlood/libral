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


namespace liboRg
{
	public class Game
	{
		private BaseGameWindow m_pGameWindow;
		internal string		   m_strDisplay;

		public Display		   Display { get { return m_pGameWindow.Display; } }
		public Rectangle	   Bounds { get { return m_pGameWindow.Rectangle; } }
		public BaseGameWindow  Window { get { return m_pGameWindow; } }


		public Game(string strDisplay, Size size, string title, WindowStyle style)
		{
			m_strDisplay = strDisplay;
			m_pGameWindow = new BaseGameWindow(this, size, title, style );
		}

		public void Init()
		{
			m_pGameWindow.Create();
		}

		protected virtual void Create()
		{

		}
		protected virtual void Destroy()
		{

		}
		protected virtual bool Move(/*Keyboard pKeyboard, Mouse pMouse, GamePad pGamePad */)
		{
			return true;
		}
		protected virtual bool Draw()
		{
			return true;
		}
		public virtual void OnResize(Rectangle newSize)
		{
			Console.Write("\rNew Size: {0}", newSize);
		}

		public void OnMove(Rectangle position)
		{
			Console.Write("\rNew Size: {0}", position);
		}

		internal bool drawing()
		{
			if (Move() == true)
				Draw();

			// SwappBuffer
			return true;
		}
	}
}


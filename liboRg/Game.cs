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

namespace liboRg
{
	public class Game
	{
		private Rectangle m_rectBounds;
		private BaseGameWindow m_pGameWindow;

		public Game(string strDisplay, string strGameTitle, int sizeX, int sizeY)
		{
			m_rectBounds = new Rectangle(0, 0, sizeX, sizeY);
			m_pGameWindow = new BaseGameWindow(strDisplay, this);
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
		protected virtual void OnResize(Rectangle newSize)
		{

		}
	}
}


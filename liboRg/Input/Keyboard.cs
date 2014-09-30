//
//  Keyboard.cs
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
using X11;

namespace liboRg.Input
{
	public class Keyboard : Handle, IInputDevice
	{
		private InputState	m_pState;

		public Keyboard() : base("STD_KEYBOARD")
		{
			m_pState = new InputState();
			Register();
		}

		public IState GetState()
		{
			return m_pState;
		}
		public bool IsKeyPress(Keys key)
		{
			return m_pState[key];
		}

		internal void SetPress(Keys key)
		{
			m_pState[key] = true;
		}
		internal void SetRelease(Keys key)
		{
			m_pState[key] = false;
		}
	}
}


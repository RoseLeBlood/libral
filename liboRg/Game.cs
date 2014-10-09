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
using X11.Widgets;
using liboRg.Input;
using liboRg.Window;
using liboRg.Context;
using System.Common;
using liboRg.OpenGL;


namespace liboRg
{
	public class Game
	{
		private BaseGameWindow m_pGameWindow;
		private Keyboard	   m_pKeyboard;
		private GameContextConfig m_pContextConfig;

		internal string		   m_strDisplay;

		public Display		   Display { get { return m_pGameWindow.Display; } }
		public Rectangle	   Bounds { get { return m_pGameWindow.Rectangle; } }
		public BaseGameWindow  Window { get { return m_pGameWindow; } }

		public Color ClearColor
		{
			get { return GameContext.ClearColor; }
			set { GameContext.ClearColor = value; }
		}

		public GameContextConfig ContextConfig
		{
			get { return m_pContextConfig; }
		}

		public bool DepthMask
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public bool VSync
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}
		public float Time
		{
			get { throw new NotImplementedException(); }
		}

		public GameContext GameContext
		{
			get { return Window.Context; }
		}


		public Game(string strDisplay, GameContextConfig pConfig, string title, WindowStyle style)
		{
			m_strDisplay = strDisplay;
			m_pContextConfig = pConfig;

			m_pGameWindow = new BaseGameWindow(this, title, style );
			m_pKeyboard = new Keyboard();

		}

		public void Init()
		{
			m_pGameWindow.Create();
		}

		public virtual void Create()
		{
			GameContext.CreateContext();
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
			gl.glClearColor ( 0, 0.5f, 1f, 1f );
			gl.glClear ( (int)GL.COLOR_BUFFER_BIT );

			return true;
		}
		public virtual void OnResize(Rectangle newSize)
		{
			newSize.X = newSize.Y = 0;
			GameContext.Viewport = newSize;
		}

		public void OnMove(Rectangle position)
		{
		}

		internal bool drawing()
		{
			bool ret = true;
			if (Move( (InputState)m_pKeyboard.GetState() ) == true)
				ret = Draw();
				
			GameContext.Swap();
			return ret;
		}

		internal void InternalKeyPress(X11.Keys keycode)
		{
			m_pKeyboard.SetPress(keycode);
		}
		internal void InternalKeyRelease(X11.Keys keycode)
		{
			m_pKeyboard.SetRelease(keycode);
		}
	}
}


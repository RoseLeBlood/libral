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
using liboRg.Window;
using liboRg.Context;
using System.Common;
using System.API.Platform;
using System.API.Platform.Linux;


namespace System
{
	public class Game
	{
		private IGameWindow m_pGameWindow;
		private GameContextConfig m_pContextConfig;
		private bool			  m_bDisableDraw;

		internal string		   m_strDisplay;

		public IDisplay		   Display { get { return m_pGameWindow.Display; } }
		public Rectangle	   Bounds { get { return m_pGameWindow.Rectangle; } }
		public IGameWindow     Window { get { return m_pGameWindow; } }

		internal bool DisableQue
		{
			get { return m_bDisableDraw; }
			set { m_bDisableDraw = value; }
		}
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
			get { return GameContext.DepthMask; }
			set { GameContext.DepthMask = value; }
		}

		public VSyncMode VSync
		{
			get { return GameContext.VSync; }
			set { GameContext.VSync = value; }
		}
		public double Time
		{
			get { return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds; }
		}

		public GameContext GameContext
		{
			get
			{ 
				return Window.Context;
			}
		}


		public Game(string strDisplay, GameContextConfig pConfig, string title, WindowStyle style)
		{
			m_strDisplay = strDisplay;
			m_pContextConfig = pConfig;

			m_pGameWindow = new BaseGameWindow(this, title, style );
			m_pGameWindow.Create();
		}
		public virtual void Create()
		{
			GameContext.CreateContext();
		}
		public virtual void Destroy()
		{

		}
		protected virtual bool Move()
		{
			if (Window.IsKeyDown(Keys.Escape))
				Application.Current.Exit();

			return true;
		}
		protected virtual bool Draw()
		{
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
			if (DisableQue)
				return true;

			bool ret = true;
			if (Move( ))
				ret = Draw();
				
			GameContext.Swap();
			return ret;
		}
	}
}


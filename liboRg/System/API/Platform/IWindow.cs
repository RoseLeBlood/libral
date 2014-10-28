//
//  IWindow.cs
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

namespace System.API.Platform
{
	public interface IWindow : IHandle
	{
		IntPtr RawHandle { get;  }

		string 				Name			{ get; }
		IDisplay			Display			{ get;  }

		Color				Background 		{ get; set; }
		Color				Border			{ get; set; }
		Rectangle			Rectangle		{ get;  }
		string				Title			{ get; set; }
		bool				IsFocusable		{ get;  }
		string				Icon			{ get; set; }
		bool 				ShowCursor		{ get; set; }
		int					ID				{ get;  }
		string				ClassName		{ get; set; }
		int					BorderWidth		{ get; set; }
		IWindow				Parent			{ get; set; }
		string 				Namespace		{ get; set; }


		bool Resizeable 		{ get; set; }
		bool IsOpen 			{ get; }

		void Create();
		void Destroy();

		void Show();
		void Hide();

		void EnableFullscreen( bool enabled, Size size);

		void UnregisterChild(int iId);
		void SetWindowOpacity(float value);
		void OnIdle();
		bool Event(Object xevent);

		int RegisterChild(IWindow pWindow);
	}
}


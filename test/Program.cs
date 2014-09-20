//#define CREATENEW
//
//  Program.cs
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
using System.Reflection;
using X11;
using X11.Widgets;

namespace test
{
	[Serializable]
	public class TestWindow : SimpleWindow
	{
		#if CREATENEW
		public TestWindow() : base(":0", "frmMainWindow", "#4682B4CC", "32,500,800,600",
			WindowEventHandler.BaseEvent, "Hallo SimpleWindow")
		{
			Namespace = "test";
			ClassName = "TestWindow";
			Created = "frmMainWindow_Created";
			KeyPress = "frmMainWindow_KeyPressed";
			Resize = "frmMainWindpw_Resize";
			Title = "Hallo LibX#";
		}
		#else
		public TestWindow() : base(":0")
		{
		}
		#endif
		protected bool frmMainWindow_Created(Object sender, XEventArgs args)
		{
			Console.Write(Background.Alpha);
			return true;
		}
		protected bool frmMainWindow_KeyPressed(Object sender, XEventArgs args)
		{
			if (((Keys)(args.Event.KeyEvent.keycode)) == Keys.Escape)
			{
				Console.WriteLine("Naja das war der Escape cheat bye bye ...");
				return false;
			}
			else if (((Keys)(args.Event.KeyEvent.keycode)) == Keys.Num_Add)
			{
				this.SetWindowOpacity(Background.Alpha + 0.01f);
				
			}
			else if (((Keys)(args.Event.KeyEvent.keycode)) == Keys.Num_Sub)
			{
				this.SetWindowOpacity(Background.Alpha - 0.01f);

			}

			return true;
		}
		protected bool frmMainWindpw_Resize(Object sender, XEventArgs args)
		{
			Console.WriteLine(Rectangle.ToString());
			return true;
		}

		public static void Main (string[] args)
		{
			Application.Init("test");
			Application.SaveAsXml(Application.Current);

			#if CREATENEW
			TestWindow wnd = new TestWindow();
			BaseWindow.SaveAsXml(wnd);
			#else
			var wnd = BaseWindow.OpenFromXml<TestWindow>("frmMainWindow");
			#endif
			wnd.Create();

			Application.Current.MainWindow = "frmMainWindow";
			Application.Run();


		}
	}
}

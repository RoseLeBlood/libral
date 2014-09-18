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
using X11._internal;
using X11;
using X11.Widgets;
using System.Common;
using System.Reflection;
using System.Xml.Serialization;
using libral;

namespace test
{
	[Serializable]
	[XmlRoot(ElementName = "Window")]
	public class TestWindow : SimpleWindow
	{
		public TestWindow() : base(":0", "frmMainWindow", Colors.SteelBlue, new Rectangle("10,320"),
			WindowEventHandler.BaseEvent)
		{
			Namespace = "test";
			ClassName = "TestWindow";
			Created = "frmMainWindow_Created";
			KeyPress = "frmMainWindow_KeyPressed";
			Title = "Hallo LibX#";
		}
		protected bool frmMainWindow_Created(Object sender, XEventArgs args)
		{
			Console.WriteLine("Window Created");
			return true;
		}
		protected bool frmMainWindow_KeyPressed(Object sender, XEventArgs args)
		{
			if (((Keys)(args.Event.KeyEvent.keycode)) == Keys.Escape)
			{
				Console.WriteLine("Naja das war der Escape cheat bye bye ...");
				return false;
			}
			return true;
		}

		public static void Main (string[] args)
		{
			Application.Init("test.MainClass");
			new Display(":0");

			TestWindow wnd = new TestWindow();
			wnd.Create();

			Application.Current.MainWindow = "frmMainWindow";
			Application.Run();

			Application.SaveAsXml(Application.Current);
			BaseWindow.SaveAsXml(wnd);
		}
	}
}

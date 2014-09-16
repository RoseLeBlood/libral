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

namespace test
{
	[Serializable]
	[XmlRoot(ElementName = "Window")]
	public class TestWindow : SimpleWindow
	{
		public TestWindow() : base()
		{
		}
		public TestWindow(string strDisplay, string name)
			: base(strDisplay, name, Colors.SteelBlue, new TRectangle(0,0,320,320))
		{
			Namespace = "test";
			ClassName = "TestWindow";
			Created = "frmMainWindow_Created";
			Title = "Hallo LibX#";
		}
		protected void frmMainWindow_Created(Object sender, XEventArgs args)
		{
			Console.WriteLine("Window Created");
		}
	}
	class MainClass
	{

		public static void Main (string[] args)
		{
			Application.Init("test.MainClass");
			new Display(string.Format(":{0}", "0"));

			TestWindow wnd = new TestWindow(":0", "frmMainWindow");
			wnd.Create();

			Application.Current.MainWindow = "frmMainWindow";
			Application.Run();

			Application.SaveAsXml(Application.Current);
			Window.SaveAsXml(wnd);
		}
	}
}

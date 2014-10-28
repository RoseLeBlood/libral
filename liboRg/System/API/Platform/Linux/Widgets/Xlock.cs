﻿//
//  Xlock.cs
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

namespace System.API.Platform.Linux.Widgets
{
	public class Xlock : XHandle
	{
		public Xlock(IDisplay display)
			: base("XLock Display", display.RawHandle)
		{
			Lib.XLockDisplay(m_pHandle);
		}
		protected override void CleanUpManagedResources()
		{
			Lib.XUnlockDisplay(m_pHandle);
		}
		protected override void CleanUpUnManagedResources()
		{
			Application.Current.UnRegisterHandle(Name);
		}
	}
}

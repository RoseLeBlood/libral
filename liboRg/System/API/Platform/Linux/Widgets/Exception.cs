//
//  Exception.cs
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
	public class Exception : System.Exception
	{
		public Exception() 
		{
			m_Description = "[Exception] ";
			m_iReturnValue = -1;
		}
		public Exception(string File, int iLine, string Function)
		{
			m_Description = string.Format("[Exception in at: {0}:{1}:{2}", File, iLine, Function);
			m_iReturnValue = -1;

		}
		public Exception(string File, int iLine, string Function, string Description)
		{
			m_Description = string.Format("[Exception in at: {0}:{1}:{2}\n{3}", File, iLine, Function, Description);
			m_iReturnValue = -1;
		}
		public Exception(string File, int iLine, string Function, string Description, int iReturnValue)
		{
			m_Description = string.Format("[Exception in at: {0}:{1}:{2}\n{3}", File, iLine, Function, Description);
			m_iReturnValue = iReturnValue;
		}
		public virtual string Description()
		{
			return m_Description;
		}
		public virtual int ReturnValue()
		{
			return m_iReturnValue;
		}
		public override string ToString()
		{
			return string.Format(string.Format("{0}:{1}", m_iReturnValue, m_Description));
		}

		protected string m_Description;
		protected int m_iReturnValue;
	}
}


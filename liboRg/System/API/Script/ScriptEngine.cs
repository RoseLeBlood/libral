//
//  ScriptEngine.cs
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
using System.IO;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;

using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using Microsoft.CSharp;

namespace System.API.Platform.Script
{
	public class ScriptEngine
	{
		static Assembly		m_pScriptAssembly = null;
		static Type			m_pScriptType = null;
		static String		m_pSavedFile = null;
		static bool			m_hasNotSecurityException = false;
		static bool			m_canUpdatePosition = true;

		const string SCRIPT_TYPE_NAME = "raScriptClass";

		public static bool LoadScriptData(string pScriptName)
		{
			ResetObjects();

			if(pScriptName == "")
				return false;

			StreamReader pReader = null;
			try
			{
				pReader = new StreamReader(pScriptName);
				string pScriptText = pReader.ReadToEnd();

				if(pScriptText == null || pScriptText.Length == 0)
					return false;

				CSharpCodeProvider pProvider = new CSharpCodeProvider();
				CompilerParameters pParams = new CompilerParameters();
				pParams.GenerateExecutable = false;
				pParams.GenerateInMemory = false;
				m_pSavedFile = System.String.Concat(Path.GetTempFileName(), ".dll");
				pParams.OutputAssembly = m_pSavedFile;

				CompilerResults pResult = pProvider.CompileAssemblyFromSource(pParams, pScriptText);

				if(pResult.Errors.Count > 0)
					{
						System.Diagnostics.Debugger.Log(0, null,
							pResult.Errors[0].ErrorText);
						return false;
					}
				else
					{
						m_pScriptAssembly = Assembly.LoadFrom(pResult.PathToAssembly);
						m_pScriptType = m_pScriptAssembly.GetType(SCRIPT_TYPE_NAME, false, true);
						if(m_pScriptType == null)
							{
								m_pScriptAssembly = null;
								return false;
							}
						return true;
					}
			}
			catch(Exception e)
			{
				System.Diagnostics.Debugger.Log(0, null,
					e.ToString());
				ResetObjects();
				return false;
			}
		}
		public static void ResetObjects()
		{
			m_pScriptAssembly = null;
			m_pScriptType = null;
			m_hasNotSecurityException = false;

			if ((m_pSavedFile != null) && (m_pSavedFile.Length > 0))
			{
				try
				{
					File.Delete(m_pSavedFile);
				}
				catch (Exception)
				{
				}
				m_pSavedFile = null;
			}
		}	
	}
}


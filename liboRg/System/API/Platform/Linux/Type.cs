//
//  lib.cs
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

namespace System.API.Platform.Linux
{
	public enum TChar		: sbyte		{	}; 
	public enum TUchar		: byte		{	};  
	public enum TWchar		: short		{	}; 
	public enum TShort		: short		{	};  
	public enum TUshort		: ushort	{	};  
	public enum TInt		: int		{	};  
	public enum TUint		: uint		{	};  
	public enum TLong		: long		{	};  
	public enum TUlong		: ulong		{	};  
	public enum TLonglong	: long		{	};  
	public enum TUlonglong	: ulong		{	};  
	public enum TBoolean	: sbyte		{	};  
	public enum TPixel		: ulong		{	};  
	public enum TPixmap		: int		{	};  
	public enum XtEnum		: byte		{	};  
	public enum XCardinal	: uint		{	}; 
	public enum XDimension	: ushort	{	};  
	public enum XPosition	: short		{	};  
	public enum XtArgVal	: long		{	};  
	public enum XtVersionType : ulong	{	};  
	public enum XrmName		: int		{	};  
	public enum XrmClass	: int		{	};  
	public enum XtValueMask : ulong		{	};  


	public enum XChar      	: sbyte {	};   // X11 32 Bit: 1 Byte:                  -127 to 127
	public enum XUchar     	: byte  {	};    // X11 32 Bit: 1 Byte:                     0 to 255
	public enum XWchar  	: short	{	};   // X11 32 Bit: 2 Byte:                -32767 to 32767
	public enum XShort 		: short	{	};   // X11 32 Bit: 2 Byte:                -32767 to 32767
	public enum XUshort		: ushort	{	};  // X11 32 Bit: 2 Byte:                     0 to 65535
	public enum XInt		: int	{	};   // X11 32 Bit: 4 Bytes:               -32767 to 32767
	public enum XUint		: uint	{	};  // X11 32 Bit: 4 Bytes:                    0 to 65535
	public enum XLong		: int	{	};   // X11 32 Bit: 4 Bytes:          -2147483647 to 2147483647
	public enum XUlong		: uint	{	};  // X11 32 Bit: 4 Bytes:                    0 to 4294967295
	public enum XLonglong	: int	{	};   // X11 32 Bit: 8 Bytes: -9223372036854775807 to 9223372036854775807
	public enum XUlonglong	: uint	{	};  // X11 32 Bit: 8 Bytes:                    0 to 18446744073709551615

}


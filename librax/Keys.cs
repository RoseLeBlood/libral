//
//  Keys.cs
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

namespace X11
{
	public enum Keys : uint
	{
		Escape = 9,
		One = 10,
		Two = 11,
		Three = 12,
		Four = 13,
		Five = 14,
		Six = 15,
		Seven = 16,
		Eight = 17,
		Nine = 18,
		Zero = 19,
		Question = 20,
		Gravis = 21,
		Num_One = 87,
		Num_Two = 88,
		Num_Three = 89,
		Num_Four = 83,
		Num_Five = 84,
		Num_Six = 85,
		Num_Seven = 79,
		Num_Eight = 80,
		Num_Nine = 81,
		Num_Zero = 90,
		Num_Comma = 91,
		Num_Enter = 104,
		Num_Add = 86,
		Num_Sub = 82,
		Num_Mul = 63,
		Num_Div = 106,
		Num_Lock = 77,
		Pos1 = 110,
		End = 115,
		Break = 127,
		Scroll_Lock = 78,
		Arrow_Up = 111,
		Arrow_Down = 116,
		Arrow_Left = 113,
		Arrow_Right = 114,
		Enter = 36,
		Back = 22,
		ControlR = 105,
		BildUp = 112,
		BildDown = 117,
		F1 =67,
		F2 = 68,
		F3 = 69,
		F4 = 70,
		F5 = 71,
		F6 = 72,
		F7 = 73,
		F8 = 74,
		F9 = 75,
		F10 = 76,
		F11 = 95,
		F12 = 96,
		Insert = 118,
		Del = 119,
		Space = 65,
		AltL = 64,
		MenuL = 135,
		SuperL = 133,
		ContrlL = 37,
		ShiftL = 50,
		Caps_Lock = 66,
		Tab = 23,
		Circumflex = 49,
		AltGr = 108,
		Q = 24,
		W = 25,
		E = 26,
		R = 27,
		T = 28,
		Z = 29,
		U = 30,
		I = 31,
		O = 32,
		P = 33,
		UE = 34,
		Add = 35,
		A = 38,
		S = 39,
		D = 40,
		F = 41,
		G = 42,
		H = 43,
		J = 44,
		K = 45,
		L = 46,
		OE = 47,
		AE = 48,
		Sharp = 51,
		VerticalDash  = 94,
		Y = 52,
		X = 53,
		C = 54,
		V = 55,
		B = 56,
		N = 57,
		M = 58,
		Comma = 59,
		Dot = 60,
		Sub = 61,
		ShiftR = 62,
		NoKey,
	}

	public enum MouseButton : uint
	{
		None = 0,
		Left = 1,
		Middle = 2, 
		Right = 3,    
		ScrollUp = 4,
		ScrollDown = 5,
		XButton1 = 6,  
		XButton2 = 7 
	}
}


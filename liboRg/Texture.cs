//
//  Texture.cs
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
using X11;
using System.Common;
using liboRg.OpenGL;
using System.Runtime.InteropServices;
using X11.Widgets;

namespace liboRg
{
	public enum TextureDataType : uint
	{
		Byte = (uint)GL.BYTE,
		UnsignedByte = (uint)GL.UNSIGNED_BYTE,
		Short = (uint)GL.SHORT,
		UnsignedShort = (uint)GL.UNSIGNED_SHORT,
		Int = (uint)GL.INT,
		UnsignedInt = (uint)GL.UNSIGNED_INT,
		Float = (uint)GL.FLOAT,
		Double = (uint)GL.DOUBLE,

		UnsignedByte332 = (uint)GL.UNSIGNED_BYTE_3_3_2,
		UnsignedByte233Rev = (uint)GL.UNSIGNED_BYTE_2_3_3_REV,
		UnsignedShort565 = (uint)GL.UNSIGNED_SHORT_5_6_5,
		UnsignedShort565Rev = (uint)GL.UNSIGNED_SHORT_5_6_5,
		UnsignedShort4444 = (uint)GL.UNSIGNED_SHORT_4_4_4_4,
		UnsignedShort4444Rev = (uint)GL.UNSIGNED_SHORT_4_4_4_4_REV,
		UnsignedShort5551 = (uint)GL.UNSIGNED_SHORT_5_5_5_1,
		UnsignedShort1555Rev = (uint)GL.UNSIGNED_SHORT_1_5_5_5_REV,
		UnsignedInt8888 = (uint)GL.UNSIGNED_INT_8_8_8_8,
		UnsignedInt8888Rev = (uint)GL.UNSIGNED_INT_8_8_8_8_REV,
		UnsignedInt101010102 = (uint)GL.UNSIGNED_INT_10_10_10_2
	}
	public enum TextureFormat : uint
	{
		Red = (uint)GL.RED,
		RGB = (uint)GL.RGB,
		BGR = (uint)GL.BGR,
		RGBA = (uint)GL.RGBA,
		BGRA = (uint)GL.BGRA
	}
	public enum TextureInternalFormat : uint
	{
		CompressedRed = (uint)GL.COMPRESSED_RED,
		CompressedRedRGTC1 = (uint)GL.COMPRESSED_RED_RGTC1,
		CompressedRG = (uint)GL.COMPRESSED_RG,
		CompressedRGB = (uint)GL.COMPRESSED_RGB,
		CompressedRGBA = (uint)GL.COMPRESSED_RGBA,
		CompressedRGRGTC2 = (uint)GL.COMPRESSED_RG_RGTC2,
		CompressedSignedRedRGTC1 = (uint)GL.COMPRESSED_SIGNED_RED_RGTC1,
		CompressedSignedRGRGTC2 = (uint)GL.COMPRESSED_SIGNED_RG_RGTC2,
		CompressedSRGB = (uint)GL.COMPRESSED_SRGB,
		DepthStencil = (uint)GL.DEPTH_STENCIL,
		Depth24Stencil8 = (uint)GL.DEPTH24_STENCIL8,
		Depth32FStencil8 = (uint)GL.DEPTH32F_STENCIL8,
		DepthComponent = (uint)GL.DEPTH_COMPONENT,
		DepthComponent16 = (uint)GL.DEPTH_COMPONENT16,
		DepthComponent24 = (uint)GL.DEPTH_COMPONENT24,
		DepthComponent32F = (uint)GL.DEPTH_COMPONENT32F,
		R16F = (uint)GL.R16F,
		R16I = (uint)GL.R16I,
		R16SNorm = (uint)GL.R16_SNORM,
		R16UI = (uint)GL.R16UI,
		R32F = (uint)GL.R32F,
		R32I = (uint)GL.R32I,
		R32UI = (uint)GL.R32UI,
		R3G3B2 = (uint)GL.R3_G3_B2,
		R8 = (uint)GL.R8,
		R8I = (uint)GL.R8I,
		R8SNorm = (uint)GL.R8_SNORM,
		R8UI = (uint)GL.R8UI,
		Red = (uint)GL.RED,
		RG = (uint)GL.RG,
		RG16 = (uint)GL.RG16,
		RG16F = (uint)GL.RG16F,
		RG16SNorm = (uint)GL.RG16_SNORM,
		RG32F = (uint)GL.RG32F,
		RG32I = (uint)GL.RG32I,
		RG32UI = (uint)GL.RG32UI,
		RG8 = (uint)GL.RG8,
		RG8I = (uint)GL.RG8I,
		RG8SNorm = (uint)GL.RG8_SNORM,
		RG8UI = (uint)GL.RG8UI,
		RGB = (uint)GL.RGB,
		RGB10 = (uint)GL.RGB10,
		RGB10A2 = (uint)GL.RGB10_A2,
		RGB12 = (uint)GL.RGB12,
		RGB16 = (uint)GL.RGB16,
		RGB16F = (uint)GL.RGB16F,
		RGB16I = (uint)GL.RGB16I,
		RGB16UI = (uint)GL.RGB16UI,
		RGB32F = (uint)GL.RGB32F,
		RGB32I = (uint)GL.RGB32I,
		RGB32UI = (uint)GL.RGB32UI,
		RGB4 = (uint)GL.RGB4,
		RGB5 = (uint)GL.RGB5,
		RGB5A1 = (uint)GL.RGB5_A1,
		RGB8 = (uint)GL.RGB8,
		RGB8I = (uint)GL.RGB8I,
		RGB8UI = (uint)GL.RGB8UI,
		RGB9E5 = (uint)GL.RGB9_E5,
		RGBA = (uint)GL.RGBA,
		RGBA12 = (uint)GL.RGBA12,
		RGBA16 = (uint)GL.RGBA16,
		RGBA16F = (uint)GL.RGBA16F,
		RGBA16I = (uint)GL.RGBA16I,
		RGBA16UI = (uint)GL.RGBA16UI,
		RGBA2 = (uint)GL.RGBA2,
		RGBA32F = (uint)GL.RGBA32F,
		RGBA32I = (uint)GL.RGBA32I,
		RGBA32UI = (uint)GL.RGBA32UI,
		RGBA4 = (uint)GL.RGBA4,
		RGBA8 = (uint)GL.RGBA8,
		RGBA8UI = (uint)GL.RGBA8UI,
		SRGB8 = (uint)GL.SRGB8,
		SRGB8A8 = (uint)GL.SRGB8_ALPHA8,
		SRGBA = (uint)GL.SRGB_ALPHA
	}
	public enum TextureWrapping : uint
	{
		ClampEdge = (uint)GL.CLAMP_TO_EDGE,
		ClampBorder = (uint)GL.CLAMP_TO_BORDER,
		Repeat = (uint)GL.REPEAT,
		MirroredRepeat = (uint)GL.MIRRORED_REPEAT
	}
	public enum TextureFilter : uint
	{
		Nearest = (uint)GL.NEAREST,
		Linear = (uint)GL.LINEAR,
		NearestMipmapNearest = (uint)GL.NEAREST_MIPMAP_NEAREST,
		LinearMipmapNearest = (uint)GL.LINEAR_MIPMAP_NEAREST,
		NearestMipmapLinear = (uint)GL.NEAREST_MIPMAP_LINEAR,
		LinearMipmapLinear = (uint)GL.LINEAR_MIPMAP_LINEAR
	}
	public class Texture : Handle
	{
		private uint[] m_iObject;

		public Texture(string name) 
			: base(name)
		{
			m_iObject = new uint[1];
			gl.glGenTextures(1, m_iObject);
		}

		public Texture(Image image, TextureInternalFormat internalFormat = TextureInternalFormat.RGBA )
			: this(image.Name, image.ToByteArray(), TextureDataType.Byte, TextureFormat.RGBA, image.Size , internalFormat )
		{
		}
		public Texture(string name, byte[] data, TextureDataType type, TextureFormat format, 
			Size pSize, TextureInternalFormat internalFormat )
			: this("tex_" + name)
		{

			gl.glBindTexture( (uint)GL.TEXTURE_2D, m_iObject[0] );
			gl.glTexImage2D( (uint)GL.TEXTURE_2D, 0, (int)internalFormat, pSize.Width, pSize.Height, 0, (uint)format, (uint)type, 
				data );

			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_WRAP_S, (int)GL.CLAMP_TO_EDGE );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_WRAP_T, (int)GL.CLAMP_TO_EDGE );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_MIN_FILTER, (int)GL.LINEAR_MIPMAP_LINEAR );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_MAG_FILTER, (int)GL.LINEAR );

			gl.glGenerateMipmap( (uint)GL.TEXTURE_2D);

			Register(true);
		}
		~Texture()
		{
			Dispose(false);
		}
		protected override void CleanUpManagedResources()
		{
			gl.glDeleteTextures(1, m_iObject);
		}

		public void SetWrapping( TextureWrapping s )
		{
			var x = PushState();

			gl.glBindTexture( (uint)GL.TEXTURE_2D, m_iObject[0] );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_WRAP_S, (int)s );

			PopState(x);
		}
		public void SetWrapping( TextureWrapping s, TextureWrapping t )
		{
			var x = PushState();

			gl.glBindTexture( (uint)GL.TEXTURE_2D, m_iObject[0] );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_WRAP_S, (int)s );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_WRAP_T, (int)t );

			PopState(x);

		}
		public void SetWrapping( TextureWrapping s, TextureWrapping t, TextureWrapping r )
		{
			var x = PushState();

			gl.glBindTexture( (uint)GL.TEXTURE_2D, m_iObject[0] );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_WRAP_S, (int)s );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_WRAP_T, (int)t );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_WRAP_R, (int)r );

			PopState(x);

		}

		public void SetFilters( TextureFilter min, TextureFilter mag )
		{
			var x = PushState();

			gl.glBindTexture( (uint)GL.TEXTURE_2D, m_iObject[0] );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_MIN_FILTER, (int)min );
			gl.glTexParameteri( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_MAG_FILTER, (int)mag );

			PopState(x);

		}
		public void SetBorderColor( Color color )
		{
			var x = PushState();

			gl.glBindTexture( (uint)GL.TEXTURE_2D,  m_iObject[0] );
			gl.glTexParameterfv( (uint)GL.TEXTURE_2D, (uint)GL.TEXTURE_BORDER_COLOR, color.fRGBA );

			PopState(x);

		}

		public void GenerateMipmaps()
		{
			var x = PushState();

			gl.glBindTexture( (uint)GL.TEXTURE_2D,  m_iObject[0] );
			gl.glGenerateMipmap( (uint)GL.TEXTURE_2D );

			PopState(x);
		}

		private uint PushState()
		{
			int[] restoreId = new int[1]; 
			gl.glGetIntegerv( (uint)GL.TEXTURE_BINDING_2D, restoreId );
			return (uint)restoreId[0];
		}
		private void PopState(uint restoreId)
		{
			gl.glBindTexture( (uint)GL.TEXTURE_2D, restoreId );
		}

	}
}


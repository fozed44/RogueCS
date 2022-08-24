using System.Runtime.InteropServices;

using static SDL2.SDL;

namespace RogueCS.SDLWrapper.Graphics.Implementations {

    public class TextSheet : IDisposable {

      #region constants

        // Define the glyph range that will be loaded into
        public const char GLYPH_START = ' ';
		public const char GLYPH_END   = 'z';

		// MAX_GLYPHS determines the size of the memory block we
		// allocate for _pGlyphs.   
		public const int MAX_GLYPHS = 128;

      #endregion constants

      #region fields

		public readonly IntPtr     pTexture;
		public readonly SDL_Rect[] Glyphs;

		// The width and height of the characters in the loaded
		// font. 
		public readonly int CharacterWidth;
		public readonly int CharacterHeight;

		// Calculated based on the number of glyphs in the sheet
		// and _characterWidth and _characterHeight
		public readonly int TextureWidth;
		public readonly int TextureHeight;

		private bool _disposed = false;

      #endregion fields

      #region ctor

        public TextSheet(
			IntPtr pRenderer,
			string filename,
			int    fontSize,
			int    characterWidth,
			int    characterHeight,
			byte   r,
			byte   g,
			byte   b
		) {
			CharacterWidth  = characterWidth;
			CharacterHeight = characterHeight;

			// This is not a perfect calculation to make the characters tightly in a
			// texture. It is over-sized, but just a bit, so good enough.
			TextureWidth  = (int)(Math.Sqrt((GLYPH_END - GLYPH_START)) + 2) * CharacterWidth;
			TextureHeight = (int)(Math.Sqrt((GLYPH_END - GLYPH_START)) + 2) * CharacterHeight;

			Glyphs = new SDL_Rect[MAX_GLYPHS];

			// Ensure that SDL_TTF is initialized. This method should be fine to call
			// repeatedly.
			if (SDL2.SDL_ttf.TTF_Init() == 0)
				throw new Exception($"Failed to initialize ${filename}, ${SDL_GetError()}");

			IntPtr pFont = SDL2.SDL_ttf.TTF_OpenFont(filename, fontSize);
			if (pFont == IntPtr.Zero)
				throw new Exception($"Failed to load font ${filename}, ${SDL_GetError()}");

			// The surface we will copy pCharSurfaces to. When it
			// we have copied all chars to this surface, it will be
			// copied into the _pSDLTextSheet.
			IntPtr pTextureSurface = SDL_CreateRGBSurface(0, TextureWidth, TextureHeight, 32, 0, 0, 0, 0xff);
			if (pTextureSurface == IntPtr.Zero)
				throw new Exception($"Failed to create surface, ${SDL_GetError()}");

			if(0 != SDL_SetColorKey(pTextureSurface, 1, SDL_MapRGBA(Marshal.PtrToStructure<SDL_Surface>(pTextureSurface).format, 0, 0, 0, 0)))
				throw new Exception("Failed to set color key. _e");

			SDL_Rect dest = new SDL_Rect {
				x = 0,
				y = 0,
				w = characterWidth,
				h = characterHeight
			};

			for (char i = GLYPH_START; i <= GLYPH_END; i++) {

				IntPtr pCharSurface = SDL2.SDL_ttf.TTF_RenderUTF8_Blended(pFont, i.ToString(), new SDL_Color { r = r, g = g, b = b, a = 0 });
				if (pCharSurface == IntPtr.Zero)
					throw new Exception($"Failed to render blended surface. {SDL_GetError()}");

				var source = new SDL_Rect { x = 0, y = 0, w = 0, h = 0 };
				SDL2.SDL_ttf.TTF_SizeText(pFont, i.ToString(), out source.w, out source.h);

				if (dest.x + CharacterWidth >= TextureWidth) {
					dest.x = 0;
					dest.y += CharacterHeight;

					if (dest.y + CharacterHeight >= TextureHeight) {
						throw new Exception(
							$"Out of glyph space in {TextureWidth}x{TextureHeight} font atlas texture map. {SDL_GetError()}"
						);
					}
				}

				SDL_BlitScaled(pCharSurface, ref source, pTextureSurface, ref dest);

				var glyphRect = Glyphs[i];

				glyphRect.x = dest.x;
				glyphRect.y = dest.y;
				glyphRect.w = dest.w;
				glyphRect.h = dest.h;

				SDL_FreeSurface(pCharSurface);

				dest.x += CharacterWidth;
			}

			pTexture = SDL_CreateTextureFromSurface(
				pRenderer,
				pTextureSurface
			);
        }

      #endregion ctor

      #region IDisposable

        public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Dispose(bool disposing) {
			if (_disposed)
				return;

			if(pTexture != IntPtr.Zero) {
				SDL_DestroyTexture(pTexture);
			}

			_disposed = true;
		}

      #endregion IDisposable

    }
}

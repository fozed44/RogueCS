using RogueCS.SDL.Graphics.Interfaces;
using System.Runtime.InteropServices;

using static SDL2.SDL;

namespace RogueCS.SDL.Graphics.Implementations {

    internal class TextSheet : ITextSheet {
        
		// Define the glyph range that will be loaded into
		const char GLYPH_START = ' ';
		const char GLYPH_END   = 'z';

		// MAX_GLYPHS determines the size of the memory block we
		// allocate for _pGlyphs.   
		const int MAX_GLYPHS = 128;

		IntPtr     _pTextSheet;
		SDL_Rect[] _pGlyphs;

		// The width and height of the characters in the loaded
		// font. 
		int _characterWidth;
		int _characterHeight;

		// Calculated based on the number of glyphs in the sheet
		// and _characterWidth and _characterHeight
		int _textureWidth;
		int _textureHeight;

		public TextSheet(
			string filename,
			int    fontSize,
			int    characterWidth,
			int    characterHeight,
			byte   r,
			byte   g,
			byte   b
		) {
			_characterWidth  = characterWidth;
			_characterHeight = characterHeight;

			// This is not a perfect calculation to make the characters tightly in a
			// texture. It is over-sized, but just a bit, so good enough.
			_textureWidth  = (int)(Math.Sqrt((GLYPH_END - GLYPH_START)) + 2) * _characterWidth;
			_textureHeight = (int)(Math.Sqrt((GLYPH_END - GLYPH_START)) + 2) * _characterHeight;

			_pGlyphs = new SDL_Rect[MAX_GLYPHS];

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
			IntPtr pTextureSurface = SDL_CreateRGBSurface(0, _textureWidth, _textureHeight, 32, 0, 0, 0, 0xff);
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

				if (dest.x + _characterWidth >= _textureWidth) {
					dest.x = 0;
					dest.y += _characterHeight;

					if (dest.y + _characterHeight >= _textureHeight) {
						throw new Exception(
							$"Out of glyph space in {_textureWidth}x{_textureHeight} font atlas texture map. {SDL_GetError()}"
						);
					}
				}

				SDL_BlitScaled(pCharSurface, ref source, pTextureSurface, ref dest);

				var glyphRect = _pGlyphs[i];

				glyphRect.x = dest.x;
				glyphRect.y = dest.y;
				glyphRect.w = dest.w;
				glyphRect.h = dest.h;

				SDL_FreeSurface(pCharSurface);

				dest.x += _characterWidth;
			}

			_pTextSheet = SDL_CreateTextureFromSurface(
				GET_RENDERER,
				pTextureSurface
			)
        }

		public int CharacterHeight => _characterHeight;

		public int CharacterWidth => _characterHeight;

        public void RenderChar(char c, SDL_Point targetLocation) {

			if (c < GLYPH_START || c > GLYPH_END)
				throw new Exception($"character out of range {c}");

			var targetRect = new SDL_Rect {
				x = targetLocation.x,
				y = targetLocation.y,
				w = _characterWidth,
				h = _characterHeight
			};

			SDL_RenderCopy(
				static_cast<SDLImplementation*>(sdl)->GetRenderer(),
				_pTextSheet,
				ref _pGlyphs[c],
				ref targetRect
			);
        }

        public void RenderString(string s, SDL_Point targetLocation) {
			SDL_Point currentLocation = targetLocation;
			foreach (var c in s) {
				RenderChar(c, targetLocation);
				currentLocation.x += _characterWidth;
			};
        }
    }
}

using RogueCS.Core.Exceptions;
using RogueCS.SDLWrapper.Common.Interfaces;
using RogueCS.SDLWrapper.Graphics.Interfaces;
using static SDL2.SDL;

namespace RogueCS.SDLWrapper.Graphics.Implementations {

    internal class SDLGraphics : ISDLGraphics {

      #region fields

        readonly ISDLCore _sdl;

        readonly SDL_Rect _topLevelRect;

        readonly IntPtr _pRenderer;
        readonly IntPtr _pWindow;

        private bool _disposed = false;

      #endregion fields

      #region ctor

        public SDLGraphics(
            ISDLCore sdl, 
            int  topLevelWindowWidth, 
            int  topLevelWindowHeight
        ) {
            _sdl                  = sdl;

            _topLevelRect = new SDL_Rect {
                x = 0,
                y = 0,
                w = topLevelWindowWidth,
                h = topLevelWindowHeight
            };

	        SDL_Log("Init SDL window.");

            _pWindow = SDL2.SDL.SDL_CreateWindow(
               "RogueCS",
               SDL_WINDOWPOS_UNDEFINED,
               SDL_WINDOWPOS_UNDEFINED,
               topLevelWindowWidth,
               topLevelWindowHeight,
               SDL_WindowFlags.SDL_WINDOW_SHOWN
            );


            if(_pWindow == IntPtr.Zero)
                throw new InitializationException(
	        	    "Window could not be created!"
                );

            SDL_Log("  -> ok.");

	        SDL_Log("Init SDL renderer.");

            _pRenderer = SDL2.SDL.SDL_CreateRenderer(
                _pWindow,
                -1,
                SDL_RendererFlags.SDL_RENDERER_ACCELERATED
            );

            if(_pRenderer == IntPtr.Zero)
                throw new InitializationException(
	        	    "Renderer could not be created!"
	            );

            SDL_Log("  -> ok.");
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

            if(_pRenderer != IntPtr.Zero) 
                SDL_DestroyWindow(_pWindow);

            if(_pWindow != IntPtr.Zero) 
                SDL_DestroyWindow(_pWindow);

			_disposed = true;
		}

      #endregion IDisposable

      #region ISDLGraphics

        public SDL_Rect TopLevelRect => _topLevelRect;

        public TextSheet CreateTextSheet(string filename, int fontSize, int characterWidth, int characterHeight, byte r, byte g, byte b) {
            return new TextSheet(_pRenderer, filename, fontSize, characterWidth, characterHeight, r, g, b);
        }

        public void RenderClear() {
            SDL_RenderClear(_pRenderer);
        }

        public void RenderLines(SDL_Point[] points) {
            SDL_RenderDrawLines(_pRenderer, points, points.Length);
        }

        public void RenderPresent() {
            SDL_RenderPresent(_pRenderer);
        }

        public void SetRenderDrawColor(SDL_Color color) {
            SDL_SetRenderDrawColor(_pRenderer, color.r, color.g, color.b, color.a);
        }

        public void RenderChar(char c, in SDL_Point location, TextSheet textSheet) {
			if (c < TextSheet.GLYPH_START || c > TextSheet.GLYPH_END)
				throw new Exception($"character out of range {c}");

			var targetRect = new SDL_Rect {
				x = location.x,
				y = location.y,
				w = textSheet.CharacterWidth,
				h = textSheet.CharacterHeight
			};

			SDL_RenderCopy(
				_pRenderer,
				textSheet.pTexture,
				ref textSheet.Glyphs[c],
				ref targetRect
			);
        }

        public void RenderString(string s, in SDL_Point location, TextSheet textSheet) {
			SDL_Point currentLocation = location;
            var characterWidth        = textSheet.CharacterWidth;

			foreach (var c in s) {
				RenderChar(c, location, textSheet);
				currentLocation.x += characterWidth;
			};
        }

        public ISDLCore GetSDLCore() {
            return _sdl;
        }

      #endregion ISDLGraphics

    }
}

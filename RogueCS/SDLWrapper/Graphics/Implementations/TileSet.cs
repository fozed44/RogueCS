using RogueCS.Engine.Implementations.Tiles.TileSheets;
using static SDL2.SDL;

namespace RogueCS.SDLWrapper.Graphics.Implementations {

	public struct TileSetCell {
		SDL_Rect Rect;
	}

	public class TileSet : {

		#region fields

		public IntPtr     pTexture    { get; private set; }
		public SDL_Rect[] Glyphs      { get; private set; }

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

		public TileSet() { }

	  #endregion ctor

	  #region public

		public void AddTileSheet(TileSheet tilesheet) {

		}

	  #endregion public

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

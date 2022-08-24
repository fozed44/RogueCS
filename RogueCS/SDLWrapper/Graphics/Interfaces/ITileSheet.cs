using static SDL2.SDL;

namespace RogueCS.SDLWrapper.Graphics.Interfaces {
    public interface ITileSheet : IDisposable {
		void RenderTile(int tileIndex, SDL_Point location, SDL_Color color);

		int GetTileWidth();  
		int GetTileHeight();
		int GetSheetWidth();
		int GetSheetHeight();
    }
}

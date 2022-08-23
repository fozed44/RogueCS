using SDL2;

using static SDL2.SDL;

namespace RogueCS.SDL.Graphics.Interfaces {
    public interface ITextSheet {
		void RenderChar  (char c, SDL_Point targetLocation);
		void RenderString(string s, SDL_Point targetLocation);

		int CharacterWidth  { get; }
		int CharacterHeight { get; }
	};
}

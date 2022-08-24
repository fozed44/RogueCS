using RogueCS.SDLWrapper.Graphics.Implementations;
using static SDL2.SDL;

namespace RogueCS.SDLWrapper.Graphics.Interfaces {
    public interface ISDLGraphics : IDisposable {

        int TopLevelWindowWidth  { get; }
        int TopLevelWindowHeight { get; }

        void RenderClear();
        void RenderPresent();

        TextSheet CreateTextSheet(
            string filename,
            int fontSize,
            int characterWidth,
            int characterHeight,
            byte r,
            byte g,
            byte b
        );

        void SetRenderDrawColor(SDL_Color color);
        void RenderLines(SDL_Point[] points);

        void RenderChar(char c, SDL_Point location, TextSheet textSheet);
        void RenderString(string s, SDL_Point location, TextSheet textSheet);

    }
}

using RogueCS.SDLWrapper.Common.Interfaces;
using RogueCS.SDLWrapper.Graphics.Implementations;
using static SDL2.SDL;

namespace RogueCS.SDLWrapper.Graphics.Interfaces {
    public interface ISDLGraphics : IDisposable {

        SDL_Rect TopLevelRect { get; }

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

        void RenderChar(char c, in SDL_Point location, TextSheet textSheet);
        void RenderString(string s, in SDL_Point location, TextSheet textSheet);

        ISDLCore GetSDLCore();

    }
}

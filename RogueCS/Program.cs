using RogueCS.SDLWrapper.Common.Implementations;
using RogueCS.SDLWrapper.Graphics.Implementations;
using SDL2;

SDL.SDL_SetHint(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");

using (var sdlCore = new SDLCore())
using (var sdl = new SDLGraphics(sdlCore, 800, 800)) {
    SDL.SDL_Delay(5000);
    SDL.SDL_Quit();
}

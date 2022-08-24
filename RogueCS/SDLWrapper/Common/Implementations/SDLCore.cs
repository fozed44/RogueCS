using RogueCS.Core.Exceptions;
using RogueCS.SDLWrapper.Common.Interfaces;
using RogueCS.SDLWrapper.Graphics.Interfaces;

namespace RogueCS.SDLWrapper.Common.Implementations {

    public class SDLCore : ISDLCore {

      #region ctor

        public SDLCore() {
            LogInfo("Init SDL library.");

            if (SDL2.SDL.SDL_Init(SDL2.SDL.SDL_INIT_VIDEO) < 0)
                throw new InitializationException("Failed to initialize SDL Graphics");

            LogInfo("  -> ok.");
            
        }

      #endregion ctor

      #region IDisposable

        public void Dispose() {}

      #endregion IDisposable

      #region ISDLCore

        public string GetSDLError() {
            return SDL2.SDL.SDL_GetError();
        }

        public void LogDebug(string message) {
            SDL2.SDL.SDL_LogDebug(0 /*SDL_LOG_CATEGORY_APPLICATION*/, message);
        }

        public void LogInfo(string message) {
            SDL2.SDL.SDL_LogInfo(0 /*SDL_LOG_CATEGORY_APPLICATION*/, message);
        }

        public ITimer CreateAveragingMsTimer(int averageOverX) {
            return new AveragingMsTimer(this, averageOverX);
        }

        public UInt64 GetPerformanceFrequency() {
            return SDL2.SDL.SDL_GetPerformanceFrequency();
        }

        public UInt64 GetPerformanceCounter() {
            return SDL2.SDL.SDL_GetPerformanceCounter();
        }

        #endregion ISDLCore

    }
}


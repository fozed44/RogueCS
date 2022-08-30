using RogueCS.Engine.Implementations.Maps;
using static SDL2.SDL;

namespace RogueCS.Engine.Interfaces {

    public interface ISurfaceGenerator {

        /// <summary>
        /// The entity implementing this method should attempt to return a surface the same size
        /// and location as 'requestedLocation'. However, the caller is expected to handle the resulting
        /// 
        /// </summary>
        Surface CreateSurface(SDL_Rect requestedLocation);
    }
}

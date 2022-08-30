using static SDL2.SDL;

namespace RogueCS.Engine.Implementations.Characters {
    public class Player : Character {

      #region ctor

        public Player(SDL_Point location, char tile)
            : base(location, tile) { }

      #endregion ctor

    }
}

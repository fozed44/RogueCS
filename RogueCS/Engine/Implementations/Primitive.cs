using static SDL2.SDL;

namespace RogueCS.Engine.Implementations {
    public static class Primative {

        /// <summary>
        /// Return true if rect 'i' is inside rect 'o'. (sides touching is considered inside)
        /// </summary>
        public static bool RectInRect(in SDL_Rect i, in SDL_Rect o) {
            return i.x >= o.x
                && i.y >= o.y
                && i.x + i.w <= o.x + o.w
                && i.y + i.h <= o.y + o.h;
        }

        /// <summary>
        /// Return true if point 'p' is inside rect 'o'. (touching is considered inside)
        /// </summary>
        public static bool PointInRect(in SDL_Point p, in SDL_Rect o) {
            return p.x >= o.x
                && p.y >= o.y
                && p.x <= o.x + o.w
                && p.y <= o.y + o.h;
        }
    }
}

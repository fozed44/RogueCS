using RogueCS.SDLWrapper.Graphics.Interfaces;
using System.Diagnostics;
using static SDL2.SDL;

namespace RogueCS.UI.Implementations {

	/// <summary>
	/// Wrapper over a SDL_Rect that adds boundary validation against the top level window.
	///
	/// This rect type always represents a rect in coordinates relative to the top level window.
	///
	/// Note that SetRect is protected. The RenderRect is intended to be a base class of something
	/// more intelligent like a window. Since the subclass should be managing the location of
	/// the rect based on some higher level concept (a window for example keeps track of a location
	/// relative to a parent) the SetRect method is protected.
	/// </summary>
    public class RenderRect {

      #region fields

        // _rect is a rectangle relative to the top level window, and represents the section of the window
        // that this renderer can render to. 
        private SDL_Rect _rect;

        // We need a reference to the graphics object if we are going to do bounds checking.
        protected readonly ISDLGraphics _graphics;

      #endregion fields

      #region ctor

        public RenderRect(
            ISDLGraphics graphics,
            SDL_Rect     rect
        ) {
            _graphics = graphics;
            SetRenderRect(rect);
        }

      #endregion ctor

      #region protected

        /// <summary>
        /// Set a new rect. The rect must be within boundaries of the top level window.
        /// </summary>
        protected void SetRenderRect(SDL_Rect rect) {
            Debug.Assert(Engine.Implementations.Primative.RectInRect(rect, _graphics.TopLevelRect));
            _rect = rect; 
        }

      #endregion protected

      #region public 

        public SDL_Rect GetRenderRect() { return _rect; }

      #endregion public

    }
}

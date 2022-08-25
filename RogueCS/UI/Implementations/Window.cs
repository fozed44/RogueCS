using RogueCS.SDLWrapper.Graphics.Interfaces;
using RogueCS.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace RogueCS.UI.Implementations {

	/// <summary>
	/// Sets up a parent/child relationship between widows. 
	///
	/// A note on the relationship between a Window's _windowRect and the base classes (Renderer)
	/// _rect field:
	///		_windowRect is always relative to the window parent
	///		_rect is always relative to the top level window, this should make some sense
	///     since the _rect is used for rendering purposes, while _windowRect is used for window
	///     layering.
	///
	/// The window alters the _rect of the Renderer whenever _windowRect is
	/// changed, thus a windows _rect should never be altered by anything except
	/// the window its self, and only when _windowRect is updated.
	/// </summary>
	public class Window : RenderRect, IRenderable {

      #region fields

        /// <summary>
        /// _windowRect is the window rect relative to its parent. 
        /// If the window has no parent, then the position of _windowRect should always be 0, 0
        /// since this must be the top level window.
        /// </summary>
        SDL_Rect _windowRect;

		/// <summary>
		/// Points to the parent window, if there is one.
		/// </summary>
		Window? _parent;

		/// <summary>
		/// maintain a list of child windows.
		/// </summary>
		List<Window> _children;

      #endregion fields

      #region ctor

		/// <summary>
		/// Creates a window of the given width and height. The window
		/// will be initialized at position 0, 0 with no parent.
		/// </summary>
		public Window(ISDLGraphics graphics, int width, int height) 
			: base(graphics, new SDL_Rect { x = 0, y = 0, w = width, h = height } ) {
			_windowRect = new SDL_Rect { x = 0, y = 0, w = width, h = height };
			_parent     = null;
			_children   = new List<Window>();
		}

      #endregion ctor

        /// <summary>
        /// Called by SetWindowRect just after _windowRect has been updated. The method updates
        /// the _renderRect based on the new position of the _windowRect.
        ///
        /// Note that the method is called UpdateRenderRect(s). This method calls itself on
        /// children also.
        /// </summary>
        void UpdateRenderRects() {
			if (_parent != null) {
				var parentRenderRect = _parent.GetRenderRect();

				// The new UL position of this render rect will be
				// The parents render rect position (render rect is in
				// screen space) plus the this window's position relative
				// to its parent (_windowRect.{x, y})
				SetRenderRect(
					new SDL_Rect {
					 x = parentRenderRect.x + _windowRect.x,
					 y = parentRenderRect.y + _windowRect.y,
					 w = _windowRect.w,
					 h = _windowRect.h
				});
			}
			else {
				// If this window does not have a parent, the _windowRect
				// is relative to { 0, 0 }.
				SetRenderRect(_windowRect);
			}

			foreach (var child in _children)
				child.UpdateRenderRects();
		}

      #region protected

        /// <summary>
        /// Called after the WindowRect has been changed, allowing
        /// subclasses a chance to react.
        /// </summary>
        protected virtual void WindowRectChanged() { }

		/// <summary>
		/// Can be called by a sub class to render a border.
		/// </summary>
		protected virtual void RenderBorder(SDL_Color color) {
			var renderRect = GetRenderRect();

			var points = new SDL_Point[5] {
				new SDL_Point { x = renderRect.x,                y = renderRect.y },
				new SDL_Point { x = renderRect.x + renderRect.w, y = renderRect.y },
				new SDL_Point { x = renderRect.x + renderRect.w, y = renderRect.y + renderRect.h },
				new SDL_Point { x = renderRect.x,                y = renderRect.y + renderRect.h },
				new SDL_Point { x = renderRect.x,                y = renderRect.y }
			};

			_graphics.SetRenderDrawColor(color);

			_graphics.RenderLines(points);
		}

      #endregion protected

      #region public

		/// <summary>
		/// Add 'child' as a child to 'parent'. 
		///
		/// This child is moved to 'position', relative to this window.
		/// The child must be able to fit inside this window, given 'position',
		/// meaning that position.x + child.width must be <= this.width. Same for
		/// the height.
		/// 
		/// Note: the child window must not be a child of another window.
		/// </summary>
		static void AddChild(
			Window parent,
			Window child,
			SDL_Point position
		) {

			// Set the _parent pointer on the child
			child._parent = parent;

			// Add the child to the parents list of children.
			parent._children.Add(child);

			child.SetWindowRect(
				new SDL_Rect {
				x = position.x,
				y = position.y,
				w = child._windowRect.w,
				h = child._windowRect.h
			});

		}

		/// <summary>
		/// Remove a child from this window.
		/// </summary>
		public void RemoveChild(Window child) {
			_children.Remove(child);
			child._parent = null;
		}

		public SDL_Rect GetWindowRect() { 
			return _windowRect; 
		}

		/// <summary>
		/// Attempts to set the window rect. This method can fail if the new size of
		/// the window is too small to fit children.
		/// </summary>
		public bool SetWindowRect(SDL_Rect rect) {
			if (!CanSetWindowRect(rect)) {
				_graphics.GetSDLCore().LogInfo("WARNING! Cant set window rect. The rect is out of bounds.");
				return false;
			}
		
			_windowRect = rect;
		
			UpdateRenderRects();
		
			WindowRectChanged();
		
			return true;
		}


		/// <summary>
		/// Return true if rect is of a valid size and position. This method can fail
		/// if the children of this window do not fit within the new rect.
		/// </summary>
		public bool CanSetWindowRect(SDL_Rect rect) {

			// Regardless of whether or not we have a parent, the rect must be positioned
			// in a positive location.
			if (rect.x < 0 || rect.y < 0)
				return false;

			if (_parent != null) {
				// If this window has a parent, the right and bottom edges must be within
				// the parents rect.
				if (rect.x + rect.w > _parent._windowRect.w)
					return false;

				if (rect.y + rect.h > _parent._windowRect.h)
					return false;
			} else {
				// Otherwise the right and bottom edges must be within the boundaries of
				// the top level window.
				if (rect.x + rect.w > _graphics.TopLevelRect.w)
					return false;

				if (rect.y + rect.h > _graphics.TopLevelRect.h)
					return false;
			}

			// If any child's right edge is out side of the right and bottom edges of
			// the new rect, we are returning false.
			foreach(var child in _children) {
				if (child._windowRect.x + child._windowRect.w > rect.w)
					return false;

				if (child._windowRect.y + child._windowRect.h > rect.h)
					return false;
			}

			// Everything is ok.
			return true;
		}

		/// <summary>
		/// This implementation of render doesn't do much. It just
		/// calls render on children, but can be used by subclass for
		/// passing Render calls to children.
		/// </summary>
		public void Render(GameData data) {
			foreach (var child in _children)
				child.Render(data);
		}

      #endregion 

    };

}

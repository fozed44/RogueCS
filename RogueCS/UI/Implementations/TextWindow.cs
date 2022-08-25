using RogueCS.SDLWrapper.Graphics.Implementations;
using RogueCS.SDLWrapper.Graphics.Interfaces;
using static SDL2.SDL;

namespace RogueCS.UI.Implementations {

	/// <summary>
	/// Base class for windows that render text.
	/// 
	/// TextRenderer provides support for calculating the width and height (in characters) of the
	/// render area, based on the size of the render rect.
	/// </summary>
	public class TextWindow : Window {

      #region fields


		// The width of the view port in characters.
		int _maxViewPortWidthInChars;

		// The height of the view port in characters.
		int _maxViewPortHeightInChars;

		// the with of the characters in the attached text sheet
		int _characterWidth;

		// the height of the characters in the attached text sheet
		int _characterHeight;

		// The sheet containing the glyphs used by the renderer.
		TextSheet? _textSheet;

      #endregion fields

      #region ctor

		TextWindow(ISDLGraphics graphics, int width, int height)
			: base(graphics, width, height) {}

      #endregion ctor

      #region private

        // recalculates the size of the char view port.
 	    void CalculateMaxCharViewPortSize() {
			if (_textSheet != null) {
				_maxViewPortWidthInChars  = GetWindowRect().w / _textSheet.CharacterWidth;
				_maxViewPortHeightInChars = GetWindowRect().h / _textSheet.CharacterHeight;
			}
		}

	  #endregion private

      #region protected

		/// <summary>
		/// When the window rect changes, we will update the
		/// max char view port with and height. (if possible,
		/// assuming that the text sheet has been set)
		/// </summary>
		protected override void WindowRectChanged() {
			CalculateMaxCharViewPortSize();
		}

		/// <summary>
		/// called by Set text sheet. After the max characters
		/// width and height has been recalculated.
		/// </summary>
		protected virtual void TextSheetChanged() {}

      #endregion protected

      #region public

        /// <summary>
        /// Text rendering notes:
        ///
        /// The following character/string rendering DO clip characters within
        /// the boundaries of this window. The Clipping is rough, if any part
        /// of a character is outside of the window rect, the character is not
        /// rendered.
        /// </summary>

        /// <summary>
        /// Renders s at position 'pos' (pos is in pixels) using the current text sheet.
        /// location is in this windows window space. See above notes re clipping.
        /// </summary>
        public void RenderString(string s, SDL_Point pos) {
			foreach(var c in s) {
				RenderChar(c, pos);
				pos.x += _characterWidth;
			}
		}

		/// <summary>
		/// Renders c at position 'pos' (pos is in pixels) using the current 
		/// text sheet. location is in this windows window space. See the above notes
		/// re clipping.
		/// </summary>
		public void RenderChar(char c, in SDL_Point pos) {
			var renderRect = GetRenderRect();

			// Clip any characters that are outside of the window
			if (pos.x + _characterWidth  > GetWindowRect().w
			 || pos.x < 0)
				return;
			if (pos.y + _characterHeight > GetWindowRect().h
			 || pos.y < 0)
				return;

			if (_textSheet == null)
				return;

			var actualRenderPos = new SDL_Point {
				x = pos.x + renderRect.x,
				y = pos.y + renderRect.y
			};

			_graphics.RenderChar(
				c,
				actualRenderPos,
				_textSheet
			);
		}

		/// <summary>
		/// Renders s at position 'pos', where pos is defined in of a grid where
		/// each grid location is the size of a character in the current text sheet. 
		/// See above notes re character clipping.
		/// </summary>
		public void RenderStringOnGrid(string s, in SDL_Point pos) {

			RenderString(
				s, 
				new SDL_Point {
					x = pos.x * _characterWidth, 
					y = pos.y * _characterHeight 
				}
			);
		}
		
		/// <summary>
		/// Renders c at position 'pos', where pos is defined in of a grid where
		/// each grid location is the size of a character in the current text sheet. 
		/// See above notes re character clipping.
		/// </summary>
		public void RenderCharOnGrid(char c, in SDL_Point pos) {

			RenderChar(
				c, 
				new SDL_Point { 
					x = pos.x * _characterWidth, 
		     		y = pos.y * _characterHeight 
				}
			);
		}
		

		public int GetMaxViewPortWidthInChars() { 
			return _maxViewPortWidthInChars; 
		}

		public int GetMaxViewPortHeightInChars() { 
			return _maxViewPortHeightInChars;
		}

		public int GetCharacterWidth() {
			return _characterWidth;
		}

		public int GetCharacterHeight() {
			return _characterHeight;
		}

		public void SetTextSheet(TextSheet textSheet) {
			_textSheet = textSheet;

			_characterWidth  = _textSheet.CharacterWidth;
			_characterHeight = _textSheet.CharacterHeight;

			CalculateMaxCharViewPortSize();

			TextSheetChanged();
		}

      #endregion public
    };
}

using static SDL2.SDL;

namespace RogueCS.Engine.Implementations.Characters {

	public class Character {

      #region Properties

        public SDL_Point                Location  { get; private set; }
		public List<CharacterCondition> Condition { get; private set; }
		public CharacterStats           Stats     { get; private set; }
		public char                     Tile      { get; private set; }
		public SDL_Color                Color     { get; private set; }

      #endregion Properties

      #region ctor

        public Character(SDL_Point location, char tile) {
			Location   = location;
			Tile       = tile;	
			Condition = new List<CharacterCondition>();
		}

      #endregion ctor

      #region protected

        virtual protected void LocationChanged()                     {}
		virtual protected CharacterCondition ApplyingCondition(CharacterCondition condition) =>  condition;
		virtual protected void ConditionApplied(CharacterCondition condition) {}
		virtual protected void ConditionRemoved(CharacterCondition condition) {}
		virtual protected bool RemovingCondition(CharacterCondition condition) => true;

      #endregion protected

      #region public

		public void  SetLocation(in SDL_Point p) {
			Location = p;
			LocationChanged();
		}

		public void ApplyCondition(CharacterCondition condition) {
			condition = ApplyingCondition(condition);

			if (condition != CharacterCondition.None) {
				Condition.Add(condition);
				ConditionApplied(condition);
			}
		}

		public void RemoveCondition(CharacterCondition condition) {
			if (RemovingCondition(condition)) {
				Condition.Remove(condition);
				ConditionRemoved(condition);
			}
		}
		
		public bool HasCondition(CharacterCondition condition) {
			return Condition.Contains(condition);
		}

	  #endregion public

	};
}

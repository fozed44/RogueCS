using RogueCS.Engine.Implementations;
using RogueCS.Engine.Implementations.StateHandlers;
using static SDL2.SDL;

namespace RogueCS.Engine.Interfaces {

	public interface IStateHandler {

		/// <summary>
		/// Return the GameState that this state handler handles.
		/// </summary>
		GameState HandledState { get; }

		/// <summary>
		/// Called by the Game class just before when the state has changed,
		/// before the first call to HandleGameLoop for the first time
		/// after the state change.
		/// </summary>
		void EnteringState();

		/// <summary>
		/// On each iteration through the game loop the Game class
		/// will call HandleGameLoop for exactly one StateHandler
		/// sub-class... Which ever subclass matches the current game
		/// state.
		/// </summary>
		/// <param name="e">
		///	The SDL_Event. 
		/// </param>
		/// <returns>
		/// The Next game state. The StateHandler sub-class is
		/// responsible for determining if the state is going to
		/// change and if so, what it will change to. The Game class
		/// is responsible for monitoring this return value in order
		/// to determining which StateHandler will receive the next
		/// HandleGameLoop call.
		/// </returns>
		GameState HandleGameLoop(SDL_Event e, GameData data);
	}
}

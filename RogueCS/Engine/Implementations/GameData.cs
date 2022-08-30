using RogueCS.Engine.Implementations.Characters;
using RogueCS.Engine.Implementations.Maps;
using static SDL2.SDL;

namespace RogueCS.Engine.Implementations {

	/// <summary>
	/// A string along with a location. This represents a string
	/// to be rendered on the screen as an overlay. The MpScene
	/// object contains a vector of these objects that the renderer
	/// is responsible for displaying on the screen.
	/// </summary>
	public struct StringOverlay {
		SDL_Point Location;
		string    S;
	};

	/// <summary>
	/// Contains the game state data.
	/// </summary>
	public class GameData {

		/// <summary>
		/// Overlays are strings that the renderer should render to the screen. Overlays can be 
		/// rendered anywhere, the locations in the overlay objects are relative to the upper left
		/// corner of the window.
		/// </summary>
		public List<StringOverlay> StringOverlays { get; private set; }

		/// summary>
		/// The map surface contains all of the data for the current map that is currently in memory.
		/// </summary>
		public Surface? Surface { get; private set; }

		/// <summary>
		/// The player. The player is not owned by a dungeon scene, however a player is required in
		/// order to render a dungeon scene
		/// </summary>
		public Player Player { get; private set; }

		/// <summary>
		/// The character information that should be used by the map view when rendering cells outside
		/// of the map.
		/// </summary>
		public SurfaceCell EmptySpace { get; private set; }

		/// <summary>
		/// updated during the run loop using the SetCurrentFps method.
		/// </summary>
		public int FPS { get; private set; }

		public GameData() {
			Player = new Player(
				new SDL_Point {
					x = 0,
					y = 0
				},
				'@'
			);
			EmptySpace = new SurfaceCell(0);
			StringOverlays = new List<StringOverlay>();
		} 

		// FPS functions:
		public void SetCurrentFPS(int currentFps) { FPS = currentFps; }

		public void SetSurface(Surface surface) {
			Surface = surface;
		}

		public SDL_Point __DEBUG_POINT = new SDL_Point {
			x = 0,
			y = 0
		};
	};
}

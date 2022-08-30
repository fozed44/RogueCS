using RogueCS.Engine.Implementations.Maps;
using static SDL2.SDL;

namespace RogueCS.Engine.Interfaces {

	/// <summary>
	/// A file containing map data.
	/// </summary>
	public interface IMapFile {

		/// <summary>
		/// Load a MapSurface from the MapFile. The underlying map data must be contained
		/// within the given location, however the returned MapSurface may be smaller if the
		/// underlying data does not reach the right or bottom edges of the requested location.
		/// </summary>
		Surface CreateMapSurface(in SDL_Rect location);

		/// <summary>
		/// Read the width of the map. This method must be valid
		/// immediately after construction.
		/// </summary>
		int Width { get; }
		
		/// <summary>
		/// Read the height of the map. This method must be valid
		/// immediately after construction.
		/// </summary>
		int Height { get; }

		/// <summary>
		/// Get the filename associated with the map file.
		/// </summary>
		string Filename { get; }
	};
}

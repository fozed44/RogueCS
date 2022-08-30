using static SDL2.SDL;

namespace RogueCS.Engine.Implementations.Maps {

	/// <summary>
	/// An in memory rectangle of MapCells.
	/// 
	/// The map surface represents a subsection of a MapFile (or, if the MapFile is small enough, the entire
	/// map could be loaded into a MapSurface). Its position and size in file-space is 
	/// indicated by the _fileRect Rect. A subset of the MapSurace is rendered by a DungeonWindow.
	/// </summary>
	public class Surface {

      #region fields

        /// <summary>
        /// The rectangle locating the surface in file-space.
        /// </summary>
        SDL_Rect _fs_surface;
	
		/// <summary>
		/// The cells in the surface. One cell for each location in the rect, so we have 
		/// _fileRect.x  * _fileRect.y cells here.
		/// </summary>
		SurfaceCell[] _cells;

      #endregion fields

      #region ctor

        public Surface(
			SDL_Rect      rect,
			SurfaceCell[] cells
		) {
			_fs_surface = rect;
			_cells      = cells;
		}

		#endregion ctor

		SurfaceCell[] Cells  => _cells;

		SDL_Rect SurfaceRect => _fs_surface;
	
		/// <summary>
		/// Transform a point in file space to a rect in map surface space.
		/// </summary>
		SDL_Point FileSpaceToSurfaceSpace(in SDL_Point point) {
			return new SDL_Point {
				x = point.x - _fs_surface.x,
				y = point.y - _fs_surface.y
			};
		}
	
		/// <summary
		/// Transform a rect in file space to a rect in map surface space.
		/// </summary>
		SDL_Rect FileSpaceToSurfaceSpace(in SDL_Rect rect) {
			return new SDL_Rect {
				x = rect.x - _fs_surface.x,
				y = rect.y - _fs_surface.y,
				w = rect.w,
				h = rect.h
			};
		}
	
		/// <summary
		/// Transform a point in map surface space to a rect in file space.
		/// </summary>
		SDL_Point SurfaceSpaceToFileSpace(in SDL_Point point) {
			return new SDL_Point{
				x = point.x + _fs_surface.x,
				y = point.y + _fs_surface.y
			};
		}
	
		/// <summary
		/// Transform a rect in map surface space to a rect in file space.
		/// </summary>
		SDL_Rect SurfaceSpaceToFileSpace(in SDL_Rect rect) {
			return new SDL_Rect{
				x = rect.x + _fs_surface.x,
				y = rect.y + _fs_surface.y,
				w = rect.w,
				h = rect.h
			};
		}
	};
} 
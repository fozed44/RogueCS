using static SDL2.SDL;

namespace RogueCS.Engine.Implementations.Maps {

    /*
		Coordinate Systems and Conventions

		There are three coordinate systems that are used throughout the map and view
		code that we need to make sure we clearly define. These systems are as follows:

		- File Space

		File space is the coordinate system whose origin is located at the upper left
		hand corner of cells in a file.

		- Surface Space

		Surface space is the coordinate system whose origin is located at the upper left
		hand corner of cells loaded in memory. The cells loaded in memory can be a sub
		set of the larger collection of cells located in a file. 

		- View Space

		View space is the coordinate system whose origin is located at the upper left hand
		corner of a collection of cells being displayed on screen. Just like a surface
		is a subset of the cells in a file, the cells in a view are a subset of cells 
		found in a surface.

		************************* File Space ***********************************
		*                                                                      *
		*                                                                      *
		*     ****************** Surface Space ******************              *
		*     *                                                 *              *
		*     *                                                 *              *
		*     *                                                 *              *
		*     *                                                 *              *
        *     *          ** View Space ****                     *              *
		*     *          *                *                     *              *
		*     *          *                *                     *              *
		*     *          *                *                     *              *
		*     *          *                *                     *              *
        *     *          ******************                     *              *
		*     *                                                 *              *
		*     *                                                 *              *
		*     ***************************************************              *
		*                                                                      *
		************************************************************************

		To keep track of which space a particular entity belongs we should define a
		naming convention that we can use consistently throughout the code base. The
		convention will consist of a standardized prefix. fs_ for an object whose
		offset is in file-space, ss_ for surface-space and vs_ for view space.

		Note that a correct name for the rect defining the general file space is
		fs_FileSpace The file space rect has an offset that is IN file-space.

		the correct name for the surface space rect would be fs_SurfaceSpace since it
		would have an offset in file space.

		A view space rect could be called either fs_ViewSpace or ss_ViewSpace, depending
		on whether the offset of the rect is in file or surface space.
	 */

	public enum SurfaceCellFlags16 : UInt16 {
		None = 0
	}

	public enum SurfaceCellFlags32 : UInt32 {
		None = 0
	}

    /// <summary>
    ///	A cell containing information for one single cell of a surface.
    /// </summary>
    public struct SurfaceCell {
		UInt16             TileIndex;
		SurfaceCellFlags16 Flags16;
		SurfaceCellFlags32 Flags32;
		SDL_Color          Color;

		public SurfaceCell(UInt16 tileIndex) {
			TileIndex = tileIndex;
			Flags16 = 0;
			Flags32 = 0;
			Color.r = 0xFF;
			Color.g = 0xFF;
			Color.b = 0xFF;
			Color.a = 0xFF;
		}
	};
} 
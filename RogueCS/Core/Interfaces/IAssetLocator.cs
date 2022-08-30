namespace RogueCS.Core.Interfaces {


	/// <summary>
	/// asset_type is used to identify various kinds of assets 
	/// </summary>
	public enum BasicAsset {
		Executable,         // The executable 
		AssetBaseDirectory, // base directory for assets
		MapPart,            // map parts; json blocks of map data used to build larger maps
		Font,                
		World,              // directory containing saved world data
		Tile,               // base directory for tile data.
								// tiles.master.json is located here
								// tile sets are located below this directory
		TileSet,            // a folder containing bmp files and tile meta info
		TilePalette,        // data that is used by map parts to map characters to tile indices
		TileDefaults,       // tiles that are used when a tile sheet is missing image data for tiles.
	};

	/// <summary>
	/// Assets that are stored for a saved world.
	/// </summary>
	public enum WorldAsset {
		CompiledTileSheetJson,
		CompiledTileSheetBmp
	};

    public interface IAssetLocator {
		string GetAssetDirectory(BasicAsset assetType);
		IEnumerable<string> GetAssetPaths(BasicAsset assetType);
		string GetAssetPathname(BasicAsset assetType, string assetName);
		string GetWorldDirectory(string worldName);
		string GetWorldAssetDirectory(string worldName, WorldAsset worldAsset);
		string GetWorldAssetPathname(string worldName, WorldAsset worldAsset, string assetName);
		string GetTileSetDirectory(string tilesetName);
		IEnumerable<string> GetTileSets();
		IEnumerable<string> EnumerateWorlds();
		IEnumerable<string> EnumerateTileSets();
    }
}

namespace RogueCS.Core.Interfaces {


	/// <summary>
	/// asset_type is used to identify various kinds of assets 
	/// </summary>
	public enum BasicAsset {
		EXECUTABLE,      // Path to the executable
		ASSETS,          // base directory for assets
		JSON_MAP,        // directory containing json based maps
		FONT,            // directory containing fonts
		WORLD,           // directory containing saved world data
		TILE_SET,
	};

	/// <summary>
	/// Assets that are stored for a saved world.
	/// </summary>
	public enum WorldAsset {
		COMPILED_TILE_SHEET_JSON,
		COMPILED_TILE_SHEET_BMP
	};

    public interface IAssets {
		string GetAssetDirectory(BasicAsset assetType);
		IEnumerable<string> GetAssetPaths(BasicAsset assetType);
		string GetAssetPathname(BasicAsset assetType, string assetName);
		string GetWorldDirectory(string worldName);
		string GetWorldAssetDirectory(string worldName, WorldAsset worldAsset);
		string GetWorldAssetPathname(string worldName, WorldAsset worldAsset, string assetName);
		IEnumerable<string> EnumerateWorlds();
		IEnumerable<string> EnumerateTileSets();
    }
}

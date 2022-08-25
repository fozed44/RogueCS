using RogueCS.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RogueCS.Core.Implementations {
    public class Assets : IAssets {

      #region constants

        /// <summary>
        /// Asset directory's, relative to the executable
        /// directory.
        const string ASSET_SUB_DIRECTORY = "assets";
        
        /// <summary>
        /// json map data, relative to the asset directory.
        /// </summary>
        const string JSON_MAP_SUB_DIRECTORY   = "maps\\json";
        
        /// <summary>
        /// fonts, relative to the asset directory.
        /// </summary>
        const string FONT_SUB_DIRECTORY = "fonts";
        
        /// <summary>
        /// tilesheet json data, relative to the assets directory. (see TileSheet.h)
        /// </summary>
        const string TILE_SHEET_JSON_SUB_DIRECTORY = "tile-sheet\\json";
        
        /// <summary>
        /// tilesheet BMP, relative to the assets directory. (see TileSheet.h)
        /// </summary>
        const string TILE_SHEET_BMP_SUB_DIRECTORY = "tile-sheet\\bmp";
        
        /// <summary>
        /// Location of world data, relative to the assets directory.
        /// </summary>
        const string WORLDS_SUB_DIRECTORY = "worlds";
        
        /// <summary>
        /// Location of compiled tile sheet json data, relative to the worlds directory.
        /// {world-name} will be replaced with the name of the world.
        /// </summary>
        const string COMPILED_TILE_SHEET_JSON_SUB_DIRECTORY = "tile-sheet\\json";
        
        /// <summary>
        /// Location of compiled tile sheet BMP data, relative to the worlds directory.
        /// {world-name} will be replaced with the name of the world.
        /// </summary>
        const string COMPILED_TILE_SHEET_BMP_SUB_DIRECTORY = "tile-sheet\\png";

      #endregion constants

      #region ctor

        public Assets() { }

      #endregion ctor

      #region IAssets

        public IEnumerable<string> EnumerateTileSets() {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateWorlds() {
            return
                Directory.EnumerateDirectories(
                    GetAssetDirectory(BasicAsset.WORLD),
                    "*",
                    SearchOption.TopDirectoryOnly
                );
        }

        public string GetAssetDirectory(BasicAsset assetType) {
 
            switch (assetType) {
                case BasicAsset.EXECUTABLE:
                    return Path.GetDirectoryName(
                        Assembly.GetEntryAssembly()!.Location
                    )!;
                case BasicAsset.ASSETS:
                    return Path.Combine(
                        GetAssetDirectory(BasicAsset.EXECUTABLE),
                        ASSET_SUB_DIRECTORY
                    );
                case BasicAsset.JSON_MAP:
                    return Path.Combine(
                        GetAssetDirectory(BasicAsset.ASSETS),
                        JSON_MAP_SUB_DIRECTORY
                    );
                case BasicAsset.FONT:
                    return Path.Combine(
                        GetAssetDirectory(BasicAsset.ASSETS),
                        FONT_SUB_DIRECTORY
                    );
                case BasicAsset.WORLD:
                    return Path.Combine(
                        GetAssetDirectory(BasicAsset.ASSETS),
                        WORLDS_SUB_DIRECTORY
                    );
                default:
                    throw new ValidationException(
                        $"invalid asset type {assetType}"
                    );
            }
        }

        public string GetAssetPathname(BasicAsset assetType, string assetName) {
            return Path.Combine(
                GetAssetDirectory(assetType),
                assetName
            );
        }

        public IEnumerable<string> GetAssetPaths(BasicAsset assetType) {
            return Directory.EnumerateFiles(
                GetAssetDirectory(assetType)
            );
        }

        public string GetWorldAssetDirectory(string worldName, WorldAsset worldAsset) {
            switch (worldAsset) {
                case WorldAsset.COMPILED_TILE_SHEET_BMP:
                    return Path.Combine(
                        GetWorldDirectory(worldName),
                        COMPILED_TILE_SHEET_JSON_SUB_DIRECTORY
                    );

                case WorldAsset.COMPILED_TILE_SHEET_JSON:
                    return Path.Combine(
                        GetWorldDirectory(worldName),
                        COMPILED_TILE_SHEET_JSON_SUB_DIRECTORY
                    );
                default:
                    throw new ValidationException(
                        $"invalid WorldAsset type {worldAsset}"
                    );
            }
        }

        public string GetWorldAssetPathname(string worldName, WorldAsset worldAsset, string assetName) {
            return Path.Combine(
                GetWorldAssetDirectory(worldName, worldAsset),
                assetName
            );
        }

        public string GetWorldDirectory(string worldName) {
            return Path.Combine(
                GetAssetDirectory(BasicAsset.WORLD),
                worldName
            );
        }

        #endregion IAssets
    }
}

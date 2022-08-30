using RogueCS.Core.Interfaces;

namespace RogueCS.Engine.Implementations.Tiles.TileSheets {
    public class TileSheet : IFileAsset {

        #region properties

        public TileSheet

        #endregion properties

      #region ctor

        public TileSheet(string filepath) {

        }

      #endregion ctor

      #region IFileAsset

        public string Filepath => throw new NotImplementedException();

      #endregion IFileAsset
    }
}

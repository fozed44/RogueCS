using RogueCS.Core.Interfaces;
using System.Text.Json;

namespace RogueCS.Engine.Implementations.Tiles.Palettes {
    internal class TilePalette : IFileAsset {

      #region IFileAsset

        public string          Filepath { get; }
        public TilePaletteData Data     { get; }

      #endregion IFileAsset

      #region ctor

        public TilePalette(string filepath) {
            Filepath = filepath;

            using (var fs = File.OpenRead(Filepath)) {
                if(fs == null)
                    throw new FileNotFoundException(Filepath);

                Data = JsonSerializer.Deserialize<TilePaletteData>(fs)
                    ?? throw new FileLoadException(
                        $"{Filepath} is empty"
                       );
            }
        }

      #endregion ctor

    }
}

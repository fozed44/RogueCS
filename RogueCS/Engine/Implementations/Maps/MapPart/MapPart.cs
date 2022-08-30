using Microsoft.Extensions.DependencyInjection;
using RogueCS.Core.Exceptions;
using RogueCS.Core.Interfaces;
using RogueCS.Engine.Implementations.Tiles.Palettes;
using RogueCS.Engine.Interfaces;
using System.Text.Json;
using static SDL2.SDL;

namespace RogueCS.Engine.Implementations.Maps.MapPart { 

    /// <summary>
    /// A map file that is represented by a .json file.
    /// </summary>
    public class MapPart : IFileAsset, ISurfaceGenerator {

      #region fields

        private readonly TilePalette _palette;

      #endregion fields


      #region Properties

        public MapPartData Data { get; }

        public string Filepath  { get; }

      #endregion Properties

      #region ctor

        public MapPart(string filepath, IServiceProvider s) {

            Filepath = filepath;

            Data     = LoadMapPartData(filepath);

            _palette = LoadPalette(Data.Pallete, s);
        }

      #endregion ctor

      #region ISurfaceGenerator

        public Surface CreateSurface(SDL_Rect requestedLocation) {

            if (fileRect.x < 0
             || fileRect.y < 0
             || fileRect.x + fileRect.w > Data.Width
             || fileRect.y + fileRect.h > Data.Height
             || (fileRect.x | fileRect.y | fileRect.w | fileRect.h) == 0)
                throw new MapLoadException(
                    $"failed to load map surface, rect is out of bounds or zero sized." +
                    $"  -> mapfile: {Filepath}" +
                    $"  -> rect: {{ {fileRect.x}, {fileRect.y}, {fileRect.w}, {fileRect.h}, }}"
                );

            int arraySize = fileRect.w * fileRect.h;
            var result    = new SurfaceCell[arraySize];
            var x         = result[0];

            for (int y = fileRect.y; y < fileRect.y + fileRect.h; y++)
            {
                var scanLine = Data[y].Substring(fileRect.x, fileRect.w);
                for (int x = 0; x < fileRect.w; x++)
                {
                    var pos = y * fileRect.w + x;
                    result[pos].TileIndex = scanLine[x];
                    result[pos].color.r = 0xFF;
                    result[pos].color.g = 0xFF;
                    result[pos].color.b = 0xFF;
                    result[pos].color.a = 0xFF;
                }
            }

            return new Surface(fileRect, result);

        }

      #endregion ISurfaceGeneratro

      #region internal

        internal MapPartData LoadMapPartData(string filepath) {
            using (var fs = File.OpenRead(Filepath)) {
                if(fs == null)
                    throw new FileNotFoundException(Filepath);

                var result = JsonSerializer.Deserialize<MapPartData>(fs)
                    ?? throw new FileLoadException(
                        $"{Filepath} is empty"
                    );

                return result;
            }

        }

        internal TilePalette LoadPalette(string filename, IServiceProvider s) {
            return new TilePalette(
                s.GetRequiredService<IAssetLocator>().GetAssetPathname(BasicAsset.TilePalette, filename)
            );
        }

      #endregion internal

    }
}

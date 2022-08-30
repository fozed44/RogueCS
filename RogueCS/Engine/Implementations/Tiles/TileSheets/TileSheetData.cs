namespace RogueCS.Engine.Implementations.Tiles.TileSheets {

    [Flags]
    public enum TileSheetFlags : UInt64 {
        None         = 0,
        BlocksPlayer = 1 << 0,
        BlocksLight  = 1 << 1,
    }

    public class TileSheetTileData {
        public UInt16   X          { get; }
        public UInt16   Y          { get; }
        public string   TileId     { get; }
        public string[] Attributes { get; }

        public TileSheetTileData(ushort x, ushort y, string tileId, string[] attributes) {
            X          = x;
            Y          = y;
            TileId     = tileId;
            Attributes = attributes;
        }   
    }

    public class TileSheetData {

        public string BmpFilename       { get; }
        public UInt16 TileWidth         { get; }
        public UInt16 TileHieght        { get; }
        public TileSheetTileData[] Data { get; }

        public TileSheetData(
            string              bmpFilename,
            UInt16              tileWidth,
            UInt16              tileHieght,
            TileSheetTileData[] data
        ) {
            BmpFilename = bmpFilename;
            TileWidth   = tileWidth;
            TileHieght  = tileHieght;
            Data        = data;
        }
    }
}

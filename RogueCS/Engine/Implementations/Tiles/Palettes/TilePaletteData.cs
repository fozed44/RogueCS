namespace RogueCS.Engine.Implementations.Tiles.Palettes {

    public class TilePaletteMappingElement {
        char    Char   { get; set; }
        string? TileId { get; set; }

    }

    public class TilePaletteData {
        List<TilePaletteMappingElement>? Map { get; set; }
    }
}

using static SDL2.SDL;

namespace RogueCS.Engine.Implementations.Maps.MapPart {

    public class MapPartData {

        public int Width  { get; set; }
        public int Height { get; set; }

        public string Pallete { get; set; }

        // holds the data contained in the map file. We have _height strings in the vector,
        // and each string is exactly _width characters
        List<List<string>> Data       { get; set; }
        List<string>       Attributes { get; set; }

        public MapPartData(
            int                width,
            int                height,
            string             palette,
            List<List<string>> data,
            List<string>       attributes
        ) {
            Width      = width;
            Height     = height;
            Pallete    = palette;
            Data       = data;
            Attributes = attributes;
        }

    }
}

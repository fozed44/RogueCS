# Tiles 

Tiles are the smallest elements that are used to construct maps. A tile consists of a unique tileId, attributes associated with
that id, and the pixels used to draw the tile to the screen.

Tile information is spread across the following assets:

- The BasicAsset.Tile directory, containing:
    - tiles.master.json
        - Contains all of the unique tileId and the attributes associated with those ids.
    - N number of tileset directories, each containing:
        - N pairs of x.tilesheet.json and x.tilesheet.bmp files. Each pair:
            - Has a json file with the tilesheet.json extension containing the following information:
                - File level information:
                    - The name of the .bmp file containing the pixel data.
                    - The dimensions of the tiles in this tile sheet
                        - This is the dimensions of ALL tiles in this sheet.
                - Tile definitions, where each definition contains:
                    - the tileId
                        - the tileId must exist in the tiles.master.json file
                    - coordinates to the pixels located in the bmp
            - A BMP file that holds the pixels for the tiles.
                - This file is referenced by the tilesheet.json file.

## tiles.master.json

In the root of the tilesets directory is the tiles.master.json. This contains all allowed tileIds and makes the association between
tileIds and attributes.

Each element of the Tiles array may the zero base index of the element. It is not required, but if it is present,
the engine will throw an exception if it is not correct. This optional property can be helpful for debugging.

```json
{
   "Tiles" [
       {
           "[Index]": "the zero based index of the tile"
           "TileId": "the tile's unique string identifier",
           "Attributes": [ "blocks-light", "blocks-player", "etc" ]
       },
       {
           ... 
       },
       {
           ...
       }
   ]
}
```

## tilesheet.json - Tile data json Files

Makes an association between tileIds and pixels.

To make the association, the tilesheet.json file contains a BmpFilename property that points
to the .bmp that will contain the pixel data that is referenced by the tile location data.

The TileWidth and the TileHight properties indicate the size of all of the tiles in the tilesheet,
each tile being the same size. 

The tiles in the bmp must be side by side - there can be no gaps between them. Taking this
into account, each element in the tiles array only needs to indicate the X,Y location of the
tile, not the entire rect.

Furthermore, this x,y location is a zero based location in tiles, not pixels.

```json
{
   "BmpFilename": "filname.bmp",
   "TileWidth":   "width of tiles",
   "TileHeight":  "height of tiles",
   "Tiles" [
       {
           "X": "x location of the tile",
           "Y": "y location of the tile",
           "TileId": "unique tile identifier",
       },
       {
           ... 
       },
       {
           ...
       }
   ]
}
```

The location of a tile given by { x. y } must exist in 'filename.bmp'. However, there is no rule that every
location in a bmp is accounted for in the json file. Further any number of tile_ids are allowed to point to
a single tile location in the bmp. This allows tile_ids that differ by name and attributes to have the
same pixels.

## bmp - Tile BMP files

Pixel data is stored in a 24 bit BMP file. The BMP file must follow these rules:

1) Every tile in the BMP is of the same dimensions.
2) The width of the BMP must be a multiple of the tile width.
3) The height of the BMP must be a multiple of the tile height.
4) The tiles must be side by side. There can be no gaps between them.

## Compiled tilesheets

Tile sheets have two flavors. The 'hand built' BMPs and json files that are located in the 
BasicAsset.Tiles/tilesheet/ directories, and compiled tile sheets that are built by composing the 
hand built tile sheets into larger tile sheets. 

## .map.json && .map.data files

A .map.json and .map.dat files contain generated map data. This data can be generated using some
a procedural algorithm that creates map data at an the level of individual tiles, or by working with
pre-defined subsections of maps created by hand (map parts).

As just mentioned, map data is stored in two parts. 

1) A .map.json file that contains human readable meta data, such as the compiled tile sheet
filename, exit location targets etc. 
2) A .map.data file that contains just the tile indices. Tile indices are indices into the compiled
tile sheet identified by the .map.json file.

## TileSet objects

TileSet objects are used during rendering to map tile indices to pixels. A tile set contains an
SDL_Texture that contains the pixels, and an array of SDL_Rect objects. Each rect in this array
holds the location of pixels in the tile texture for a tile.

The indicies in Surfaces, views, and maps (excluding MapParts) are indicies into this SDL_Rect array. 

## Tile Palettes

A tile palette is a mapping between arbitrary characters "a, b, c, 8, etc" and a tile_id. These mappings are
used by map part json files when defining the tiles in a the map section being described in the map part
file.

a palette has a simple structure:

```json
{
    "map":
    [
        {
            "char": "x",
            "tile_id": "id_a"
        },
        {
            "char":    "y",
            "tile_id": "id_b"
        }

    ]
}
```

Given this palette a map part file can define its data by first referencing this tile sheet, and then using the
characters defined by this tile sheet in its 'data section'

# map parts

Map parts are small sections of a map, having a predefined size (and maybe other attributes). Map parts can be used to
build the actual .map.json and .map.data files that are used by the engine.

## How to change a tile set once a map/compiled tilesheet association had been made.

A generated map contains indicies into a compiled tilesheet. Therefore once a map has been created and associated with
a tilesheet, the tiles that are in that compiled tile sheet and the order of those tiles cannot be changed.

However, it is going to have to be possible to re-associate each tile in an already compiled tilesheet with pixel data
from another tilesheet. This will be possible since for each tile element in the compiled tile sheet, we have the the unique
tileId for each element. All we need to do is rebuild the bmp and re-map the indices. 

The only issue is that there is no guarentee that the tile set contains all of the tiles in the already compiled tile sheet,
if this happens defaults must be used.

# default tiles images.

Default tile images can be used when a tile set does not contain pixel data for a tile. There needs to be tiles that can 
he used for a few different attributes. For example a tile with the BlocksPlayer attribute needs a image that looks something
like a wall, and a tile that does not block light, should look something like a floor.

# Steps to build and render a view

1. build a compiled tile sheet from a tile set directory
2. MapParts + TilePalettes + algorithm = a map file
    - the map file is associated with the compiled tile sheet 
        - the map file contains the filepath of the compiled tilesheet
    - the tile indices in the map file reference the position a tile in the compiled tile sheet.
3. create a tileset from the compiled tilesheet
    - Since the tile indices in the map file reference the position of the tile in the compiled tile
      sheet the positions of the indicies in the tileset must match the position of the tiles in the
      compiled tile sheet.
    - Note that the tile set contains only the texture and the texture location data for the tiles. Other information
      about the tiles including the tile attributes are held in the surface at runtime.
4. Load a surface from the map file.
5. Render a view from the surface.


2. TilePalette

# Tiles are supported by the following classes

- mp::SDL:SDLTileSheet
- mp::

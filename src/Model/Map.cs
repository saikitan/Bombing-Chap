using System.IO;
using System.Collections.Generic;

namespace MyGame
{
    /// <summary>
    ///     Map of the game, it update and draw all the things of map (except Player and bomb)
    /// </summary>
    public static class Map
    {
        // Location of the map
        public const int MAP_X = 8;
        public const int MAP_Y = 70;
        // Tile count
        private static int _xTileCount;
        private static int _yTileCount;
        private static Tile [,] _tiles;
        // PowerUps that are on the map
        private static List<PowerUps> _powerUps = new List<PowerUps> ();
        // Tiles that are exploded
        private static List<ExplodableTile> _explodedTiles = new List<ExplodableTile> ();
        

        public static Tile [,] Tiles
        {
            get
            {
                return _tiles;
            }
            
        }

        public static List<PowerUps> PowerUps
        {
            get
            {
                return _powerUps;
            }
            
        }

        public static List<ExplodableTile> ExplodedTiles {
            get {
                return _explodedTiles;
            }
        }

        public static int xTileCount {
            get {
                return _xTileCount;
            }
        }

        public static int yTileCount {
            get {
                return _yTileCount;
            }
        }

        /// <summary>
        ///     Load the map of the game
        /// </summary>
        /// <param name="map">Map's filename</param>
        public static void LoadMap (string map)
        {
            StreamReader reader = new StreamReader (map);
            _yTileCount = reader.ReadInteger ();
            _xTileCount = reader.ReadInteger ();

            // Create a two dimension array with the tile count read from the file
            _tiles = new Tile [_yTileCount, _xTileCount];

            // Create the tile according to the symbol in the map file
            for (int i = 0; i < _yTileCount; i++)
            {
                string line = reader.ReadLine ();

                for (int j = 0; j < _xTileCount; j++)
                {
                    switch (line [j]) // Access the specific character in the line
                    {
                        case '.':
                            _tiles [i, j] = new NormalTile (MAP_X + (j * GameResources.TILE_DIMENSION), MAP_Y + (i * GameResources.TILE_DIMENSION));
                            break;
                        case '*':
                            _tiles [i, j] = new ExplodableTile (MAP_X + (j * GameResources.TILE_DIMENSION), MAP_Y + (i * GameResources.TILE_DIMENSION));
                            break;
                        case 'x':
                            _tiles [i, j] = new SolidTile (MAP_X + (j * GameResources.TILE_DIMENSION), MAP_Y + (i * GameResources.TILE_DIMENSION));
                            break;
                    }
                    
                }
            }
        }

        /// <summary>
        ///     Draw the map on screen
        /// </summary>
        public static void Draw()
        { 
            foreach (Tile tile in _tiles) {
                tile.Draw ();
            }

            foreach (PowerUps powerUp in _powerUps)
            {
                powerUp.Draw();
            }
        }

        /// <summary>
        ///     Update all items of the map
        /// </summary>
        public static void Update()
        {
            foreach (Tile tile in _tiles)
            {
                tile.Update ();
            }

            // Change the exploded tile into normal tile
            foreach(ExplodableTile tile in _explodedTiles)
            {
                int tileX;
                int tileY;

                UtilityFunctions.GetTilePosition (tile.X, tile.Y, out tileX, out tileY);

                _tiles [tileY, tileX] = new NormalTile (tile.X, tile.Y);
            }

            // Clear the list of exploded tiles
            _explodedTiles.Clear ();
        }

        /// <summary>
        ///     Remove the old map and everything that are bind to the map such as PowerUps
        /// </summary>
        public static void FreeMap()
        {
            _tiles = null;
            _powerUps.Clear ();
            _explodedTiles.Clear();
        }
    }
}
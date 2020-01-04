using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public static class GameResources
    {
        /// <summary>
        ///     Declare all the variables and constants required
        /// </summary>
        public const int TILE_DIMENSION = 64;
        public const int BOMB_DIMENSION = 48;
        public const int POWER_UP_DIMENSION = 32;
        public const int FLAME_DIMENSION = 48;
        public const int PLAYER_WIDTH = 50;
        public const int PLAYER_HEIGHT = 64;
        private static Dictionary<string, Bitmap> _images = new Dictionary<string, Bitmap> ();
        private static Dictionary<string, Music> _musics = new Dictionary<string, Music> ();
        private static Dictionary<string, Font> _fonts = new Dictionary<string, Font> ();
        private static Dictionary<string, SoundEffect> _sounds = new Dictionary<string, SoundEffect> ();

        public static Bitmap Image(string name)
        {
            return _images [name];
        }

        public static Font Font (string name)
        {
            return _fonts [name];
        }

        public static Music Music (string name)
        {
            return _musics [name];
        }

        public static SoundEffect SoundEffect (string name)
        {
            return _sounds [name];
        }

        /// <summary>
        ///     Loads all the resources required to use in the game
        /// </summary>
        public static void LoadResources ()
        {
            LoadImages ();
            LoadFonts ();
            LoadMusics ();
            LoadSoundEffects ();
        }

        /// <summary>
        ///     Loads all the images required to use in the game
        /// </summary>
        private static void LoadImages()
        {
            //Screen
            NewImage ("mainMenu", "menu.png");
            NewImage ("credit", "credit.png");
            NewImage ("how", "how.png");
            NewImage ("endGame", "endgame.png");
            NewImage ("endGameDraw", "endgamedraw.png");


            // Tile images
            NewImage ("normalTile", "BackgroundTile.png");
            NewImage ("explodableTile", "ExplodableBlock.png");
            NewImage ("solidTile", "SolidBlock.png");

            // PowerUps Images
            NewImage ("bombPowerUp", "BombPowerup.png");
            NewImage ("flamePowerUp", "FlamePowerup.png");
            NewImage ("speedPowerUp", "SpeedPowerup.png");

            // Bomb images
            NewImage ("bomb1", "Bomb_f01.png");
            NewImage ("bomb2", "Bomb_f02.png");

            // Flame images
            NewImage ("flame1", "Flame_f00.png");
            NewImage ("flame2", "Flame_f01.png");
            NewImage ("flame3", "Flame_f02.png");
            NewImage ("flame4", "Flame_f03.png");
            NewImage ("flame5", "Flame_f04.png");

            // Player Image - Yellow (f: front, b: back, l:left, r:right)
            NewImage ("yellowF1", "yellow_f1.png");
            NewImage ("yellowF2", "yellow_f2.png");
            NewImage ("yellowF3", "yellow_f3.png");
            NewImage ("yellowB1", "yellow_b1.png");
            NewImage ("yellowB2", "yellow_b2.png");
            NewImage ("yellowB3", "yellow_b3.png");
            NewImage ("yellowL1", "yellow_l1.png");
            NewImage ("yellowL2", "yellow_l2.png");
            NewImage ("yellowL3", "yellow_l3.png");
            NewImage ("yellowR1", "yellow_r1.png");
            NewImage ("yellowR2", "yellow_r2.png");
            NewImage ("yellowR3", "yellow_r3.png");
            NewImage ("yellowS", "yellowS.png");

            // Player Image - Pink (f: front, b: back, l:left, r:right)
            NewImage ("pinkF1", "pink_f1.png");
            NewImage ("pinkF2", "pink_f2.png");
            NewImage ("pinkF3", "pink_f3.png");
            NewImage ("pinkB1", "pink_b1.png");
            NewImage ("pinkB2", "pink_b2.png");
            NewImage ("pinkB3", "pink_b3.png");
            NewImage ("pinkL1", "pink_l1.png");
            NewImage ("pinkL2", "pink_l2.png");
            NewImage ("pinkL3", "pink_l3.png");
            NewImage ("pinkR1", "pink_r1.png");
            NewImage ("pinkR2", "pink_r2.png");
            NewImage ("pinkR3", "pink_r3.png");
            NewImage ("pinkS", "pinkS.png");
        }

        /// <summary>
        ///     Loads all the fonts required to use in the game
        /// </summary>
        private static void LoadFonts()
        {
            NewFont ("Bencoleng", "Bencoleng.otf", 30);
        }

        /// <summary>
        ///     Loads all the musics required to use in the game
        /// </summary>
        private static void LoadMusics()
        {
            NewMusic ("main", "Wallpaper.wav");
            NewMusic ("game", "Itty Bitty 8 Bit.wav");
        }

        /// <summary>
        ///     Loads all the sound effects required to use in the game
        /// </summary>
        private static void LoadSoundEffects()
        {
            NewSound ("bomb", "bomb.wav");
        }

        /// <summary>
        ///     Add a new image
        /// </summary>
        /// <param name="name">Name of the image</param>
        /// <param name="filename">Filename of the image</param>
        private static void NewImage(string name, string filename)
        {
            _images.Add (name, SwinGame.LoadBitmap (filename));
        }

        /// <summary>
        ///     Add a new music
        /// </summary>
        /// <param name="name">Name of the music</param>
        /// <param name="filename">Filename of the music</param>
        private static void NewMusic(string name, string filename)
        {
            _musics.Add (name, SwinGame.LoadMusic (filename));
        }

        /// <summary>
        ///     Add a new sound effect
        /// </summary>
        /// <param name="name">Name of the sound effect</param>
        /// <param name="filename">Filename of the music</param>
        private static void NewSound (string name, string filename)
        {
            _sounds.Add (name, SwinGame.LoadSoundEffect (filename));
        }

        /// <summary>
        ///     Add a new font
        /// </summary>
        /// <param name="name">Name of the font</param>
        /// <param name="filename">Filename of the font</param>
        /// <param name="size">Size of the font</param>
        private static void NewFont (string name, string filename, int size)
        {
            _fonts.Add (name, SwinGame.LoadFont (filename, size));
        }

        
    }
}
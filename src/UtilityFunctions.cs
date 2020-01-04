using System;
using SwinGameSDK;

namespace MyGame
{
    public static class UtilityFunctions
    {
        /// <summary>
        ///     Use to draw the animation which have 2 frames
        /// </summary>
        /// <param name="name">Name of the image</param>
        /// <param name="speed">Speed of the animation (higher the value, slower the animation)</param>
        /// <param name="X">X coordinate of the image</param>
        /// <param name="Y">Y Coordinate of the image</param>
        public static void Draw2FAnimation(string name, int speed, float X, float Y)
        {
            switch(SwinGame.GetTicks () / speed % 2)
            {
                case 0:
                    SwinGame.DrawBitmap (GameResources.Image ($"{name}1"), X, Y);
                    break;
                case 1:
                    SwinGame.DrawBitmap (GameResources.Image ($"{name}2"), X, Y);
                    break;
            }
        }

        /// <summary>
        ///     Use to draw the animation which have 5 frames
        /// </summary>
        /// <param name="name">Name of the image</param>
        /// <param name="speed">Speed of the animation (higher the value, slower the animation)</param>
        /// <param name="X">X coordinate of the image</param>
        /// <param name="Y">Y coordinate of the image</param>
        public static void Draw5FAnimation (string name, int speed, float X, float Y)
        {
            switch (SwinGame.GetTicks () / speed % 5) {
                case 0:
                    SwinGame.DrawBitmap (GameResources.Image ($"{name}1"), X, Y);
                    break;
                case 1:
                    SwinGame.DrawBitmap (GameResources.Image ($"{name}2"), X, Y);
                    break;
                case 2:
                    SwinGame.DrawBitmap (GameResources.Image ($"{name}3"), X, Y);
                    break;
                case 3:
                    SwinGame.DrawBitmap (GameResources.Image ($"{name}4"), X, Y);
                    break;
                case 4:
                    SwinGame.DrawBitmap (GameResources.Image ($"{name}5"), X, Y);
                    break;
            }
        }

        /// <summary>
        ///     This method will find out which tile the item belong to
        /// </summary>
        /// <param name="X">X coordinate of the item</param>
        /// <param name="Y">Y coordinate of the item</param>
        /// <param name="tileX">Return the X of the tile</param>
        /// <param name="tileY">Return the Y of the tile</param>
        public static void GetTilePosition(float X, float Y, out int tileX, out int tileY)
        {
            tileX = (int)((X - Map.MAP_X) / GameResources.TILE_DIMENSION);
            tileY = (int)((Y - Map.MAP_Y) / GameResources.TILE_DIMENSION);
        }
    }
}
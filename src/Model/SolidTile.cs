using SwinGameSDK;

namespace MyGame
{
    public class SolidTile : Tile
    {
        public SolidTile (float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Draw the tile on screen
        /// </summary>
        public override void Draw ()
        {
            SwinGame.DrawBitmap (GameResources.Image ("solidTile"), X, Y);
        }
    }
}
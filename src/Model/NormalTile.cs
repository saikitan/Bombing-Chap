using SwinGameSDK;

namespace MyGame
{
    public class NormalTile : Tile
    {
        public NormalTile (float x, float y)
        {
            X = x;
            Y = y;
            OnFire = false;
            HaveBomb = false;
        }

        /// <summary>
        ///     Draw the tile on screen
        /// </summary>
        public override void Draw ()
        {
            SwinGame.DrawBitmap (GameResources.Image ("normalTile"), X, Y);
            if (OnFire == true)
            {
                DrawFlame ();
            }
        }
    }
}

using SwinGameSDK;

namespace MyGame
{
    public class FlamePowerUps : PowerUps
    {
        public FlamePowerUps (float x, float y)
        {
            X = x + (GameResources.TILE_DIMENSION - GameResources.POWER_UP_DIMENSION) / 2;
            Y = y + (GameResources.TILE_DIMENSION - GameResources.POWER_UP_DIMENSION) / 2;
        }

        /// <summary>
        ///     Draw the powerup on screen
        /// </summary>
        public override void Draw ()
        {
            SwinGame.DrawBitmap (GameResources.Image ("flamePowerUp"), X, Y);
        }

        /// <summary>
        ///     Apply all effects of this powerup to the player
        /// </summary>
        /// <param name="p">Player that collect this powerup</param>
        public override void Collected (Player p)
        {
            // Increase the bomb strength of the player
            p.IncreaseStrength (PlayerStrength.BombStrength);

            //Increase the player's score by 30
            p.IncreaseScore (30);

            // Ask the map to remove this powerup
            Map.PowerUps.Remove (this);
        }
    }
}

using SwinGameSDK;

namespace MyGame
{
    public class BombPowerUps : PowerUps
    {
        public BombPowerUps (float x, float y)
        {
            X = x + (GameResources.TILE_DIMENSION - GameResources.POWER_UP_DIMENSION) / 2;
            Y = y + (GameResources.TILE_DIMENSION - GameResources.POWER_UP_DIMENSION) / 2;
        }

        /// <summary>
        ///     Draw the powerup on screen
        /// </summary>
        public override void Draw ()
        {
            SwinGame.DrawBitmap (GameResources.Image ("bombPowerUp"), X, Y);
        }

        /// <summary>
        ///     Apply all effects of this powerup to the player
        /// </summary>
        /// <param name="p">Player that collect this powerup</param>
        public override void Collected (Player p)
        {
            // Increase the no of bombs player have
            p.IncreaseStrength (PlayerStrength.BombNo);
            // Increase player's score by 25
            p.IncreaseScore (25);
            // Ask the Map to remove this powerups
            Map.PowerUps.Remove (this);
        }
    }
}

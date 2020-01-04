using SwinGameSDK;

namespace MyGame
{
    public class SpeedPowerUps : PowerUps
    {
        public SpeedPowerUps(float x, float y)
        {
            X = x + (GameResources.TILE_DIMENSION - GameResources.POWER_UP_DIMENSION) / 2;
            Y = y + (GameResources.TILE_DIMENSION - GameResources.POWER_UP_DIMENSION) / 2;
        }

        /// <summary>
        ///     Draw the powerup on screen
        /// </summary>
        public override void Draw ()
        {
            SwinGame.DrawBitmap (GameResources.Image ("speedPowerUp"), X, Y);
        }

        /// <summary>
        ///     Apply all effects of this powerup to the player
        /// </summary>
        /// <param name="p">Player that collect this powerup</param>
        public override void Collected (Player p)
        {
            // Increase the moving speed of the player
            p.IncreaseStrength (PlayerStrength.MovingSpeed);

            // Increase the player's score by 20
            p.IncreaseScore (20);

            // Ask the map to remove this powerup
            Map.PowerUps.Remove (this);
        }
    }
}

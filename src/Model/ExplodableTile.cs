using System;
using SwinGameSDK;

namespace MyGame
{
    public class ExplodableTile : Tile
    {
        public ExplodableTile (float x, float y)
        {
            X = x;
            Y = y;
            OnFire = false;
        }

        /// <summary>
        ///     Draw the tile on screen
        /// </summary>
        public override void Draw ()
        {
            SwinGame.DrawBitmap (GameResources.Image ("explodableTile"), X, Y);

            // Draw the flame if the tile is on fire
            if (OnFire == true) {
                DrawFlame ();
            }
        }

        /// <summary>
        ///     Update the status of the tile (e.g. whether the tile is on fire)
        /// </summary>
        public override void Update ()
        {
            if (OnFire == true && _initTick == 0) {
                _initTick = SwinGame.GetTicks ();
            }

            if (OnFire == true && SwinGame.GetTicks () > _initTick + 1000) {
                Destroy ();

                // Add this tile to the exploded tile so that it can be change to the normal tile
                Map.ExplodedTiles.Add (this);
                OnFire = false;
            }
        }

        /// <summary>
        ///     Destroy the tile and generate new powerups
        /// </summary>
        private void Destroy ()
        {
            // Randomly generate number
            Random randomNumberGenerator = new Random ();
            int num = randomNumberGenerator.Next (100);

            // Generate a power up if the random number is less than 70
            if (num < 70) {

                // Randomly generate power up
                num = randomNumberGenerator.Next (100);
                switch (num % 9)
                { 
                    case 0:
                    case 5:
                    case 7:
                    case 8:
                        Map.PowerUps.Add(new BombPowerUps(X, Y));
                        break;
                    case 1:
                    case 3:
                    case 6:
                    case 9:
                        Map.PowerUps.Add(new FlamePowerUps(X, Y));
                        break;
                    case 2:
                    case 4:
                        Map.PowerUps.Add(new SpeedPowerUps(X, Y));
                        break;
                }
            }
        }
    }
}

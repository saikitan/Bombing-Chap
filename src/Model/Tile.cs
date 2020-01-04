using SwinGameSDK;

namespace MyGame
{
    public abstract class Tile : GameObject
    {
        protected uint _initTick = 0;

        public bool HaveBomb {
            get; set;
        }

        public bool OnFire
        {
            get; set;
        }

        /// <summary>
        ///     Draw the flame on the tile
        /// </summary>
        protected void DrawFlame()
        {
            UtilityFunctions.Draw5FAnimation ("flame", 300, X + 8, Y + 8);
        }

        /// <summary>
        ///     Update the status of the tile (e.g. whether it is on fire)
        /// </summary>
        public virtual void Update ()
        {
            if (OnFire == true) {
                if (_initTick == 0) {
                    _initTick = SwinGame.GetTicks ();
                }
                else if (SwinGame.GetTicks() > _initTick + 1000) {
                    _initTick = 0;
                    OnFire = false;
                }
            }
        }
    }
}

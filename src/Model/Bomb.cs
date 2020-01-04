using SwinGameSDK;

namespace MyGame
{
    public class Bomb : GameObject
    {
        private int _tileX;
        private int _tileY;
        private int _strength;
        private bool _exploded = false;
        // Use to record the ticks of the program when create the object
        private uint _initTick;

        public Bomb (int tileX, int tileY, int strength)
        {
            _tileX = tileX;
            _tileY = tileY;

            // Inform the tile that it has bomb on it
            Map.Tiles [_tileY, _tileX].HaveBomb = true;

            X = Map.Tiles [_tileY, _tileX].X + (GameResources.TILE_DIMENSION - GameResources.BOMB_DIMENSION)/2;
            Y = Map.Tiles [_tileY, _tileX].Y + (GameResources.TILE_DIMENSION - GameResources.BOMB_DIMENSION)/2;
            _strength = strength;
            _initTick = SwinGame.GetTicks ();
        }

        public bool Exploded
        {
            get
            {
                return _exploded;
            }
        }

        /// <summary>
        ///     Draw the bomb on screen
        /// </summary>
        public override void Draw ()
        {
            UtilityFunctions.Draw2FAnimation ("bomb", 230, X, Y);
        }


        /// <summary>
        ///     This method will ask the bomb to explode
        /// </summary>
        /// <param name="p">Player of the bomb</param>
        public void Explode (Player p)
        {
            // Explode the bomb after 3 seconds
            if (SwinGame.GetTicks() > _initTick + 3000 && _exploded == false)
            {
                SwinGame.PlaySoundEffect(GameResources.SoundEffect("bomb"));

                // Put fire on the left tiles
                for (int i = _tileX; i >= _tileX - _strength; i--)
                {
                    if (Map.Tiles[_tileY, i] is SolidTile)
                    {
                        break;
                    }
                    else
                    {
                        Map.Tiles [_tileY, i].OnFire = true;
                        if (Map.Tiles [_tileY, i] is ExplodableTile)
                        {
                            p.IncreaseScore (10);
                            break;
                        }
                    }
                }

                // Put fire on the right tiles
                for (int i = _tileX + 1; i <= _tileX + _strength; i++)
                {
                    if (Map.Tiles [_tileY, i] is SolidTile) {
                        break;
                    }
                    else
                    {
                        Map.Tiles [_tileY, i].OnFire = true;
                        if (Map.Tiles [_tileY, i] is ExplodableTile)
                        {
                            p.IncreaseScore (10);
                            break;
                        }
                    }
                }

                // Put fire on the upper tiles
                for (int i = _tileY; i >= _tileY - _strength; i--) {
                    if (Map.Tiles [i, _tileX] is SolidTile) {
                        break;
                    }
                    else
                    {
                        Map.Tiles [i, _tileX].OnFire = true;
                        if (Map.Tiles [i, _tileX] is ExplodableTile) {
                            p.IncreaseScore (10);
                            break;
                        }
                    }
                }

                // Put fire on the lower tiles
                for (int i = _tileY + 1; i <= _tileY + _strength; i++) {
                    if (Map.Tiles [i, _tileX] is SolidTile) {
                        break;
                    }
                    else
                    {
                        Map.Tiles [i, _tileX].OnFire = true;
                        if (Map.Tiles [i, _tileX] is ExplodableTile) {
                            p.IncreaseScore (10);
                            break;
                        }
                    }
                }

                
                _exploded = true;

                // Inform the tile that it has no bomb on it
                Map.Tiles [_tileY, _tileX].HaveBomb = false;
            }
        }
    }
}

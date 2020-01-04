using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public class Player : GameObject
    {
        private PlayerDirection _currentDirection = PlayerDirection.Down;
        private PlayerState _currentState = PlayerState.Standing;
        private string _character; // Image for this player
        private bool _alive = true;
        private int _score = 0;
        private int _numOfBombs = 1;
        private int _bombStrength = 1;
        private int _movingSpeed = 2;
        // A list of bomb that have been deployed
        private List<Bomb> _bombs = new List<Bomb> ();

        // A list of bomb that should be ignore for the collision test
        private static List<Bomb> _ignoreBombs = new List<Bomb> ();

        public Player (string character, float x, float y)
        {
            _character = character;
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Use to determine what is the current state of a player
        /// </summary>
        private enum PlayerState
        {
            Standing,
            Walking
        };

        public List<Bomb> Bombs
        {
            get
            {
                return _bombs;
            }
        }

        public static List<Bomb> IgnoreBombs {
            get {
                return _ignoreBombs;
            }
        }

        public string Character {
            get {
                return _character;
            }
        }

        public int Score {
            get {
                return _score;
            }
        }

        public bool Alive {
            get {
                return _alive;
            }
        }

        /// <summary>
        ///     This method will change the player state to Standing
        /// </summary>
        public void StopMove()
        {
            _currentState = PlayerState.Standing;
        }

        /// <summary>
        ///     This method will move the player around the map according to which direction he is going
        ///     (if there is no obstacles)
        /// </summary>
        /// <param name="direction">Moving direction</param>
        public void Move(PlayerDirection direction)
        {
            _currentState = PlayerState.Walking;

            for (int i = 1; i <= _movingSpeed; i += 1) {
                Tile tempTile;

                switch (direction) {
                    case PlayerDirection.Left:
                        tempTile = CollisionWithTiles (-1, 0);

                        if (tempTile != null) {

                            // Auto move the player if he is on the edge of the tile
                            if (Y + 44 < tempTile.Y) {
                                Y -= 1;
                            }
                            else if (Y + 20 > tempTile.Y + 64) {
                                Y += 1;
                            }

                        }
                        else if (!CollisionWithBombs(-1,0)) {
                            X -= 1;
                        }
                        _currentDirection = PlayerDirection.Left;
                        break;

                    case PlayerDirection.Right:
                        tempTile = CollisionWithTiles (1, 0);
                        if (tempTile != null) {

                            // Auto move the player if he is on the edge of the tile
                            if (Y + 44 < tempTile.Y) {
                                Y -= 1;
                            } else if (Y + 20 > tempTile.Y + 64) {
                                Y += 1;
                            }

                        }
                        else if (!CollisionWithBombs (1, 0)) {
                            X += 1;
                        }
                        _currentDirection = PlayerDirection.Right;
                        break;

                    case PlayerDirection.Up:
                        tempTile = CollisionWithTiles (0, -1);
                        if (tempTile != null) {

                            // Auto move the player if he is on the edge of the tile
                            if (X + 30 < tempTile.X) {
                                X -= 1;
                            } else if (X + 20 > tempTile.X + 64) {
                                X += 1;
                            }

                        }
                        else if (!CollisionWithBombs (0, -1))
                        {
                            Y -= 1;
                        }
                        _currentDirection = PlayerDirection.Up;
                        break;

                    case PlayerDirection.Down:
                        tempTile = CollisionWithTiles (0, 1);
                        if (tempTile != null) {

                            // Auto move the player if he is on the edge of the tile
                            if (X + 30 < tempTile.X) {
                                X -= 1;
                            } else if (X + 20 > tempTile.X + 64) {
                                X += 1;
                            }

                        }
                        else if (!CollisionWithBombs (0, 1))
                        {
                            Y += 1;
                        }
                        _currentDirection = PlayerDirection.Down;
                        break;
                }
            }
        }

        /// <summary>
        ///     Ask the player to put bomb on tile
        /// </summary>
        public void PutBomb()
        {
            int tileX;
            int tileY;

            // Get the position of the tile relative to the center of the player
            UtilityFunctions.GetTilePosition (X + (GameResources.PLAYER_WIDTH/2), Y + (GameResources.PLAYER_HEIGHT / 2), out tileX, out tileY);

            // Put a bomb on the tile if player have enough bomb and the tile is vacant
            if (_bombs.Count < _numOfBombs && Map.Tiles[tileY,tileX].HaveBomb == false)
            {
                Bomb bomb = new Bomb (tileX, tileY, _bombStrength);
                _bombs.Add (bomb);
                _ignoreBombs.Add (bomb);
            }
        }

        /// <summary>
        ///     Increase the strength of the player
        /// </summary>
        /// <param name="kind">Strength of the player</param>
        public void IncreaseStrength (PlayerStrength kind)
        {
            switch (kind)
            {
                case PlayerStrength.BombNo:
                    _numOfBombs += 1;
                    break;
                case PlayerStrength.BombStrength:
                    _bombStrength += 1;
                    break;
                case PlayerStrength.MovingSpeed:
                        _movingSpeed += 1;
                    break;
            }
        }

        /// <summary>
        ///     Update everything of player
        /// </summary>
        public void Update()
        {
            PowerUps collectedPowerUps;
            List<Bomb> _explodedBombs = new List<Bomb>();
            int tileX;
            int tileY;

            // Get the position of the tile that the player at
            UtilityFunctions.GetTilePosition (X + (GameResources.PLAYER_WIDTH / 2), Y + (GameResources.PLAYER_HEIGHT / 2), out tileX, out tileY);

            // Player dead if the tile he at is on fire
            if (Map.Tiles[tileY, tileX].OnFire) {
                _alive = false;
            }

            // Explode all the bombs of the player
            foreach (Bomb bomb in _bombs) {
                if (bomb.Exploded)
                {
                    _explodedBombs.Add(bomb);
                }
                else {
                    bomb.Explode (this);
                }
            }

            // Remove the exploded bomb from the player
            foreach (Bomb bomb in _explodedBombs)
            {
                _bombs.Remove(bomb);
            }

            // Check whether player have collected powerup
            collectedPowerUps = CollectPowerUps ();

            if (collectedPowerUps != null) {
                collectedPowerUps.Collected (this);
            }


        }

        /// <summary>
        ///     Draw the player on screen
        /// </summary>
        public override void Draw ()
        {
            string direction;

            switch (_currentDirection) {
            case PlayerDirection.Up:
                direction = "B";
                break;
            case PlayerDirection.Left:
                direction = "L";
                break;
            case PlayerDirection.Right:
                direction = "R";
                break;
            default:
                direction = "F";
                break;
            }

            if (_currentState == PlayerState.Standing) {
                SwinGame.DrawBitmap (GameResources.Image ($"{_character}{direction}3"), X, Y);
            } else {
                UtilityFunctions.Draw2FAnimation ($"{_character}{direction}", 200, X, Y);
            }

        }

        /// <summary>
        ///     Increase the score of the player
        /// </summary>
        /// <param name="value">Value to be added to the score</param>
        public void IncreaseScore (int value)
        {
            _score += value;
        }

        /// <summary>
        ///     Check whether the player will collide with the tile in the next move
        /// </summary>
        /// <param name="velocityX">Velocity of player in X direction</param>
        /// <param name="velocityY">Velocity of player in Y direction</param>
        /// <returns>Tile that the player will collide. If no collision, return null</returns>
        private Tile CollisionWithTiles (float velocityX, float velocityY)
        {
            foreach (Tile tile in Map.Tiles) {
                if (tile is SolidTile || tile is ExplodableTile) {
                    if (SwinGame.BitmapCollision (GameResources.Image ($"{_character}F3"), X + velocityX , Y + velocityY , GameResources.Image ("solidTile"), tile.X, tile.Y))
                    {
                        return tile;
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Check whether the player will collide with the bomb in the next move
        /// </summary>
        /// <param name="velocityX">Velocity of player in X direction</param>
        /// <param name="velocityY">Velocity of player in Y direction</param>
        /// <returns>True if collision occur, otherwise false</returns>
        private bool CollisionWithBombs(float velocityX, float velocityY)
        {
            foreach (Player p in GameController.CurrentGame.Players) {
                foreach (Bomb bomb in p.Bombs) {
                    // Ignore the bomb in the list
                    if (_ignoreBombs.Contains(bomb)) {
                        continue;
                    }

                    if (SwinGame.BitmapCollision (GameResources.Image ($"{_character}F3"), X + velocityX, Y + velocityY, GameResources.Image ("bomb1"), bomb.X, bomb.Y)) {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        ///     Check whether the play collect the powerup
        /// </summary>
        /// <returns>Powerup that the player collected. If no powerup collected, return null</returns>
        private PowerUps CollectPowerUps()
        {
            foreach (PowerUps powerup in Map.PowerUps) {
                if (SwinGame.BitmapCollision (GameResources.Image ($"{_character}F3"), X, Y, GameResources.Image ("bombPowerUp"), powerup.X, powerup.Y)) {
                    return powerup;
                }
            }

            return null;
        }

    }
}

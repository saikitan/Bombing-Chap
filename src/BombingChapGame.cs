using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public class BombingChapGame
    {
        // Gaming time 
        private const int GAME_TIME = 5;
        private List<Player> _players = new List<Player> ();
        private bool _gameOver = false;
        private bool _timeUp = false;
        private Timer _timer = new Timer();

        // Reason the game end
        private enum EndGameReason
        {
            Dead,
            TimeUp
        }

        public BombingChapGame ()
        {
            // Clear the old map
            Map.FreeMap ();

            // Load New Map
            Map.LoadMap ("map.txt");

            // Set Up the players
            _players.Add (new Player ("yellow", Map.MAP_X + GameResources.TILE_DIMENSION + ((GameResources.TILE_DIMENSION - GameResources.PLAYER_WIDTH)/2), Map.MAP_Y + GameResources.TILE_DIMENSION));
            _players.Add (new Player ("pink", Map.MAP_X + ((Map.xTileCount - 2) * GameResources.TILE_DIMENSION) + ((GameResources.TILE_DIMENSION - GameResources.PLAYER_WIDTH) / 2), Map.MAP_Y + ((Map.yTileCount - 2) * GameResources.TILE_DIMENSION)));
            SwinGame.PlayMusic (GameResources.Music ("game"));
            _timer.Start ();
        }

        public List<Player> Players {
            get {
                return _players;
            }
        }

        /// <summary>
        ///     This method will call another method according what users press
        /// </summary>
        public void HandleUserInput ()
        {
            // Player 1 Control
            if (SwinGame.KeyDown(KeyCode.WKey))
            {
                _players[0].Move(PlayerDirection.Up);
            }
            else if (SwinGame.KeyDown(KeyCode.SKey))
            {
                _players[0].Move(PlayerDirection.Down);
            }
            else if (SwinGame.KeyDown(KeyCode.AKey))
            {
                _players[0].Move(PlayerDirection.Left);
            }
            else if (SwinGame.KeyDown(KeyCode.DKey))
            {
                _players[0].Move(PlayerDirection.Right);
            }
            else
            {
                _players[0].StopMove();
            }

            if (SwinGame.KeyTyped(KeyCode.SpaceKey))
            {
                _players[0].PutBomb();
            }

            // Player 2 Control
            if (SwinGame.KeyDown (KeyCode.UpKey)) {
                _players [1].Move (PlayerDirection.Up);
            } else if (SwinGame.KeyDown (KeyCode.DownKey)) {
                _players [1].Move (PlayerDirection.Down);
            } else if (SwinGame.KeyDown (KeyCode.LeftKey)) {
                _players [1].Move (PlayerDirection.Left);
            } else if (SwinGame.KeyDown (KeyCode.RightKey)) {
                _players [1].Move (PlayerDirection.Right);
            } else {
                _players [1].StopMove ();
            }

            if (SwinGame.KeyTyped (KeyCode.LKey)) {
                _players [1].PutBomb ();
            }

            if (!_gameOver) {
                UpdateGame ();
            }
            
        }

        /// <summary>
        ///     This method will remove the bomb from the ignore bomb list if both of the players no longer collide with the bomb
        /// </summary>
        private void RemoveIgnoreBomb ()
        {
            // This list is use to remove the bomb from another list
            List<Bomb> tempBombList = new List<Bomb> ();

            // Check whether the player collide with the ignore bomb
            foreach (Bomb bomb in Player.IgnoreBombs) {
                if (!(SwinGame.BitmapCollision (GameResources.Image ($"{_players [0].Character}F3"), _players [0].X, _players [0].Y, GameResources.Image ("bomb1"), bomb.X, bomb.Y) || SwinGame.BitmapCollision (GameResources.Image ($"{_players [1].Character}F3"), _players [1].X, _players [1].Y, GameResources.Image ("bomb1"), bomb.X, bomb.Y))) {
                    tempBombList.Add (bomb);
                }
            }

            // Remove the bomb from the list
            foreach (Bomb bomb in tempBombList) {
                Player.IgnoreBombs.Remove (bomb);
            }
        }


        /// <summary>
        ///     This method will update everything in the game
        /// </summary>
        private void UpdateGame ()
        { 
            RemoveIgnoreBomb ();

            if (_timeUp) {
                EndGame (EndGameReason.TimeUp);
            }

            foreach (Player p in _players) {
                if (!p.Alive) {
                    EndGame (EndGameReason.Dead);
                }
                p.Update ();
            }

            Map.Update ();
        }

        /// <summary>
        ///     This method will draw everything of the game on screen
        /// </summary>
        public void DrawScreen ()
        {
            DrawScoreBoard();
            DrawTimer();
            Map.Draw ();
            foreach (Player p in _players)
            {
                foreach (Bomb bomb in p.Bombs)
                {
                    bomb.Draw();
                }
            }
            foreach (Player p in _players) {
                p.Draw ();
            }
        }

        /// <summary>
        ///     This method will display the score of each player on the screen
        /// </summary>
        private void DrawScoreBoard()
        {
            // Box position and size
            int x1 = 90;
            int y = 20;
            int x2 = 527;
            int boxWidth = 100;
            int boxHeight = 34;

            // Player 1 Score
            SwinGame.FillRectangle(Color.Black, x1, y, boxWidth, boxHeight);
            SwinGame.FillRectangle(SwinGame.RGBColor(88,116,86), x1 + 2, y + 2, boxWidth - 4, boxHeight - 4);
            SwinGame.DrawBitmap(GameResources.Image("yellowS"), 35, 10);
            SwinGame.DrawText ($"{_players [0].Score}", Color.White, GameResources.Font ("Bencoleng"), x1 + 8, y);

            // Player 2 Score
            SwinGame.FillRectangle(Color.Black, x2, y, boxWidth, boxHeight);
            SwinGame.FillRectangle(SwinGame.RGBColor(88, 116, 86), x2 + 2, y + 2, boxWidth - 4, boxHeight - 4);
            SwinGame.DrawBitmap(GameResources.Image("pinkS"), 647, 10);
            SwinGame.DrawText($"{_players[1].Score}", Color.White, GameResources.Font("Bencoleng"), x2 + 8, y);
        }


        /// <summary>
        ///     This method will display the time left on the screen
        /// </summary>
        private void DrawTimer()
        {
            // Time left
            double minsLeft;
            double secsLeft;

            // Box position and size
            int x = 310;
            int y = 20;
            int boxWidth = 100;
            int boxHeight = 34;

            // Calculate the time left
            CalculateTimeLeft (out minsLeft, out secsLeft);

            SwinGame.FillRectangle(Color.Black, x, y, boxWidth, boxHeight);
            SwinGame.FillRectangle(SwinGame.RGBColor(88, 116, 86), x + 2, y + 2, boxWidth - 4, boxHeight - 4);
            if (secsLeft < 10)
            {
                SwinGame.DrawText($"{minsLeft} : 0{secsLeft}", Color.White, GameResources.Font("Bencoleng"), x + 20, y);
            }
            else
            {
                SwinGame.DrawText($"{minsLeft} : {secsLeft}", Color.White, GameResources.Font("Bencoleng"), x + 20, y);
            }
               
        }

        /// <summary>
        ///     Calculate the time left
        /// </summary>
        /// <param name="minsLeft">Return the minutes left</param>
        /// <param name="secsLeft">Return the seconds left</param>
        private void CalculateTimeLeft(out double minsLeft, out double secsLeft)
        {
            uint timeLeft = 0;

            if (_timer.Ticks < GAME_TIME * 60000)
            {
                timeLeft = (GAME_TIME * 60000) - _timer.Ticks;
            }
            else {
                _timeUp = true;
            }
            

            minsLeft = Math.Floor(Convert.ToDouble(timeLeft / 60000));
            secsLeft = Math.Floor (Convert.ToDouble (timeLeft % 60000 / 1000));
        }

        /// <summary>
        ///     This method will find out the winner and end the game
        /// </summary>
        /// <param name="reason"></param>
        private void EndGame(EndGameReason reason)
        {
            switch(reason) {
                case EndGameReason.Dead:
                    foreach(Player p in _players) {
                        if (p.Alive) {
                            GameController.Winner = p.Character;
                        }
                    }
                break;
                case EndGameReason.TimeUp:
                    if (_players[0].Score > _players [1].Score) {
                        GameController.Winner = _players[0].Character;
                    }
                    else if (_players[0].Score < _players[1].Score)
                    {
                        GameController.Winner = _players [1].Character;
                    }
                    else {
                        GameController.Winner = null;
                    }
                    break;
            }

            GameController.EndGame ();
        }

    }
}

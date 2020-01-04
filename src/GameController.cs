using System;
using SwinGameSDK;

namespace MyGame
{
    public static class GameController
    {
        private static GameState _currentState = GameState.MainMenu;
        private static BombingChapGame _game;
        private static Timer _timer = new Timer();

        public static BombingChapGame CurrentGame {
            get {
                return _game;
            }
        }

        public static string Winner {
            get;set;
        }

        /// <summary>
        ///     This method will call another method according what users press
        /// </summary>
        public static void HandleUserInput ()
        {
            switch (_currentState) {
                case GameState.MainMenu:
                    HandleInputInMenu ();
                    break;
                case GameState.Credit:
                    HandleInputInCredit ();
                    break;
                case GameState.HowToPlay:
                    HandleInputInHowToPlay ();
                    break;
                case GameState.Playing:
                    _game.HandleUserInput ();
                    break;
                case GameState.EndGame:
                    HandleInputInEndScreen ();
                break;
            }
        }

        /// <summary>
        ///     This method will draw the screen that correspond to the status of the game
        /// </summary>
        public static void DrawScreen ()
        {
            switch (_currentState) {
                case GameState.MainMenu:
                    DrawScreen("mainMenu");
                    break;
                case GameState.Credit:
                    DrawScreen ("credit");
                    break;
                case GameState.HowToPlay:
                    DrawScreen ("how");
                    break;
                case GameState.Playing:
                    _game.DrawScreen ();
                    break;
                case GameState.EndGame:
                    DrawEndScreen ();
                    break;
            }
        }

        /// <summary>
        ///     This method will start a new game
        /// </summary>
        public static void StartGame ()
        {
            _game = new BombingChapGame ();
            _currentState = GameState.Playing;
        }

        /// <summary>
        ///     This method will end the current game
        /// </summary>
        public static void EndGame ()
        {
            _currentState = GameState.EndGame;
            SwinGame.PlayMusic (GameResources.Music ("main"));
            _game = null;
            _timer.Start ();
        }

        /// <summary>
        ///     The method will handle the user input in the main menu
        /// </summary>
        private static void HandleInputInMenu()
        {
            if (SwinGame.KeyTyped (KeyCode.SpaceKey)) {
                _currentState = GameState.HowToPlay;
                _timer.Reset();
                _timer.Start();
            } else if (SwinGame.KeyTyped (KeyCode.CKey)) {
                _currentState = GameState.Credit;
            }
        }

        /// <summary>
        ///     The method will handle the user input in the credit page
        /// </summary>
        private static void HandleInputInCredit ()
        {
            if (SwinGame.KeyTyped (KeyCode.BKey)) {
                _currentState = GameState.MainMenu;
            }
        }

        /// <summary>
        ///     The method will handle the user input in the instruction page
        /// </summary>
        private static void HandleInputInHowToPlay ()
        {
            if (_timer.Ticks > 5000) {
                _timer.Stop ();
                _timer.Reset ();
                StartGame ();
            } else if (SwinGame.KeyTyped (KeyCode.SpaceKey)) {
                StartGame ();
            }
        }

        /// <summary>
        ///     The method will handle the user input in the end screen
        /// </summary>
        private static void HandleInputInEndScreen()
        {
            if (_timer.Ticks > 1000)
            { 
                if (SwinGame.KeyTyped (KeyCode.SpaceKey)) {
                    _timer.Reset();
                    _timer.Start();
                    _currentState = GameState.HowToPlay;
                } else if (SwinGame.KeyTyped (KeyCode.BKey)) {
                    _currentState = GameState.MainMenu;
                }
            }
        }

        /// <summary>
        ///     This method will draw the ending screen
        /// </summary>
        private static void DrawEndScreen()
        {
            if (Winner != null) {
                DrawScreen ("endGame");
                UtilityFunctions.Draw2FAnimation ($"{Winner}F", 300, 330 , 400);
            }
            else {
                DrawScreen ("endGameDraw");
            }
        }

        /// <summary>
        ///     This method will draw the screen that passed in
        /// </summary>
        /// <param name="name">Screen name</param>
        private static void DrawScreen(string name)
        {
            SwinGame.DrawBitmap (GameResources.Image (name), 0, 0);
        }

    }
}
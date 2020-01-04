using System;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
            //Open the game window
            SwinGame.OpenGraphicsWindow("Bombing Chap", 720, 780);
            //SwinGame.ShowSwinGameSplashScreen();

            // Load the resources of game
            GameResources.LoadResources ();

            // Play the main menu music
            SwinGame.PlayMusic (GameResources.Music ("main"));

            
            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();

                //Clear the screen and draw the framerate
                SwinGame.ClearScreen(SwinGame.RGBColor(83,94,103));

                // Handle the user input
                GameController.HandleUserInput ();

                // Draw the screen
                GameController.DrawScreen ();

                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}
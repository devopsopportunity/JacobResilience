/*
 * GameMenu.cs
 * @ Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------
 * This class manages the game menu.
 * -------------------------------------------
 * @hacktlon July 15, 2024
 */
using System;

namespace Modules
{
    public class GameMenu
    {
        private int options = 0;

        /// <summary>
        /// Displays the game menu on the console.
        /// </summary>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="showMenu">Flag indicating if the menu should be shown.</param>
        /// <param name="returnToGameAction">Action to return to the game.</param>
        /// <param name="exitGameAction">Action to exit the game.</param>
        public void ShowMenu(int screenHeight, int screenWidth, bool showMenu, Action returnToGameAction, Action exitGameAction)
        {
            options = 0; // Reset the option count each time the menu is shown
            string line = "------------------------------------------";
            Console.SetCursorPosition(0, screenHeight);
            Console.WriteLine("\n\nCHOOSE FROM MENU                               ");
            Console.WriteLine(line);
            Console.WriteLine("H - Hide the menu and return to the game           "); options++;
            Console.WriteLine("Q - Exit the game                                  "); options++;
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine(line);
            ManageMenuInput(screenHeight, screenWidth, showMenu, returnToGameAction, exitGameAction);
        }

        /// <summary>
        /// Manages user input for the menu options.
        /// </summary>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="showMenu">Flag indicating if the menu is currently shown.</param>
        /// <param name="returnToGameAction">Action to return to the game.</param>
        /// <param name="exitGameAction">Action to exit the game.</param>
        private void ManageMenuInput(int screenHeight, int screenWidth, bool showMenu, Action returnToGameAction, Action exitGameAction)
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.H)
                    {
                        ClearMenuArea(screenHeight, screenWidth);
                        returnToGameAction(); // Hide the menu and return to the game
                        break;
                    }
                    else if (key == ConsoleKey.Q)
                    {
                        exitGameAction(); // Exit the game
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Clears the menu area on the console.
        /// </summary>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        private void ClearMenuArea(int screenHeight, int screenWidth)
        {
            for (int y = screenHeight; y < screenHeight + 4 + options; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.WriteLine(new string(' ', screenWidth));
            }
        }
    }
}

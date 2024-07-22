/*
 * GameMenu.cs
 * @ Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------
 * This class manages the game menu.
 * -------------------------------------------
 * @hacktlon July 15, 2024
 */
namespace Modules
{
    public class GameMenu
    {
        private const int ADJUST_LINES = 7;
        private static readonly string[] menuOptions = new string[]
        {
            "H - Hide the menu and return to the game",
            "S - Sound {0}", // Expecting one argument for sound status
            "A - Archive player state (last saved: {0})", // Expecting one argument for the last saved date
            "Q - Exit the game"
        };

        private bool soundOn = true; // Track sound status
        private DateTime lastSavedDate = DateTime.MinValue; // Track last archive date

        private void TextMenu(int screenHeight, int screenWidth) 
        {
            Console.SetCursorPosition(0, screenHeight);
            Console.WriteLine("\n\nCHOOSE FROM MENU                               ");
            Console.WriteLine(new string('-', screenWidth)); // Separator line
            
            // Display menu options
            for (int i = 0; i < menuOptions.Length; i++)
            {
                string option = i switch
                {
                    1 => string.Format(menuOptions[i], soundOn ? "ON 🔊" : "OFF 🔇"), // Format sound option
                    2 => string.Format(menuOptions[i], lastSavedDate == DateTime.MinValue ? "Never" : lastSavedDate.ToString("g")), // Format archive option
                    _ => menuOptions[i] // Default case for other options
                };

                Console.WriteLine(option);
            }

            Console.WriteLine(new string(' ', screenWidth)); // Extra line for spacing
            Console.WriteLine(new string('-', screenWidth)); // Separator line
        }

        /// <summary>
        /// Displays the game menu on the console.
        /// </summary>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="showMenu">Flag indicating if the menu should be shown.</param>
        /// <param name="returnToGameAction">Action to return to the game.</param>
        /// <param name="exitGameAction">Action to exit the game.</param>
        /// <param name="toggleSoundAction">Action to toggle sound on or off.</param>
        /// <param name="archivePlayerAction">Action to archive player state.</param>
        public void ShowMenu(int screenHeight, int screenWidth, bool showMenu, Action returnToGameAction, Action exitGameAction, Action toggleSoundAction, Action archivePlayerAction)
        {
            TextMenu(screenHeight, screenWidth);
            ManageMenuInput(screenHeight, screenWidth, returnToGameAction, exitGameAction, toggleSoundAction, archivePlayerAction);
        }

        /// <summary>
        /// Manages user input for the menu options.
        /// </summary>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="returnToGameAction">Action to return to the game.</param>
        /// <param name="exitGameAction">Action to exit the game.</param>
        /// <param name="toggleSoundAction">Action to toggle sound on or off.</param>
        /// <param name="archivePlayerAction">Action to archive player state.</param>
        private void ManageMenuInput(int screenHeight, int screenWidth, Action returnToGameAction, Action exitGameAction, Action toggleSoundAction, Action archivePlayerAction)
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.H:
                            ClearMenuArea(screenHeight, screenWidth);
                            returnToGameAction(); // Hide the menu and return to the game
                            return; // Exit the loop after action is taken
                        
                        case ConsoleKey.S:
                            ToggleSound(toggleSoundAction); // Toggle sound
                            ClearMenuArea(screenHeight, screenWidth);
                            TextMenu(screenHeight, screenWidth);
                            break;
                        
                        case ConsoleKey.A:
                            ClearMenuArea(screenHeight, screenWidth);
                            ArchivePlayerState(archivePlayerAction); // Archive the player state
                            TextMenu(screenHeight, screenWidth);
                            break;

                        case ConsoleKey.Q:
                            ClearMenuArea(screenHeight, screenWidth);
                            exitGameAction(); // Exit the game
                            return; // Exit the loop after action is taken
                    }
                }
            }
        }

        /// <summary>
        /// Archives the player state and updates the last saved date.
        /// </summary>
        /// <param name="archivePlayerAction">Action to archive player state.</param>
        private void ArchivePlayerState(Action archivePlayerAction)
        {
            archivePlayerAction(); // Execute the action associated with archiving player state
            lastSavedDate = DateTime.Now; // Update the last saved date
        }

        /// <summary>
        /// Toggles the sound status and invokes the corresponding action.
        /// </summary>
        /// <param name="toggleSoundAction">Action to toggle sound on or off.</param>
        private void ToggleSound(Action toggleSoundAction)
        {
            soundOn = !soundOn;
            toggleSoundAction(); // Execute the action associated with sound toggling
        }

        /// <summary>
        /// Clears the menu area on the console.
        /// </summary>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        private void ClearMenuArea(int screenHeight, int screenWidth)
        {
            for (int y = screenHeight; y < screenHeight + ADJUST_LINES + menuOptions.Length; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.WriteLine(new string(' ', screenWidth));
            }
        }
    }
}

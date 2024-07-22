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
        private const int ADJUST_LINES_START = 3;
        private const int ADJUST_LINES_END = 7;
        private static readonly string[] menuOptions = new string[]
        {
            "H - Hide the menu and return to the game",
            "S - Sound {0}", // Expecting one argument for sound status
            "A - Archive player state (last saved: {0})", // Expecting one argument for the last saved date
            "W - Sound wave {0}", // Expecting one argument for sound wave status
            "Q - Exit the game"
        };

        private bool soundOn = true; // Track sound status
        private bool waveOn = true; // Track sound wave status
        public static bool audioOn = true; // Flag indicating if the audio is activated
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
                    1 => string.Format(menuOptions[i], soundOn ? "ON 🔊" : "OFF 🔊 🔇"), // Format sound option
                    2 => string.Format(menuOptions[i], lastSavedDate == DateTime.MinValue ? "Never" : lastSavedDate.ToString("g")), // Format archive option
                    3 => string.Format(menuOptions[i], waveOn ? "ON 🎵" : "OFF 🎵 🔇"), // Format sound wave option
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
        /// <param name="toggleWaveAction">Action to toggle sound wave on or off.</param>
        public void ShowMenu(int screenHeight, int screenWidth, Action returnToGameAction, Action exitGameAction, Action toggleSoundAction, Action archivePlayerAction, Action toggleWaveAction)
        {
            ClearMenuArea(screenHeight, screenWidth);
            TextMenu(screenHeight, screenWidth);
            ManageMenuInput(screenHeight, screenWidth, returnToGameAction, exitGameAction, toggleSoundAction, archivePlayerAction, toggleWaveAction);
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
        /// <param name="toggleWaveAction">Action to toggle sound wave on or off.</param>
        private void ManageMenuInput(int screenHeight, int screenWidth, Action returnToGameAction, Action exitGameAction, Action toggleSoundAction, Action archivePlayerAction, Action toggleWaveAction)
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

                        case ConsoleKey.W:
                            ToggleWave(toggleWaveAction); // Toggle sound wave
                            ClearMenuArea(screenHeight, screenWidth);
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
        /// Toggles the sound wave status and invokes the corresponding action.
        /// </summary>
        /// <param name="toggleWaveAction">Action to toggle sound wave on or off.</param>
        private void ToggleWave(Action toggleWaveAction)
        {
            waveOn = !waveOn;
            toggleWaveAction(); // Execute the action associated with wave toggling
        }

        /// <summary>
        /// Clears the menu area on the console.
        /// </summary>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        private void ClearMenuArea(int screenHeight, int screenWidth)
        {
            for (int y = screenHeight + ADJUST_LINES_START; y < screenHeight + ADJUST_LINES_END + menuOptions.Length; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.WriteLine(new string(' ', screenWidth));
            }
        }
    }
}

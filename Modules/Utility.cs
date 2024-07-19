/*
 * Utility.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the Utility class, providing helper methods
 * for the Jacob's Resilience game. It includes methods for text
 * formatting and other utility functions.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using JacobResilienceGame;
using Config;

namespace Modules
{
    public class Utility
    {
        private Program program;

        // Constructor accepting a Program object
        public Utility(Program program)
        {
            this.program = program;
        }

        // Add more utility methods here

        // Helper function to wrap text into lines of specified width without breaking words
        private string[] WrapText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return new string[] { "" };

            var lines = new List<string>();
            var words = text.Split(' ');

            string currentLine = "";

            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + 1 <= maxLength)
                {
                    currentLine += (currentLine.Length == 0 ? "" : " ") + word;
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = word;
                }
            }

            if (currentLine.Length > 0)
                lines.Add(currentLine);

            return lines.ToArray();
        }

        // Function to print the level title and description in formatted blocks
        public void PrintLevelTitle()
        {
            var gameLevel = program.levels<program.gameLevelInitializer.GameLevels.Count()
                            ? program.gameLevelInitializer.GameLevels[program.levels] 
                            : program.gameLevelInitializer.GameLevels[0];
            // You can now use `gameLevel` as needed

            string currentLevel = program.levels switch
            {
                0 => $"{program.game.LevelEmojiChar}  ",
                1 => $"{program.game.Level1EmojiChar}  ",
                2 => $"{program.game.Level2EmojiChar}  ",
                _ => $"{program.game.LevelEmojiChar}  "
            };

            string levelText = $"LEVEL {currentLevel}";
            int screenWidth = GameConfig.SCREEN_WIDTH;

            // Calculate left padding to center the text
            int leftPadding = (screenWidth - levelText.Length) / 2;

            // Print the centered level title
            Console.Write(levelText);
            Console.SetCursorPosition(leftPadding, 0);

            // Max chars of the description
            int maxChars = GameLevelInitializer.MAX_DESCRIPTION_LENGTH / 2;

            // Split the description into lines of maxChars characters each, avoiding breaking words
            string[] lines = WrapText(gameLevel.Description, maxChars);

            // Add an empty line if the description has less than 2 lines
            if (lines.Length < 2)
            {
                Array.Resize(ref lines, 2);
                lines[1] = ""; // Add an empty line
            }

            // Calculate left padding for description lines
            int descriptionLeftPadding = (screenWidth - maxChars) / 2;

            // Output the description centered
            for (int i = 0; i < lines.Length; i++)
            {

                if (lines[i].Length > maxChars)
                {
                    lines[i] = lines[i].Substring(0, maxChars); // Truncate to maxChars
                }
                
                Console.SetCursorPosition(descriptionLeftPadding, i); // Start from the second line for description
                Console.WriteLine(lines[i].PadRight(maxChars));
            }

            // Set the cursor position to the next line after the description
            Console.SetCursorPosition(0, lines.Length);
        }
    }
}

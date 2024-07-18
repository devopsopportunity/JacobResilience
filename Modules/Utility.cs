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
            string currentLevel = program.levels switch
            {
                0 => $"{program.game.LevelEmojiChar}  ",
                1 => $"{program.game.Level1EmojiChar}  ",
                2 => $"{program.game.Level2EmojiChar}  ",
                _ => $"{program.game.Level2EmojiChar}  "
            };

            string levelText = $"LEVEL {currentLevel}";
            int screenWidth = GameConfig.SCREEN_WIDTH;

            // Calculate left padding to center the text
            int leftPadding = (screenWidth - levelText.Length) / 2;

            // Print the centered level title
            Console.Write(levelText);
            Console.SetCursorPosition(leftPadding, 0);

            // Positioning the description
            string descLevel = program.levels switch
            {
                0 => "You are now in the Savannah! You'll face poachers' traps that attempt to reduce your health.",
                1 => "Now you are swimming through the Kazinga Channel for almost a mile and encountering hippos along your path! They reduce your resilience by one point.",
                2 => "A more challenging level as you navigate through another part of the Kazinga Channel, now infested with crocodiles that decrease your stamina level!",
                _ => "Game schema not yet set..."
            };

            int maxChars=80;
            // Split the description into lines of 50 characters each, avoiding breaking words
            string[] lines = WrapText(descLevel, maxChars);

            // Calculate left padding for description lines
            int descriptionLeftPadding = (screenWidth - maxChars) / 2;

            // Output the description centered
            for (int i = 0; i < lines.Length; i++)
            {
                Console.SetCursorPosition(descriptionLeftPadding, i); // Start from the second line for description
                Console.WriteLine(lines[i].PadRight(maxChars)); // Pad to 50 characters
            }

            // Set the cursor position to the next line after the description
            Console.SetCursorPosition(0, lines.Length);
        }


    }
}

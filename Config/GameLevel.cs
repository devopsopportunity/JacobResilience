/*
 * GameLevel.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the GameLevel class that contains a dynamic array of CheckersConfig.
 * It also includes attributes for level and description.
 * -------------------------------------------------------------
 * @hacktlon July 18, 2024
 */

namespace Config
{
    public class GameLevel
    {
        public string Description { get; set; } = ""; // Description of the level, initialized to empty string
        public List<CheckersConfig> CheckersConfigs { get; } = new List<CheckersConfig>(); // Dynamic array of CheckersConfig, initialized as empty list

        public int TotalScoreDuration { get; set; } // Total score duration for the level

        // Default constructor
        public GameLevel(string description) {
            this.Description = description;
        }
    }
}
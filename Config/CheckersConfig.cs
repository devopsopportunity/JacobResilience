/*
 * CheckersConfig.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the game configuration constants.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
namespace Config
{
    public class CheckersConfig
    {
        /// <summary>
        /// Minimum number of entities allowed in this configuration.
        /// </summary>
        public int MinEntities { get; set; }

        /// <summary>
        /// Maximum number of entities allowed in this configuration.
        /// </summary>
        public int MaxEntities { get; set; }

        /// <summary>
        /// Minimum height on the screen where entities can appear.
        /// </summary>
        public int MinHeight { get; set; }

        /// <summary>
        /// Maximum height on the screen where entities can appear.
        /// </summary>
        public int MaxHeight { get; set; }

        /// <summary>
        /// Emoji character associated with entities configured by this instance.
        /// </summary>
        public string Character { get; set; } // Property for the character

        /// <summary>
        /// Default constructor initializing with default values.
        /// </summary>
        public CheckersConfig()
        {
            // Default values
            MinEntities = 0;
            MaxEntities = 1;
            MinHeight = GameConfig.SCREEN_HEIGHT / 2;
            MaxHeight = GameConfig.SCREEN_HEIGHT - 1;
            Character = "😊"; // Default character value
        }
    }
}

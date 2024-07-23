/*
 * CheckersConfig.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines game configuration constants and settings 
 * for entities in a Checkers game simulation.
 * 
 * It includes properties for defining the minimum and maximum 
 * number of entities, their minimum and maximum screen heights,
 * and the emoji character associated with each entity type.
 * 
 * It also includes a dynamic list of score intervals associated
 * with each configuration.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */

namespace Config
{
    // Class representing configuration settings for entities in a Checkers game
    public class CheckersConfig
    {
        /// <summary>
        /// Emoji character associated with entities configured by this instance.
        /// This character visually represents the entities in the game.
        /// </summary>
        public string Character { get; set; }

        /// <summary>
        /// Minimum number of entities allowed in this configuration.
        /// This sets the lower bound for how many entities can be present.
        /// </summary>
        public int MinEntities { get; set; }

        /// <summary>
        /// Maximum number of entities allowed in this configuration.
        /// This sets the upper bound for how many entities can be present.
        /// </summary>
        public int MaxEntities { get; set; }

        /// <summary>
        /// Minimum height on the screen where entities can appear.
        /// This defines the lower limit for the vertical position of entities.
        /// </summary>
        public int MinHeight { get; set; }

        /// <summary>
        /// Maximum height on the screen where entities can appear.
        /// This defines the upper limit for the vertical position of entities.
        /// </summary>
        public int MaxHeight { get; set; }

        /// <summary>
        /// List of score intervals associated with this configuration.
        /// These intervals define the ranges of scores for different configurations.
        /// </summary>
        public List<ScoreInterval> ScoreIntervals { get; set; }

        /// <summary>
        /// Default constructor initializing with default values.
        /// Sets the default values for properties of the CheckersConfig class.
        /// </summary>
        public CheckersConfig()
        {
            Character = "😊"; // Default character value

            // Default values
            MinEntities = 0;
            MaxEntities = 1;
            MinHeight = GameConfig.Instance.SCREEN_HEIGHT / 2;
            MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1;

            // Initialize the list of score intervals
            ScoreIntervals = new List<ScoreInterval>();
        }
    }
}

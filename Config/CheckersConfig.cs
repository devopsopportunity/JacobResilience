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
 * @hacktlon July 15, 2024
 */

namespace Config
{
    public class CheckersConfig
    {
        /// <summary>
        /// Emoji character associated with entities configured by this instance.
        /// </summary>
        public string Character { get; set; }


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
        /// List of score intervals associated with this configuration.
        /// </summary>
        public List<ScoreInterval> ScoreIntervals { get; set; }

        /// <summary>
        /// Default constructor initializing with default values.
        /// </summary>
        public CheckersConfig()
        {
            Character = "😊"; // Default character value

            // Default values
            MinEntities = 0;
            MaxEntities = 1;
            MinHeight = GameConfig.SCREEN_HEIGHT / 2;
            MaxHeight = GameConfig.SCREEN_HEIGHT - 1;

            // Initialize the list of score intervals
            ScoreIntervals = new List<ScoreInterval>();
        }
    }
}

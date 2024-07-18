/*
 * GameConfig.cs
 * @ Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------
 * This file defines the game configuration constants.
 * -------------------------------------------
 * @hacktlon July 15, 2024
 */
namespace Config
{
    /// <summary>
    /// Static class that contains game configuration constants.
    /// </summary>
    public static class GameConfig
    {
        /// <summary>
        /// Width of the game screen.
        /// </summary>
        public const string FOLDER_WAV = "wav/";

        /// <summary>
        /// Width of the game screen.
        /// </summary>
        public const int SCREEN_WIDTH = 100;

        /// <summary>
        /// Height of the game screen.
        /// </summary>
        public const int SCREEN_HEIGHT = 25;

        /// <summary>
        /// Initial X position of the player.
        /// </summary>
        public const int PLAYER_INITIAL_X = 5;

        /// <summary>
        /// Initial Y position of the player.
        /// </summary>
        public const int PLAYER_INITIAL_Y = 21;

        /// <summary>
        /// Initial jump velocity of the player.
        /// </summary>
        public const int START_JUMP_VELOCITY = 5;

        /// <summary>
        /// Pause control value to control game speed.
        /// </summary>
        public const int PAUSE_CONTROL = 50;

        /// <summary>
        /// Initial resilience of the player.
        /// </summary>
        public const int MAX_RESILIENCE = 30;

        public const int INIT_RESILIENCE = 3;
        /// <summary>
        /// Initial stamina of the player.
        /// </summary>
        public const int MAX_STAMINA = 30;
        public const int INIT_STAMINA = 3;

        /// <summary>
        /// Initial lives of the player.
        /// </summary>
        public const int MAX_LIVES = 30;

        public const int INIT_LIVES = 3;

        public const int STEP_LEVELS = 3;

    }
}
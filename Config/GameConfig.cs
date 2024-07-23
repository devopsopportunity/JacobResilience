/*
 * GameConfig.cs
 * @ Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------
 * This file defines the game configuration constants.
 * Singleton class that contains game configuration constants.
 * -------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
namespace Config
{
    /// <summary>
    /// Singleton class that contains game configuration constants.
    /// </summary>
    public sealed class GameConfig
    {
        // File paths for leaderboard and player status
        public const string PLAYER_STATUS_FILE = "playerStatus.txt";

        /// <summary>
        /// Leaderboard
        /// </summary>
        public const string LEADER_BOARD_FILE = "leaderboard.txt";

        /// <summary>
        /// Models
        /// </summary>
        public const string FOLDER_MODELS = "GameModels/";

        /// <summary>
        /// Wav sounds.
        /// </summary>
        public const string FOLDER_WAV = "wav/";

        /// <summary>
        /// Playback Game Sound
        /// </summary>
        public const string PLAY_BACK_GAME_SOUND = "game_sound";

        /// <summary>
        /// Playback Wait for keyboard
        /// </summary>
        public const string PLAY_BACK_WAIT_FOR_KEY = "wait_for_key";

        public const int SCREEN_WIDTH_INTERVAL = 20;

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

        private const int DefaultScreenWidth = 106;
        private const int DefaultScreenHeight = 40;

        private const int DeltaScreenWidth = 6;
        private const int DeltatScreenHeight = 15;

        /// <summary>
        /// Width of the game screen.
        /// </summary>
        public int SCREEN_WIDTH { get; private set; }

        /// <summary>
        /// Height of the game screen.
        /// </summary>
        public int SCREEN_HEIGHT { get; private set; }

        // The singleton instance
        private static readonly Lazy<GameConfig> instance = new Lazy<GameConfig>(() => new GameConfig());

        /// <summary>
        /// Private constructor to prevent external instantiation.
        /// </summary>
        private GameConfig()
        {
            // Read environment variables
            string? widthEnv = Environment.GetEnvironmentVariable("SCREEN_WIDTH");
            string? heightEnv = Environment.GetEnvironmentVariable("SCREEN_HEIGHT");

            // Set SCREEN_WIDTH and SCREEN_HEIGHT with environment variables if available, otherwise use default values
            SCREEN_WIDTH = (int.TryParse(widthEnv, out int width) ? width : DefaultScreenWidth) - DeltaScreenWidth;
            SCREEN_HEIGHT = (int.TryParse(heightEnv, out int height) ? height : DefaultScreenHeight) - DeltatScreenHeight;
        }

        /// <summary>
        /// Gets the singleton instance of the GameConfig class.
        /// </summary>
        public static GameConfig Instance => instance.Value;
    }
}

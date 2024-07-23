/*
 * CheckersConfigInitializer.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file contains the initialization logic for the CheckersConfig array.
 * It initializes configurations for various game components such as animals, coins,
 * enemies, and energies.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */

namespace Config
{
    public static class CheckersConfigInitializer
    {
        /// <summary>
        /// Initializes an array of CheckersConfig with predefined settings for various game components.
        /// </summary>
        /// <param name="game">An instance of the Game class providing emoji characters for configuration.</param>
        /// <returns>An array of CheckersConfig objects with specific settings for different game entities.</returns>
        public static CheckersConfig[] InitializeCheckersConfigs(Game game)
        {
            // Initialize the CheckersConfig array
            CheckersConfig[] checkersConfigs = new CheckersConfig[15]; // Increase array size to accommodate new configurations

            // Animals
            // Crocodile
            checkersConfigs[0] = new CheckersConfig()
            {
                MinEntities = 1,
                MaxEntities = 4,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT / 2,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.HippopotamusEmojiChar
            };

            // Hippopotamus
            checkersConfigs[1] = new CheckersConfig()
            {
                MinEntities = 2,
                MaxEntities = 5,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.CrocodileEmojiChar
            };

            // Snake 1
            checkersConfigs[2] = new CheckersConfig()
            {
                MinEntities = 1,
                MaxEntities = 2,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT / 2,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.Snake1EmojiChar
            };

            // Snake 2
            checkersConfigs[3] = new CheckersConfig()
            {
                MinEntities = 0,
                MaxEntities = 1,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.Snake2EmojiChar
            };

            // Coins
            // Coin 1
            checkersConfigs[4] = new CheckersConfig()
            {
                MinEntities = 1,
                MaxEntities = 4,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT / 2,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.Coin1EmojiChar
            };

            // Coin 2
            checkersConfigs[5] = new CheckersConfig()
            {
                MinEntities = 0,
                MaxEntities = 1,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.Coin2EmojiChar
            };

            // Coin 3
            checkersConfigs[6] = new CheckersConfig()
            {
                MinEntities = 0,
                MaxEntities = 2,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT / 2,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.Coin3EmojiChar
            };

            // Enemies
            // Danger
            checkersConfigs[7] = new CheckersConfig()
            {
                MinEntities = 1,
                MaxEntities = 2,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.DangerEmojiChar
            };

            // Fire
            checkersConfigs[8] = new CheckersConfig()
            {
                MinEntities = 2,
                MaxEntities = 3,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT / 2,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.FireEmojiChar
            };

            // Poachers
            checkersConfigs[9] = new CheckersConfig()
            {
                MinEntities = 1,
                MaxEntities = 4,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.PoachersEmojiChar
            };

            // Trap
            checkersConfigs[10] = new CheckersConfig()
            {
                MinEntities = 2,
                MaxEntities = 5,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.TrapEmojiChar
            };

            // Energies
            // Apple
            checkersConfigs[11] = new CheckersConfig()
            {
                MinEntities = 3,
                MaxEntities = 4,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT / 2,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.AppleEmojiChar
            };

            // Energy
            checkersConfigs[12] = new CheckersConfig()
            {
                MinEntities = 2,
                MaxEntities = 3,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT / 2,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.EnergyEmojiChar
            };

            // Meat 1
            checkersConfigs[13] = new CheckersConfig()
            {
                MinEntities = 1,
                MaxEntities = 2,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.Meat1EmojiChar
            };

            // Meat 2
            checkersConfigs[14] = new CheckersConfig()
            {
                MinEntities = 1,
                MaxEntities = 3,
                MinHeight = GameConfig.Instance.SCREEN_HEIGHT / 2,
                MaxHeight = GameConfig.Instance.SCREEN_HEIGHT - 1,
                Character = game.Meat2EmojiChar
            };

            return checkersConfigs;
        }
    }
}

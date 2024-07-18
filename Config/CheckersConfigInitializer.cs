/*
 * CheckersConfigInitializer.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file contains the initialization logic for the CheckersConfig array.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
namespace Config
{
    public static class CheckersConfigInitializer
    {
        public static CheckersConfig[] InitializeCheckersConfigs(Game game)
        {
            CheckersConfig[] checkersConfigs = new CheckersConfig[6]; // Increase array size to accommodate new configuration

            checkersConfigs[0] = new CheckersConfig()
            {
                MinEntities = 1,
                MaxEntities = 4,
                MinHeight = GameConfig.SCREEN_HEIGHT / 2,
                MaxHeight = GameConfig.SCREEN_HEIGHT - 1,
                Character = game.Coin1EmojiChar
            };

            checkersConfigs[1] = new CheckersConfig()
            {
                MinEntities = 0,
                MaxEntities = 1,
                MinHeight = GameConfig.SCREEN_HEIGHT - 1,
                MaxHeight = GameConfig.SCREEN_HEIGHT - 1,
                Character = game.Coin2EmojiChar
            };

            checkersConfigs[2] = new CheckersConfig()
            {
                MinEntities = 0,
                MaxEntities = 2,
                MinHeight = GameConfig.SCREEN_HEIGHT / 2,
                MaxHeight = GameConfig.SCREEN_HEIGHT - 1,
                Character = game.Coin3EmojiChar
            };

            checkersConfigs[3] = new CheckersConfig()
            {
                MinEntities = 1,
                MaxEntities = 2,
                MinHeight = GameConfig.SCREEN_HEIGHT - 1,
                MaxHeight = GameConfig.SCREEN_HEIGHT - 1,
                Character = game.PoachersEmojiChar
            };

            checkersConfigs[4] = new CheckersConfig()
            {
                MinEntities = 3,
                MaxEntities = 4,
                MinHeight = GameConfig.SCREEN_HEIGHT - 1,
                MaxHeight = GameConfig.SCREEN_HEIGHT - 1,
                Character = game.HippopotamusEmojiChar
            };

            // Initialize the sixth CheckersConfig object with CrocodileEmojiChar
            checkersConfigs[5] = new CheckersConfig()
            {
                MinEntities = 2,
                MaxEntities = 5,
                MinHeight = GameConfig.SCREEN_HEIGHT  -1,
                MaxHeight = GameConfig.SCREEN_HEIGHT - 1,
                Character = game.CrocodileEmojiChar
            };

            return checkersConfigs;
        }
    }
}

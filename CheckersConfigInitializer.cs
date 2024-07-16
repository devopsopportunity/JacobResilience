/*
 * CheckersConfigInitializer.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file contains the initialization logic for the CheckersConfig array.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Modules;

namespace JacobResilienceGame
{
    public static class CheckersConfigInitializer
    {
        public static CheckersConfig[] InitializeCheckersConfigs(Game game)
        {
            CheckersConfig[] checkersConfigs = new CheckersConfig[4];

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

            return checkersConfigs;
        }
    }
}

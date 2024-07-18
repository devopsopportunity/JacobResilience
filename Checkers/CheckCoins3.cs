/*
 * CheckCoins3.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckCoins3 class for checking coin type 3 in the Jacob's Resilience game.
 * It specializes the abstract FactoryCheckerAbstract class, implementing the CheckForItems method
 * to specifically handle coin type 3 checks in accordance with the Liskov Substitution Principle (LSP).
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Checkers
{
    public class CheckCoins3 : FactoryCheckerAbstract
    {
        public CheckCoins3(Game game, Program program) : base(game, program) { }

        public override async Task CheckForItems(int y, int x)
        {
            int adjustedX = (x + program.offset) % GameConfig.SCREEN_WIDTH;

            if (adjustedX >= 0 && adjustedX < GameConfig.SCREEN_WIDTH && y >= 0 && y < GameConfig.SCREEN_HEIGHT && program.screen[y, adjustedX] == game.Coin3EmojiChar)
            {
                program.screen[y, adjustedX] = " ";
                program.credit += 10;
                await soundPlayer.PlayAsync("coin");
            }
        }
    }
}

/*
 * CheckCoins1.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckCoins1 class for checking coin type 1 in the Jacob's Resilience game.
 * It specializes the abstract FactoryCheckerAbstract class, implementing the CheckForItems method
 * to specifically handle coin type 1 checks in accordance with the Liskov Substitution Principle (LSP).
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using System;
using System.Threading.Tasks;
using Modules;

namespace JacobResilienceGame.Checkers
{
    public class CheckCoins1 : FactoryCheckerAbstract
    {
        public CheckCoins1(Game game, Program program) : base(game, program) { }

        public override async Task CheckForItems(int y, int x)
        {
            int adjustedX = (x + program.offset) % GameConfig.SCREEN_WIDTH;

            if (adjustedX >= 0 && adjustedX < GameConfig.SCREEN_WIDTH && y >= 0 && y < GameConfig.SCREEN_HEIGHT && program.screen[y, adjustedX] == game.Coin1EmojiChar)
            {
                program.screen[y, adjustedX] = " ";
                program.credit++;
                await soundPlayer.PlayAsync("coin");
            }
        }
    }
}

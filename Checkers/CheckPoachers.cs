/*
 * CheckPoachers.cs
 * @ Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckPoachers class for handling poachers in the Jacob's Resilience game.
 * It specializes the abstract FactoryCheckerAbstract class, implementing the CheckForItems method
 * to specifically handle poachers in accordance with the Liskov Substitution Principle (LSP).
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using System;
using System.Threading.Tasks;
using Modules;

namespace JacobResilienceGame.Checkers
{
    public class CheckPoachers : FactoryCheckerAbstract
    {
        public CheckPoachers(Game game, Program program) : base(game, program) { }

        public override async Task CheckForItems(int y, int x)
        {
            int adjustedX = (x + program.offset) % GameConfig.SCREEN_WIDTH;

            if (adjustedX >= 0 && adjustedX < GameConfig.SCREEN_WIDTH && y >= 0 && y < GameConfig.SCREEN_HEIGHT && program.screen[y, adjustedX] == game.PoachersEmojiChar)
            {
                program.screen[y, adjustedX] = " ";
                program.lives -= 1; // Decrease player's lives when a poacher is found
                await soundPlayer.PlayAsync("poacher");
            }
        }
    }
}

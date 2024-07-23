/*
 * CheckDiamond.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckDiamond class for handling Diamond in the Jacob's Resilience game.
 * It specializes the abstract DelegateCoinChecker class, implementing the CoinBehavior method
 * to specifically handle Diamond as an item in accordance with the Liskov Substitution Principle (LSP).
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Coins
{
    public class CheckDiamond : DelegateCoinChecker
    {
        public CheckDiamond(Game game, Program program) : base(game, program, game.DiamondEmojiChar, "diamond") { }

        /// <summary>
        /// Defines the behavior when encountering Diamond.
        /// </summary>
        public override void CoinBehavior()
        {
            program.credit++;
            program.credit2++;
        }
    }
}

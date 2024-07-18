/*
 * CheckCoins2.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckCoins2 class for checking coin type 2 in the Jacob's Resilience game.
 * It specializes the abstract DelegateCoinChecker class, implementing the CoinBehavior method
 * to specifically handle coin type 2 checks in accordance with the Liskov Substitution Principle (LSP).
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Coins
{
    public class CheckCoins2 : DelegateCoinChecker
    {
        public CheckCoins2(Game game, Program program) : base(game, program, game.Coin2EmojiChar, "coin") { }

        /// <summary>
        /// Defines the behavior when encountering coin type 2.
        /// </summary>
        public override void CoinBehavior()
        {
            program.credit2++;
        }
    }
}

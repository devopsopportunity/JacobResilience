/*
 * CheckCoins1.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckCoins1 class for checking coin type 1 in the Jacob's Resilience game.
 * It specializes the abstract DelegateCoinChecker class, implementing the CoinBehavior method
 * to specifically handle coin type 1 checks in accordance with the Liskov Substitution Principle (LSP).
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Coins
{
    public class CheckCoins1 : DelegateCoinChecker
    {
        public CheckCoins1(Game game, Program program) : base(game, program, game.Coin1EmojiChar, "coin") { }

        /// <summary>
        /// Defines the behavior when encountering coin type 1.
        /// </summary>
        public override void CoinBehavior()
        {
            program.credit++;
        }
    }
}

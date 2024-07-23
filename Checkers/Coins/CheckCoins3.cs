/*
 * CheckCoins3.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckCoins3 class for checking coin type 3 in the Jacob's Resilience game.
 * It specializes the abstract DelegateCoinChecker class, implementing the CoinBehavior method
 * to specifically handle coin type 3 checks in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateCoinChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the CoinBehavior event.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
using Config;

namespace JacobResilienceGame.Coins
{
    public class CheckCoins3 : DelegateCoinChecker
    {
        public CheckCoins3(Game game, Program program) : base(game, program, game.Coin3EmojiChar, "coin") { }

        /// <summary>
        /// Defines the behavior when encountering coin type 3.
        /// </summary>
        public override void CoinBehavior()
        {
            program.credit += 10;
        }
    }
}

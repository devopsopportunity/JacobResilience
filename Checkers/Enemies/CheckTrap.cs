/*
 * CheckTrap.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckTrap class for handling traps in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnemyChecker class, implementing the EnemyBehavior method
 * to specifically handle traps in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateEnemyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnemyBehavior event.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
using Config;

namespace JacobResilienceGame.Enemies
{
    public class CheckTrap : DelegateEnemyChecker
    {
        public CheckTrap(Game game, Program program) : base(game, program, game.TrapEmojiChar, "trap") { }

        /// <summary>
        /// Defines the behavior when encountering a trap.
        /// </summary>
        public override void EnemyBehavior()
        {
            program.resilience -= 1; // Decrease player's resilience when a trap is encountered
        }
    }
}

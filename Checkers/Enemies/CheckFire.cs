/*
 * CheckFire.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckFire class for handling fire hazards in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnemyChecker class, implementing the EnemyBehavior method
 * to specifically handle fire hazards in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateEnemyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnemyBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Enemies
{
    public class CheckFire : DelegateEnemyChecker
    {
        public CheckFire(Game game, Program program) : base(game, program, game.FireEmojiChar, "fire") { }

        /// <summary>
        /// Defines the behavior when encountering fire hazards.
        /// </summary>
        public override void EnemyBehavior()
        {
            program.stamina -= 1; // Decrease player's stamina when encountering fire hazards
        }
    }
}

/*
 * CheckDanger.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckDanger class for handling dangerous situations in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnemyChecker class, implementing the EnemyBehavior method
 * to specifically handle dangerous situations in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateEnemyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnemyBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Enemies
{
    public class CheckDanger : DelegateEnemyChecker
    {
        public CheckDanger(Game game, Program program) : base(game, program, game.DangerEmojiChar, "danger") { }

        /// <summary>
        /// Defines the behavior when encountering dangerous situations.
        /// </summary>
        public override void EnemyBehavior()
        {
            program.lives -= 1; // Decrease player's lives when encountering dangerous situations
            program.stamina -= 1; // Decrease player's stamina when encountering dangerous situations
        }
    }
}

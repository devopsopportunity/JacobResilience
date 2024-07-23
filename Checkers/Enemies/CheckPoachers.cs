/*
 * CheckPoachers.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckPoachers class for handling poachers in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnemyChecker class, implementing the EnemyBehavior method
 * to specifically handle poachers in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateEnemyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnemyBehavior event.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
using Config;

namespace JacobResilienceGame.Enemies
{
    public class CheckPoachers : DelegateEnemyChecker
    {
        public CheckPoachers(Game game, Program program) : base(game, program, game.PoachersEmojiChar, "poacher") { }

        /// <summary>
        /// Defines the behavior when encountering poachers.
        /// </summary>
        public override void EnemyBehavior()
        {
            program.lives -= 1; // Decrease player's lives when a poacher is found
        }
    }
}

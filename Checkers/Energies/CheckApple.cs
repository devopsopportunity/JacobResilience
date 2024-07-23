/*
 * CheckApple.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckApple class for handling apples in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnergyChecker class, implementing the EnergyBehavior method
 * to specifically handle apples as items in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateEnergyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnergyBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Energies
{
    public class CheckApple : DelegateEnergyChecker
    {
        public CheckApple(Game game, Program program) : base(game, program, game.AppleEmojiChar, "apple")
        {
            // Additional initialization specific to apples can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering an apple.
        /// </summary>
        public override void EnergyBehavior()
        {
            // Increase player's energy when an apple is collected
            program.lives++;
            if(program.lives>GameConfig.MAX_LIVES) program.lives = GameConfig.MAX_LIVES;
        }
    }
}

/*
 * CheckWatermelon.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckWatermelon class for handling Watermelon in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnergyChecker class, implementing the EnergyBehavior method
 * to specifically handle Watermelon as an item in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateEnergyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnergyBehavior event.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
using Config;

namespace JacobResilienceGame.Energies
{
    public class CheckWatermelon : DelegateEnergyChecker
    {
        public CheckWatermelon(Game game, Program program) : base(game, program, game.WatermelonEmojiChar, "watermelon")
        {
            // Additional initialization specific to Watermelon can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering Watermelon.
        /// </summary>
        public override void EnergyBehavior()
        {
            // Increase player's resilience when Watermelon is collected
            program.resilience++;
            if (program.resilience > GameConfig.MAX_RESILIENCE) 
                program.resilience = GameConfig.MAX_RESILIENCE; // Ensure resilience does not exceed maximum limit
        }
    }
}

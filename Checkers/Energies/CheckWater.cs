/*
 * CheckWater.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckWater class for handling Water in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnergyChecker class, implementing the EnergyBehavior method
 * to specifically handle Water as an item in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateEnergyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnergyBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Energies
{
    public class CheckWater : DelegateEnergyChecker
    {
        public CheckWater(Game game, Program program) : base(game, program, game.WaterEmojiChar, "water_drop")
        {
            // Additional initialization specific to Water can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering Water.
        /// </summary>
        public override void EnergyBehavior()
        {
            // Increase player's resilience when Water is collected
            program.resilience++;
        }
    }
}

/*
 * CheckMeat2.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckMeat2 class for handling Meat_2 in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnergyChecker class, implementing the EnergyBehavior method
 * to specifically handle Meat_2 as an item in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateEnergyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnergyBehavior event.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
using Config;

namespace JacobResilienceGame.Energies
{
    public class CheckMeat2 : DelegateEnergyChecker
    {
        public CheckMeat2(Game game, Program program) : base(game, program, game.Meat2EmojiChar, "lion_roaring")
        {
            // Additional initialization specific to Meat_2 can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering Meat_2.
        /// </summary>
        public override void EnergyBehavior()
        {
            // Increase player's stamina and resilience when Meat_2 is collected
            program.stamina++;
            if (program.stamina > GameConfig.MAX_STAMINA) 
                program.stamina = GameConfig.MAX_STAMINA; // Ensure stamina does not exceed maximum limit

            program.resilience++;
            if (program.resilience > GameConfig.MAX_RESILIENCE) 
                program.resilience = GameConfig.MAX_RESILIENCE; // Ensure resilience does not exceed maximum limit
        }
    }
}

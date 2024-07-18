/*
 * CheckMeat1.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckMeat1 class for handling Meat_1 in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnergyChecker class, implementing the EnergyBehavior method
 * to specifically handle Meat_1 as an item in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateEnergyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnergyBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Energies
{
    public class CheckMeat1 : DelegateEnergyChecker
    {
        public CheckMeat1(Game game, Program program) : base(game, program, game.Meat1EmojiChar, "lion_roaring")
        {
            // Additional initialization specific to Meat_1 can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering Meat_1.
        /// </summary>
        public override void EnergyBehavior()
        {
            // Increase player's stamina when Meat_1 is collected
            program.stamina++;
        }
    }
}

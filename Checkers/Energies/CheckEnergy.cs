/*
 * CheckEnergy.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckEnergy class for handling generic energy items in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnergyChecker class, implementing the EnergyBehavior method
 * to handle generic energy items by increasing player's resilience.
 * 
 * This class inherits from DelegateEnergyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnergyBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Energies
{
    public class CheckEnergy : DelegateEnergyChecker
    {
        public CheckEnergy(Game game, Program program) : base(game, program, game.EnergyEmojiChar, "energy")
        {
            // Additional initialization specific to Energy can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering generic energy items.
        /// </summary>
        public override void EnergyBehavior()
        {
            // Increase player's resilience when the generic energy item is collected
            program.resilience++;
        }
    }
}

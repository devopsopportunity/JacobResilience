/*
 * CheckMagicPotion.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckMagicPotion class for handling MagicPotion in the Jacob's Resilience game.
 * It specializes the abstract DelegateEnergyChecker class, implementing the EnergyBehavior method
 * to specifically handle MagicPotion as an item in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateEnergyChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the EnergyBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Energies
{
    public class CheckMagicPotion : DelegateEnergyChecker
    {
        public CheckMagicPotion(Game game, Program program) : base(game, program, game.MagicPotionEmojiChar, "magic_potion")
        {
            // Additional initialization specific to MagicPotion can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering MagicPotion.
        /// </summary>
        public override void EnergyBehavior()
        {
            // Increase player's lives, stamina, and resilience when MagicPotion is collected
            program.lives++;
            if(program.lives > GameConfig.MAX_LIVES) program.lives = GameConfig.MAX_LIVES;
            
            program.stamina++;
            if(program.stamina > GameConfig.MAX_STAMINA) program.stamina = GameConfig.MAX_STAMINA;
            
            program.resilience++;
            if(program.resilience > GameConfig.MAX_RESILIENCE) program.resilience = GameConfig.MAX_RESILIENCE;
        }
    }
}

/*
 * DelegateEnergyChecker.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the DelegateEnergyChecker class for managing energy-related logic in the Jacob's Resilience game.
 * It specializes the abstract FactoryCheckerAbstract class, implementing the CheckForItems method
 * to specifically handle energy items in accordance with the Liskov Substitution Principle (LSP).
 * 
 * The class also utilizes the Open/Closed Principle (OCP), allowing for extension via derived classes
 * without modifying existing behavior, and incorporates the Abstract Factory Pattern to define a common interface
 * for creating instances of energy behavior handlers. Additionally, it employs the Design Pattern Delegate
 * to dynamically delegate and structure the behavior of encountering different energy types through the
 * EnergyBehavior event, enabling customizable behavior for each specific energy type.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using JacobResilienceGame.Checkers;
using Config;

namespace JacobResilienceGame.Energies
{
    /// <summary>
    /// Abstract base class representing an energy behavior delegate in the game.
    /// It inherits from FactoryCheckerAbstract for item checking functionality.
    /// </summary>
    public abstract class DelegateEnergyChecker : FactoryCheckerAbstract
    {
        protected readonly string EmojiCharacter; // Represents the emoji character associated with the energy item
        protected readonly string Sound; // Represents the sound associated with encountering the energy item

        protected DelegateEnergyChecker(Game game, Program program, string emojiCharacter, string sound) : base(game, program)
        {
            EmojiCharacter = emojiCharacter;
            Sound = sound;
            // Additional initialization specific to energy checkers can go here if needed
        }

        /// <summary>
        /// Overrides the CheckForItems method to add energy-specific checking logic.
        /// </summary>
        /// <param name="y">Y coordinate to check.</param>
        /// <param name="x">X coordinate to check.</param>
        public override async Task CheckForItems(int y, int x)
        {
            int adjustedX = (x + program.offset) % GameConfig.SCREEN_WIDTH;

            if (adjustedX >= 0 && adjustedX < GameConfig.SCREEN_WIDTH && y >= 0 && y < GameConfig.SCREEN_HEIGHT 
                && program.screen[y, adjustedX] == EmojiCharacter)
            {
                program.screen[y, adjustedX] = " ";

                EnergyBehavior(); // Invoke the dynamically assigned behavior for this energy item

                // Play the associated sound for encountering the energy item
                await soundPlayer.PlayAsync(Sound);
            }
        }

        /// <summary>
        /// Delegate signature for handling specific energy behavior.
        /// </summary>
        public abstract void EnergyBehavior();
    }
}

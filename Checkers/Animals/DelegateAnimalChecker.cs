/*
 * DelegateAnimalChecker.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the DelegateAnimalChecker class for managing animal-related logic in the Jacob's Resilience game.
 * It specializes the abstract FactoryCheckerAbstract class, implementing the CheckForItems method
 * to specifically handle animals as enemies in accordance with the Liskov Substitution Principle (LSP).
 * 
 * The class also utilizes the Open/Closed Principle (OCP), allowing for extension via derived classes
 * without modifying existing behavior, and incorporates the Abstract Factory Pattern to define a common interface
 * for creating instances of animal behavior handlers. Additionally, it employs the Design Pattern Delegate
 * to dynamically delegate and structure the behavior of encountering different animal types through the
 * AnimalBehavior event, enabling customizable behavior for each specific animal.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using JacobResilienceGame.Checkers;
using Config;

namespace JacobResilienceGame.Animals
{
    /// <summary>
    /// Abstract base class representing an animal behavior delegate in the game.
    /// It inherits from FactoryCheckerAbstract for item checking functionality.
    /// </summary>
    public abstract class DelegateAnimalChecker : FactoryCheckerAbstract
    {
        protected readonly string EmojiCharacter; // Represents the emoji character associated with the animal
        protected readonly string Sound; // Represents the sound associated with encountering the animal

        protected DelegateAnimalChecker(Game game, Program program, string emojiCharacter, string sound) : base(game, program)
        {
            EmojiCharacter = emojiCharacter;
            Sound = sound;
            // Additional initialization specific to animal checkers can go here if needed
        }

        /// <summary>
        /// Overrides the CheckForItems method to add animal-specific checking logic.
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

                AnimalBehavior(); // Invoke the dynamically assigned behavior for this animal

                // Play the associated sound for encountering the animal
                await soundPlayer.PlayAsync(Sound);
            }
        }

        /// <summary>
        /// Delegate signature for handling specific animal behavior.
        /// </summary>
        public abstract void AnimalBehavior();
    }
}

/*
 * DelegateEnemyChecker.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the DelegateEnemyChecker abstract class for managing enemies (including traps, poachers, etc.) in the Jacob's Resilience game.
 * It specializes the abstract FactoryCheckerAbstract class, implementing the CheckForItems method
 * to handle enemy items by invoking specific behavior defined in derived classes.
 * 
 * The class allows for extension via derived classes without modifying existing behavior,
 * adhering to the Open/Closed Principle (OCP), and incorporates the Abstract Factory Pattern
 * to define a common interface for creating instances of enemy behavior handlers.
 * Additionally, it employs the Design Pattern Delegate to dynamically delegate and structure
 * the behavior of encountering different enemy types through the EnemyBehavior event, enabling
 * customizable behavior for each specific enemy.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
using JacobResilienceGame.Checkers;
using Config;

namespace JacobResilienceGame.Enemies // Plural namespace name
{
    /// <summary>
    /// Abstract base class representing an enemy behavior delegate in the game.
    /// It inherits from FactoryCheckerAbstract for item checking functionality.
    /// </summary>
    public abstract class DelegateEnemyChecker : FactoryCheckerAbstract
    {
        protected readonly string EmojiCharacter; // Represents the emoji character associated with the enemy
        protected readonly string Sound; // Represents the sound associated with encountering the enemy

        protected DelegateEnemyChecker(Game game, Program program, string emojiCharacter, string sound) : base(game, program)
        {
            EmojiCharacter = emojiCharacter;
            Sound = sound;
            // Additional initialization specific to enemy can go here if needed
        }

        /// <summary>
        /// Overrides the CheckForItems method to add enemy-specific checking logic.
        /// </summary>
        /// <param name="y">Y coordinate to check.</param>
        /// <param name="x">X coordinate to check.</param>
        public override async Task CheckForItems(int y, int x)
        {
            int adjustedX = (x + program.offset) % GameConfig.Instance.SCREEN_WIDTH;

            if (adjustedX >= 0 && adjustedX < GameConfig.Instance.SCREEN_WIDTH && y >= 0 && y < GameConfig.Instance.SCREEN_HEIGHT 
                && program.screen[y, adjustedX] == EmojiCharacter)
            {
                program.screen[y, adjustedX] = " ";

                EnemyBehavior(); // Invoke the dynamically assigned behavior for this enemy

                // Play the associated sound for encountering the enemy
                await soundPlayer.PlayAsync(Sound);
            }
        }

        /// <summary>
        /// Delegate signature for handling specific enemy behavior.
        /// </summary>
        public abstract void EnemyBehavior();
    }
}

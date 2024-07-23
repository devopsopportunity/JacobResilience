/*
 * DelegateCoinChecker.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the DelegateCoinChecker abstract class for managing coin-related logic in the Jacob's Resilience game.
 * It specializes the abstract FactoryCheckerAbstract class, implementing the CheckForItems method
 * to handle coin items by invoking specific behavior defined in derived classes.
 * 
 * The class allows for extension via derived classes without modifying existing behavior,
 * adhering to the Open/Closed Principle (OCP), and incorporates the Abstract Factory Pattern
 * to define a common interface for creating instances of coin behavior handlers.
 * Additionally, it employs the Design Pattern Delegate to dynamically delegate and structure
 * the behavior of encountering different coin types through the CoinBehavior event, enabling
 * customizable behavior for each specific coin.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
using JacobResilienceGame.Checkers;
using Config;

namespace JacobResilienceGame.Coins
{
    /// <summary>
    /// Abstract base class representing a coin behavior delegate in the game.
    /// It inherits from FactoryCheckerAbstract for item checking functionality.
    /// </summary>
    public abstract class DelegateCoinChecker : FactoryCheckerAbstract
    {
        protected readonly string EmojiCharacter; // Represents the emoji character associated with the coin
        protected readonly string Sound; // Represents the sound associated with encountering the coin

        protected DelegateCoinChecker(Game game, Program program, string emojiCharacter, string sound) : base(game, program)
        {
            EmojiCharacter = emojiCharacter;
            Sound = sound;
            // Additional initialization specific to coin can go here if needed
        }

        /// <summary>
        /// Overrides the CheckForItems method to add coin-specific checking logic.
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

                CoinBehavior(); // Invoke the dynamically assigned behavior for this coin

                // Play the associated sound for encountering the coin
                await soundPlayer.PlayAsync(Sound);
            }
        }

        /// <summary>
        /// Delegate signature for handling specific coin behavior.
        /// </summary>
        public abstract void CoinBehavior();
    }
}

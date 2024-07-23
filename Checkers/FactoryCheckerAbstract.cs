/*
 * FactoryCheckerAbstract.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the abstract FactoryCheckerAbstract class
 * for checking items in the Jacob's Resilience game. It serves
 * as a base class for different types of Item Checkers.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
using Modules;
using Config;

namespace JacobResilienceGame.Checkers
{
    /// <summary>
    /// Abstract base class for item checkers in the Jacob's Resilience game.
    /// Implements the Abstract Factory Design Pattern to provide a common interface
    /// for checking items at specified coordinates. Derived classes will implement 
    /// the specific checking logic for different types of items.
    /// This pattern enhances code reusability and maintainability while adhering
    /// to SOLID principles like the Open/Closed Principle and Liskov Substitution Principle (LSP).
    /// </summary>
    public abstract class FactoryCheckerAbstract
    {
        protected readonly Game game; // Reference to the Game instance for emoji and configuration access
        protected Program program; // Reference to the main Program instance for interaction
        protected readonly SoundPlayer soundPlayer; // Sound player instance for playing game sounds

        /// <summary>
        /// Initializes a new instance of the FactoryCheckerAbstract class.
        /// </summary>
        /// <param name="game">Instance of the Game class for accessing emojis and configurations.</param>
        /// <param name="program">Instance of the Program class for interaction.</param>
        protected FactoryCheckerAbstract(Game game, Program program)
        {
            this.game = game;
            this.program = program;
            this.soundPlayer = new SoundPlayer(); // Initialize sound player
        }

        /// <summary>
        /// Abstract method to be implemented by derived classes.
        /// Checks for items at the specified coordinates in the game.
        /// </summary>
        /// <param name="y">Y coordinate to check.</param>
        /// <param name="x">X coordinate to check.</param>
        public abstract Task CheckForItems(int y, int x);
    }
}

/*
 * FactoryCheckerAbstract.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the abstract FactoryCheckerAbstract class
 * for checking items in the Jacob's Resilience game. It serves
 * as a base class for different types of Item Checkers.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using System;
using System.Threading.Tasks;
using Modules; // Assuming Emoji and EmojiDatabase classes are in this namespace

namespace JacobResilienceGame.Checkers
{
    /// <summary>
    /// Abstract base class using the Design Pattern Abstract Factory for checking items in the game.
    /// It defines a common interface (CheckForItems method) that specific item checker classes must implement.
    /// This pattern promotes code reusability, enhances maintainability, and adheres to SOLID principles
    /// such as Open/Closed and Liskov Substitution Principle (LSP).
    /// </summary>
    public abstract class FactoryCheckerAbstract
    {
        protected readonly Game game; // Reference to the Game instance for emoji and configuration access
        protected Program program; // Reference to the main Program instance for interaction
        protected readonly SoundPlayer soundPlayer; // Sound player instance for playing game sounds

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

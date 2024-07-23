/*
 * CheckCrocodile.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckCrocodile class for handling crocodiles in the Jacob's Resilience game.
 * It specializes the abstract DelegateAnimalChecker class, implementing the AnimalBehavior method
 * to specifically handle crocodiles as enemies in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateAnimalChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the AnimalBehavior event.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
using Config;

namespace JacobResilienceGame.Animals
{
    /// <summary>
    /// Concrete implementation of DelegateAnimalChecker for handling crocodiles.
    /// </summary>
    public class CheckCrocodile : DelegateAnimalChecker
    {
        /// <summary>
        /// Initializes a new instance of the CheckCrocodile class.
        /// </summary>
        /// <param name="game">Instance of the Game class for accessing emojis and configurations.</param>
        /// <param name="program">Instance of the Program class for interaction.</param>
        public CheckCrocodile(Game game, Program program) : base(game, program, game.CrocodileEmojiChar, "crocodile")
        {
            // Additional initialization specific to crocodiles can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering a crocodile.
        /// </summary>
        public override void AnimalBehavior()
        {
            // Decrease player's stamina when a crocodile bites
            program.stamina -= 1;
        }
    }
}

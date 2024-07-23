/*
 * CheckSnake1.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckSnake1 class for handling Snake1 in the Jacob's Resilience game.
 * It specializes the abstract DelegateAnimalChecker class, implementing the AnimalBehavior method
 * to specifically handle Snake1 as an enemy in accordance with the Liskov Substitution Principle (LSP).
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
    /// Concrete implementation of DelegateAnimalChecker for handling Snake1.
    /// </summary>
    public class CheckSnake1 : DelegateAnimalChecker
    {
        /// <summary>
        /// Initializes a new instance of the CheckSnake1 class.
        /// </summary>
        /// <param name="game">Instance of the Game class for accessing emojis and configurations.</param>
        /// <param name="program">Instance of the Program class for interaction.</param>
        public CheckSnake1(Game game, Program program) : base(game, program, game.Snake1EmojiChar, "snake1")
        {
            // Additional initialization specific to Snake1 can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering Snake1.
        /// </summary>
        public override void AnimalBehavior()
        {
            // Decrease player's lives when Snake1 is encountered
            program.lives -= 1;
        }
    }
}

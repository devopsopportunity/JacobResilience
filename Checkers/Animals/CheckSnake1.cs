/*
 * CheckSnake1.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckSnake1 class for handling Snake1 in the Jacob's Resilience game.
 * It specializes the abstract FactoryCheckerAbstract class, implementing the CheckForItems method
 * to specifically handle Snake1 as an enemy in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateAnimalChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the AnimalBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Animals
{
    public class CheckSnake1 : DelegateAnimalChecker
    {
        public CheckSnake1(Game game, Program program) : base(game, program, game.Snake1EmojiChar, "snake1")
        {
            // Additional initialization specific to Snake1 can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering Snake1.
        /// </summary>
        public override void AnimalBehavior()
        {
            // Decrease player's resilience or health when Snake1 is encountered
            program.lives -= 1;
        }
    }
}

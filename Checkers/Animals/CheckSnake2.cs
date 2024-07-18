/*
 * CheckSnake2.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckSnake2 class for handling Snake2 in the Jacob's Resilience game.
 * It specializes the abstract FactoryCheckerAbstract class, implementing the CheckForItems method
 * to specifically handle Snake2 as an enemy in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateAnimalChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the AnimalBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Animals
{
    public class CheckSnake2 : DelegateAnimalChecker
    {
        public CheckSnake2(Game game, Program program) : base(game, program, game.Snake2EmojiChar, "snake2")
        {
            // Additional initialization specific to Snake2 can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering Snake2.
        /// </summary>
        public override void AnimalBehavior()
        {
            // Decrease player's resilience or health when Snake2 is encountered
            program.lives -= 1;
        }
    }
}

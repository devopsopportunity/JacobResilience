/*
 * CheckHippopotamus.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckHippopotamus class for handling hippos in the Jacob's Resilience game.
 * It specializes the abstract FactoryCheckerAbstract class, implementing the CheckForItems method
 * to specifically handle hippos as enemies in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateAnimalChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the AnimalBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Animals
{
    public class CheckHippopotamus : DelegateAnimalChecker
    {
        public CheckHippopotamus(Game game, Program program) : base(game, program, game.HippopotamusEmojiChar, "hippo")
        {
            // Additional initialization specific to hippos can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering a hippopotamus.
        /// </summary>
        public override void AnimalBehavior()
        {
            // Decrease player's resilience or health when a hippo is encountered
            // Example: Decrease resilience by 1
            program.resilience -= 1;
        }
    }
}

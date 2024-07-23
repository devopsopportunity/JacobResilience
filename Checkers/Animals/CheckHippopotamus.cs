/*
 * CheckHippopotamus.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckHippopotamus class for handling hippos in the Jacob's Resilience game.
 * It specializes the abstract DelegateAnimalChecker class, implementing the AnimalBehavior method
 * to specifically handle hippos as enemies in accordance with the Liskov Substitution Principle (LSP).
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
    /// Concrete implementation of DelegateAnimalChecker for handling hippopotamuses.
    /// </summary>
    public class CheckHippopotamus : DelegateAnimalChecker
    {
        /// <summary>
        /// Initializes a new instance of the CheckHippopotamus class.
        /// </summary>
        /// <param name="game">Instance of the Game class for accessing emojis and configurations.</param>
        /// <param name="program">Instance of the Program class for interaction.</param>
        public CheckHippopotamus(Game game, Program program) : base(game, program, game.HippopotamusEmojiChar, "hippo")
        {
            // Additional initialization specific to hippos can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering a hippopotamus.
        /// </summary>
        public override void AnimalBehavior()
        {
            // Decrease player's resilience when a hippopotamus is encountered
            program.resilience -= 1;
        }
    }
}

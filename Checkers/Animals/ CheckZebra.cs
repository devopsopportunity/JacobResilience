/*
 * CheckZebra.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the CheckZebra class for handling zebras in the Jacob's Resilience game.
 * It specializes the abstract DelegateAnimalChecker class, implementing the AnimalBehavior method
 * to specifically handle zebras as enemies in accordance with the Liskov Substitution Principle (LSP).
 * 
 * This class inherits from DelegateAnimalChecker to leverage the abstract behavior definition and
 * dynamically assigned behavior via the AnimalBehavior event.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using Config;

namespace JacobResilienceGame.Animals
{
    public class CheckZebra : DelegateAnimalChecker
    {
        public CheckZebra(Game game, Program program) : base(game, program, game.ZebraEmojiChar, "zebra")
        {
            // Additional initialization specific to zebras can go here if needed
        }

        /// <summary>
        /// Defines the behavior when encountering a zebra.
        /// </summary>
        public override void AnimalBehavior()
        {
            // Decrease player's stamina and resilience when a zebra is encountered
            program.stamina -= 1;
            program.resilience -= 1;
        }
    }
}

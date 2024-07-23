/*
 * PlayerStatus.cs
 * @authors 
 * Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the PlayerStatus class, which manages the
 * player's current game status, including scores, credits, and
 * levels. It includes properties to store the player's progress
 * and methods to format and display the status. The class provides
 * functionality to convert the status to a string representation
 * and to output it in a readable format.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */

namespace Modules
{
    public class PlayerStatus
    {
        public int Score { get; set; }
        public int LevelScore { get; set; }
        public int Credit { get; set; }
        public int Credit2 { get; set; }
        public int Level { get; set; }
        
        /// <summary>
        /// Constructor for PlayerStatus class.
        /// </summary>
        /// <param name="score">The player's total score.</param>
        /// <param name="levelScore">The score for the current level.</param>
        /// <param name="credit">The player's credit.</param>
        /// <param name="credit2">The player's secondary credit.</param>
        /// <param name="level">The player's current level.</param>
        public PlayerStatus(int score, int levelScore, int credit, int credit2, int level)
        {
            Score = score;
            LevelScore = levelScore;
            Credit = credit;
            Credit2 = credit2;
            Level = level;
        }

        /// <summary>
        /* Overrides the ToString method to return the player status
         * as a comma-separated string.
        */
        /// </summary>
        /// <returns>A string representation of the player status.</returns>
        public override string ToString()
        {
            return $"{Score},{LevelScore},{Credit},{Credit2},{Level}";
        }

        /// <summary>
        /* Displays the current player status in a readable format. */
        /// </summary>
        public void DisplayStatus()
        {
            Console.WriteLine($"Score: {Score}, LevelScore: {LevelScore}, Credit: {Credit}, Credit2: {Credit2}, Level: {Level}");
        }
    }
}

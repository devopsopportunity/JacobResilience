/*
 * ScoreInterval.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the score interval class used in game configurations.
 * It represents a range of scores during which specific game entities appear.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */

namespace Config
{
    public class ScoreInterval
    {
        public int Start { get; set; }  // Start of the score interval
        public int End { get; set; }    // End of the score interval

        // Default constructor
        public ScoreInterval()
        {
            // Default values are set to 0 for Start and End
        }

        // Parameterized constructor
        public ScoreInterval(int start, int end)
        {
            Start = start;
            End = end;
        }

        // Method to override ToString() for better debugging and logging
        public override string ToString()
        {
            return $"[Start: {Start}, End: {End}]";
        }
    }
}

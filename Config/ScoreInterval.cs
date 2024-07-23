/*
 * ScoreInterval.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the ScoreInterval class used in game configurations.
 * It represents a range of scores during which specific game entities appear.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */

namespace Config
{
    /// <summary>
    /// Represents a range of scores during which specific game entities appear.
    /// </summary>
    public class ScoreInterval
    {
        /// <summary>
        /// Gets or sets the start of the score interval.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the end of the score interval.
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// Default constructor initializing the interval with default values.
        /// </summary>
        public ScoreInterval()
        {
            // Default values are set to 0 for Start and End
        }

        /// <summary>
        /// Parameterized constructor to initialize the interval with specified values.
        /// </summary>
        /// <param name="start">The start of the score interval.</param>
        /// <param name="end">The end of the score interval.</param>
        public ScoreInterval(int start, int end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Overrides the ToString() method to provide a string representation of the ScoreInterval.
        /// </summary>
        /// <returns>A string representing the ScoreInterval.</returns>
        public override string ToString()
        {
            return $"[Start: {Start}, End: {End}]";
        }
    }
}

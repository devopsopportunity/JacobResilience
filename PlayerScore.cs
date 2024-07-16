/*
 * PlayerScore.cs
 * -------------------------------------------------------------
 * This file defines the PlayerScore class which represents
 * a player's score in the Jacob's Resilience game. It includes
 * properties for username, score, total credits, current date
 * and time, position in the leaderboard, and level achieved.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */

using System;

namespace JacobResilienceGame
{
    public class PlayerScore
    {
        public string Username { get; set; }
        public int Score { get; set; }
        public int TotalCredits { get; set; }
        public DateTime Date { get; set; }
        public int Level { get; set; }    // Level achieved

        // Constructor to initialize the PlayerScore object
        public PlayerScore(string username, int score, int totalCredits, int level)
        {
            Username = username;
            Score = score;
            TotalCredits = totalCredits;
            Date = DateTime.Now; // Set current date and time
            Level = level;
        }

        // Method to display the player's score information with a lion emoji
        public void DisplayScore()
        {
            Console.WriteLine($"{Username}, Score: {Score}, Total Credits: {TotalCredits}, Date: {Date.ToShortDateString()}, Time: {Date.ToShortTimeString()}, Level: {Level}");
        }
    }
}

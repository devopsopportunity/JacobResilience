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
namespace Modules
{
    public class PlayerScore
    {
        public string Username { get; set; }
        public int Score { get; set; }
        public int TotalCredits { get; set; }
        public int Level { get; set; }    // Level achieved
        public DateTime Date { get; set; }

        // Constructor to initialize the PlayerScore object
        public PlayerScore(string username, int score, int totalCredits, int level)
        {
            Username = username;
            Score = score;
            TotalCredits = totalCredits;
            Level = level;
            Date = DateTime.Now; // Set current date and time
        }


        // Constructor to initialize the PlayerScore object
        public PlayerScore(string username, int score, int totalCredits, int level, DateTime dateTime)
        {
            Username = username;
            Score = score;
            TotalCredits = totalCredits;
            Level = level;
            Date = dateTime;
        }

        // Method to display the player's score information with a lion emoji
        public void DisplayScore()
        {
            Console.WriteLine($"{Username}, Score: {Score}, Total Credits: {TotalCredits}, Level: {Level}, Date: {Date.ToShortDateString()}, Time: {Date.ToShortTimeString()}");
        }

        // Override ToString method to provide a string representation of the PlayerScore object
        public override string ToString()
        {
            return $"{Username},{Score},{TotalCredits},{Level},{Date.ToString("yyyy-MM-ddTHH:mm:ss")}";
        }
    }
}

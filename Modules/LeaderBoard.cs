/*
 * LeaderBoard.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the LeaderBoard class, managing the high
 * scores and player rankings for the Jacob's Resilience game.
 * It includes methods for adding, sorting, and displaying scores.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using JacobResilienceGame;

namespace Modules
{
    public class LeaderBoard
    {
        private Program program;

        // Static array for the leaderboard of the top 10 scores.
        private  PlayerScore[] leaderboard = new PlayerScore[10];

        // Constructor accepting a Program object
        public LeaderBoard(Program program)
        {
            this.program = program;
        }

        // Add more leaderboard methods here

        // Static array for the leaderboard of the top 10 scores
        public void InitializeLeaderboard()
        {
            for (int i = 0; i < leaderboard.Length; i++)
            {
                leaderboard[i] = new PlayerScore("AAA", 0, 0, 0); // Default score
            }
        }

        // Updated youLose method to handle leaderboard
        public void youLose()
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.PlayAsync("game_over");

            Console.Clear();

            // Display "You Lose!" screen
            Console.WriteLine("💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀");
            Console.WriteLine("💀                                💀");
            Console.WriteLine("💀            YOU LOSE!            💀");
            Console.WriteLine($"💀          Score: {program.score.ToString().PadLeft(6)}         💀");
            Console.WriteLine($"💀          Credit: {(program.credit + (program.credit2 * 100)).ToString().PadLeft(6)}         💀");
            Console.WriteLine("💀                                💀");
            Console.WriteLine("💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀");

            string initials = string.Empty;
            bool isValidInitials = false;

            // Loop until valid initials are entered
            while (!isValidInitials)
            {
                Console.WriteLine("\nEnter your initials for high score (3 letters): ");

                // Read input character by character
                initials = ReadInitials();

                // Check if initials are valid
                if (initials.Length == 3)
                {
                    isValidInitials = true;
                }
                else
                {
                    Console.WriteLine("Invalid initials. Please enter exactly 3 letters.");
                }
            }

            // After valid initials are entered, update the leaderboard
            PlayerScore currentPlayer = new PlayerScore(initials.ToUpper(), program.score, program.credit + (program.credit2 * 100), program.levels);
            UpdateLeaderboard(currentPlayer);

            // Display leaderboard
            DisplayLeaderboard();

            // After valid initials are entered, express gratitude and invite to restart
            Console.WriteLine($"\n🙏 Thank you, {initials}, for playing! Your score: {program.score} 🎮");

            // After valid initials are entered, restart the game
            Console.WriteLine("\nPress any key to restart...");
            Console.ReadKey(true);
        }

        private string ReadInitials()
        {
            string initials = string.Empty;
            int initialsCount = 0;

            while (initialsCount < 3)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Check if the key pressed is a letter
                if (char.IsLetter(keyInfo.KeyChar))
                {
                    initials += keyInfo.KeyChar;
                    initialsCount++;
                    Console.Write(keyInfo.KeyChar);
                }
                // Check if the user pressed Enter to submit initials
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            return initials;
        }

        // Method to update the leaderboard with a new score
        private void UpdateLeaderboard(PlayerScore newScore)
        {
            // Update only if the new score is higher than any of the top 10
            for (int i = 0; i < leaderboard.Length; i++)
            {
                if (newScore.Score > leaderboard[i].Score)
                {
                    // Insert the new score at the i-th position
                    Array.Copy(leaderboard, i, leaderboard, i + 1, leaderboard.Length - i - 1);
                    leaderboard[i] = newScore;
                    break;
                }
            }
        }

        // Method to display the leaderboard
        public void DisplayLeaderboard()
        {
            Console.WriteLine("🦁 Leaderboard 🏆");
            Console.WriteLine(new string('-', 20)); // Dashed line

            for (int i = 0; i < leaderboard.Length; i++)
            {
                Console.Write($"🦁 {i + 1}: ");
                leaderboard[i].DisplayScore(); // Uses the DisplayScore method of the PlayerScore class
            }
            
            Console.WriteLine(new string('-', 20)); // Dashed line after the leaderboard
        }

    }
}

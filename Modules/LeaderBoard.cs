/*
 * LeaderBoard.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the LeaderBoard class, which manages the high
 * scores and player rankings for the Jacob's Resilience game.
 * It includes functionality for adding, sorting, displaying scores,
 * and handling encryption and decryption of leaderboard data.
 * -------------------------------------------------------------
 * @Hackathon July 13th to 23rd, 2024
 */
using System.Security.Cryptography;
using System.Text;
using Config;
using JacobResilienceGame;

namespace Modules
{
    public class LeaderBoard
    {
        private Program program;
        private AudioManager audioManager;

        // Static array for the leaderboard of the top 10 scores.
        private PlayerScore[] leaderboard = new PlayerScore[10];

        // Encryption key and IV
        private readonly byte[] encryptionKey = Encoding.UTF8.GetBytes("A1B2C3D4E5F60708"); // 16 bytes key for AES-128
        private readonly byte[] encryptionIV = Encoding.UTF8.GetBytes("1A2B3C4D5E6F7089"); // 16 bytes IV for AES

        // Constructor accepting a Program object
        public LeaderBoard(Program program)
        {
            this.program = program;
            audioManager = new AudioManager(GameConfig.PLAY_BACK_WAIT_FOR_KEY);
            InitializeLeaderboard();
            ReadFromFile();
            LoadPlayerStatus();
        }

        // Initialize leaderboard with default values
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
            program.StopWaveMusic();

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

            if (Program.audioOn)
            {
                // Play the audio for audio on, if necessary
                audioManager.PlayBackgroundMusicAsync();
            }

            // After valid initials are entered, update the leaderboard
            PlayerScore currentPlayer = new PlayerScore(initials.ToUpper(), program.score, program.credit + (program.credit2 * 100), program.levels);
            UpdateLeaderboard(currentPlayer);

            // Display leaderboard
            DisplayLeaderboard();

            // Save player status
            PlayerStatus playerStatus = new PlayerStatus(program.score, program.levelScore, program.credit, program.credit2, program.levels);
            SavePlayerStatus(playerStatus);

            // After valid initials are entered, express gratitude and invite to restart
            Console.WriteLine($"\n🙏 Thank you, {initials}, for playing! Your score: {program.score} 🎮");

            // Prompt the user to press the space bar to continue
            Console.WriteLine("\nPress space bar to continue...");
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
            {
                // Wait for the space bar to be pressed
            }

            if (Program.audioOn)
            {
                // Stop the audio for audio off, if necessary
                audioManager.StopBackgroundMusic();
            }
        }

        // Method to display "You Win!" screen
        public void youWin()
        {
            program.StopWaveMusic();

            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.PlayAsync("win"); // Play a win sound

            Console.Clear();

            // Display "You Win!" screen
            Console.WriteLine("🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉");
            Console.WriteLine("🎉                                🎉");
            Console.WriteLine("🎉            YOU WIN!            🎉");
            Console.WriteLine($"🎉          Score: {program.score.ToString().PadLeft(6)}         🎉");
            Console.WriteLine($"🎉          Credit: {(program.credit + (program.credit2 * 100)).ToString().PadLeft(6)}         🎉");
            Console.WriteLine("🎉                                🎉");
            Console.WriteLine("🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉🎉");

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

            if (Program.audioOn)
            {
                // Play the audio for audio on, if necessary
                audioManager.PlayBackgroundMusicAsync();
            }

            // After valid initials are entered, update the leaderboard
            PlayerScore currentPlayer = new PlayerScore(initials.ToUpper(), program.score, program.credit + (program.credit2 * 100), program.levels);
            UpdateLeaderboard(currentPlayer);

            // Display leaderboard
            DisplayLeaderboard();

            // After valid initials are entered, express gratitude and invite to restart
            Console.WriteLine($"\n🙏 Thank you, {initials}, for playing! Your score: {program.score} 🎮");

            // Prompt the user to press the space bar to continue
            Console.WriteLine("\nPress space bar to continue...");
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
            {
                // Wait for the space bar to be pressed
            }

            if (Program.audioOn)
            {
                // Stop the audio for audio off, if necessary
                audioManager.StopBackgroundMusic();
            }
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

        // Method to read leaderboard from file
        public void ReadFromFile()
        {
            if (File.Exists(GameConfig.LEADER_BOARD_FILE))
            {
                string encryptedData = File.ReadAllText(GameConfig.LEADER_BOARD_FILE);
                string decryptedData = Decrypt(encryptedData);

                string[] lines = decryptedData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i = 0; i < lines.Length && i < leaderboard.Length; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (parts.Length == 5)
                    {
                        leaderboard[i] = new PlayerScore(
                            parts[0],
                            int.Parse(parts[1]),
                            int.Parse(parts[2]),
                            int.Parse(parts[3]),
                            DateTime.Parse(parts[4])
                        );
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found. Initializing default leaderboard.");
                InitializeLeaderboard();
                WriteToFile(); // Create a new file with default values
            }
        }

        // Method to write leaderboard to file
        public void WriteToFile()
        {
            string[] lines = new string[leaderboard.Length];
            for (int i = 0; i < leaderboard.Length; i++)
            {
                lines[i] = leaderboard[i].ToString();
            }
            string dataToEncrypt = string.Join(Environment.NewLine, lines);
            string encryptedData = Encrypt(dataToEncrypt);
            File.WriteAllText(GameConfig.LEADER_BOARD_FILE, encryptedData);
        }

        // Method to encrypt data
        private string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.IV = encryptionIV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        // Method to decrypt data
        private string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.IV = encryptionIV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        // Method to save player status to file
        public void SavePlayerStatus(PlayerStatus playerStatus)
        {
            string dataToEncrypt = playerStatus.ToString();
            string encryptedData = Encrypt(dataToEncrypt);
            File.WriteAllText(GameConfig.PLAYER_STATUS_FILE, encryptedData);
        }

        // Method to load player status from file
        public void LoadPlayerStatus()
        {
            if (File.Exists(GameConfig.PLAYER_STATUS_FILE))
            {
                string encryptedData = File.ReadAllText(GameConfig.PLAYER_STATUS_FILE);
                string decryptedData = Decrypt(encryptedData);

                string[] parts = decryptedData.Split(',');
                if (parts.Length == 5)
                {
                    PlayerStatus lastPlayerStatus = new PlayerStatus(
                        int.Parse(parts[0]),
                        int.Parse(parts[1]),
                        int.Parse(parts[2]),
                        int.Parse(parts[3]),
                        int.Parse(parts[4])
                    );

                    // Restore the player's state
                    program.score = lastPlayerStatus.Score - lastPlayerStatus.LevelScore;
                    program.levelScore = 0;
                    program.credit = lastPlayerStatus.Credit;
                    program.credit2 = lastPlayerStatus.Credit2;
                    program.levels = lastPlayerStatus.Level;
                }
            }
            else
            {
                // Console.WriteLine("Player status file not found. Starting a new game.");
            }
        }
    } // end class
} // end module

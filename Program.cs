/*
 * Program.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the main Program class for managing
 * the Jacob's Resilience game. It includes game initialization,
 * input handling, world updating, and screen drawing.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */

using EmojiGame; // Assuming Emoji and EmojiDatabase classes are in this namespace
using Modules;   // Namespace containing GameMenu, SoundPlayer class
using JacobResilienceGame.Checkers;

namespace JacobResilienceGame
{
    public class Program
    {
        // Static array for the leaderboard of the top 10 scores.
        private static PlayerScore[] leaderboard = new PlayerScore[10];

        // Game components
        private GameMenu gameMenu;
        private Game game;
        private FactoryCheckerAbstract checkCoins1;
        private FactoryCheckerAbstract checkCoins2;
        private FactoryCheckerAbstract checkCoins3;
        private FactoryCheckerAbstract checkPoachers;

        // Input handling
        private readonly Queue<ConsoleKeyInfo> inputQueue = new Queue<ConsoleKeyInfo>();
        private readonly object lockObject = new object();

        // Game state variables
        private bool showMenu;              // Flag indicating if the menu is visible
        private bool restart;               // Flag indicating if you want to restart
        public string[,] screen;            // Matrix representing the game screen
        private string[,] screenBackup;     // Matrix backup
        public int offset;                  // Offset for screen scrolling
        private int playerPosX;             // X position of the player
        private int playerPosY;             // Y position of the player
        private int jumpVelocity;           // Player's jump velocity
        private int score;                  // Player's score
        public int credit;                  // Player's credits
        public int credit2;                 // Player's credits2
        public int resilience;              // Player's resilience
        public int stamina;                 // Player's stamina
        public int lives;                   // Player's lives
        public int levels;                  // Game's levels

        // Array of CheckersConfig
        private CheckersConfig[]? checkersConfigs = null;

        public Program()
        {
            // Initialize game components
            gameMenu = new GameMenu(); 
            game = new Game();
            screen = new string[GameConfig.SCREEN_HEIGHT, GameConfig.SCREEN_WIDTH];
            screenBackup = new string[GameConfig.SCREEN_HEIGHT, GameConfig.SCREEN_WIDTH];
            checkCoins1 = new CheckCoins1(game, this);
            checkCoins2 = new CheckCoins2(game, this);
            checkCoins3 = new CheckCoins3(game, this);
            checkPoachers = new CheckPoachers(game, this);
            InitializeLeaderboard();
            InitializeGame();
        }

        private void InitializeGame() {
            // Initialize player position and game variables
            playerPosX = GameConfig.PLAYER_INITIAL_X;
            playerPosY = GameConfig.PLAYER_INITIAL_Y;
            jumpVelocity = 0;
            showMenu = false;
            offset = 0;
            score = 0;
            credit = 0;
            credit2 = 0;
            resilience = GameConfig.INIT_RESILIENCE;
            stamina = GameConfig.INIT_STAMINA;
            lives = GameConfig.INIT_LIVES;
            levels = 0;

            InitializeWorld(); // Initialize the game world
            checkersConfigs = CheckersConfigInitializer.InitializeCheckersConfigs(game); // Initialize the CheckersConfig array
            Console.Clear();
        }

        // Static array for the leaderboard of the top 10 scores
        private static void InitializeLeaderboard()
        {
            for (int i = 0; i < leaderboard.Length; i++)
            {
                leaderboard[i] = new PlayerScore("AAA", 0, 0, 0); // Default score
            }
        }

        // Method to update the leaderboard with a new score
        private static void UpdateLeaderboard(PlayerScore newScore)
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
        private static void DisplayLeaderboard()
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

        /// <summary>
        /// Entry point of the application. Starts the game loop asynchronously.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.RunAsync();
        }

        /// <summary>
        /// Asynchronously runs the main game loop.
        /// </summary>
        public async Task RunAsync()
        {
            Console.CursorVisible = false;

            Thread inputThread = new Thread(HandleInput);
            inputThread.Start();            

            while (true) {
            
                ShowWelcomeScreen();
                InitializeGame();

                restart = false;

                SoundPlayer soundPlayer = new SoundPlayer();
                await soundPlayer.PlayAsync("game_start");

                while (!restart)
                {
                    if (!showMenu)
                    {
                        ProcessInput();
                        await UpdateWorld();                    
                        DrawScreen();

                        // Check for game over condition
                        if (lives < 0)
                        {
                            youLose(); // Call the method to display death screen
                        }
                    }
                    else
                    {
                        gameMenu.ShowMenu(GameConfig.SCREEN_HEIGHT, GameConfig.SCREEN_WIDTH, showMenu, ReturnToGame, ExitGame);
                    }

                    await Task.Delay(GameConfig.PAUSE_CONTROL);
                } // while 1
            } // while 2   
        }

        /// <summary>
        /// Handles keyboard input asynchronously.
        /// </summary>
        private void HandleInput()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    lock (lockObject)
                    {
                        inputQueue.Enqueue(key);
                    }
                }
                Thread.Sleep(1); // Small delay to avoid overloading the CPU
            }
        }

        /// <summary>
        /// Processes the queued input from the player.
        /// </summary>
        private void ProcessInput()
        {
            lock (lockObject)
            {
                while (inputQueue.Count > 0)
                {
                    var key = inputQueue.Dequeue();
                    if (key.Key == ConsoleKey.UpArrow && playerPosY == GameConfig.SCREEN_HEIGHT - 1)
                        jumpVelocity = GameConfig.START_JUMP_VELOCITY;
                    else if (key.Key == ConsoleKey.RightArrow && playerPosX < GameConfig.SCREEN_WIDTH - 5)
                        playerPosX++;
                    else if (key.Key == ConsoleKey.LeftArrow && playerPosX > 0)
                        playerPosX--;
                    else if (key.Key == ConsoleKey.M)
                        showMenu = true;
                    else if (key.Key == ConsoleKey.R)
                        restart = true;
                    else if (key.Key == ConsoleKey.Q)
                        ExitGame();
                }
            }
        }

        /// <summary>
        /// Returns the game to the main screen from the menu.
        /// </summary>
        private void ReturnToGame()
        {
            showMenu = false;
        }


        /// <summary>
        /// Exits the game application.
        /// </summary>
        public void ExitGame()
        {
            Console.Clear();
            Console.WriteLine("Thank you for playing Jacob's Resilience! 🦁");
            Environment.Exit(0);
        }

        /// <summary>
        /// Initializes the game world, including the screen and initial game variables.
        /// </summary>
        private void InitializeWorld()
        {
            screen = new string[GameConfig.SCREEN_HEIGHT, GameConfig.SCREEN_WIDTH];
            screenBackup = new string[GameConfig.SCREEN_HEIGHT, GameConfig.SCREEN_WIDTH];

            // Initialize the screen with the background and initial positions
            for (int y = 0; y < GameConfig.SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < GameConfig.SCREEN_WIDTH; x++)
                {
                    if (x == GameConfig.SCREEN_WIDTH - 1 && y == 1)
                        screen[y, x] = game.CloudsunEmojiChar;
                    else if (x == GameConfig.SCREEN_WIDTH - 3 && y == 1)
                        screen[y, x] = game.RainbowEmojiChar;
                    else if (y == 2 && x % 6 == 0 && !((x == GameConfig.SCREEN_WIDTH - 1) && (x == GameConfig.SCREEN_WIDTH - 2)))
                        screen[y, x] = game.CloudEmojiChar;
                    else if (y == 3 && x % 5 == 0 && !(x % 7 == 0))
                        screen[y, x] = game.CloudEmojiChar;
                    else if (y == 3 && x % 7 == 0)
                        screen[y, x] = game.MountainEmojiChar;
                    else
                        screen[y, x] = " ";

                    // Save a copy in the backup screen
                    screenBackup[y, x] = screen[y, x];
                }
            }
        }

        /// <summary>
        /// Draws the current state of the game screen.
        /// </summary>
        private void DrawScreen()
        {
            // Draw the game screen
            Console.SetCursorPosition(0, 0);

            string currentLevel = "";
            switch(levels) {
                case 0: currentLevel = game.LevelEmojiChar; break;
                case 1: currentLevel = game.Level1EmojiChar; break;
                case 2: currentLevel = game.Level2EmojiChar; break;
                default: currentLevel = game.Level2EmojiChar; break;
            }
            Console.WriteLine("LEVEL " + currentLevel);

            for (int y = 0; y < GameConfig.SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < GameConfig.SCREEN_WIDTH; x++)
                {
                    int adjustedX = (x + offset) % GameConfig.SCREEN_WIDTH;
                    if (x == playerPosX && y == playerPosY)
                    {
                        Console.Write("\b" + game.PlayerEmojiChar);
                    }
                    else
                    {
                        Console.Write(screen[y, adjustedX]);
                    }
                }
                Console.WriteLine();
            }
            for (int x = 0; x < GameConfig.SCREEN_WIDTH / 2; x++) Console.Write(game.VegetationEmojiChar);
            Console.WriteLine("\nPress 'M' = Show Menu, 'R' = Restart Game");
            Console.WriteLine($"Score: {score}");
            Console.WriteLine("CREDIT: " + game.Coin1EmojiChar + " " + $"{credit}");
            Console.WriteLine("BAG OF COINS: " + game.Coin2EmojiChar + " " + $"{credit2}");
            //
            Console.Write("RESILIENCE: ");
            for(int i=0; i<resilience; i++) Console.Write(game.ResilienceEmojiChar + " ");
            for(int i=resilience; i<GameConfig.MAX_RESILIENCE; i++) Console.Write("  ");
            Console.WriteLine("");
            Console.Write("STAMINA: ");
            for(int i=0; i<stamina; i++) Console.Write(game.StaminaEmojiChar + "  ");
            for(int i=stamina; i<GameConfig.MAX_STAMINA; i++) Console.Write("  ");
            Console.WriteLine("");
            Console.Write("LIVES: ");
            for(int i=0; i<lives; i++) Console.Write(game.LivesEmojiChar + "  ");
            for(int i=lives; i<GameConfig.MAX_LIVES; i++) Console.Write("  ");
            Console.WriteLine("");
        }

        /// <summary>
        /// Checks if the player can move to the specified position.
        /// </summary>
        /// <param name="x">X coordinate of the position.</param>
        /// <param name="y">Y coordinate of the position.</param>
        /// <returns>True if the player can move to the position, false otherwise.</returns>
        private bool CanMoveTo(int x, int y)
        {
            if (y < 0 || y >= GameConfig.SCREEN_HEIGHT) return false;
            int adjustedX = (x + offset) % GameConfig.SCREEN_WIDTH;
            return screen[y, adjustedX] != game.WallEmojiChar;
        }

        /// <summary>
        /// Updates the game world based on player actions and game rules.
        /// </summary>
        private async Task UpdateWorld()
        {
            int oldPlayerPosY = playerPosY;

            // Player jump handling
            if (jumpVelocity > 0 || playerPosY < GameConfig.SCREEN_HEIGHT - 1)
            {
                int newY = playerPosY - jumpVelocity;
                newY = Math.Max(0, Math.Min(newY, GameConfig.SCREEN_HEIGHT - 1));

                for (int y = Math.Min(oldPlayerPosY, newY); y <= Math.Max(oldPlayerPosY, newY); y++)
                {
                    if (CanMoveTo(playerPosX, y))
                    {
                        await checkCoins1.CheckForItems(y, playerPosX);
                        await checkCoins2.CheckForItems(y, playerPosX);
                        await checkCoins3.CheckForItems(y, playerPosX);
                        await checkPoachers.CheckForItems(y, playerPosX);
                    }
                    else
                    {
                        jumpVelocity = 0;
                        break;
                    }
                }

                playerPosY = newY;
                jumpVelocity--;
            }

            // Applying gravity
            if (playerPosY < GameConfig.SCREEN_HEIGHT - 1)
            {
                int newY = playerPosY + 1;
                if (CanMoveTo(playerPosX, newY))
                {
                    playerPosY = newY;
                    await checkCoins1.CheckForItems(playerPosY, playerPosX);
                    await checkCoins2.CheckForItems(playerPosY, playerPosX);
                    await checkCoins3.CheckForItems(playerPosY, playerPosX);
                    await checkPoachers.CheckForItems(playerPosY, playerPosX);
                }
            }

            // Screen scrolling
            offset++;
            if (offset >= GameConfig.SCREEN_WIDTH)
            {
                offset = 0;
                score++;
                if(score%GameConfig.STEP_LEVELS==0) levels++;
                restoreScreen();
                if (checkersConfigs != null)
                {
                    for (int i = 0; i < checkersConfigs.Length; i++)
                    {
                        SetEntities(
                            checkersConfigs[i].MinEntities,
                            checkersConfigs[i].MaxEntities,
                            checkersConfigs[i].Character,
                            checkersConfigs[i].MinHeight,
                            checkersConfigs[i].MaxHeight
                        );
                    }
                }            
            }

            // Additional coin checks around the player
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    await checkCoins1.CheckForItems(playerPosY + dy, playerPosX + dx);
                    await checkCoins2.CheckForItems(playerPosY + dy, playerPosX + dx);
                    await checkCoins3.CheckForItems(playerPosY + dy, playerPosX + dx);
                    await checkPoachers.CheckForItems(playerPosY + dy, playerPosX + dx);
                }
            }
        }

        /// <summary>
        /// Restores the game screen from the backup.
        /// </summary>
        private void restoreScreen()
        {
            for (int y = 0; y < GameConfig.SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < GameConfig.SCREEN_WIDTH; x++)
                {
                    screen[y, x] = screenBackup[y, x];
                }
            }
        }

        /// <summary>
        /// Places a random number of entities on the game map at a given height.
        /// </summary>
        /// <param name="minEntities">Minimum number of entities to place.</param>
        /// <param name="maxEntities">Maximum number of entities to place.</param>
        /// <param name="emojiChar">Emoji character representing the entity.</param>
        /// <param name="minHeight">Minimum height to place the entities.</param>
        /// <param name="maxHeight">Maximum height to place the entities.</param>
        private void SetEntities(int minEntities, int maxEntities, string emojiChar, int minHeight, int maxHeight)
        {
            Random random = new Random();
            int numEntities = random.Next(minEntities, maxEntities + 1);

            for (int i = 0; i < numEntities; i++)
            {
                int entityX = random.Next(GameConfig.SCREEN_WIDTH - 7, GameConfig.SCREEN_WIDTH - 1);
                int entityY = random.Next(minHeight, maxHeight);

                screen[entityY, entityX] = emojiChar;
            }
        }

        /// <summary>
        /// Displays the welcome screen of the game.
        /// </summary>
        public void ShowWelcomeScreen()
        {
            Console.Clear();
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            string[] welcomeMessage = {
                "Welcome to Jacob's Resilience! 🦁",
                "",
                "Use ↑ to jump",
                "Use ← and → to move left and right",
                "Press P for Players",
                "Press L to view legend",
                "Press Q to quit"
            };

            string[] story = {
                "The Story of Jacob's Resilience:",
                "",
                "Jacob, the lion who defied the odds, has become a symbol of resilience and courage.",
                "In this epic adventure, Jacob finds himself pursued by ruthless poachers 🪤",
                "and faced with the daunting challenge of crossing the treacherous Kazinga Channel.",
                "With determination and bravery, Jacob must navigate through dangers such as crocodiles 🐊",
                "and hippos 🦛, while collecting vital resources to strengthen his resilience.",
                "Will you guide Jacob to safety and witness his triumphant journey?",
                "Prepare for the ultimate test of survival and discover the true meaning of resilience."
            };

            // Print welcome message
            int startRow = (windowHeight - welcomeMessage.Length) / 2 - 8;
            foreach (string line in welcomeMessage)
            {
                Console.SetCursorPosition((windowWidth - line.Length) / 2, startRow++);
                Console.WriteLine(line);
            }

            // Print story
            startRow +=8; // 8 lines gap after welcome message
            foreach (string line in story)
            {
                Console.SetCursorPosition((windowWidth - line.Length) / 2, startRow++);
                Console.WriteLine(line);
            }

            // Blink "Press Space Bar to start"
            int blinkRow = (windowHeight - welcomeMessage.Length) / 2 + 2;
            string blinkText = "Press Space Bar to start. You must endure! 💪";
            int blinkColumn = (windowWidth - blinkText.Length) / 2;
            bool isVisible = true;
            bool spaceBarPressed = false;

            DateTime nextBlinkTime = DateTime.Now;

            while (!spaceBarPressed)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        spaceBarPressed = true;
                        break;
                    }
                    else if (key.Key == ConsoleKey.P)
                    {
                        Console.Clear();
                        DisplayLeaderboard(); // Show leaderboard
                        Console.WriteLine("\nPress any key to return to the welcome screen...");
                        Console.ReadKey(true);
                        ShowWelcomeScreen(); // Return to welcome screen
                        return;
                    }
                    else if (key.Key == ConsoleKey.L)
                    {
                        Console.Clear();
                        EmojiDatabase.PrintEmojiLegend();
                        ShowWelcomeScreen(); // Return to welcome screen
                        return;
                    }
                    else if (key.Key == ConsoleKey.Q)
                    {
                        ExitGame();
                        Console.SetCursorPosition((windowWidth - "Press any key to exit...".Length) / 2, Console.CursorTop);
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey(true);
                        return;
                    }
                }

                if (DateTime.Now >= nextBlinkTime)
                {
                    Console.SetCursorPosition(blinkColumn, blinkRow);
                    Console.Write(isVisible ? blinkText : new string(' ', blinkText.Length));
                    isVisible = !isVisible;
                    nextBlinkTime = DateTime.Now.AddMilliseconds(500);
                }
            }
        }

        // Updated youLose method to handle leaderboard
        private void youLose()
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.PlayAsync("game_over");

            Console.Clear();

            // Display "You Lose!" screen
            Console.WriteLine("💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀");
            Console.WriteLine("💀                                💀");
            Console.WriteLine("💀            YOU LOSE!            💀");
            Console.WriteLine($"💀          Score: {score.ToString().PadLeft(6)}         💀");
            Console.WriteLine($"💀          Credit: {(credit + (credit2 * 100)).ToString().PadLeft(6)}         💀");
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
            PlayerScore currentPlayer = new PlayerScore(initials.ToUpper(), score, credit + (credit2 * 100), levels);
            UpdateLeaderboard(currentPlayer);

            // Display leaderboard
            DisplayLeaderboard();

            // After valid initials are entered, express gratitude and invite to restart
            Console.WriteLine($"\n🙏 Thank you, {initials}, for playing! Your score: {score} 🎮");

            // Set restart to true after valid initials are entered
            restart = true;

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
    } // End of Program class    
}

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
using Modules;   // Namespace containing GameMenu, LeaderBoard, PlayerScore, SoundPlayer, Utility
using Config;    // CheckersConfig.cs, CheckersConfigInitializer, Game, GameConfig

namespace JacobResilienceGame
{
    public class Program
    {
        private LeaderBoard leaderBoard;
        private Utility utility;

        private SoundPlayer soundPlayer;
        private AudioManager audioManager;

        // Game components
        private GameMenu gameMenu;
        public Game game;
        private GameComponents gameComponents;
        public GameLevelInitializer gameLevelInitializer;

        // Input handling
        private readonly Queue<ConsoleKeyInfo> inputQueue = new Queue<ConsoleKeyInfo>();
        private readonly object lockObject = new object();

        // Game state variables
        private bool showMenu;              // Flag indicating if the menu is visible
        public static bool soundOn=true;    // Flag indicating if the sound is activated
        public static bool audioOn=true;    // Flag indicating if the audio is activated
        private bool restart;               // Flag indicating if you want to restart
        private bool restartPlayerStatus;   // Flag indicating if you want to restart from PlayerStatus
        public string[,] screen;            // Matrix representing the game screen
        private string[,] screenBackup;     // Matrix backup
        public int offset;                  // Offset for screen scrolling
        private int playerPosX;             // X position of the player
        private int playerPosY;             // Y position of the player
        private int jumpVelocity;           // Player's jump velocity
        public int score;                   // Player's score
        public int levelScore;              // Player's levelScore
        public int credit;                  // Player's credits
        public int credit2;                 // Player's credits2
        public int resilience;              // Player's resilience
        public int stamina;                 // Player's stamina
        public int lives;                   // Player's lives
        public int levels;                  // Game's levels

        public Program()
        {
            leaderBoard = new LeaderBoard(this);
            utility = new Utility(this);
            soundPlayer = new SoundPlayer();
            audioManager = new AudioManager(GameConfig.PLAY_BACK_GAME_SOUND);

            // Initialize game components
            gameMenu = new GameMenu(); 
            game = new Game();
            screen = new string[GameConfig.SCREEN_HEIGHT, GameConfig.SCREEN_WIDTH];
            screenBackup = new string[GameConfig.SCREEN_HEIGHT, GameConfig.SCREEN_WIDTH];
            gameComponents = new GameComponents(game, this);
            gameLevelInitializer = new GameLevelInitializer(game);
            // gameLevelInitializer.Verify();
            // Console.WriteLine("levels="+gameLevelInitializer.GameLevels.Count());
            // Environment.Exit(1);
            leaderBoard.ReadFromFile();
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
            levelScore = 0;
            credit = 0;
            credit2 = 0;
            resilience = GameConfig.INIT_RESILIENCE;
            stamina = GameConfig.INIT_STAMINA;
            lives = GameConfig.INIT_LIVES;
            levels = 0;

            InitializeWorld(); // Initialize the game world
            // checkersConfigs = CheckersConfigInitializer.InitializeCheckersConfigs(game); // Initialize the CheckersConfig array
            Console.Clear();
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

                if(restartPlayerStatus) {
                    leaderBoard.LoadPlayerStatus();
                    restartPlayerStatus = false;
                }
                
                restart = false;

                await audioManager.PlayBackgroundMusicAsync();

                while (!restart)
                {
                    if (!showMenu)
                    {
                        ProcessInput();
                        await UpdateWorld();
                        DrawScreen();
                        
                        // Check for resilience condition
                        if (resilience < 0) {
                            lives--;
                            resilience = GameConfig.INIT_RESILIENCE;
                        }
                        // Check for stamina condition
                        if (stamina < 0) {
                            lives--;
                            stamina = GameConfig.INIT_STAMINA;
                        }
                        // Check for game over condition
                        if (lives < 0)
                        {
                            leaderBoard.youLose(); // Call the method to display death screen
                            leaderBoard.WriteToFile();
                            // Set restart to true after valid initials are entered
                            restart = true;
                        }
                    }
                    else
                    {
                        gameMenu.ShowMenu(GameConfig.SCREEN_HEIGHT + 1, GameConfig.SCREEN_WIDTH,
                            ReturnToGame, ExitGame, ToggleSound, ArchivePlayer, ToggleWave);
                    }

                    await Task.Delay(GameConfig.PAUSE_CONTROL);
                } // while 1

                audioManager.StopBackgroundMusic();

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
        /// Toggles the sound status of the game.
        /// </summary>
        public void ToggleSound()
        {
            soundOn = !soundOn; // Toggle the sound status
            showMenu = false;

            if (soundOn)
            {
                // Play the sound for sound on, if necessary
                // You might want to call a method to actually enable the sound
                soundPlayer.PlayAsync("coin"); // Example method call, replace with your actual method
            }
            else
            {
                // Play the sound for sound off, if necessary
                // You might want to call a method to actually disable the sound
                soundPlayer.PlayAsync("coin"); // Example method call, replace with your actual method
            }
        }

        /// <summary>
        /// Save player status
        /// </summary>
        public void ArchivePlayer()
        {
            PlayerStatus playerStatus = new PlayerStatus(score, levelScore, credit, credit2, levels);
            leaderBoard.SavePlayerStatus(playerStatus);
        }

        /// <summary>
        /// Toggles the audio status of the game.
        /// </summary>
        public void ToggleWave()
        {
            audioOn = !audioOn; // Toggle the audio status
            showMenu = false;

            if (audioOn)
            {
                // Play the audio for audio on, if necessary
                audioManager.PlayBackgroundMusicAsync();
            }
            else
            {
                // Stop the audio for audio off, if necessary
                audioManager.StopBackgroundMusic();
            }
        }

        /// <summary>
        /// Exits the game application.
        /// </summary>
        public void ExitGame()
        {
            Console.Clear();
            StopWaveMusic();
            Console.WriteLine("Thank you for playing Jacob's Resilience! 🦁");
            Environment.Exit(0);
        }

        /// <summary>
        /// Force stop the audio of the game.
        /// </summary>
        public void StopWaveMusic()
        {
            if (audioOn)
            {
                // Play the audio for audio off, if necessary
                audioManager.StopBackgroundMusic();
            }
        }

        /// <summary>
        /// Force restart the audio of the game.
        /// </summary>
        public void RestartWaveMusic()
        {
            if (audioOn)
            {
                // Play the audio for audio on, if necessary
                audioManager.PlayBackgroundMusicAsync();
            }
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

            utility.PrintLevelTitle();

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
            
            /*
            * Code snippet handling the gpavementLevel representation based on the level:
            * Here we determine the symbol representing the ground or surface.
            */
            var gameLevel = gameLevelInitializer.GameLevels[levels];
            string pavementLevel = gameLevel.PavementLevel<0 || gameLevel.PavementLevel>=game.PavementLevel.Length ? game.PavementLevel[levels%10] : game.PavementLevel[gameLevel.PavementLevel];

            for (int x = 0; x < GameConfig.SCREEN_WIDTH / 2; x++) Console.Write(pavementLevel);

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
            Console.WriteLine(soundOn ? "🔊  " : "🔊 🔇");
            Console.WriteLine(audioOn ? "🎵  " : "🎵 🔇");
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
            // int adjustedX = (x + offset) % GameConfig.SCREEN_WIDTH;
            // return screen[y, adjustedX] != game.WallEmojiChar;
            return true;
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
                        await gameComponents.InvokeAll(y, playerPosX);
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
                    await gameComponents.InvokeAll(playerPosY, playerPosX);
                }
            }

            // Screen scrolling
            offset++;
            if (offset >= GameConfig.SCREEN_WIDTH)
            {
                restoreScreen();
                
                offset = 0;
                score++;
                levelScore++;

                if (levels < gameLevelInitializer.GameLevels.Count-1) {
                    var gameLevel = gameLevelInitializer.GameLevels[levels];
                    // You can now use `gameLevel` as needed

                    if(levelScore < gameLevel.TotalScoreDuration) {
                    
                        foreach(CheckersConfig checkersConfig in gameLevel.CheckersConfigs)
                        {
                            foreach(ScoreInterval scoreInterval in checkersConfig.ScoreIntervals)
                            {
                                // Console.WriteLine(checkersConfig.Character);
                                if(levelScore>=scoreInterval.Start && levelScore<=scoreInterval.End) {
                                    SetEntities(checkersConfig.MinEntities, 
                                                checkersConfig.MaxEntities,
                                                checkersConfig.Character,
                                                checkersConfig.MinHeight,
                                                checkersConfig.MaxHeight
                                    );
                                }
                            }
                        }
                    } else {
                            levels++;
                            levelScore = 0;
                            await soundPlayer.PlayAsync("next_level");
                    }
                } else {
                    leaderBoard.youWin();
                    leaderBoard.WriteToFile();
                    restart = true;
                }
            }

            // Additional coin checks around the player
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    await gameComponents.InvokeAll(playerPosY + dy, playerPosX + dx);
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
                "Press Q to quit",
                "Press R to restart from the last level played"
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

            DateTime nextBlinkTime = DateTime.Now;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        break;
                    }
                    else if (key.Key == ConsoleKey.P)
                    {
                        Console.Clear();
                        leaderBoard.DisplayLeaderboard(); // Show leaderboard
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
                    else if (key.Key == ConsoleKey.R)
                    {
                        restartPlayerStatus = true;
                        break;
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

    } // End of Program class    
} // End Module

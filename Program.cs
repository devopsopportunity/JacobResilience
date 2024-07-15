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

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using EmojiGame; // Assuming Emoji and EmojiDatabase classes are in this namespace
using Modules;   // Namespace containing GameMenu, SoundPlayer class
using JacobResilienceGame.Checkers;

namespace JacobResilienceGame
{
    public class Program
    {
        // Game components
        private GameMenu gameMenu;
        private Game game;
        private FactoryCheckerAbstract checkCoins1;
        private FactoryCheckerAbstract checkCoins2;
        private FactoryCheckerAbstract checkCoins3;

        // Input handling
        private readonly Queue<ConsoleKeyInfo> inputQueue = new Queue<ConsoleKeyInfo>();
        private readonly object lockObject = new object();

        // Game state variables
        private bool showMenu;              // Flag indicating if the menu is visible
        public string[,] screen;            // Matrix representing the game screen
        private string[,] screenBackup;     // Matrix backup
        public int offset;                  // Offset for screen scrolling
        public int credit;                  // Player's credits
        public int credit2;                 // Player's credits2
        private int playerPosX;             // X position of the player
        private int playerPosY;             // Y position of the player
        private int jumpVelocity;           // Player's jump velocity
        private int score;                  // Player's score

        // Array of CheckersConfig
        private CheckersConfig[] checkersConfigs;

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

            // Initialize player position and game variables
            playerPosX = GameConfig.PLAYER_INITIAL_X;
            playerPosY = GameConfig.PLAYER_INITIAL_Y;
            jumpVelocity = 0;
            offset = 0;
            score = 0;
            credit = 0;
            showMenu = false;

            InitializeWorld(); // Initialize the game world
            checkersConfigs = CheckersConfigInitializer.InitializeCheckersConfigs(game); // Initialize the CheckersConfig array
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

            while (true)
            {
                if (!showMenu)
                {
                    ProcessInput();
                    await UpdateWorld();
                    DrawScreen();
                }
                else
                {
                    gameMenu.ShowMenu(GameConfig.SCREEN_HEIGHT, GameConfig.SCREEN_WIDTH, showMenu, ReturnToGame, ExitGame);
                }

                await Task.Delay(GameConfig.PAUSE_CONTROL);
            }
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
        private void ExitGame()
        {
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
            Console.WriteLine("LEVEL " + game.LevelEmojiChar);
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
            Console.WriteLine("\nPress 'm' | 'M' = Show Menu");
            Console.WriteLine($"Score: {score}");
            Console.WriteLine("CREDIT: " + game.Coin1EmojiChar + " " + $"{credit}");
            Console.WriteLine("BAG OF COINS: " + game.Coin2EmojiChar + " " + $"{credit2}");
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
            int oldPlayerPosX = playerPosX;

            // Player jump handling
            if (jumpVelocity > 0 || playerPosY < GameConfig.SCREEN_HEIGHT - 1)
            {
                int newY = playerPosY - jumpVelocity;
                newY = Math.Max(0, Math.Min(newY, GameConfig.SCREEN_HEIGHT - 1));

                for (int y = Math.Min(oldPlayerPosY, newY); y <= Math.Max(oldPlayerPosY, newY); y++)
                {
                    if (CanMoveTo

(playerPosX, y))
                    {
                        await checkCoins1.CheckForItems(y, playerPosX);
                        await checkCoins2.CheckForItems(y, playerPosX);
                        await checkCoins3.CheckForItems(y, playerPosX);
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
                }
            }

            // Screen scrolling
            offset++;
            if (offset >= GameConfig.SCREEN_WIDTH)
            {
                offset = 0;
                score++;
                restoreScreen();
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

            // Additional coin checks around the player
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    await checkCoins1.CheckForItems(playerPosY + dy, playerPosX + dx);
                    await checkCoins2.CheckForItems(playerPosY + dy, playerPosX + dx);
                    await checkCoins3.CheckForItems(playerPosY + dy, playerPosX + dx);
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

    } // End of Program class
}

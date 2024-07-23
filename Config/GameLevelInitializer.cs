/*
 * GameLevelInitializer.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file contains the initialization logic for the GameLevel array.
 * It reads from a text file to initialize configurations for various game levels.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
namespace Config
{
    public class GameLevelInitializer
    {
        public static readonly int MAX_DESCRIPTION_LENGTH = 160;

        public List<GameLevel> GameLevels { get; private set; }
        private Dictionary<string, string> emojiMap;
        private Game game;

        /// <summary>
        /// Constructor to initialize GameLevelInitializer with a game instance.
        /// </summary>
        /// <param name="game">Game instance providing emoji characters.</param>
        public GameLevelInitializer(Game game)
        {
            this.game = game;
            GameLevels = new List<GameLevel>();
            emojiMap = new Dictionary<string, string>();
            InitializeEmojiMap();

            // Scan the folder GameConfig.FOLDER_MODELS
            string folderPath = GameConfig.FOLDER_MODELS;
            string[] files = Directory.GetFiles(folderPath, "*.txt");

            if (files.Length == 0)
            {
                Console.WriteLine("Please set at least one Game Level Model file containing the data: LEVEL_DESCRIPTION:, TOTAL_SCORE_DURATION:, PAVEMENT:, ENTITY:, SCORE_INTERVAL:, MIN_E:, MAX_E:, MIN_H:, MAX_H:");
                Environment.Exit(1);
            }

            // Sort files alphabetically
            Array.Sort(files);

            foreach (string filePath in files)
            {
                // Console.WriteLine("LOADING FILE LEVEL: " + filePath);
                LoadGameLevelsFromFile(filePath);
            }
        }

        /// <summary>
        /// Initializes the emoji map with mappings from entity names to emoji characters.
        /// </summary>
        private void InitializeEmojiMap()
        {
            emojiMap = new Dictionary<string, string>()
            {
                // Animals
                {"hippo", game.HippopotamusEmojiChar},
                {"crocodile", game.CrocodileEmojiChar},
                {"snake1", game.Snake1EmojiChar},
                {"snake2", game.Snake2EmojiChar},
                {"zebra", game.ZebraEmojiChar},

                // Coins
                {"coin1", game.Coin1EmojiChar},
                {"coin2", game.Coin2EmojiChar},
                {"coin3", game.Coin3EmojiChar},
                {"diamond", game.DiamondEmojiChar},

                // Enemies
                {"danger", game.DangerEmojiChar},
                {"fire", game.FireEmojiChar},
                {"poachers", game.PoachersEmojiChar},
                {"trap", game.TrapEmojiChar},

                // Energies
                {"apple", game.AppleEmojiChar},
                {"energy", game.EnergyEmojiChar},
                {"magicpotion", game.MagicPotionEmojiChar},
                {"meat1", game.Meat1EmojiChar},
                {"meat2", game.Meat2EmojiChar},
                {"watermelon", game.WatermelonEmojiChar}
            };
        }

        /// <summary>
        /// Loads game levels from a specified file and populates GameLevels list.
        /// </summary>
        /// <param name="filePath">Path to the file containing game level configurations.</param>
        private void LoadGameLevelsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} does not exist.");
            }

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length;)
            {
                if (lines[i].StartsWith("LEVEL_DESCRIPTION:"))
                {
                    string description = lines[i].Substring("LEVEL_DESCRIPTION:".Length).Trim();
                    if (description.Length > MAX_DESCRIPTION_LENGTH)
                    {
                        description = description.Substring(0, MAX_DESCRIPTION_LENGTH);
                    }
                    else
                    {
                        if(description=="") description="Empty Level";
                        description = description.PadRight(MAX_DESCRIPTION_LENGTH);
                    }

                    // Console.WriteLine(description + " " + description.Length);

                    GameLevel currentLevel = new GameLevel(description);

                    i++;
                    if (lines[i].StartsWith("TOTAL_SCORE_DURATION:"))
                    {
                        currentLevel.TotalScoreDuration = int.Parse(lines[i].Split(':')[1].Trim());
                        i++;
                        
                        if (lines[i].StartsWith("PAVEMENT:"))
                        {
                            string pavement = lines[i].Split(':')[1].Trim();
                            currentLevel.PavementLevel = pavement.Equals("AUTO") ? -1 : int.Parse(pavement);
                            i++;

                            for (; i < lines.Length; i++)
                            {
                                if (lines[i].StartsWith("ENTITY"))
                                {
                                    CheckersConfig checkersConfig = new CheckersConfig();

                                    for (; i < lines.Length; i++)
                                    {
                                        var keyValue = lines[i].Split(':');
                                        var key = keyValue[0].Trim();
                                        var value = keyValue[1].Trim();

                                        switch (key)
                                        {
                                            case "ENTITY":
                                                checkersConfig.Character = ParseCharacter(value);
                                                break;
                                            case "SCORE_INTERVAL":
                                                checkersConfig.ScoreIntervals = ParseScoreIntervals(value);
                                                break;
                                            case "MIN_E":
                                                checkersConfig.MinEntities = int.Parse(value);
                                                break;
                                            case "MAX_E":
                                                checkersConfig.MaxEntities = int.Parse(value);
                                                break;
                                            case "MIN_H":
                                                checkersConfig.MinHeight = ParseHeight(value);
                                                break;
                                            case "MAX_H":
                                                checkersConfig.MaxHeight = ParseHeight(value);
                                                break;
                                        }

                                        if (lines[i].StartsWith("MAX_H:"))
                                        {
                                            break;
                                        }
                                    }
                                    currentLevel.CheckersConfigs.Add(checkersConfig);
                                }

                                if (lines[i].StartsWith("LEVEL_DESCRIPTION:"))
                                {
                                    break;
                                }
                            } // for
                        }
                    }
                    GameLevels.Add(currentLevel);
                }
            }
        }

        /// <summary>
        /// Parses the score intervals from the specified value string.
        /// </summary>
        /// <param name="value">String containing score intervals in format [start, end].</param>
        /// <returns>List of tuples representing score intervals.</returns>
        private List<ScoreInterval> ParseScoreIntervals(string value)
        {
            var intervals = new List<ScoreInterval>();
            var parts = value.Split(new[] { "], [" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                var range = part.Replace("[", "").Replace("]", "").Split(',');
                intervals.Add(new ScoreInterval(int.Parse(range[0].Trim()), int.Parse(range[1].Trim())));
            }

            return intervals;
        }

        /// <summary>
        /// Parses the height value from the specified string.
        /// </summary>
        /// <param name="value">String containing height value or expression.</param>
        /// <returns>Parsed integer height value.</returns>
        private int ParseHeight(string value)
        {
            int baseValue = GameConfig.Instance.SCREEN_HEIGHT;
            int parsedHeight;

            if (value.Contains("H -"))
            {
                parsedHeight = baseValue - int.Parse(value.Split('-')[1].Trim());
            }
            else if (value.Contains("/"))
            {
                parsedHeight = baseValue / int.Parse(value.Split('/')[1].Trim());
            }
            else if (value.Contains("H"))
            {
                parsedHeight = baseValue;
            }
            else
            {
                parsedHeight = int.Parse(value);
            }

            // Ensure the parsed height is within the valid range (5 to H - 1)
            if (parsedHeight < 5 || parsedHeight >= baseValue)
            {
                parsedHeight = baseValue - 1; // H corresponds to the ground
            }

            return parsedHeight;
        }

        /// <summary>
        /// Parses the character value from the specified string using emoji map.
        /// </summary>
        /// <param name="value">String representing the entity name.</param>
        /// <returns>Emoji character associated with the entity.</returns>
        private string ParseCharacter(string value)
        {
            string key = value.Trim().ToLower();
            return emojiMap[key];
        }

        /// <summary>
        /// Verifies and prints the loaded game levels and their configurations.
        /// </summary>
        public void Verify()
        {
            int levelIndex = 0;
            foreach (var level in GameLevels)
            {
                Console.WriteLine($"LEVEL: {levelIndex} LEVEL_DESCRIPTION: {level.Description}");
                Console.WriteLine($"TOTAL_SCORE_DURATION: {level.TotalScoreDuration}");

                foreach (var config in level.CheckersConfigs)
                {
                    Console.WriteLine($"ENTITY: {config.Character}");
                    Console.WriteLine($"SCORE_INTERVAL: {string.Join(", ", config.ScoreIntervals.Select(si => $"[{si.Start}, {si.End}]"))}");
                    Console.WriteLine($"MIN_E: {config.MinEntities}");
                    Console.WriteLine($"MAX_E: {config.MaxEntities}");
                    Console.WriteLine($"MIN_H: {config.MinHeight}");
                    Console.WriteLine($"MAX_H: {config.MaxHeight}");
                }
                levelIndex++;
            }
        }
    }
}

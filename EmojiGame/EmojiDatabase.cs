/*
 * EmojiDatabase.cs
 * @ Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------
 * This file defines the Emoji and EmojiDatabase classes to manage an emoji database.
 * -------------------------------------------
 * @hacktlon July 15, 2024
 */
namespace EmojiGame
{
    public class Emoji
    {
        public int Id { get; set; }           // Unique identifier of the emoji
        public string Name { get; set; }      // Name of the emoji
        public string Character { get; set; } // Unicode character of the emoji
        public string Description { get; set; } // Emoji description

        // Constructor to initialize an instance of Emoji
        public Emoji(int id, string name, string character, string description)
        {
            Id = id;
            Name = name;
            Character = character;
            Description = description;
        }
    }

    public class EmojiDatabase
    {
        public List<Emoji> Emojis { get; private set; } // List of all emojis in the database

        // Constructor to initialize the database with emojis
        public EmojiDatabase()
        {
            Emojis = new List<Emoji>
            {
                new Emoji(0, "Wall", "🧱", "Represents the wall in the game."),
                new Emoji(1, "Jacob", "🦁", "Represents Jacob, the lion protagonist of the game."),
                new Emoji(2, "Crocodile", "🐊", "Symbolizes crocodiles, which are a danger in the channel."),
                new Emoji(3, "Hippopotamus", "🦛", "Represents hippos, another danger in the game environment."),
                new Emoji(4, "Vegetation", "🌿", "Represents vegetation and natural habitat."),
                new Emoji(5, "Danger", "🚫", "Indicates dangerous zones or situations to avoid."),
                new Emoji(6, "Trap", "🎣", "Represents poachers' traps."),
                new Emoji(7, "Water", "💧", "Represents the water of the Kazinga Channel."),
                new Emoji(8, "Mountain", "⛰️", "Represents mountains or cliffs along the path."),
                new Emoji(9, "Time", "🌅", "Represents the time of day in the game."),
                new Emoji(10, "Finish", "🥇", "Represents the finish line or final goal."),
                new Emoji(11, "Snake1", "🐍", "Represents venomous snakes."),
                new Emoji(12, "Poachers", "🪤", "Represents poachers' traps in the river."),
                new Emoji(13, "Level#0", "0️⃣", "Represents the introductory level."),
                new Emoji(14, "Level#1", "1️⃣", "Represents level 1."),
                new Emoji(15, "Level#2", "2️⃣", "Represents level 2."),
                new Emoji(16, "Level#3", "3️⃣", "Represents level 3."),
                new Emoji(17, "Level#4", "4️⃣", "Represents level 4."),
                new Emoji(18, "Level#5", "5️⃣", "Represents level 5."),
                new Emoji(19, "Level#6", "6️⃣", "Represents level 6."),
                new Emoji(20, "Level#7", "7️⃣", "Represents level 7."),
                new Emoji(21, "Level#8", "8️⃣", "Represents level 8."),
                new Emoji(22, "Level#9", "9️⃣", "Represents level 9."),
                new Emoji(23, "Snake2", "🪱", "Symbolizes a snake in the desert."),
                new Emoji(24, "Fire", "🔥", "Represents fiery energy in the game."),
                new Emoji(25, "TreasureMap", "🗺️", "Represents a treasure map or secret document."),
                new Emoji(26, "MagicPotion", "🧪", "Represents magical potions in the game."),
                new Emoji(27, "Coin_1", "🟡", "Represents currency or coins used in the game."),
                new Emoji(28, "X", "✖️", "Represents the X mark on the map."),
                new Emoji(29, "Apple", "🍎", "Represents energy apples in the game."),
                new Emoji(30, "Objective", "🎯", "Represents objectives or targets to achieve."),
                new Emoji(31, "Magic", "💫", "Represents magical effects in the game."),
                new Emoji(32, "Shield", "🛡️", "Represents defensive capabilities."),
                new Emoji(33, "Velocity", "🚀", "Represents speed or agility."),
                new Emoji(34, "Health", "❤️", "Represents health or vitality."),
                new Emoji(35, "Star", "🌟", "Represents bonus points or achievements."),
                new Emoji(36, "Weapons", "⚔️", "Represents weapons or combat abilities."),
                new Emoji(37, "Resilience", "💪", "Represents resilience or strength."),
                new Emoji(38, "Channel", "🏞️", "Represents the Kazinga Channel in Uganda on the path."),
                new Emoji(39, "Island", "🏝️", "Represents an island in the Kazinga Channel."),
                new Emoji(40, "Wave", "🌊", "Represents a wave in the channel."),
                new Emoji(41, "Droplet", "💦", "Represents a water droplet in the Kazinga Channel."),
                new Emoji(42, "Cloud", "☁️", "Represents a fluffy cloud in the sky."),
                new Emoji(43, "Cloudsun", "⛅", "Represents a fluffy cloud in the sky with sun."),
                new Emoji(44, "Rainbow", "🌈", "Represents a rainbow."),
                new Emoji(45, "Energy", "🔋", "Represents energy."),
                new Emoji(46, "Resilience", "💪", "Represents resilience."),
                new Emoji(47, "Coin_2", "💰", "Represents currency or coins used in the game."),
                new Emoji(48, "Coin_3", "🪙", "Represents currency or coins used in the game."),
                new Emoji(49, "Meat_1", "🥩", "Represents meat as a source of energy in the game."),
                new Emoji(50, "Meat_2", "🍖", "Represents another type of meat for energy in the game."),
                new Emoji(51, "Vegetation_2", "🍀", "Represents vegetation and natural habitat."),
                new Emoji(52, "Mushroom", "🍄", "Represents mushroom and natural habitat."),
                new Emoji(53, "Vegetation_3", "🌱", "Represents vegetation and natural habitat."),
                new Emoji(54, "Watermelon", "🍉", "Represents energy Watermelon in the game.")
                
            };
        }

        /// <summary>
        /// Prints the legend of emojis with their descriptions.
        /// </summary>
        public static void PrintEmojiLegend()
        {
            EmojiDatabase db = new EmojiDatabase();
            Console.WriteLine("Emoji Legend:");
            foreach (var emoji in db.Emojis)
            {
                Console.WriteLine($"{emoji.Character} - {emoji.Description}");
            }
            Console.WriteLine("\nPress any key to come back to the welcome screen...");
            Console.ReadKey();
        }
    }
}

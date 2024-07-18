/*
 * Game.cs
 * @ Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------
 * This class manages the emoji game.
 * -------------------------------------------
 * @hacktlon July 15, 2024
 */
using EmojiGame; // Assuming Emoji and EmojiDatabase classes are in this namespace

namespace Config
{
    public class Game
    {
        // Emoji characters with fallback values (using null-coalescing)
        private string playerEmojiChar;
        private string vegetationEmojiChar;
        private string wallEmojiChar;
        private string cloudEmojiChar;
        private string cloudsunEmojiChar;
        private string rainbowEmojiChar;
        private string mountainEmojiChar;
        private string levelEmojiChar;
        private string energyEmojiChar;
        private string resilienceEmojiChar;
        private string coin1EmojiChar;
        private string coin2EmojiChar;
        private string coin3EmojiChar;
        private string poachersEmojiChar;
        private string staminaEmojiChar;
        private string livesEmojiChar;
        private string level1EmojiChar;
        private string level2EmojiChar;
        private string hippopotamusEmojiChar;
        private string crocodileEmojiChar;
        private string waveEmojiChar;
        private string dropletEmojiChar;

        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        public Game()
        {
            EmojiDatabase emojiDatabase = new EmojiDatabase(); // Emoji database containing all emojis used in the game

            playerEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Jacob")?.Character ?? " ";
            vegetationEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Vegetation")?.Character ?? " ";
            wallEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Wall")?.Character ?? " ";
            cloudEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Cloud")?.Character ?? " ";
            cloudsunEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Cloudsun")?.Character ?? " ";
            rainbowEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Rainbow")?.Character ?? " ";
            mountainEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Mountain")?.Character ?? " ";
            levelEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Level#0")?.Character ?? " ";
            energyEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Energy")?.Character ?? " ";
            resilienceEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Resilience")?.Character ?? " ";
            coin1EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Coin_1")?.Character ?? " ";
            coin2EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Coin_2")?.Character ?? " ";
            coin3EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Coin_3")?.Character ?? " ";
            poachersEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Poachers")?.Character ?? " ";
            staminaEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Shield")?.Character ?? " ";
            livesEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Health")?.Character ?? " ";
            level1EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Level#1")?.Character ?? " ";
            level2EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Level#2")?.Character ?? " ";
            hippopotamusEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Hippopotamus")?.Character ?? " ";
            crocodileEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Crocodile")?.Character ?? " ";
            waveEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Wave")?.Character ?? " ";
            dropletEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Droplet")?.Character ?? " ";
        }

        // Public properties to get emoji characters
        public string PlayerEmojiChar => playerEmojiChar;
        public string VegetationEmojiChar => vegetationEmojiChar;
        public string WallEmojiChar => wallEmojiChar;
        public string CloudEmojiChar => cloudEmojiChar;
        public string CloudsunEmojiChar => cloudsunEmojiChar;
        public string RainbowEmojiChar => rainbowEmojiChar;
        public string MountainEmojiChar => mountainEmojiChar;
        public string LevelEmojiChar => levelEmojiChar;
        public string EnergyEmojiChar => energyEmojiChar;
        public string ResilienceEmojiChar => resilienceEmojiChar;
        public string Coin1EmojiChar => coin1EmojiChar;
        public string Coin2EmojiChar => coin2EmojiChar;
        public string Coin3EmojiChar => coin3EmojiChar;
        public string PoachersEmojiChar => poachersEmojiChar;
        public string StaminaEmojiChar => staminaEmojiChar;
        public string LivesEmojiChar => livesEmojiChar;
        public string Level1EmojiChar => level1EmojiChar;
        public string Level2EmojiChar => level2EmojiChar;
        public string HippopotamusEmojiChar => hippopotamusEmojiChar;
        public string CrocodileEmojiChar => crocodileEmojiChar;
        public string WaveEmojiChar => waveEmojiChar;
        public string DropletEmojiChar => dropletEmojiChar;
    }
}

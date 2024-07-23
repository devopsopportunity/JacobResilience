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
        // Array to store level emojis where index corresponds to the level number
        private string[] levelEmojis;

        // Array to store pavement Level where index corresponds to the level number
        private string[] pavementLevel;

        // Emoji characters with fallback values (using null-coalescing)
        private string playerEmojiChar;
        private string cloudEmojiChar;
        private string cloudsunEmojiChar;
        private string rainbowEmojiChar;
        private string mountainEmojiChar;
        private string energyEmojiChar;
        private string resilienceEmojiChar;
        private string coin1EmojiChar;
        private string coin2EmojiChar;
        private string coin3EmojiChar;
        private string poachersEmojiChar;
        private string staminaEmojiChar;
        private string livesEmojiChar;
        private string hippopotamusEmojiChar;
        private string crocodileEmojiChar;
        private string appleEmojiChar;
        private string meat1EmojiChar;
        private string meat2EmojiChar;
        private string trapEmojiChar;
        private string fireEmojiChar;
        private string dangerEmojiChar;
        private string snake1EmojiChar;
        private string snake2EmojiChar;      
        private string watermelonEmojiChar;
        private string magicPotionEmojiChar;
        private string diamondEmojiChar;
        private string zebraEmojiChar;

        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        public Game()
        {
            EmojiDatabase emojiDatabase = new EmojiDatabase(); // Emoji database containing all emojis used in the game

            // Initialize the level emojis array
            levelEmojis = new string[10];
            for (int i = 0; i <= 9; i++)
            {
                levelEmojis[i] = emojiDatabase.Emojis.Find(e => e.Name == $"Level#{i}")?.Character ?? " ";
            }

            playerEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Jacob")?.Character ?? " ";
            cloudEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Cloud")?.Character ?? " ";
            cloudsunEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Cloudsun")?.Character ?? " ";
            rainbowEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Rainbow")?.Character ?? " ";
            mountainEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Mountain")?.Character ?? " ";
            energyEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Energy")?.Character ?? " ";
            resilienceEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Resilience")?.Character ?? " ";
            coin1EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Coin_1")?.Character ?? " ";
            coin2EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Coin_2")?.Character ?? " ";
            coin3EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Coin_3")?.Character ?? " ";
            poachersEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Poachers")?.Character ?? " ";
            staminaEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Stamina")?.Character ?? " ";
            livesEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Health")?.Character ?? " ";
            hippopotamusEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Hippopotamus")?.Character ?? " ";
            crocodileEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Crocodile")?.Character ?? " ";
            appleEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Apple")?.Character ?? " ";
            meat1EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Meat_1")?.Character ?? " ";
            meat2EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Meat_2")?.Character ?? " ";
            trapEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Trap")?.Character ?? " ";
            fireEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Fire")?.Character ?? " ";
            dangerEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Danger")?.Character ?? " ";
            snake1EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Snake1")?.Character ?? " ";
            snake2EmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Snake2")?.Character ?? " ";
            watermelonEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Watermelon")?.Character ?? " ";
            magicPotionEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "MagicPotion")?.Character ?? " ";
            diamondEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Diamond")?.Character ?? " ";
            zebraEmojiChar = emojiDatabase.Emojis.Find(e => e.Name == "Zebra")?.Character ?? " ";

            pavementLevel = new string[10];
            buildPavementLevel(emojiDatabase);
        }

        private void buildPavementLevel(EmojiDatabase emojiDatabase)
        {
            // Initialize the pavement Level array
            pavementLevel[0] = emojiDatabase.Emojis.Find(e => e.Name == "Vegetation")?.Character ?? " ";
            pavementLevel[1] = emojiDatabase.Emojis.Find(e => e.Name == "Wave")?.Character ?? " ";
            pavementLevel[2] = emojiDatabase.Emojis.Find(e => e.Name == "Droplet")?.Character ?? " ";
            pavementLevel[3] = emojiDatabase.Emojis.Find(e => e.Name == "Wall")?.Character ?? " ";
            pavementLevel[4] = emojiDatabase.Emojis.Find(e => e.Name == "Water")?.Character ?? " ";

            pavementLevel[5] = " " + emojiDatabase.Emojis.Find(e => e.Name == "Channel")?.Character ?? " ";
            pavementLevel[6] = " " + emojiDatabase.Emojis.Find(e => e.Name == "Island")?.Character ?? " ";

            pavementLevel[7] = emojiDatabase.Emojis.Find(e => e.Name == "Vegetation_2")?.Character ?? " ";            
            pavementLevel[8] = emojiDatabase.Emojis.Find(e => e.Name == "Mushroom")?.Character ?? " ";            
            pavementLevel[9] = emojiDatabase.Emojis.Find(e => e.Name == "Vegetation_3")?.Character ?? " ";
        }

        // Public properties to get emoji characters
        public string[] LevelEmojis => levelEmojis;
        public string[] PavementLevel => pavementLevel;
        public string PlayerEmojiChar => playerEmojiChar;
        public string CloudEmojiChar => cloudEmojiChar;
        public string CloudsunEmojiChar => cloudsunEmojiChar;
        public string RainbowEmojiChar => rainbowEmojiChar;
        public string MountainEmojiChar => mountainEmojiChar;
        public string EnergyEmojiChar => energyEmojiChar;
        public string ResilienceEmojiChar => resilienceEmojiChar;
        public string Coin1EmojiChar => coin1EmojiChar;
        public string Coin2EmojiChar => coin2EmojiChar;
        public string Coin3EmojiChar => coin3EmojiChar;
        public string PoachersEmojiChar => poachersEmojiChar;
        public string StaminaEmojiChar => staminaEmojiChar;
        public string LivesEmojiChar => livesEmojiChar;
        public string HippopotamusEmojiChar => hippopotamusEmojiChar;
        public string CrocodileEmojiChar => crocodileEmojiChar;
        public string AppleEmojiChar => appleEmojiChar;
        public string Meat1EmojiChar => meat1EmojiChar;
        public string Meat2EmojiChar => meat2EmojiChar;
        public string TrapEmojiChar => trapEmojiChar;
        public string FireEmojiChar => fireEmojiChar;
        public string DangerEmojiChar => dangerEmojiChar;
        public string Snake1EmojiChar => snake1EmojiChar;
        public string Snake2EmojiChar => snake2EmojiChar;
        public string WatermelonEmojiChar => watermelonEmojiChar;        
        public string MagicPotionEmojiChar => magicPotionEmojiChar;
        public string DiamondEmojiChar => diamondEmojiChar;
        public string ZebraEmojiChar => zebraEmojiChar;
    }
}

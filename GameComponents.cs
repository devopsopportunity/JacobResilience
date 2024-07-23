/*
 * GameComponents.cs
 * Authors: Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the GameComponents class that encapsulates
 * various game components and functionalities for managing
 * the Jacob's Resilience game. It includes game initialization,
 * input handling, world updating, and screen drawing.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */

using Config;
using JacobResilienceGame.Animals;
using JacobResilienceGame.Coins;
using JacobResilienceGame.Enemies;
using JacobResilienceGame.Energies;

namespace JacobResilienceGame
{
    public class GameComponents
    {
        private readonly Game game;          // Reference to the main Game object
        private readonly Program program;    // Reference to the main Program object

        // Checkers for animals
        private CheckCrocodile? checkCrocodile;         // Crocodile checker
        private CheckHippopotamus? checkHippopotamus;   // Hippopotamus checker
        private CheckSnake1? checkSnake1;               // Snake 1 checker
        private CheckSnake2? checkSnake2;               // Snake 2 checker        
        private CheckZebra? checkZebra;                 // Zebra checker

        // Checkers for coins
        private CheckCoins1? checkCoins1;               // Coins 1 checker
        private CheckCoins2? checkCoins2;               // Coins 2 checker
        private CheckCoins3? checkCoins3;               // Coins 3 checker
        private CheckDiamond? checkDiamond;             // Diamond checker

        // Checkers for enemies
        private CheckDanger? checkDanger;               // Danger checker
        private CheckFire? checkFire;                   // Fire checker
        private CheckPoachers? checkPoachers;           // Poachers checker
        private CheckTrap? checkTrap;                   // Trap checker

        // Checkers for energies
        private CheckApple? checkApple;                 // Apple checker
        private CheckEnergy? checkEnergy;               // Energy checker        
        private CheckMagicPotion? checkMagicPotion;    // Magic Potion checker
        private CheckMeat1? checkMeat1;                 // Meat 1 checker
        private CheckMeat2? checkMeat2;                 // Meat 2 checker
        private CheckWatermelon? checkWatermelon;       // Watermelon checker

        // Constructors
        public GameComponents(Game game, Program program)
        {
            this.game = game;
            this.program = program;
            Build();
        }

        // Build method to initialize game components
        private void Build()
        {
            BuildAnimals();
            BuildCoins();
            BuildEnemies();
            BuildEnergies();
        }

        // Builder for animal-related components
        private void BuildAnimals()
        {
            checkCrocodile = new CheckCrocodile(game, program);           // Crocodile checker
            checkHippopotamus = new CheckHippopotamus(game, program);     // Hippopotamus checker
            checkSnake1 = new CheckSnake1(game, program);                 // Snake 1 checker
            checkSnake2 = new CheckSnake2(game, program);                 // Snake 2 checker
            checkZebra = new CheckZebra(game, program);                   // Zebra checker
        }

        // Builder for coin-related components
        private void BuildCoins()
        {
            checkCoins1 = new CheckCoins1(game, program);                 // Coins 1 checker
            checkCoins2 = new CheckCoins2(game, program);                 // Coins 2 checker
            checkCoins3 = new CheckCoins3(game, program);                 // Coins 3 checker
            checkDiamond = new CheckDiamond(game, program);               // Diamond checker
        }

        // Builder for enemy-related components
        private void BuildEnemies()
        {
            checkDanger = new CheckDanger(game, program);                 // Danger checker
            checkFire = new CheckFire(game, program);                     // Fire checker
            checkPoachers = new CheckPoachers(game, program);             // Poachers checker
            checkTrap = new CheckTrap(game, program);                     // Trap checker
        }

        // Builder for energy-related components
        private void BuildEnergies()
        {
            checkApple = new CheckApple(game, program);                   // Apple checker
            checkEnergy = new CheckEnergy(game, program);                 // Energy checker
            checkMagicPotion = new CheckMagicPotion(game, program);       // Magic Potion checker
            checkMeat1 = new CheckMeat1(game, program);                   // Meat 1 checker
            checkMeat2 = new CheckMeat2(game, program);                   // Meat 2 checker
            checkWatermelon = new CheckWatermelon(game, program);         // Watermelon checker
        }

        // Method to invoke all mentioned objects with integer parameters
        public async Task InvokeAll(int y, int x)
        {
            // Animals
            if (checkCrocodile != null) await checkCrocodile.CheckForItems(y, x); // Check Crocodile
            if (checkHippopotamus != null) await checkHippopotamus.CheckForItems(y, x); // Check Hippopotamus
            if (checkSnake1 != null) await checkSnake1.CheckForItems(y, x); // Check Snake 1
            if (checkSnake2 != null) await checkSnake2.CheckForItems(y, x); // Check Snake 2
            if (checkZebra != null) await checkZebra.CheckForItems(y, x); // Check Zebra

            // Coins
            if (checkCoins1 != null) await checkCoins1.CheckForItems(y, x); // Check Coins 1
            if (checkCoins2 != null) await checkCoins2.CheckForItems(y, x); // Check Coins 2
            if (checkCoins3 != null) await checkCoins3.CheckForItems(y, x); // Check Coins 3
            if (checkDiamond != null) await checkDiamond.CheckForItems(y, x); // Check Diamond

            // Enemies
            if (checkDanger != null) await checkDanger.CheckForItems(y, x); // Check Danger
            if (checkFire != null) await checkFire.CheckForItems(y, x); // Check Fire
            if (checkPoachers != null) await checkPoachers.CheckForItems(y, x); // Check Poachers
            if (checkTrap != null) await checkTrap.CheckForItems(y, x); // Check Trap

            // Energies
            if (checkApple != null) await checkApple.CheckForItems(y, x); // Check Apple
            if (checkEnergy != null) await checkEnergy.CheckForItems(y, x); // Check Energy
            if (checkMagicPotion != null) await checkMagicPotion.CheckForItems(y, x); // Check Magic Potion
            if (checkMeat1 != null) await checkMeat1.CheckForItems(y, x); // Check Meat 1
            if (checkMeat2 != null) await checkMeat2.CheckForItems(y, x); // Check Meat 2
            if (checkWatermelon != null) await checkWatermelon.CheckForItems(y, x); // Check Watermelon
        }
    }
}

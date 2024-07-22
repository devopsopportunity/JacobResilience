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
        private Game game;          // Reference to the main Game object
        private Program program;    // Reference to the main Program object

        // Checkers for animals
        private CheckCrocodile? checkCrocodile;         // Crocodile checker
        private CheckHippopotamus? checkHippopotamus;   // Hippopotamus checker
        private CheckSnake1? checkSnake1;               // Snake 1 checker
        private CheckSnake2? checkSnake2;               // Snake 2 checker

        // Checkers for coins
        private CheckCoins1? checkCoins1;               // Coins 1 checker
        private CheckCoins2? checkCoins2;               // Coins 2 checker
        private CheckCoins3? checkCoins3;               // Coins 3 checker

        // Checkers for enemies
        private CheckDanger? checkDanger;               // Danger checker
        private CheckFire? checkFire;                   // Fire checker
        private CheckPoachers? checkPoachers;           // Poachers checker
        private CheckTrap? checkTrap;                   // Trap checker

        // Checkers for energies
        private CheckApple? checkApple;                 // Apple checker
        private CheckEnergy? checkEnergy;               // Energy checker
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
        }

        // Builder for coin-related components
        private void BuildCoins()
        {
            checkCoins1 = new CheckCoins1(game, program);                 // Coins 1 checker
            checkCoins2 = new CheckCoins2(game, program);                 // Coins 2 checker
            checkCoins3 = new CheckCoins3(game, program);                 // Coins 3 checker
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
            checkMeat1 = new CheckMeat1(game, program);                   // Meat 1 checker
            checkMeat2 = new CheckMeat2(game, program);                   // Meat 2 checker
            checkWatermelon = new CheckWatermelon(game, program);         // Watermelon checker
        }

        // Method to invoke all mentioned objects with integer parameters
        public async Task InvokeAll(int y, int x)
        {
            // Animals
            await checkCrocodile!.CheckForItems(y, x);            // Check Crocodile
            await checkHippopotamus!.CheckForItems(y, x);         // Check Hippopotamus
            await checkSnake1!.CheckForItems(y, x);               // Check Snake 1
            await checkSnake2!.CheckForItems(y, x);               // Check Snake 2

            // Coins
            await checkCoins1!.CheckForItems(y, x);               // Check Coins 1
            await checkCoins2!.CheckForItems(y, x);               // Check Coins 2
            await checkCoins3!.CheckForItems(y, x);               // Check Coins 3

            // Enemies
            await checkDanger!.CheckForItems(y, x);               // Check Danger
            await checkFire!.CheckForItems(y, x);                 // Check Fire
            await checkPoachers!.CheckForItems(y, x);             // Check Poachers
            await checkTrap!.CheckForItems(y, x);                 // Check Trap

            // Energies
            await checkApple!.CheckForItems(y, x);                // Check Apple
            await checkEnergy!.CheckForItems(y, x);               // Check Energy
            await checkMeat1!.CheckForItems(y, x);                // Check Meat 1
            await checkMeat2!.CheckForItems(y, x);                // Check Meat 2
            await checkWatermelon!.CheckForItems(y, x);           // Check Watermelon
        }
    }
}

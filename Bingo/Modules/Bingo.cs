using Bingo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bingo.Modules {
    class BingoGame {
        private readonly BingoField [,] BingoFields = new BingoField [5, 5];
        private readonly List<int> GuessedNumbers = new List<int>();
        private int Tries = 0;
        public BingoGame() {
            List<int> usedNumbers = new List<int>();
            Random rand = new Random();
            for(int i = 0; i < 5; i++) {
                for(int j = 0; j < 5; j++) {
                    int number;
                    do {
                        number = rand.Next(1, 53);
                    } while (usedNumbers.Contains(number));
                    usedNumbers.Add(number);
                    BingoFields[i, j] = new BingoField(number);
                }
            }
        }
        public void StartGame() {
            Console.WriteLine("New Bingo Game!");
            Console.WriteLine("Here is your game field:");
            Print();
            Console.WriteLine("Press any key to start the game");
            Console.ReadKey();
            do {
                Console.Clear();
                Console.WriteLine($"Number is {SetNewNumber()}");
                Console.WriteLine("Your game field:");
                Print();
                Thread.Sleep(1000);
                
            } while (!IsFinished());
            Console.WriteLine("Congratulations! You have won");
            Console.WriteLine($"Total attempts - { Tries}");
        }
        private int SetNewNumber() {
            Random rand = new Random();
            int num;
            do {
                num = rand.Next(1, 53);
            } while (GuessedNumbers.Contains(num));
            GuessedNumbers.Add(num);
            Tries++;
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    if (BingoFields[i, j].Value == num) {
                        BingoFields[i, j].Guessed = true;
                    }
                }
            }
            return num;
        }
        private bool IsFinished() {
            if(Tries < 5) {
                return false;
            }
            // check if have all numbers guessed on any of diagonal
            bool finishedDiagonal = true;
            bool finishedDiagonalReverse = true;
            for (int i = 0; i < 5; i++) {
                if (!BingoFields[i, i].Guessed) {
                    finishedDiagonal = false;
                }
                if (!BingoFields[i, 4 - i].Guessed) {
                    finishedDiagonalReverse = false;
                }
                if (!finishedDiagonal && !finishedDiagonalReverse) {
                    break;
                }
            }
            if (finishedDiagonal || finishedDiagonalReverse) {
                return true;
            }
            // check all rows and columns
            for (int i = 0; i < 5; i++) {
                bool finishedRow = true;
                bool finishedCol = true;
                for (int j = 0; j < 5; j++) {
                    if(!BingoFields[i, j].Guessed) {
                        finishedRow = false;
                    }
                    if (!BingoFields[j, i].Guessed) {
                        finishedCol = false;
                    }
                    if(!finishedRow && !finishedCol) {
                        break;
                    }
                }
                if(finishedRow || finishedCol) {
                    return true;
                }
            }
            return false;
        }
        private void Print() {
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    BingoFields[i, j].Print();
                }
                Console.WriteLine();
            }
        }
    }
}

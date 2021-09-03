using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo.Models {
    struct BingoField {
        public BingoField(int val) {
            Value = val;
            Guessed = false;
        }
        public int Value { get; }
        public bool Guessed { get;  set; }
        public void Print() {
            if (Guessed) {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if(Value > 9) {
                Console.Write($" {Value} ");
            }
            else {
                Console.Write($" {Value}  ");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _2
{
    class Program
    {
        private const string REAL_INPUT = "./in.real";
        private const string TEST_INPUT = "./in.tst";
        
        static void Solve(int first, int second)
        {
            var allText = File.ReadAllText(REAL_INPUT);
            var commands = allText.Split(',').Select(Int32.Parse).ToList();
            commands[1] = first;
            commands[2] = second;
            for (var programCounter = 0; programCounter < commands.Count() && commands[programCounter] != 99; programCounter += 4) 
            {
                var cmd = commands[programCounter];
                
                if (cmd == 99) 
                    break;

                var P = programCounter;
                int a = commands[P + 1], 
                    b = commands[P + 2], 
                    c = commands[P + 3];
                if (cmd == 1)
                {
                    commands[c] = commands[a] + commands[b];
                }
                else if (cmd == 2)
                {
                    commands[c] = commands[a] * commands[b];
                }
            }
            Console.WriteLine(commands[0]);
        }

        static void Main(string[] args)
        {
            Solve(12, 2);
            // Second integer is only added once, so just find first factor within range and up it,
            // some more stuff probably but this works
            Solve(52, 96);
            Console.WriteLine(52 * 100 + 96);
        }
    }
}

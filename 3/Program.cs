using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _3
{


    class Program
    {
        private const string REAL_INPUT = "./in.real";
        private const string TEST_INPUT = "./in.tst";
        private const string TEST_INPUT2 = "./in.tst";

        class Point : Tuple<int, int>
        {
            public Point(int x, int y) : base(x, y) {}

            public int X 
            {
                get 
                {
                    return Item1;
                }
            }

            public int Y 
            {
                get
                {
                    return Item2;
                }
            }

            public Point Travel(Point diff, int length)
            {
                return new Point(
                    X + diff.X * length,
                    Y + diff.Y * length
                );
            }

            public int Dist() 
            {
                return Math.Abs(X) + Math.Abs(Y);
            }

        }
        

        static void Main(string[] args)
        {
            var inputStrings = File.ReadAllLines(REAL_INPUT);
            var firstLine = inputStrings[0];
            var firstDirections = firstLine.Split(",");
            var secondDirections = inputStrings[1].Split(",");            

            var direction = new Dictionary<char, Point>()
            {
              { 'U', new Point(0, 1) },
              { 'D', new Point(0, -1) },
              { 'R', new Point(1, 0) },
              { 'L', new Point(-1, 0) }
            };

            var originalSteps = firstDirections
                .Aggregate(
                    (new Dictionary<Point, int>(), new Point(0, 0), 0),
                    (aggregator, currentDir) => {
                        (var dict, var startPos, var step) = aggregator;
                        var diff = direction[currentDir[0]];
                        var length = Int32.Parse(currentDir.Substring(1));
                        for (var idx = 1; idx <= length; ++idx)
                        {
                            var currentPos = startPos.Travel(diff, idx);
                            var currentStep = step + idx;
                            var updatedStep = dict.ContainsKey(currentPos) ?
                                dict[currentPos] : currentStep;
                            dict[currentPos] = currentStep;
                        }
                        return (dict, startPos.Travel(diff, length), step + length);
                    }
                ).Item1;
            
            Console.WriteLine(originalSteps.Count());
            
            var secondSteps = secondDirections
                .Aggregate(
                    (new Dictionary<Point, int>(), new Point(0, 0), 0),
                    (aggregator, currentDir) => {
                        (var dict, var startPos, var step) = aggregator;
                        var diff = direction[currentDir[0]];
                        var length = Int32.Parse(currentDir.Substring(1));
                        for (var idx = 1; idx <= length; ++idx)
                        {
                            var currentPos = startPos.Travel(diff, idx);
                            var currentStep = step + idx;
                            var updatedStep = dict.ContainsKey(currentPos) ?
                                dict[currentPos] : currentStep;
                            dict[currentPos] = currentStep;
                        }
                        return (dict, startPos.Travel(diff, length), step + length);
                    }
                ).Item1;
            
            var crossings = secondSteps.Keys.Aggregate(
                new List<(Point, int)>(),
                (aggregator, currentPos) => {
                    if (originalSteps.ContainsKey(currentPos)) 
                    {
                        var wireLength = secondSteps[currentPos] + originalSteps[currentPos];
                        aggregator.Add((currentPos, wireLength));
                    }
                    return aggregator;
                }
            );

            var minimalManhattan = crossings.Aggregate(
                (bestCrossing, currentCrossing) => {
                    var bestCrossingPoint = bestCrossing.Item1;
                    var currentCrossingPoint = currentCrossing.Item1;
                    if (currentCrossingPoint.Dist() < bestCrossingPoint.Dist())
                    {
                        return currentCrossing;
                    }
                    return bestCrossing;
                }
            ).Item1.Dist();

            var bestWireLength = crossings.Aggregate(
                (bestCrossing, currentCrossing) => {
                    if (currentCrossing.Item2 < bestCrossing.Item2)
                    {
                        return currentCrossing;
                    }
                    return bestCrossing;
                }
            ).Item2;

            Console.WriteLine($"Advent 3 - Challenge #1 = {minimalManhattan}");
            Console.WriteLine($"Advent 3 - Challenge #2 = {bestWireLength}");
        }
    }
}

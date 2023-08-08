using System;
using System.Collections.Generic;
using System.Linq;
namespace ChipSecuritySystem
{
    class Program
    {
        private static readonly SolutionEquality SequenceComparer = new SolutionEquality();

        static void Main()
        {
            Color start = Color.Blue;
            Color end = Color.Green;

            ColorChip[] bag = new ColorChip[]
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Orange, Color.Purple),

                new ColorChip(Color.Green, Color.Blue),
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Orange),
                new ColorChip(Color.Purple, Color.Red)
            };

            Console.WriteLine("Chip Security System");
            Console.WriteLine();
            Console.WriteLine("Solution must span");
            Console.WriteLine("\tfrom {0}", start);
            Console.WriteLine("\t  to {0}", end);
            Console.WriteLine();
            Console.WriteLine("Using colored chips from the set:");
            foreach (ColorChip chip in bag)
            {
                Console.WriteLine("\t- {0}", chip);
            }
            Console.WriteLine();

            IEnumerable<IEnumerable<ColorChip>> solutions = Solver.Solve(start, end, bag);
            IEnumerable<IEnumerable<ColorChip>> uniqueSolutions = solutions.Distinct(SequenceComparer);

            Console.WriteLine("Found {0} total solution(s)", solutions.Count());
            Console.WriteLine("  and {0} distinct solutions(s)", uniqueSolutions.Count());
            Console.WriteLine(" with {0} distinct solution length(s)", uniqueSolutions.Select(x => x.Count()).Distinct().Count());
            Console.WriteLine();

            if (solutions.Count() > 0)
            {
                //Newer versions of C# have linq extensions to handle this sort of query more gracefully
                int minLength = solutions.Min(x => x.Count());
                IEnumerable<ColorChip> shortestSolution = solutions.First(x => x.Count() == minLength);

                Console.WriteLine("Shortest:");
                foreach (string s in FormatSolution(start, shortestSolution, end))
                {
                    Console.WriteLine("\t{0}", s);
                }
                Console.WriteLine();

                int maxLength = solutions.Max(x => x.Count());
                IEnumerable<ColorChip> longestSolution = solutions.First(x => x.Count() == maxLength);

                Console.WriteLine("Longest:");
                foreach (string s in FormatSolution(start, longestSolution, end))
                {
                    Console.WriteLine("\t{0}", s);
                }
                Console.WriteLine();

                Console.WriteLine("All Solutions:");
                foreach(IEnumerable<ColorChip> solution in solutions)
                {
                    Console.WriteLine(String.Join(" - ", solution.Select(x => String.Format("[{0}]", x.ToString()))));
                }
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Given the parameters of a security system, transform the terms from beginning to end into strings
        /// </summary>
        private static IEnumerable<string> FormatSolution(Color start, IEnumerable<ColorChip> sequence, Color end)
        {
            yield return start.ToString();
            foreach (ColorChip chip in sequence) yield return "- " + chip.ToString();
            yield return end.ToString();
        }
    }
}

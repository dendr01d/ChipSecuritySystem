using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    public static class Solver
    {
        /// <summary>
        /// <see cref="ColorChip"/> doesn't implement its own member equality check, and since I can't write one in
        /// directly, let's just define a comparer for it.
        /// </summary>
        private static readonly ColorChipEquality ColorChipComparer = new ColorChipEquality();

        /// <summary>
        /// Find all valid solutions to a chip security system that spans from <paramref name="head"/> to <paramref name="tail"/>
        /// using the given <paramref name="chips"/>
        /// </summary>
        /// <param name="head">The starting color of the system</param>
        /// <param name="tail">The ending color of the system</param>
        /// <param name="chips">A collection of <see cref="ColorChip"/>s to be used to complete the security system</param>
        /// <returns>A collection of ordered sequences of <see cref="ColorChip"/>s that complete the chain from
        /// <paramref name="head"/> to <paramref name="tail"/></returns>
        public static IEnumerable<IEnumerable<ColorChip>> Solve(Color head, Color tail, IEnumerable<ColorChip> chips)
        {
            foreach (ColorChip chip in chips)
            {
                if (chip.StartColor == head && chip.EndColor == tail)
                {
                    //If the chip in question completes the solution, return it as the beginning of a new sequence
                    yield return new ColorChip[] { chip };
                }
                
                //these two conditionals are NOT mutually exclusive
                
                if (chip.EndColor == tail)
                {
                    //if the chip can be attached to the end of the solution,
                    //treat it as the new tail of a smaller problem, and recurse

                    IEnumerable<ColorChip> remainingChips = chips.RemoveFirstInstance(chip, ColorChipComparer);

                    IEnumerable<IEnumerable<ColorChip>> solutions = Solve(head, chip.StartColor, remainingChips);

                    foreach (IEnumerable<ColorChip> solution in solutions) yield return solution.ExtendWith(chip);
                }
            }
        }
    }
}

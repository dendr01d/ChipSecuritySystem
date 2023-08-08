using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    public sealed class SolutionEquality : EqualityComparer<IEnumerable<ColorChip>>
    {
        private static readonly ColorChipEquality Comparer = new ColorChipEquality();
        public override bool Equals(IEnumerable<ColorChip> x, IEnumerable<ColorChip> y)
        {
            return x.SequenceEqual(y, Comparer);
        }

        public override int GetHashCode(IEnumerable<ColorChip> obj)
        {
            return obj.Where(x => x != null).Aggregate(0, (h, x) => h ^ Comparer.GetHashCode(x));
        }
    }
}

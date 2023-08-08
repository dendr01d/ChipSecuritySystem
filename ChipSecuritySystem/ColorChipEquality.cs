using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    public sealed class ColorChipEquality : EqualityComparer<ColorChip>
    {
        public override bool Equals(ColorChip x, ColorChip y)
        {
            return x.StartColor == y.StartColor && x.EndColor == y.EndColor;
        }

        public override int GetHashCode(ColorChip obj) => (int)obj.StartColor ^ (int)obj.EndColor;
    }
}

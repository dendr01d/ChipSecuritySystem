using System.Linq;

using ChipSecuritySystem;

namespace FunctionTests
{
    [TestClass]
    public class Test_ColorChipEquality
    {
        [TestMethod]
        public void TrueCase()
        {
            ColorChip chip1 = new ColorChip(Color.Blue, Color.Yellow);
            ColorChip chip2 = new ColorChip(Color.Blue, Color.Yellow);

            ColorChipEquality comparer = new ColorChipEquality();

            Assert.AreEqual(true, comparer.Equals(chip1, chip2));
        }

        [TestMethod]
        public void FalseCase()
        {
            ColorChip chip1 = new ColorChip(Color.Blue, Color.Yellow);
            ColorChip chip2 = new ColorChip(Color.Blue, Color.Orange);
            ColorChip chip3 = new ColorChip(Color.Purple, Color.Yellow);

            ColorChipEquality comparer = new ColorChipEquality();

            Assert.AreEqual(false, comparer.Equals(chip1, chip2));
            Assert.AreEqual(false, comparer.Equals(chip1, chip3));
            Assert.AreEqual(false, comparer.Equals(chip2, chip3));
        }
    }
}
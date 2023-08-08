using System.Linq;

using ChipSecuritySystem;

namespace FunctionTests
{
    [TestClass]
    public class Test_RemoveFirstInstance
    {
        [TestMethod]
        public void IntegerTest()
        {
            int[] ints = new int[] { 1, 2, 3, 4, 5 };

            EqualityComparer<int> comparer = EqualityComparer<int>.Default;

            var no2 = ints.RemoveFirstInstance(2, comparer);
            Assert.AreEqual(4, no2.Count());
            Assert.AreNotEqual(ints, no2);
            Assert.IsFalse(no2.Contains(2));

            var no5 = ints.RemoveFirstInstance(5, comparer);
            Assert.AreEqual(4, no5.Count());
            Assert.AreNotEqual(ints, no5);
            Assert.IsFalse(no5.Contains(5));

            var noEven = ints.RemoveFirstInstance(2, comparer).RemoveFirstInstance(4, comparer);
            Assert.AreEqual(3, noEven.Count());
            Assert.AreNotEqual(ints, noEven);
            Assert.IsFalse(noEven.Contains(2));
            Assert.IsFalse(noEven.Contains(4));
        }

        [TestMethod]
        public void StringTest()
        {
            string[] strings = new string[] { "mary", "had", "a", "little", "lamb" };

            EqualityComparer<string> comparer = EqualityComparer<string>.Default;

            var noMary = strings.RemoveFirstInstance("mary", comparer);
            Assert.AreEqual(4, noMary.Count());
            Assert.AreNotEqual(strings, noMary);
            Assert.IsFalse(noMary.Contains("mary"));

            var noA = strings.RemoveFirstInstance("a", comparer);
            Assert.AreEqual(4, noA.Count());
            Assert.AreNotEqual(strings, noA);
            Assert.IsFalse(noA.Contains("a"));

            var noNouns = strings.RemoveFirstInstance("mary", comparer).RemoveFirstInstance("lamb", comparer);
            Assert.AreEqual(3, noNouns.Count());
            Assert.AreNotEqual(strings, noNouns);
            Assert.IsFalse(noNouns.Contains("mary"));
            Assert.IsFalse(noNouns.Contains("lamb"));
        }

        [TestMethod]
        public void ChipTest()
        {
            ColorChip[] chips = new ColorChip[]
            {
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Red, Color.Orange)
            };

            EqualityComparer<ColorChip> comparer = new ColorChipEquality();

            var noFirst = chips.RemoveFirstInstance(chips[0], comparer);
            Assert.AreEqual(5, noFirst.Count());
            Assert.AreNotEqual(chips, noFirst);
            Assert.IsFalse(noFirst.Contains(chips[0], comparer));

            var noLast = chips.RemoveFirstInstance(chips[4], comparer);
            Assert.AreEqual(5, noLast.Count());
            Assert.AreNotEqual(chips, noLast);
            Assert.IsFalse(noLast.Contains(chips[4], comparer));

            var noOpp = chips.RemoveFirstInstance(chips[0], comparer).RemoveFirstInstance(chips[1], comparer);
            Assert.AreEqual(4, noOpp.Count());
            Assert.AreNotEqual(chips, noOpp);
            Assert.IsFalse(noOpp.Contains(chips[0], comparer));
            Assert.IsFalse(noOpp.Contains(chips[1], comparer));

            ColorChip seaChip = new ColorChip(Color.Blue, Color.Green);

            var noSea1 = chips.RemoveFirstInstance(seaChip, comparer);
            Assert.AreEqual(5, noSea1.Count());
            Assert.AreNotEqual(chips, noSea1);
            Assert.IsTrue(noSea1.Contains(seaChip, comparer));

            var noSea2 = noSea1.RemoveFirstInstance(seaChip, comparer);
            Assert.AreEqual(4, noSea2.Count());
            Assert.AreNotEqual(chips, noSea2);
            Assert.AreNotEqual(noSea1, noSea2);
            Assert.IsFalse(noSea2.Contains(seaChip, comparer));
        }
    }
}
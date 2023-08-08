using System.Linq;

using ChipSecuritySystem;

namespace FunctionTests
{
    [TestClass]
    public class Test_Solver
    {
        [TestMethod]
        public void SimpleTest()
        {
            Color start = Color.Red;
            Color end = Color.Purple;

            ColorChip[] chips = new ColorChip[]
            {
                new ColorChip(Color.Red, Color.Purple)
            };

            var solutions = Solver.Solve(start, end, chips);

            Assert.AreEqual(1, solutions.Count());
            IEnumerable<ColorChip> solution = solutions.First();

            Assert.AreEqual(1, solution.Count());
            Assert.IsTrue(solution.Contains(chips[0]));
        }

        [TestMethod]
        public void RedundantTest()
        {
            Color start = Color.Yellow;
            Color end = Color.Yellow;

            ColorChip[] chips = new ColorChip[]
            {
                new ColorChip(Color.Yellow, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Yellow)
            };

            var solutions = Solver.Solve(start, end, chips);

            //should be four solutions
            //two with either of the initial chips
            //and two combining both chips in both possible orders

            Assert.AreEqual(4, solutions.Count());

            int minLength = solutions.Min(x => x.Count());
            int maxLength = solutions.Max(x => x.Count());

            Assert.AreEqual(1, minLength);
            Assert.AreEqual(2, maxLength);

            Assert.AreEqual(2, solutions.Where(x => x.Count() == minLength).Count());
            Assert.AreEqual(2, solutions.Where(x => x.Count() == maxLength).Count());
        }

        [TestMethod]
        public void CanonicalTest()
        {
            Color start = Color.Blue;
            Color end = Color.Green;

            ColorChip[] chips = new ColorChip[]
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Orange, Color.Purple)
            };

            var solutions = Solver.Solve(start, end, chips);
            Assert.AreEqual(1, solutions.Count());

            var solution = solutions.First().ToArray();
            Assert.AreEqual(3, solution.Length);

            ColorChipEquality comparer = new ColorChipEquality();

            Assert.IsTrue(comparer.Equals(solution[0], chips[0]));
            Assert.IsTrue(comparer.Equals(solution[1], chips[1]));
            Assert.IsTrue(comparer.Equals(solution[2], chips[2]));

            Assert.IsFalse(solution.Contains(chips[3]));
        }

        [TestMethod]
        public void AlternativeTest()
        {
            Color start = Color.Red;
            Color end = Color.Yellow;

            ColorChip[] chips = new ColorChip[]
            {
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Green, Color.Yellow),
                new ColorChip(Color.Red, Color.Blue),
                new ColorChip(Color.Blue, Color.Yellow)
            };

            var solutions = Solver.Solve(start, end, chips);

            //should be two valid solutions

            Assert.AreEqual(2, solutions.Count());

            var solution1 = solutions.First().ToArray();
            Assert.AreEqual(2, solution1.Length);

            var solution2 = solutions.Last().ToArray();
            Assert.AreEqual(2, solution2.Length);

            ColorChipEquality comparer = new ColorChipEquality();

            Assert.IsTrue(comparer.Equals(solution1[0], chips[0]));
            Assert.IsTrue(comparer.Equals(solution1[1], chips[1]));

            Assert.IsTrue(comparer.Equals(solution2[0], chips[2]));
            Assert.IsTrue(comparer.Equals(solution2[1], chips[3]));
        }


        [TestMethod]
        public void UnbalancedTest()
        {
            Color start = Color.Red;
            Color end = Color.Yellow;

            ColorChip[] chips = new ColorChip[]
            {
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Red, Color.Orange),
                new ColorChip(Color.Orange, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Green, Color.Blue),
                new ColorChip(Color.Blue, Color.Purple),
                new ColorChip(Color.Purple, Color.Yellow)
            };

            var solutions = Solver.Solve(start, end, chips);

            Assert.AreEqual(3, solutions.Count());
        }
    }
}
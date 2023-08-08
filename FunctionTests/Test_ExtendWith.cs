using System.Linq;

using ChipSecuritySystem;

namespace FunctionTests
{
    [TestClass]
    public class Test_ExtendWith
    {
        [TestMethod]
        public void TestExtension()
        {
            int[] sequence = new int[] { 0, 1, 2 };
            int newElement = 3;

            IEnumerable<int> extended = sequence.ExtendWith(newElement);

            Assert.AreEqual(4, extended.Count());
            Assert.AreEqual(0, extended.First());
            Assert.AreEqual(3, extended.Last());
        }
    }
}
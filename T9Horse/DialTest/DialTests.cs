using T9Horse;

namespace DialTest
{
    [TestClass]
    public class DialTests
    {
        [TestMethod]
        public void TestDialFromOne()
        {
            int[,] matrix = new int[,] { { 1, 2, 3 },
                                          { 4, 5, 6 },
                                          { 7, 8, 9 },
                                          { -1, 0, -1 } };
            Dial dial = new Dial(matrix, new KeyValuePair<int, int>(0, 0), 2);
            Assert.AreEqual(dial.countDistinctNumbers(), 5);
        }
        [TestMethod]
        public void TestDialFromZero()
        {
            int[,] matrix = new int[,] { { 1, 2, 3 },
                                          { 4, 5, 6 },
                                          { 7, 8, 9 },
                                          { -1, 0, -1 } };
            Dial dial = new Dial(matrix, new KeyValuePair<int, int>(3, 1), 2);
            Assert.AreEqual(dial.countDistinctNumbers(), 6);
        }
        [TestMethod]
        public void TestDialFromFive()
        {
            int[,] matrix = new int[,] { { 1, 2, 3 },
                                          { 4, 5, 6 },
                                          { 7, 8, 9 },
                                          { -1, 0, -1 } };
            Dial dial = new Dial(matrix, new KeyValuePair<int, int>(1, 1), 2);
            Assert.AreEqual(dial.countDistinctNumbers(), 1);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Staring location number must be between 0 and 9. Currently is -1")]

        public void TestDialFromMinusOne()
        {
            int[,] matrix = new int[,] { { 1, 2, 3 },
                                          { 4, 5, 6 },
                                          { 7, 8, 9 },
                                          { -1, 0, -1 } };
            Dial dial = new Dial(matrix, new KeyValuePair<int, int>(3, 2), 2);
            dial.countDistinctNumbers();
        }
        [TestMethod]
        public void TestDialZeroJumps()
        {
            int[,] matrix = new int[,] { { 1, 2, 3 },
                                          { 4, 5, 6 },
                                          { 7, 8, 9 },
                                          { -1, 0, -1 } };
            Dial dial = new Dial(matrix, new KeyValuePair<int, int>(0, 0), 0);
            Assert.AreEqual(dial.countDistinctNumbers(), 1);

        }
    }
}
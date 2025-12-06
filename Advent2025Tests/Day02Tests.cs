namespace Advent2025.Tests
{
    [TestClass()]
    public class Day02Tests
    {
        [TestMethod()]
        public void PalimdromifyierTest()
        {
            Day02 day = new Day02("");
            long result = day.Palimdromifyier(123, 3);
            Assert.AreEqual(123123132, result);
        }
    }
}
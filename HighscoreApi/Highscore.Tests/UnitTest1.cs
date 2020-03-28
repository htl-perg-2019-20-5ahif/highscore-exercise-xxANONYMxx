using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Highscore.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            [Fact]
        public void GetNullHighscoreList()
        {
            var logic = new Class1();

            Assert.Equal(null, logic.addScore(null, null));
        }
    }
    }
}

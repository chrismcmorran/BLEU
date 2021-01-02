using System.Linq;
using BLEU;
using NUnit.Framework;

namespace UnitTests
{
    public class ArrayTests
    {
        [Test]
        public void TestBasicAverageZero()
        {
            var expected = 0;
            var d = System.Array.Empty<double>();
            Assert.AreEqual(expected, d.AverageValue());
        }
        
        [Test]
        public void TestBasicAverageTwo()
        {
            var expected = 2;
            var d = new double[] {2, 2};
            Assert.AreEqual(expected, d.AverageValue());
        }
    }
}
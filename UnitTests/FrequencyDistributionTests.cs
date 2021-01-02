using BLEU.Structures;
using NUnit.Framework;

namespace UnitTests
{
    public class FrequencyDistributionTests
    {
        [Test]
        public void TestMostFrequentValue()
        {
            var dist = new FrequencyDistribution<string>("a a a a b c d e e ffff".Split(" "));
            Assert.AreEqual(4, dist.MostFrequentValue());
        }
        
        [Test]
        public void TestMostFrequentKey()
        {
            var dist = new FrequencyDistribution<string>("a a a a b c d e e ffff".Split(" "));
            Assert.AreEqual("a", dist.MostFrequent());
        }
    }
}
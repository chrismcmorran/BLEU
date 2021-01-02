using BLEU;
using BLEU.Collectors;
using NUnit.Framework;

namespace UnitTests
{
    public class NGramTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetSingleGrams()
        {
            var testString = "this is a test string";
            var expected = new []{"this", "is", "a", "test", "string"};
            var collector = new NGramCollector(testString);
            var result = collector.Collect();
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void TestGetBiGrams()
        {
            var testString = "this is a test string";
            var expected = new []{"this is", "is a", "a test", "test string"};
            var collector = new NGramCollector(testString, 2);
            var result = collector.Collect();
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void TestGetTriGrams()
        {
            var testString = "this is a test string";
            var expected = new []{"this is a", "is a test", "a test string"};
            var collector = new NGramCollector(testString, 3);
            var result = collector.Collect();
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void TestGetTriGramNoPunctuation()
        {
            var testString = "this, is a test string";
            var expected = new []{"this is a", "is a test", "a test string"};
            var collector = new NGramCollector(testString, 3, stripPunctuation: true);
            var result = collector.Collect();
            Assert.AreEqual(expected, result);
        }
    }
}
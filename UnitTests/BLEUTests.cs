using System.Collections.Generic;
using BLEU;
using BLEU.Collectors;
using NUnit.Framework;

namespace UnitTests
{
    public class BLEUTests
    {
        [Test]
        public void TestSimplePrecision()
        {
            var candidate = "the the the the the the the.";
            var ref1 = "The cat is on the mat.";

            var candidateCollector = new NGramCollector(candidate);
            var refCollector = new NGramCollector(ref1);
            
            var bleu = new BleuScore();
            var precision = bleu.ModifiedUnigramPrecision(refCollector, candidateCollector);
            double expected = 2.0d / 7.0d;
            Assert.AreEqual(expected, precision);
        }
        
        [Test]
        public void TestSimplePrecisionStrings()
        {
            var candidate = "the the the the the the the.";
            var reference = "The cat is on the mat.";

            var bleu = new BleuScore();
            var precision = bleu.ModifiedUnigramPrecision(reference, candidate);
            double expected = 2.0d / 7.0d;
            Assert.AreEqual(expected, precision);
        }
        
        [Test]
        public void TestBiGramPrecisionStrings()
        {
            var candidate = "The cat the cat on the mat.";
            var references = new List<string> {"The cat is on the mat.", "There is a cat on the mat."};

            var bleu = new BleuScore();
            var precision = bleu.ModifiedNGramPrecision(references, candidate, 2);
            double expected = 2.0 / 3.0;
            Assert.AreEqual(expected, precision);
        }
    }
}
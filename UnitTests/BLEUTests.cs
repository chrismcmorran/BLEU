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
        
        [Test]
        public void TestBiGramPrecisionSanity()
        {
            var candidate = "The cat the cat on the mat.";
            var references = new List<string> {candidate};

            var bleu = new BleuScore();
            var precision = bleu.ModifiedBiGramPrecision(references, candidate);
            double expected = 1.0;
            Assert.AreEqual(expected, precision);
        }
        
        [Test]
        public void TestTriGramPrecisionSanity()
        {
            var candidate = "The cat the cat on the mat.";
            var references = new List<string> {candidate};

            var bleu = new BleuScore();
            var precision = bleu.ModifiedNGramPrecision(references, candidate, 3);
            double expected = 1.0;
            Assert.AreEqual(expected, precision);
        }
        
        [Test]
        public void TestUniGramPrecisionSanity()
        {
            var candidate = "The cat the cat on the mat.";
            var references = new List<string> {candidate};

            var bleu = new BleuScore();
            var precision = bleu.ModifiedUnigramPrecision(references, candidate);
            double expected = 1.0;
            Assert.AreEqual(expected, precision);
        }

        [Test]
        public void TestSentenceBleuScorePerfect()
        {
            var bleu = new BleuScore();
            var reference = new List<string> {"this is a test", "this is a test"};
            var candidate = "this is a test";
            var score = bleu.Score(reference, candidate);
            Assert.AreEqual(1.0, score);
        }
        
        [Test]
        public void TestSentenceBleuOneWordChanges()
        {
            var bleu = new BleuScore();
            var reference = new List<string> {"the quick brown fox jumped over the lazy dog"};
            var candidate = "the fast brown fox jumped over the lazy dog";
            var score = bleu.RoundedScore(reference, candidate);
            Assert.AreEqual(0.75, score);
        }
    }
}
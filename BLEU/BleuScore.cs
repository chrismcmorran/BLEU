using System;
using BLEU.Structures;

namespace BLEU
{
    public class BleuScore
    {


        /// <summary>
        /// Calculates the modified unigram precision score as described in section 2.1 of the paper.
        /// </summary>
        /// <param name="reference">The reference as a collector.</param>
        /// <param name="candidate">The candidate as a collector.</param>
        /// <returns>The Modified n-gram precision score.</returns>
        public double ModifiedUnigramPrecision(ICollector<string> reference, ICollector<string> candidate)
        {
            var referenceDist = new FrequencyDistribution<string>(reference.Collect());
            return referenceDist.MostFrequentValue() / (double) candidate.Size();
        }
        
        /// <summary>
        /// Calculates the modified unigram precision score as described in section 2.1 of the paper.
        /// </summary>
        /// <param name="reference">The reference as a string.</param>
        /// <param name="candidate">The candidate as a string.</param>
        /// <returns>The Modified n-gram precision score.</returns>
        public double ModifiedUnigramPrecision(string reference, string candidate)
        {
            var referenceCollector = new NGramCollector(reference);
            var candidateCollector = new NGramCollector(candidate);
            return ModifiedUnigramPrecision(referenceCollector, candidateCollector);
        }
        
        /// <summary>
        /// Computes the BrevityPenalty (BP). See section 2.3 of the paper.
        /// </summary>
        /// <param name="candidateTranslationLength">Variable c.</param>
        /// <param name="referenceCorpusLength">Variable r.</param>
        /// <returns>The BP as a double.</returns>
        public double BrevityPenalty(int candidateTranslationLength, int referenceCorpusLength)
        {
            if (candidateTranslationLength > referenceCorpusLength)
            {
                return 1;
            }

            return Math.Exp(1 - (referenceCorpusLength / candidateTranslationLength));
        }
    }
}
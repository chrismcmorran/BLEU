using System;
using System.Collections.Generic;
using System.Linq;
using BLEU.Collectors;
using BLEU.Structures;

namespace BLEU
{
    public class BleuScore
    {
        public double ModifiedNGramPrecision(ICollection<string> references, string candidate, int grams=2)
        {
            var count = new FrequencyDistribution<string>(new NGramCollector(candidate, grams).Collect());
            var countClip = new Dictionary<string, int>();
            
            foreach (var word in count.Keys)
            {
                countClip[word] = 0;
                foreach (var reference in references)
                {
                    var dist = new FrequencyDistribution<string>(new NGramCollector(reference, grams).Collect());
                    if (dist.ContainsKey(word))
                    {
                        countClip[word] = dist.FrequencyOf(word);
                        break;
                    }
                }
            }

            return countClip.Values.Sum() / (double) count.Values.Sum();
        }

        public double ModifiedBiGramPrecision(ICollection<string> references, string candidate)
        {
            return ModifiedNGramPrecision(references, candidate, 2);
        }

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
        /// Calculates the modified unigram precision score as described in section 2.1 of the paper.
        /// </summary>
        /// <param name="references">The reference as a list.</param>
        /// <param name="candidate">The candidate as a string.</param>
        /// <returns>The Modified n-gram precision score.</returns>
        public double ModifiedUnigramPrecision(List<string> references, string candidate)
        {
            return ModifiedNGramPrecision(references, candidate, 1);
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
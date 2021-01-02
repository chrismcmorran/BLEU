using System;
using System.Collections.Generic;
using System.Linq;
using BLEU.Collectors;
using BLEU.Structures;

namespace BLEU
{
    public class BleuScore
    {
        /// <summary>
        /// Calculates the BLEU score.
        /// </summary>
        /// <param name="references">The reference sentences.</param>
        /// <param name="candidate">The MT candidate.</param>
        /// <param name="weights">The weights.</param>
        /// <param name="N">The n-gram count.</param>
        /// <returns>The score as a double.</returns>
        public double Score(ICollection<string> references, string candidate, List<double> weights, int N=4)
        {
            var bp = BrevityPenalty(candidate.Length, references.Count);
            var sum = 0.0d;
            for (int i = 0; i < N; i++)
            {
                sum += weights[i] * Ln(ModifiedNGramPrecision(references, candidate, i + 1));
            }

            double average = sum / N;

            return bp * Math.Exp(average);
        }
        
        /// <summary>
        /// Calculates the BLEU score.
        /// </summary>
        /// <param name="references">The reference sentences.</param>
        /// <param name="candidate">The MT candidate.</param>
        /// <param name="N">The n-gram count.</param>
        /// <returns>The score as a double.</returns>
        public double Score(List<string> references, string candidate, int N=4)
        {
            return Score(references, candidate, GetDefaultWeights(N));
        }

        /// <summary>
        /// Calculates the BLEU score rounded to a specific number of decimal places. The default is 2.
        /// </summary>
        /// <param name="references">The list of references.</param>
        /// <param name="candidate">The candidate sentence.</param>
        /// <param name="decimalPlaces">The number of decimal places to round to.</param>
        /// <returns>The score as a double.</returns>
        public double RoundedScore(List<string> references, string candidate, int decimalPlaces=2)
        {
            return Math.Round(Score(references, candidate), decimalPlaces);
        }

        /// <summary>
        /// Calculates the BLEU score rounded to a specific number of decimal places. The default is 2.
        /// </summary>
        /// <param name="references">The list of references.</param>
        /// <param name="candidate">The candidate sentence.</param>
        /// <param name="weights">The list of weights.</param>
        /// <param name="decimalPlaces">The number of decimal places to round to.</param>
        /// <returns>The score as a double.</returns>
        public double RoundedScore(List<string> references, string candidate, List<double> weights, int decimalPlaces=2)
        {
            return Math.Round(Score(references, candidate, weights), decimalPlaces);
        }

        /// <summary>
        /// Gets a list of N weights.
        /// </summary>
        /// <param name="N">The number of weights</param>
        /// <returns>A list of N weights.</returns>
        private List<double> GetDefaultWeights(int N=4)
        {
            var weights = new List<double>();
            for (int i = 0; i < N; i++)
            {
                weights.Add(1.0);
            }

            return weights;
        }

        private double Ln(double value)
        {
            return Math.Log(value, Math.E);
        }
        
        /// <summary>
        /// Gets the Modified N-Gram precision score.
        /// </summary>
        /// <param name="references">The collection of reference sentences.</param>
        /// <param name="candidate">The MT candidate.</param>
        /// <param name="grams">The number of grams (default is 2).</param>
        /// <returns>The precision as a double.</returns>
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

        /// <summary>
        /// Gets the Modified N-Gram precision.
        /// </summary>
        /// <param name="reference">The reference sentence.</param>
        /// <param name="candidate">The candidate sentence.</param>
        /// <param name="grams">The number of grams (default is 2).</param>
        /// <returns>The score as a double.</returns>
        public double ModifiedNGramPrecision(string reference, string candidate, int grams = 2)
        {
            var collection = new List<string> {reference};
            return ModifiedNGramPrecision(collection, candidate, grams);
        }

        /// <summary>
        /// Gets the Modified N-Gram precision.
        /// </summary>
        /// <param name="references">The reference sentences.</param>
        /// <param name="candidate">The candidate sentence.</param>
        /// <returns>The score as a double.</returns>
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
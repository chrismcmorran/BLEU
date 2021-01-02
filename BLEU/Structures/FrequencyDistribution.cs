using System.Collections.Generic;
using System.Linq;

namespace BLEU.Structures
{
    /// <summary>
    /// Defines a simple Frequency Distribution which can be used to represent any label type.
    /// </summary>
    /// <typeparam name="T">The label type.</typeparam>
    public class FrequencyDistribution<T> : Dictionary<T, int>
    {
        /// <summary>
        /// Creates a new frequency distribution from the input collection.
        /// </summary>
        /// <param name="input">The enumerable input.</param>
        public FrequencyDistribution(IEnumerable<T> input)
        {
            foreach (var key in input)
            {
                if (this.ContainsKey(key))
                {
                    this[key] += 1;
                }
                else
                {
                    this[key] = 1;
                }
            }
        }
        
        /// <summary>
        /// Gets the label (key) which has the highest frequency in the frequency distribution.
        /// </summary>
        /// <returns>A key.</returns>
        public T MostFrequentLabel()
        {
            return this.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }
        
        /// <summary>
        /// Gets the total count for the label that has the highest frequency.
        /// </summary>
        /// <returns>An int.</returns>
        public int MostFrequentValue()
        {
            return this[MostFrequentLabel()];
        }

        public int FrequencyOf(T label)
        {
            return this.ContainsKey(label) ? this[label] : 0;
        }
    }
}
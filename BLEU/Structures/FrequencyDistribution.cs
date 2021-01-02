using System.Collections.Generic;
using System.Linq;

namespace BLEU.Structures
{
    public class FrequencyDistribution<T> : Dictionary<T, int>
    {
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
        public T MostFrequent()
        {
            return this.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }
        
        public int MostFrequentValue()
        {
            return this[MostFrequent()];
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BLEU.Collectors;

namespace BLEU.Extensions
{
    public static class ArrayExtensions
    {
        public static double AverageValue(this double[] array)
        {
            if (array == null || array.Length < 1)
            {
                return 0;
            }
            
            return array.Sum() / array.Length;
        }


        public static List<ICollector<string>> ToCollectors(this ICollection<string> collection, int grams=1)
        {
            var list = new List<ICollector<string>>(collection.Count);
            list.AddRange(collection.Select(reference => new NGramCollector(reference, grams)));
            return list;
        }
    }
}
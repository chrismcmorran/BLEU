using System;
using System.Linq;

namespace BLEU
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
    }
}
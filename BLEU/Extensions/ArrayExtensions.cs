using System;
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
        
        /// <summary>
        /// Calculates the Geometric Mean of the array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The geometric mean as a double.</returns>
        public static double GeometricMean(this double[] array)
        {
            if (array == null || array.Length < 1)
            {
                return 0;
            }

            var product = array.Product();
            return NthRoot(product, array.Length);
        }
        
        /// <summary>
        /// Calculates the Geometric Mean of the array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The geometric mean as a double.</returns>
        public static double GeometricMean(this int[] array)
        {
            if (array == null || array.Length < 1)
            {
                return 0;
            }

            var product = array.Product();
            return NthRoot(product, array.Length);
        }
        
        /// <summary>
        /// Calculates the Geometric Mean of the array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The geometric mean as a double.</returns>
        public static double GeometricMean(this float[] array)
        {
            if (array == null || array.Length < 1)
            {
                return 0;
            }

            var product = array.Product();
            return NthRoot(product, array.Length);
        }
        
        private static double NthRoot(double A, int N)
        {
            return Math.Pow(A, 1.0 / N);
        }
        
        public static double Product(this double[] array)
        {
            if (array == null || array.Length < 1)
            {
                return 0;
            }

            double product = 1.0;
            foreach (var item in array)
            {
                product *= item;
            }

            return product;
        }
        
        public static double Product(this int[] array)
        {
            if (array == null || array.Length < 1)
            {
                return 0;
            }

            int product = 1;
            foreach (var item in array)
            {
                product *= item;
            }

            return product;
        }
        
        public static double Product(this float[] array)
        {
            if (array == null || array.Length < 1)
            {
                return 0;
            }

            float product = 1.0f;
            foreach (var item in array)
            {
                product *= item;
            }

            return product;
        }


        public static List<ICollector<string>> ToCollectors(this ICollection<string> collection, int grams=1)
        {
            var list = new List<ICollector<string>>(collection.Count);
            list.AddRange(collection.Select(reference => new NGramCollector(reference, grams)));
            return list;
        }
    }
}
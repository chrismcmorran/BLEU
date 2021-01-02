using System;
using System.Linq;

namespace BLEU
{
    public static class StringExtensions
    {
        public static string RemovePunctuation(this string str)
        {
            return new string(str.Where(c => !char.IsPunctuation(c)).ToArray());
        }
        
        public static string RemoveDigits(this string str)
        {
            return new string(str.Where(c => !char.IsDigit(c)).ToArray());
        }
        
        public static string Remove(this string str, Func<char, bool> predicate)
        {
            return new string(str.Where(predicate).ToArray());
        }
    }
}
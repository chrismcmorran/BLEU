using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLEU
{
    public enum Casing
    {
        Upper,
        Lower,
        Default
    }

    public class NGramCollector : ICollector<string>
    {
        public int Grams { get; set; }
        public string Word { get; set; }

        public Casing UseCase { get; set; }
        
        public string Separator { get; set; }
        
        public bool RemovePunctuation { get; set; }
        
        public bool RemoveDigits { get; set; }
        
        public NGramCollector(string word="", int grams=1, Casing useCase=Casing.Lower, string separator=" ", bool stripPunctuation=false, bool removeDigits=false)
        {
            Grams = grams;
            Word = word;
            UseCase = useCase;
            Separator = separator;
            RemovePunctuation = stripPunctuation;
            RemoveDigits = removeDigits;
        }

        public string[] Collect()
        {
            var parts = GetParts();
            var result = new string[parts.Length - Grams + 1];
            for (int i = 0; i < parts.Length - Grams + 1; i++)
            {
                var sb = new StringBuilder();
                for (int k = 0; k < Grams; k++)
                {
                    if (k > 0) sb.Append(' ');
                    sb.Append(parts[i + k]);
                }

                result[i] = sb.ToString();
            }

            return result;
        }

        private string[] GetParts()
        {
            string[] parts;

            var word = Word;
            
            if (RemovePunctuation)
            {
                word = word.RemovePunctuation();
            }

            if (RemoveDigits)
            {
                word = word.RemoveDigits();
            }
            
            if (this.UseCase == Casing.Lower)
            {
                parts = word.ToLower().Split(" ");
            }
            else if (this.UseCase == Casing.Upper)
            {
                parts = word.ToUpper().Split(" ");
            }
            else
            {
                parts = word.Split(" ");
            }
            
            
            return parts;
        }
    }
}
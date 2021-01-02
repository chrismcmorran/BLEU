using System.Text;
using BLEU.Extensions;

namespace BLEU.Collectors
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
        
        public NGramCollector(string word="", int grams=1, Casing useCase=Casing.Lower, string separator=" ", bool stripPunctuation=true, bool removeDigits=true)
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

        public int Size()
        {
            return GetParts().Length;
        }

        private string[] GetParts()
        {
            var word = Word;
            
            if (RemovePunctuation)
            {
                word = word.RemovePunctuation();
            }

            if (RemoveDigits)
            {
                word = word.RemoveDigits();
            }

            var parts = this.UseCase switch
            {
                Casing.Lower => word.ToLower().Split(Separator),
                Casing.Upper => word.ToUpper().Split(Separator),
                _ => word.Split(Separator)
            };

            return parts;
        }
    }
}
namespace BLEU.Collectors
{
    public class BiGramCollector : NGramCollector
    {
        public BiGramCollector(string word) : base(word, 2)
        {
            
        }
    }
}
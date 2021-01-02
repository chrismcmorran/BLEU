namespace BLEU.Collectors
{
    public interface ICollector<T>
    {
        T[] Collect();

        int Size();
    }
}
namespace BLEU
{
    public interface ICollector<T>
    {
        T[] Collect();
    }
}
namespace AnimalsEscape
{
    public interface IFactory<out T>
    {
        T Create();
    }
}

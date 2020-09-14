namespace Example.Interfaces
{
    [InjectableInterface]
    public interface ICustomLogger
    {
        void Log(string message);
    }
}
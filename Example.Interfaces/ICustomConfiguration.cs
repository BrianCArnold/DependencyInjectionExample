namespace Example.Interfaces
{
    [InjectableInterface]
    public interface ICustomConfiguration
    {
        string DefaultFileAccessDirectory { get; set; }
        
    }
}
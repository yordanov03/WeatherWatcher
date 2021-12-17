namespace WeatherWatcher.Api.Factories
{
    public interface IFactory<T>
    {
        T Build();
    }
}

public interface IObserver
{
    void Update();
}

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

public class WeatherData : ISubject
{
    private List<IObserver> observers;
    private float temperature;
    private float humidity;
    private float pressure;

    public WeatherData()
    {
        observers = new List<IObserver>();
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update();
        }
    }

    public float GetTemperature()
    {
        return temperature;
    }

    public float GetHumidity()
    {
        return humidity;
    }

    public float GetPressure()
    {
        return pressure;
    }

    public void SetMeasurements(float temperature, float humidity, float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;
        NotifyObservers();
    }
}

public class CurrentConditionsDisplay : IObserver
{
    private WeatherData weatherData;

    public CurrentConditionsDisplay(WeatherData weatherData)
    {
        this.weatherData = weatherData;
        weatherData.RegisterObserver(this);
    }

    public void Update()
    {
        float temperature = weatherData.GetTemperature();
        float humidity = weatherData.GetHumidity();
        Display(temperature, humidity);
    }

    public void Display(float temperature, float humidity)
    {
        Console.WriteLine($"Current conditions: {temperature}F degrees and {humidity}% humidity");
    }
}

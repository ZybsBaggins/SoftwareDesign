// Observer interface
public interface IObserver
{
    void Update(float temperature, float humidity, float pressure);
}

// Subject interface
public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

// Concrete Subject
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
            observer.Update(temperature, humidity, pressure);
        }
    }

    public void SetMeasurements(float temperature, float humidity, float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;
        NotifyObservers();
    }
}

// Concrete Observer
public class CurrentConditionsDisplay : IObserver
{
    private float temperature;
    private float humidity;
    private ISubject weatherData;

    public CurrentConditionsDisplay(ISubject weatherData)
    {
        this.weatherData = weatherData;
        weatherData.RegisterObserver(this);
    }

    public void Update(float temperature, float humidity, float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        Display();
    }

    public void Display()
    {
        Console.WriteLine($"Current conditions: {temperature}F degrees and {humidity}% humidity");
    }
}

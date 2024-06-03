// Data Access Layer
public class DataAccessLayer
{
    public List<string> GetData()
    {
        // Simulate data access
        return new List<string> { "Data1", "Data2", "Data3" };
    }
}

// Business Logic Layer
public class BusinessLogicLayer
{
    private readonly DataAccessLayer _dataAccessLayer;

    public BusinessLogicLayer()
    {
        _dataAccessLayer = new DataAccessLayer();
    }

    public List<string> ProcessData()
    {
        var data = _dataAccessLayer.GetData();
        // Simulate business logic processing
        return data.Select(d => d.ToUpper()).ToList();
    }
}

// Presentation Layer
public class PresentationLayer
{
    private readonly BusinessLogicLayer _businessLogicLayer;

    public PresentationLayer()
    {
        _businessLogicLayer = new BusinessLogicLayer();
    }

    public void DisplayData()
    {
        var processedData = _businessLogicLayer.ProcessData();
        foreach (var data in processedData)
        {
            Console.WriteLine(data);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var presentationLayer = new PresentationLayer();
        presentationLayer.DisplayData();
    }
}

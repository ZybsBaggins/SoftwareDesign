// Product interface
public interface IDocument
{
    void Open();
    void Save();
}

// Concrete Products
public class WordDocument : IDocument
{
    public void Open() => Console.WriteLine("Opening Word document...");
    public void Save() => Console.WriteLine("Saving Word document...");
}

public class PDFDocument : IDocument
{
    public void Open() => Console.WriteLine("Opening PDF document...");
    public void Save() => Console.WriteLine("Saving PDF document...");
}

// Creator abstract class
public abstract class DocumentFactory
{
    public abstract IDocument CreateDocument();

    public void OpenDocument()
    {
        var document = CreateDocument();
        document.Open();
    }

    public void SaveDocument()
    {
        var document = CreateDocument();
        document.Save();
    }
}

// Concrete Creators
public class WordDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument() => new WordDocument();
}

public class PDFDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument() => new PDFDocument();
}

// Client code
class Program
{
    static void Main(string[] args)
    {
        DocumentFactory factory = new WordDocumentFactory();
        factory.OpenDocument();
        factory.SaveDocument();

        factory = new PDFDocumentFactory();
        factory.OpenDocument();
        factory.SaveDocument();
    }
}

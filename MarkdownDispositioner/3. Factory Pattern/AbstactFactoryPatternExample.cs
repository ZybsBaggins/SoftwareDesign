// Abstract Product Interfaces
public interface IButton
{
    void Render();
}

public interface ICheckbox
{
    void Render();
}

// Concrete Products for Dark Theme
public class DarkButton : IButton
{
    public void Render() => Console.WriteLine("Rendering dark button...");
}

public class DarkCheckbox : ICheckbox
{
    public void Render() => Console.WriteLine("Rendering dark checkbox...");
}

// Concrete Products for Light Theme
public class LightButton : IButton
{
    public void Render() => Console.WriteLine("Rendering light button...");
}

public class LightCheckbox : ICheckbox
{
    public void Render() => Console.WriteLine("Rendering light checkbox...");
}

// Abstract Factory Interface
public interface IUIFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}

// Concrete Factories
public class DarkThemeFactory : IUIFactory
{
    public IButton CreateButton() => new DarkButton();
    public ICheckbox CreateCheckbox() => new DarkCheckbox();
}

public class LightThemeFactory : IUIFactory
{
    public IButton CreateButton() => new LightButton();
    public ICheckbox CreateCheckbox() => new LightCheckbox();
}

// Client code
class Program
{
    static void Main(string[] args)
    {
        IUIFactory factory = new DarkThemeFactory();
        IButton button = factory.CreateButton();
        ICheckbox checkbox = factory.CreateCheckbox();
        button.Render();
        checkbox.Render();

        factory = new LightThemeFactory();
        button = factory.CreateButton();
        checkbox = factory.CreateCheckbox();
        button.Render();
        checkbox.Render();
    }
}

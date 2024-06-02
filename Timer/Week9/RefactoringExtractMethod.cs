using System;

class Customer
{
    private string _name;

    public Customer(string name)
    {
        _name = name;
    }

    public void PrintOwing(double amount)
    {
        PrintBanner();
        PrintDetails(amount);
    }

    private void PrintBanner()
    {
        Console.WriteLine("**************************");
        Console.WriteLine("**** Customer Owes ******");
        Console.WriteLine("**************************");
    }

    private void PrintDetails(double amount)
    {
        Console.WriteLine($"name: {_name}");
        Console.WriteLine($"amount: {amount}");
    }
}

class Program
{
    static void Main()
    {
        Customer customer = new Customer("John Doe");
        customer.PrintOwing(150.0);
    }
}

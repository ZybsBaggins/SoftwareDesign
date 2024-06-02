using System;

public class Address
{
    public string Street { get; }
    public string City { get; }
    public string ZipCode { get; }

    public Address(string street, string city, string zipCode)
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Address other = (Address)obj;
        return Street == other.Street && City == other.City && ZipCode == other.ZipCode;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, City, ZipCode);
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {ZipCode}";
    }
}

public class Customer
{
    public string Id { get; }
    public string Name { get; }
    public Address Address { get; }

    public Customer(string id, string name, Address address)
    {
        Id = id;
        Name = name;
        Address = address;
    }
}

class Program
{
    static void Main()
    {
        Address address1 = new Address("123 Main St", "Springfield", "12345");
        Address address2 = new Address("456 Elm St", "Shelbyville", "67890");

        Customer customer1 = new Customer("001", "John Doe", address1);
        Customer customer2 = new Customer("002", "Jane Smith", address2);

        Console.WriteLine($"Customer 1: {customer1.Name}, Address: {customer1.Address}");
        Console.WriteLine($"Customer 2: {customer2.Name}, Address: {customer2.Address}");
    }
}

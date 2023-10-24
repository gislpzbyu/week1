using System;
using System.Collections.Generic;

class Address
{
    public string streetAddress;
    public string city;
    public string stateProvince;
    public string country;

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        this.streetAddress = streetAddress;
        this.city = city;
        this.stateProvince = stateProvince;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.Equals("USA", StringComparison.OrdinalIgnoreCase);
    }

    public string GetFormattedAddress()
    {
        return $"{streetAddress}\n{city}, {stateProvince}\n{country}";
    }
}

class Customer
{
    public string name;
    public Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool IsInUSA()
    {
        return address.IsInUSA();
    }

    public string GetShippingLabel()
    {
        return $"Customer: {name}\n{address.GetFormattedAddress()}";
    }
}

class Product
{
    public string name;
    public string productId;
    public decimal price;
    public int quantity;

    public Product(string name, string productId, decimal price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public decimal CalculateTotalPrice()
    {
        return price * quantity;
    }

    public string GetPackingLabel()
    {
        return $"Product: {name} (ID: {productId})";
    }
}

class Order
{
    public List<Product> products;
    public Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public decimal CalculateTotalCost()
    {
        decimal totalCost = 0;
        foreach (var product in products)
        {
            totalCost += product.CalculateTotalPrice();
        }
        totalCost += customer.IsInUSA() ? 5 : 35; // Shipping cost
        return totalCost;
    }

    public string GetPackingLabel()
    {
        string packingLabel = $"Packing Label:\n";
        foreach (var product in products)
        {
            packingLabel += product.GetPackingLabel() + "\n";
        }
        return packingLabel;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{customer.GetShippingLabel()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Address usaAddress = new Address("123 Main St", "Anytown", "CA", "USA");
        Customer usCustomer = new Customer("John Doe", usaAddress);

        Product product1 = new Product("Widget", "W123", 10.00m, 2);
        Product product2 = new Product("Gadget", "G456", 20.00m, 1);

        Order order1 = new Order(usCustomer);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Address canadaAddress = new Address("456 Elm St", "Other City", "ON", "Canada");
        Customer canadaCustomer = new Customer("Jane Smith", canadaAddress);

        Product product3 = new Product("Doohickey", "D789", 15.00m, 3);

        Order order2 = new Order(canadaCustomer);
        order2.AddProduct(product3);

        Console.WriteLine("Order 1:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.CalculateTotalCost():F2}\n");

        Console.WriteLine("Order 2:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.CalculateTotalCost():F2}");
    }
}

using System.Text.Json;

namespace BikeRental;

internal class CustomerRegistry
{
    private readonly List<Customer> customers = new();
    private static readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };

    public CustomerRegistry()
    {
        string Jsonfile = File.ReadAllText("Customers.json");

        customers = JsonSerializer.Deserialize<List<Customer>>(Jsonfile) ?? new List<Customer>();
    }

    public bool AddCustomer(Customer customer)
    {
        if (customers.Any(c => c.Name.Equals(customer.Name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"Kunden med namnet '{customer.Name}' finns redan.");
            return false;
        }
        customers.Add(customer);
        SaveJson(customers);
        return true;
    }

    public Customer? GetCustomerByName(string name)
    {
        return customers.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    static void SaveJson(List<Customer> customersToSave)
    {
        string jsonText = JsonSerializer.Serialize(customersToSave, options);

        File.WriteAllText("Customers.json", jsonText);
    }
}

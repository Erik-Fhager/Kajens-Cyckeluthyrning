namespace BikeRental;

public class Customer
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public bool Membership { get; set; }

    public Customer(string name, int age, bool membership)
    {
        Name = name;
        Age = age;
        Membership = membership;
    }
}
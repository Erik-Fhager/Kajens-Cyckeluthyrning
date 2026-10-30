namespace BikeRental;

public class Rental
{
    public Customer Customer { get; set; }
    public Bike Bike { get; set; }
    public int RentalDuration { get; set; }

    public Rental(Customer customer, Bike bike, int rentalDuration)
    {
        if (rentalDuration <= 0)
        {
            throw new ArgumentException("Rental duration must be a positive number.");
        }
        Customer = customer;
        Bike = bike;
        RentalDuration = rentalDuration;
    }
    public decimal Cost()
    {
        return Bike.CalculateRentalCost(RentalDuration);
    }
    public decimal Discount()
    {
        if (Customer.Membership)
        {
            return Cost() * 0.9m; // 10% rabbat för medlemmar
        }
        return Cost();
    }
    public decimal TotalCost()
    {
        return Discount();
    }
    public void Receipt()
    {
        Console.WriteLine("----- Kvitto -----");
        Console.WriteLine($"{Bike.GetBike()}, {RentalDuration} timmar, {Cost()}Kr");
        if (Customer.Membership)
        {
            Console.WriteLine($"Medlemsrabatt (10%): {Cost() - Discount()}Kr");
        }
        Console.WriteLine($"Att betala: {TotalCost():C}");
    }
}

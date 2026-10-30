namespace BikeRental;

public class Bike
{
    public int Id { get; set; }
    public decimal Priceperhour { get; set; }
    public bool IsAvailable { get; set; }

    public Bike(int id, decimal priceperhour)
    {
        Id = id;
        Priceperhour = priceperhour;
        IsAvailable = true;
    }

    public virtual string GetBike()
    {
        return "Cykel";
    }

    public void RentBike()
    {
        if (IsAvailable)
        {
            IsAvailable = false;
            Console.WriteLine($"Cykel {Id} har hyrts.");
        }
        else
        {
            Console.WriteLine($"Cykel {Id} är inte tillgänglig för uthyrning.");
        }
    }

    public void ReturnBike()
    {
        if (!IsAvailable)
        {
            IsAvailable = true;
            Console.WriteLine($"Cykel {Id} har lämnats tillbaka.");
        }
        else
        {
            Console.WriteLine($"Cykel {Id} har inte hyrts.");
        }
    }

    public decimal CalculateRentalCost(int hours)
    {
        return Priceperhour * hours;
    }
}
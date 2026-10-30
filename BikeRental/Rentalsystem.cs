namespace BikeRental;

public class Rentalsystem
{
    private readonly List<Bike> bikes = new();
    private readonly List<Rental> rentals = new();

    public Rentalsystem()
    {
        // Lägg till exempelcyklar
        bikes.Add(new Bike(1, 40.0m));
        bikes.Add(new Bike(2, 40.0m));
        bikes.Add(new EBike(3, 70.0m, 80));
        bikes.Add(new EBike(4, 70.0m, 60));
    }

    public void ShowBikes()
    {
        Console.WriteLine();
        Console.WriteLine("{0,-5}{1,-12}{2,-10}{3,-10}{4,-10}",
            "Id:", "Typ:", "Status:", "Pris:", "Batteri:");

        foreach (Bike bike in bikes)
        {
            string status = bike.IsAvailable ? "Ledig" : "Uthyrd";

            string battery = bike is EBike electricBike ? $"{electricBike.BatteryCapacity} %" : "";

            Console.WriteLine( "{0,-5}{1,-12}{2,-10}{3,-10}{4,-10}",
                bike.Id, bike.GetBike(), status, bike.Priceperhour,battery);
        }
    }

    // Hämta cykeltypen med hjälp av ID
    public string getBikeType(int bikeId)
    {
        Bike? bike = bikes.FirstOrDefault(b => b.Id == bikeId);

        if (bike == null)
        {
            return "Okänd cykel";
        }

        return bike.GetBike();
    }

    // Hyr en cykel med kundnamn och cykel-ID
    public void RentBike(Customer customer, int bikeId)
    {
        Bike? bike = bikes.FirstOrDefault(b => b.Id == bikeId);

        if (bike == null)
        {
            Console.WriteLine("Cykeln hittades inte.");
            return;
        }

        if (!bike.IsAvailable)
        {
            Console.WriteLine("Cykeln är redan uthyrd.");
            return;
        }

        // Kontrollera ålder och batteri för elcykel
        if (bike is EBike electricBike)
        {
            if (customer.Age < 18)
            {
                Console.WriteLine("Kunden måste vara minst 18 år för att hyra en elcykel.");
                return;
            }

            if (electricBike.BatteryCapacity <= 30)
            {
                Console.WriteLine("Elcykeln kan inte hyras eftersom batteriet är 30 % eller lägre.");
                return;
            }
        }

        Console.Write("Ange antal timmar: ");

        if (!int.TryParse(Console.ReadLine(), out int rentalDuration) || rentalDuration <= 0)
        {
            Console.WriteLine("Ange ett giltigt antal timmar.");
            return;
        }

        // Kontrollera batteriet innan uthyrningen godkänns
        if (bike is EBike electricBikeCheck)
        {
            int batteryNeeded = rentalDuration * 10;

            if (electricBikeCheck.BatteryCapacity - batteryNeeded < 0)
            {
                Console.WriteLine("Batteriet räcker inte för hela uthyrningstiden.");
                return;
            }

            // Minska batteriet med 10 % per uthyrd timme
            electricBikeCheck.BatteryCapacity -= batteryNeeded;
        }

        // Skapa uthyrningen
        Rental rental = new Rental(customer, bike, rentalDuration);
        rentals.Add(rental);

        // Markera cykeln som uthyrd
        bike.RentBike();

        Console.WriteLine($"{customer.Name} har hyrt {getBikeType(bikeId)} " + $"för {rentalDuration} timmar.");
    }

    // Lämna tillbaka cykel funktion
    public void ReturnBike()
    {
        Console.Write("Ange cykelns ID: ");

        if (!int.TryParse(Console.ReadLine(), out int bikeTypeId))
        {
            Console.WriteLine("Ogiltigt cykel-ID.");
            return;
        }

        Bike? bike = bikes.FirstOrDefault(b => b.Id == bikeTypeId);

        if (bike == null)
        {
            Console.WriteLine("Cykeln hittades inte.");
            return;
        }

        if (bike.IsAvailable)
        {
            Console.WriteLine("Cykeln är inte uthyrd.");
            return;
        }

        Rental? rental = rentals.FirstOrDefault(r => r.Bike.Id == bikeTypeId);

        if (rental == null)
        {
            Console.WriteLine("Ingen uthyrning hittades för denna cykel.");
            return;
        }

        // Skriv ut kvittot
        rental.Receipt();
        // Markera cykeln som ledig
        bike.ReturnBike();
        // Ta bort den avslutade uthyrningen
        rentals.Remove(rental);
        ShowBikes();
    }

    // Ladda elcykel funktionen
    public void Charge()
    {
        Console.Write("Ange elcykelns ID: ");

        if (!int.TryParse(Console.ReadLine(), out int bikeTypeId))
        {
            Console.WriteLine("Ogiltigt cykel-ID.");
            return;
        }

        Bike? bike = bikes.FirstOrDefault(b => b.Id == bikeTypeId);

        if (bike == null)
        {
            Console.WriteLine("Cykeln hittades inte.");
            return;
        }

        if (bike is not EBike electricBike)
        {
            Console.WriteLine("Den valda cykeln är inte en elcykel.");
            return;
        }

        if (!bike.IsAvailable)
        {
            Console.WriteLine("Du kan inte ladda en elcykel som är uthyrd.");
            return;
        }

        electricBike.BatteryCapacity = 100;

        Console.WriteLine("Elcykeln är nu fulladdad!");

        ShowBikes();
    }
}
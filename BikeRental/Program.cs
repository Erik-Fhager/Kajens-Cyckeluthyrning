using BikeRental;

Rentalsystem system = new();
CustomerRegistry customerRegistry = new();

bool running = true;
while (running)
{
    Console.WriteLine();
    Console.WriteLine("=== Kajens Cykeluthyrning ===");
    Console.WriteLine("1. Visa fordon");
    Console.WriteLine("2. Registrera kund");
    Console.WriteLine("3. Hyr ut");
    Console.WriteLine("4. Lämna tillbaka");
    Console.WriteLine("5. Ladda");
    Console.WriteLine("6. Avsluta");
    Console.Write("Val: ");

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Ogiltigt val. Försök igen.");
        continue;
    }

    if (choice == 1)
    {
        system.ShowBikes();
    }

    else if (choice == 2)
    {
        Console.WriteLine("Namn:");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Namnet får inte vara tomt.");
            continue;
        }
        Console.WriteLine("Ålder:");

        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Ogiltig ålder. Försök igen.");
            continue;
        }
        else if (age < 0)
        {
            Console.WriteLine("Åldern kan inte vara negativ. Försök igen.");
            continue;
        }

        Console.WriteLine("Medlemskap (ja/nej):");
        string? answer = Console.ReadLine()?.Trim().ToLower();

        bool membership;

        if (answer == "ja")
        {
            membership = true;
        }
        else if (answer == "nej")
        {
            membership = false;
        }
        else
        {
            Console.WriteLine("Ogiltigt svar. Svara ja eller nej.");
            continue;
        }

        var customer = new Customer(name, age, membership);
        if (customerRegistry.AddCustomer(customer))
        {
            Console.WriteLine($"Kund {name} registrerad och sparad.");
        }
    }

    else if (choice == 3)
    {
        Console.Write("Ange kundens namn: ");
        string customerName = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(customerName))
        {
            Console.WriteLine("Kundnamnet får inte vara tomt.");
            continue;
        }

        Customer? customer = customerRegistry.GetCustomerByName(customerName);
        if (customer == null)
        {
            Console.WriteLine("Kunden hittades inte.");
            continue;
        }

        system.ShowBikes();

        Console.Write("Ange cykelns ID: ");

        if (!int.TryParse(Console.ReadLine(), out int bikeTypeId))
        {
            Console.WriteLine("Ogiltigt cykel-ID.");
            continue;
        }

        // Hyr cykeln, ange hyrestid
        system.RentBike(customer, bikeTypeId);
    }

    else if (choice == 4)
    {
        system.ReturnBike();
    }

    else if (choice == 5)
    {
        system.Charge();
    }

    else if (choice == 6)
    {
        running = false;
        Console.WriteLine("Tack för besöket!");
    }

    else
    {
        Console.WriteLine("Ogiltigt val. Försök igen.");
    }
}

namespace BikeRental;

public class EBike : Bike
{
    public int BatteryCapacity { get; set; }
    public override string GetBike()
    {
        return "El-cykel";
    }
    public EBike(int id, decimal priceperhour, int batteryCapacity)
        : base(id, priceperhour)
    {
        if (batteryCapacity < 0 || batteryCapacity > 100)
        { 
            throw new ArgumentException("Batterinivå måste vara mellan 0 och 100.");
        }
        BatteryCapacity = batteryCapacity;
    }
    public void ChargeBattery(int procent)
    {
        if (procent <= 0)
        {
            Console.WriteLine("Laddningsbeloppet måste vara positivt.");
            return;
        }
        BatteryCapacity = Math.Min(100, BatteryCapacity + procent);
    }
}

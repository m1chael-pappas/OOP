public class Car
{

    private double fuelEfficiency;
    private double fuelInTank;
    private double totalMilesDriven;

    private const double FUEL_COST_PER_LITRE = 1.385;

    // Constructor
    public Car(double mpg)
    {
        fuelEfficiency = mpg;
        fuelInTank = 0;
        totalMilesDriven = 0;
    }

    // Accessor methods
    public double getFuel()
    {
        return fuelInTank;
    }
    public double getTotalMiles()
    {
        return totalMilesDriven;
    }

    // Mutator methods
    public void setTotalMiles(double miles)
    {
        totalMilesDriven = miles;
    }

    // Methods
    public string printFuelCost()
    {
        return FUEL_COST_PER_LITRE.ToString("$#,##0.00");
    }

    public void addFuel(double litres)
    {
        fuelInTank += litres;
        double cost = calcCost(litres);
        Console.WriteLine($"Added {litres:F2} litres of fuel.");
        Console.WriteLine($"Cost of fuel added: {cost:$#,##0.00}");
        Console.WriteLine($"Total fuel in tank: {fuelInTank:F2} litres");
    }


    public double calcCost(double litres)
    {
        return litres * FUEL_COST_PER_LITRE;
    }


    public double convertToLitres(double gallons)
    {
        return gallons * 4.546;
    }


    public void drive(double miles)
    {

        totalMilesDriven += miles;
        double gallonsUsed = miles / fuelEfficiency;
        double litresUsed = convertToLitres(gallonsUsed);

        fuelInTank -= litresUsed;


        double journeyCost = calcCost(litresUsed);


        Console.WriteLine($"Drove {miles:F2} miles.");
        Console.WriteLine($"Fuel used: {gallonsUsed:F2} gallons ({litresUsed:F2} litres)");
        Console.WriteLine($"Total cost of journey: {journeyCost:$#,##0.00}");
        Console.WriteLine($"Remaining fuel in tank: {fuelInTank:F2} litres");
        Console.WriteLine($"Total miles driven: {totalMilesDriven:F2} miles");
    }


    public void displayStatus()
    {
        Console.WriteLine($"Car Status:");
        Console.WriteLine($"Fuel Efficiency: {fuelEfficiency:F1} mpg");
        Console.WriteLine($"Fuel in Tank: {fuelInTank:F2} litres");
        Console.WriteLine($"Total Miles Driven: {totalMilesDriven:F2} miles");
        Console.WriteLine($"Current Fuel Cost: {printFuelCost()} per litre");
    }
}

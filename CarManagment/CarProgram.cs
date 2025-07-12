

namespace CarManagement
{
    public class CarProgram
    {
        static void Main()
        {
            Console.WriteLine("=== Car Management System ===\n");


            Car myCar = new(30.0);

            Console.WriteLine("Initial Car Status:");
            myCar.displayStatus();
            Console.WriteLine(new string('-', 50));


            Console.WriteLine($"\nTesting getFuel(): {myCar.getFuel():F2} litres");
            Console.WriteLine($"Testing getTotalMiles(): {myCar.getTotalMiles():F2} miles");
            Console.WriteLine($"Testing printFuelCost(): {myCar.printFuelCost()} per litre");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nTesting addFuel() - Adding 50 litres:");
            myCar.addFuel(50.0);
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nTesting calcCost() - Cost of 25 litres: {myCar.calcCost(25.0):$#,##0.00}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nTesting convertToLitres() - 10 gallons = {myCar.convertToLitres(10.0):F2} litres");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nTesting drive() - Driving 150 miles:");
            myCar.drive(150.0);
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nTesting setTotalMiles() - Setting total miles to 200:");
            myCar.setTotalMiles(200.0);
            Console.WriteLine($"Total miles after setting: {myCar.getTotalMiles():F2} miles");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nAdding more fuel and taking another trip:");
            myCar.addFuel(30.0);
            Console.WriteLine();
            myCar.drive(90.0);
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nFinal Car Status:");
            myCar.displayStatus();

            Console.WriteLine($"\n=== Testing Different Scenarios ===\n");
            Car economyCar = new(45.0);
            Car sportsCar = new(20.0);
            Console.WriteLine("Economy Car (45 mpg):");
            economyCar.addFuel(40.0);
            economyCar.drive(100.0);
            Console.WriteLine();

            Console.WriteLine("Sports Car (20 mpg):");
            sportsCar.addFuel(40.0);
            sportsCar.drive(100.0);
            Console.WriteLine();

            Console.WriteLine("Comparison for 100-mile journey:");
            Console.WriteLine($"Economy Car fuel used: {economyCar.convertToLitres(100.0 / 45.0):F2} litres");
            Console.WriteLine($"Sports Car fuel used: {sportsCar.convertToLitres(100.0 / 20.0):F2} litres");
            Console.WriteLine($"Difference in fuel cost: {sportsCar.calcCost(sportsCar.convertToLitres(100.0 / 20.0)) - economyCar.calcCost(economyCar.convertToLitres(100.0 / 45.0)):C}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
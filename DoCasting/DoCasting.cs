

class DoCasting
{
    static void Main()
    {
        Console.WriteLine("Casting  Program");
        Console.WriteLine("=============================");

        int sum = 17;
        int count = 5;

        Console.WriteLine($"Sum = {sum}");
        Console.WriteLine($"Count = {count}");
        Console.WriteLine();

        int intAverage;
        intAverage = sum / count;

        Console.WriteLine("Integer Division:");
        Console.WriteLine($"intAverage = sum / count = {sum} / {count} = {intAverage}");


        double doubleAverage;
        doubleAverage = sum / count;

        Console.WriteLine("Double variable with integer division:");
        Console.WriteLine($"doubleAverage = sum / count = {sum} / {count} = {doubleAverage}");

        doubleAverage = (double)sum / count;

        Console.WriteLine("Casting sum to double:");
        Console.WriteLine($"doubleAverage = (double)sum / count = (double){sum} / {count} = {doubleAverage}");


        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
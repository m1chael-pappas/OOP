using System;

class DivisibleFour
{
    static void Main()
    {
        Console.WriteLine("Numbers Divisible by 4 ");
        Console.WriteLine("===================================");

        try
        {
            Console.Write("Enter the upper bound (n): ");
            int n = Convert.ToInt32(Console.ReadLine());

            if (n <= 0)
            {
                Console.WriteLine("Please enter a positive number.");
                return;
            }

            Console.WriteLine($"\nNumbers between 1 and {n} that are divisible by 4 but NOT by 5:");
            Console.WriteLine("================================================================");

            bool foundNumbers = false;

            for (int i = 1; i <= n; i++)
            {
                if (i % 4 == 0 && i % 5 != 0)
                {
                    Console.WriteLine(i);
                    foundNumbers = true;
                }
            }

            if (!foundNumbers)
            {
                Console.WriteLine("No numbers found that meet the criteria.");
            }

            Console.WriteLine($"\nSearch completed for range 1 to {n}.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Please enter a valid integer.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Error: Number is too large.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
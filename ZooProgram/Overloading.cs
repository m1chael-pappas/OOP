using System;

public class Overloading
{
    public static void MethodToBeOverloaded(string name)
    {
        Console.WriteLine("Name: " + name);
    }

    public static void MethodToBeOverloaded(string name, int age)
    {
        Console.WriteLine("Name: " + name + "\nAge: " + age);
    }

    public static void MethodToBeOverloaded(int number)
    {
        Console.WriteLine("Number: " + number);
    }

    public static void MethodToBeOverloaded(double value)
    {
        Console.WriteLine("Value: " + value);
    }

    public static void MethodToBeOverloaded(string firstName, string lastName)
    {
        Console.WriteLine("Full Name: " + firstName + " " + lastName);
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("=== Method Overloading Demonstration ===\n");

        Console.WriteLine("Calling MethodToBeOverloaded with string:");
        MethodToBeOverloaded("John");

        Console.WriteLine("\nCalling MethodToBeOverloaded with string and int:");
        MethodToBeOverloaded("Alice", 25);

        Console.WriteLine("\nCalling MethodToBeOverloaded with int:");
        MethodToBeOverloaded(42);

        Console.WriteLine("\nCalling MethodToBeOverloaded with double:");
        MethodToBeOverloaded(3.14);

        Console.WriteLine("\nCalling MethodToBeOverloaded with two strings:");
        MethodToBeOverloaded("Jane", "Doe");

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}

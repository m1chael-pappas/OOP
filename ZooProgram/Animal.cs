using System;

public class Animal
{
    private string name;
    private string diet;
    private string location;
    private double weight;
    private int age;
    private string colour;

    public Animal(string name, string diet, string location, double weight, int age, string colour)
    {
        this.name = name;
        this.diet = diet;
        this.location = location;
        this.weight = weight;
        this.age = age;
        this.colour = colour;
    }

    public void eat()
    {
        Console.WriteLine($"{name} is eating {diet}");
    }

    public void sleep()
    {
        Console.WriteLine($"{name} is sleeping peacefully");
    }

    public void makeNoise()
    {
        Console.WriteLine($"{name} makes a generic animal noise");
    }


    public void makeLionNoise()
    {
        Console.WriteLine($"{name} roars loudly: ROAARRR!");
    }

    public void makeEagleNoise()
    {
        Console.WriteLine($"{name} screeches: SCREECH!");
    }

    public void makeWolfNoise()
    {
        Console.WriteLine($"{name} howls: HOWWWWL!");
    }

    public void eatMeat()
    {
        Console.WriteLine($"{name} eats meat hungrily");
    }

    public void eatBerries()
    {
        Console.WriteLine($"{name} pecks at berries");
    }

    public void DisplayInfo()
    {
        Console.WriteLine(
            $"Name: {name}, Diet: {diet}, Location: {location}, Weight: {weight}kg, Age: {age}, Colour: {colour}"
        );
    }
}

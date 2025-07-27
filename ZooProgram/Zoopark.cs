// using System;

// public class ZooPark
// {
//     public static void Main(string[] args)
//     {
//         Console.WriteLine("=== Zoo Park - Without Inheritance ===\n");

//         Animal williamWolf = new Animal("William the Wolf", "Meat", "Dog Village", 50.6, 9, "Grey");
//         Animal tonyTiger = new Animal(
//             "Tony the Tiger",
//             "Meat",
//             "Cat Land",
//             110,
//             6,
//             "Orange and White"
//         );
//         Animal edgarEagle = new Animal("Edgar the Eagle", "Fish", "Bird Mania", 20, 15, "Black");

//         Console.WriteLine("Animal Information:");
//         williamWolf.DisplayInfo();
//         tonyTiger.DisplayInfo();
//         edgarEagle.DisplayInfo();

//         Console.WriteLine("\nAnimal Behaviors:");
//         williamWolf.makeWolfNoise();
//         tonyTiger.makeLionNoise();
//         edgarEagle.makeEagleNoise();

//         williamWolf.eatMeat();
//         tonyTiger.eatMeat();
//         edgarEagle.eatBerries();

//         williamWolf.sleep();
//         tonyTiger.sleep();
//         edgarEagle.sleep();

//         Console.WriteLine("\nPress any key to continue...");
//         Console.ReadKey();
//     }
// }

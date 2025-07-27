namespace ZooProgram
{
    public class ZooParkWithInheritance
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Zoo Park - With Inheritance ===\n");

            Animal baseAnimal = new(
                "Animal Name",
                "Animal Diet",
                "Animal Location",
                0.0,
                0,
                "Animal Colour"
            );

            Tiger tonyTiger = new(
                "Tony the Tiger",
                "Meat",
                "Cat Land",
                110,
                6,
                "Orange and White",
                "Siberian",
                "White"
            );

            Wolf williamWolf = new("William the Wolf", "Meat", "Dog Village", 50.6, 9, "Grey");

            Eagle edgarEagle = new(
                "Edgar the Eagle",
                "Fish",
                "Bird Mania",
                20,
                15,
                "Black",
                "Harpy",
                98.5
            );

            Lion larry = new(
                "Larry the Lion",
                "Meat",
                "Savanna",
                180,
                8,
                "Golden",
                "African Lion",
                "Full Mane"
            );

            Penguin penny = new(
                "Penny the Penguin",
                "Fish",
                "Antarctica",
                25,
                5,
                "Black and White",
                "Emperor",
                76.0
            );

            Console.WriteLine("=== Testing makeNoise() method ===");
            tonyTiger.MakeNoise();
            baseAnimal.MakeNoise();

            Console.WriteLine("\n=== Testing eat() method ===");
            tonyTiger.Eat();
            williamWolf.Eat();
            baseAnimal.Eat();

            Console.WriteLine("\n=== Testing sleep() method ===");
            baseAnimal.Sleep();
            tonyTiger.Sleep();
            williamWolf.Sleep();
            edgarEagle.Sleep();
            tonyTiger.Eat();

            Console.WriteLine("\n=== Testing specific animal behaviors ===");
            williamWolf.MakeNoise();
            edgarEagle.MakeNoise();
            larry.MakeNoise();
            penny.MakeNoise();

            Console.WriteLine("\n=== Testing eating behaviors ===");
            edgarEagle.Eat();
            larry.Eat();
            penny.Eat();

            Console.WriteLine("\n=== Testing unique methods ===");
            Eagle.LayEgg();
            edgarEagle.Fly();

            penny.Fly();
            Penguin.Swim();
            Penguin.Slide();

            Tiger.Pounce();
            Lion.LeadPride();
            Wolf.Hunt();

            Console.WriteLine("\n=== Testing move() method ===");
            baseAnimal.Move();
            tonyTiger.Move();
            williamWolf.Move();
            edgarEagle.Move();
            larry.Move();
            penny.Move();

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}

namespace Polymorphism
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // STEP 1 & 2: Create and test Bird objects
            Console.WriteLine("=== Testing Bird Class ===");
            Bird bird1 = new Bird();
            Bird bird2 = new Bird();

            bird1.name = "Feathers";
            bird2.name = "Polly";

            Console.WriteLine(bird1.ToString());
            bird1.fly();

            Console.WriteLine(bird2.ToString());
            bird2.fly();

            Console.ReadLine();
            Console.WriteLine();

            // STEP 1: Create and test Penguin and Duck objects
            Console.WriteLine("=== Testing Penguin and Duck Classes ===");
            Penguin penguin1 = new Penguin();
            Penguin penguin2 = new Penguin();

            penguin1.name = "Happy Feet";
            penguin2.name = "Gloria";

            Console.WriteLine(penguin1.ToString());
            penguin1.fly();

            Console.WriteLine(penguin2.ToString());
            penguin2.fly();

            Duck duck1 = new Duck();
            Duck duck2 = new Duck();

            duck1.name = "Daffy";
            duck1.size = 15;
            duck1.kind = "Mallard";

            duck2.name = "Donald";
            duck2.size = 20;
            duck2.kind = "Decoy";

            Console.WriteLine(duck1.ToString());
            Console.WriteLine(duck2.ToString());
            Console.WriteLine();

            // STEP 2: Polymorphism demonstration
            Console.WriteLine("=== Polymorphism with List<Bird> ===");
            List<Bird> birds = new List<Bird>();
            Bird bird3 = new Bird();
            bird3.name = "Feathers";
            Bird bird4 = new Bird();
            bird4.name = "Polly";

            Penguin penguin3 = new Penguin();
            penguin3.name = "Happy Feet";
            Penguin penguin4 = new Penguin();
            penguin4.name = "Gloria";

            Duck duck3 = new Duck();
            duck3.name = "Daffy";
            duck3.size = 15;
            duck3.kind = "Mallard";
            Duck duck5 = new Duck();
            duck5.name = "Donald";
            duck5.size = 20;
            duck5.kind = "Decoy";

            birds.Add(bird3);
            birds.Add(bird4);
            birds.Add(penguin3);
            birds.Add(penguin4);
            birds.Add(duck3);
            birds.Add(duck5);

            birds.Add(new Bird() { name = "Birdy" });

            foreach (Bird bird in birds)
            {
                Console.WriteLine(bird);
            }
            Console.WriteLine();

            // STEP 3: Covariance demonstration
            Console.WriteLine("=== Covariance Example ===");

            // Create a List of Duck objects
            Duck duck6 = new Duck();
            duck6.name = "Daffy";
            duck6.size = 15;
            duck6.kind = "Mallard";
            Duck duck7 = new Duck();
            duck7.name = "Donald";
            duck7.size = 20;
            duck7.kind = "Decoy";

            List<Duck> ducksToAdd = new List<Duck>() { duck6, duck7 };

            // Create an IEnumerable of type Bird and assign list of ducks to it
            IEnumerable<Bird> upcastDucks = ducksToAdd;

            // Create a list of Bird objects
            List<Bird> birds2 = new List<Bird>();
            birds2.Add(new Bird() { name = "Feather" });

            // Add the IEnumerable<Bird> collection to the Bird list using AddRange
            birds2.AddRange(upcastDucks);

            // Use loop to display all birds
            Console.WriteLine("Birds list after adding ducks via covariance:");
            foreach (Bird bird in birds2)
            {
                Console.WriteLine(bird);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

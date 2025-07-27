namespace ZooProgram
{
    public class Penguin(
        string name,
        string diet,
        string location,
        double weight,
        int age,
        string colour,
        string species,
        double wingSpan
    ) : Bird(name, diet, location, weight, age, colour, species, wingSpan)
    {
        public override void MakeNoise()
        {
            Console.WriteLine("Penguin squawks: SQUAWK!");
        }

        public override void Eat()
        {
            Console.WriteLine("Penguin eats 2lbs of fish");
        }

        public override void Fly()
        {
            Console.WriteLine("Penguin cannot fly, but waddles on land");
        }

        public static void Swim()
        {
            Console.WriteLine("Penguin swims gracefully underwater");
        }

        public static void Slide()
        {
            Console.WriteLine("Penguin slides on its belly across the ice");
        }

        public override void Move()
        {
            Console.WriteLine("Penguin waddles on land and swims in water");
        }
    }
}

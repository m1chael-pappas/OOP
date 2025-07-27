namespace ZooProgram
{
    public class Wolf(
        string name,
        string diet,
        string location,
        double weight,
        int age,
        string colour
    ) : Animal(name, diet, location, weight, age, colour)
    {
        public override void MakeNoise()
        {
            Console.WriteLine("Wolf howls: HOWWWWL!");
        }

        public override void Eat()
        {
            Console.WriteLine("Wolf eats 10lbs of meat");
        }

        public override void Move()
        {
            Console.WriteLine("Wolf runs with the pack");
        }

        public static void Hunt()
        {
            Console.WriteLine("Wolf hunts in a pack");
        }
    }
}

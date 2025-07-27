namespace ZooProgram
{
    public class Eagle(
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
            Console.WriteLine("Eagle screeches: SCREECH!");
        }

        public override void Eat()
        {
            Console.WriteLine("Eagle eats 1lb of fish");
        }

        public static void LayEgg()
        {
            Console.WriteLine("Eagle lays an egg in its nest");
        }

        public override void Fly()
        {
            Console.WriteLine("Eagle soars high in the sky");
        }

        public override void Move()
        {
            Console.WriteLine("Eagle glides majestically through the air");
        }
    }
}

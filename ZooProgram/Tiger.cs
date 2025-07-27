namespace ZooProgram
{
    public class Tiger(
        string name,
        string diet,
        string location,
        double weight,
        int age,
        string colour,
        string species,
        string colourStripes
    ) : Feline(name, diet, location, weight, age, colour, species)
    {
        private readonly string colourStripes = colourStripes;

        public override void MakeNoise()
        {
            Console.WriteLine("ROARRRRRRR");
        }

        public override void Eat()
        {
            Console.WriteLine("I can eat 20lbs of meat");
        }

        public override void Move()
        {
            Console.WriteLine("Tiger prowls through the jungle");
        }

        public static void Pounce()
        {
            Console.WriteLine("Tiger pounces on its prey");
        }
    }
}

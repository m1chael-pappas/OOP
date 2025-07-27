namespace ZooProgram
{
    public class Lion(
        string name,
        string diet,
        string location,
        double weight,
        int age,
        string colour,
        string species,
        string maneType
    ) : Feline(name, diet, location, weight, age, colour, species)
    {
        private readonly string maneType = maneType;

        public override void MakeNoise()
        {
            Console.WriteLine("Lion roars: ROAAARRR!");
        }

        public override void Eat()
        {
            Console.WriteLine("Lion eats 15lbs of meat");
        }

        public override void Move()
        {
            Console.WriteLine("Lion prowls the savanna");
        }

        public static void LeadPride()
        {
            Console.WriteLine("Lion leads the pride majestically");
        }

        public override void Purr()
        {
            Console.WriteLine("Lion purrs deeply");
        }
    }
}

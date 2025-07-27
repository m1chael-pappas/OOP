namespace ZooProgram
{
    public class Feline(
        string name,
        string diet,
        string location,
        double weight,
        int age,
        string colour,
        string species
    ) : Animal(name, diet, location, weight, age, colour)
    {
        private readonly string species = species;

        public override void Sleep()
        {
            Console.WriteLine("Feline curls up and sleeps like a cat");
        }

        public virtual void Purr()
        {
            Console.WriteLine("Feline purrs contentedly");
        }

        public virtual void Scratch()
        {
            Console.WriteLine("Feline scratches with sharp claws");
        }

        protected string Species
        {
            get { return species; }
        }
    }
}

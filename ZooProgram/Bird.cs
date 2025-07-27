namespace ZooProgram
{
    public class Bird(
        string name,
        string diet,
        string location,
        double weight,
        int age,
        string colour,
        string species,
        double wingSpan
    ) : Animal(name, diet, location, weight, age, colour)
    {
        private readonly string species = species;
        private readonly double wingSpan = wingSpan;

        public virtual void Fly()
        {
            Console.WriteLine("Bird flies through the air");
        }

        public virtual void BuildNest()
        {
            Console.WriteLine("Bird builds a nest");
        }

        public override void Move()
        {
            Console.WriteLine("Bird moves by flying");
        }

        protected string Species
        {
            get { return species; }
        }
        protected double WingSpan
        {
            get { return wingSpan; }
        }
    }
}

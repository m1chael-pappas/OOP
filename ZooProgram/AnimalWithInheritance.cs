namespace ZooProgram
{
    public class Animal(
        string name,
        string diet,
        string location,
        double weight,
        int age,
        string colour
    )
    {
        private readonly string name = name;
        private readonly string diet = diet;
        private readonly string location = location;
        private readonly double weight = weight;
        private readonly int age = age;
        private readonly string colour = colour;

        public virtual void Eat()
        {
            Console.WriteLine("An animal eats");
        }

        public virtual void Sleep()
        {
            Console.WriteLine("An animal sleeps");
        }

        public virtual void MakeNoise()
        {
            Console.WriteLine("An animal makes a noise");
        }

        public virtual void Move()
        {
            Console.WriteLine("An animal moves around");
        }

        public void DisplayInfo()
        {
            Console.WriteLine(
                $"Name: {name}, Diet: {diet}, Location: {location}, Weight: {weight}kg, Age: {age}, Colour: {colour}"
            );
        }

        protected string Name
        {
            get { return name; }
        }
        protected string Diet
        {
            get { return diet; }
        }
        protected string Location
        {
            get { return location; }
        }
        protected double Weight
        {
            get { return weight; }
        }
        protected int Age
        {
            get { return age; }
        }
        protected string Colour
        {
            get { return colour; }
        }
    }
}

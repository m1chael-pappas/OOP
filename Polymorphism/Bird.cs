namespace Polymorphism
{
    public class Bird
    {
        public string? name { get; set; }

        public Bird() { }

        public virtual void fly()
        {
            Console.WriteLine("Flap, Flap, Flap");
        }

        public override string ToString()
        {
            return "A bird called " + name;
        }
    }
}

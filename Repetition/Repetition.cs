

class Repetition
{
    public static void Main()
    {
        int sum = 0;
        double average = 0;
        int upperbound = 100;

        int number = 1;
        do
        {
            sum += number;
            number++;
            Console.WriteLine($"Current number: {number}, Current sum: {sum}");
        } while (number <= upperbound);
        if (upperbound > 0)
        {
            average = (double)sum / upperbound;
        }
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
    }
}

class Microwave
{
    static void Main()
    {
        Console.WriteLine("Microwave Heating Time Calculator");
        Console.WriteLine("=================================");

        try
        {

            Console.Write("Enter the number of items to heat: ");
            int numberOfItems = Convert.ToInt32(Console.ReadLine());

            switch (numberOfItems)
            {
                case 1:
                case 2:
                case 3:

                    Console.Write("Enter the heating time for a single item (in seconds): ");
                    int singleItemTime = Convert.ToInt32(Console.ReadLine());


                    double recommendedTime;

                    switch (numberOfItems)
                    {
                        case 1:
                            recommendedTime = singleItemTime;
                            Console.WriteLine($"\nRecommended heating time: {recommendedTime} seconds");
                            break;
                        case 2:
                            recommendedTime = singleItemTime * 1.5; // Add 50%
                            Console.WriteLine($"\nRecommended heating time: {recommendedTime} seconds");
                            Console.WriteLine("(50% extra time added for 2 items)");
                            break;
                        case 3:
                            recommendedTime = singleItemTime * 2; // Double the time
                            Console.WriteLine($"\nRecommended heating time: {recommendedTime} seconds");
                            Console.WriteLine("(Time doubled for 3 items)");
                            break;
                    }
                    break;
                default:
                    if (numberOfItems > 3)
                    {
                        Console.WriteLine("\nWARNING: Heating more than 3 items at once is not recommended!");
                        Console.WriteLine("Please heat items in smaller batches for best results.");
                    }
                    else
                    {
                        Console.WriteLine("\nERROR: Please enter a positive number of items.");
                    }
                    break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("\nERROR: Please enter valid numbers only.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("\nERROR: Number is too large.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nERROR: An unexpected error occurred: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
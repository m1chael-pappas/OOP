
class GuessingNumber
{
    static void Main()
    {
        int targetNumber = 0;
        bool validInput = false;

        Console.WriteLine("User 1: Set the number for User 2 to guess");

        while (!validInput)
        {
            Console.Write("User 1, enter a number between 1 and 10: ");

            try
            {
                targetNumber = Convert.ToInt32(Console.ReadLine());

                if (targetNumber < 1 || targetNumber > 10)
                {
                    Console.WriteLine("Please enter a number between 1 and 10 only!");
                }
                else
                {
                    validInput = true;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a valid number between 1 and 10.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Number too large! Please enter a number between 1 and 10.");
            }
        }

        Console.Clear();
        Console.WriteLine("Number Guessing Game - Two Player Mode");
        Console.WriteLine("=====================================");
        Console.WriteLine("User 1 has set a number between 1 and 10.");
        Console.WriteLine("User 2: Try to guess the number!\n");

        int guess = 0;
        bool correctGuess = false;
        int attempts = 0;

        while (!correctGuess)
        {
            Console.Write("User 2, guess the number between 1 and 10: ");

            try
            {
                guess = Convert.ToInt32(Console.ReadLine());
                attempts++;

                if (guess < 1 || guess > 10)
                {
                    Console.WriteLine("Please enter a number between 1 and 10 only!");
                    attempts--;
                    continue;
                }

                if (guess == targetNumber)
                {
                    Console.WriteLine($"Congratulations User 2! You guessed the number {targetNumber} correctly!");
                    Console.WriteLine($"It took you {attempts} attempt(s) to guess the number.");
                    correctGuess = true;
                }
                else
                {
                    Console.WriteLine($"Wrong! The number {guess} is not correct. Try again!");

                    if (guess < targetNumber)
                    {
                        Console.WriteLine("Hint: Try a higher number.");
                    }
                    else
                    {
                        Console.WriteLine("Hint: Try a lower number.");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a valid number between 1 and 10.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Number too large! Please enter a number between 1 and 10.");
            }
        }
    }

}
using System;

namespace ExceptionHandlingTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exception Handling Demonstration\n");

            // 1. NullReferenceException
            Console.WriteLine("1. Demonstrating NullReferenceException:");
            try
            {
                string nullString = null;
                int length = nullString.Length;
            }
            catch (NullReferenceException exception)
            {
                Console.WriteLine(
                    "The following error detected: "
                        + exception.GetType().ToString()
                        + " with message \""
                        + exception.Message
                        + "\""
                );
            }
            Console.WriteLine();

            // 2. IndexOutOfRangeException
            Console.WriteLine("2. Demonstrating IndexOutOfRangeException:");
            try
            {
                int[] numbers = { 1, 2, 3, 4, 5 };
                int value = numbers[10];
            }
            catch (IndexOutOfRangeException exception)
            {
                Console.WriteLine(
                    "The following error detected: "
                        + exception.GetType().ToString()
                        + " with message \""
                        + exception.Message
                        + "\""
                );
            }
            Console.WriteLine();

            // 3. StackOverflowException
            Console.WriteLine("3. Demonstrating StackOverflowException:");
            Console.WriteLine(
                "Note: StackOverflowException cannot be safely caught in a try-catch block."
            );
            Console.WriteLine(
                "It would cause the program to terminate. Code example provided but not executed."
            );
            // Uncomment the line below.
            // CauseStackOverflow();
            Console.WriteLine();
            // 4. OutOfMemoryException
            Console.WriteLine("4. Demonstrating OutOfMemoryException:");
            Console.WriteLine("Note: OutOfMemoryException is difficult to demonstrate safely.");
            Console.WriteLine("Code example provided but not executed to prevent system issues.");
            // Uncomment the line below.
            // CauseOutOfMemory();
            Console.WriteLine();

            // 5. DivideByZeroException
            Console.WriteLine("5. Demonstrating DivideByZeroException:");
            try
            {
                int numerator = 10;
                int denominator = 0;
                int result = numerator / denominator;
            }
            catch (DivideByZeroException exception)
            {
                Console.WriteLine(
                    "The following error detected: "
                        + exception.GetType().ToString()
                        + " with message \""
                        + exception.Message
                        + "\""
                );
            }
            Console.WriteLine();

            // 6. ArgumentNullException
            Console.WriteLine("6. Demonstrating ArgumentNullException:");
            try
            {
                ProcessString(null);
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine(
                    "The following error detected: "
                        + exception.GetType().ToString()
                        + " with message \""
                        + exception.Message
                        + "\""
                );
            }
            Console.WriteLine();

            // 7. ArgumentOutOfRangeException
            Console.WriteLine("7. Demonstrating ArgumentOutOfRangeException:");
            try
            {
                GetCharacterAt("Hello", 10);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine(
                    "The following error detected: "
                        + exception.GetType().ToString()
                        + " with message \""
                        + exception.Message
                        + "\""
                );
            }
            Console.WriteLine();

            // 8. FormatException
            Console.WriteLine("8. Demonstrating FormatException:");
            try
            {
                string invalidNumber = "not_a_number";
                int number = int.Parse(invalidNumber);
            }
            catch (FormatException exception)
            {
                Console.WriteLine(
                    "The following error detected: "
                        + exception.GetType().ToString()
                        + " with message \""
                        + exception.Message
                        + "\""
                );
            }
            Console.WriteLine();

            // 9. ArgumentException
            Console.WriteLine("9. Demonstrating ArgumentException:");
            try
            {
                CreateRectangle(-5, 10);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(
                    "The following error detected: "
                        + exception.GetType().ToString()
                        + " with message \""
                        + exception.Message
                        + "\""
                );
            }
            Console.WriteLine();

            // 10. SystemException
            Console.WriteLine("10. SystemException:");
            Console.WriteLine("SystemException is a base class for other exceptions.");
            Console.WriteLine(
                "It's typically not thrown directly but can be caught to handle system-level exceptions."
            );
            Console.WriteLine();

            Console.WriteLine("Exception demonstration completed.");
        }

        // Helper method for ArgumentNullException
        static void ProcessString(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "Input string cannot be null");
            }
            Console.WriteLine($"Processing: {input}");
        }

        // Helper method for ArgumentOutOfRangeException
        static char GetCharacterAt(string text, int index)
        {
            if (index < 0 || index >= text.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    $"Index {index} is out of range for string of length {text.Length}"
                );
            }
            return text[index];
        }

        // Helper method for ArgumentException
        static void CreateRectangle(double width, double height)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Width must be positive", nameof(width));
            }
            if (height <= 0)
            {
                throw new ArgumentException("Height must be positive", nameof(height));
            }
            Console.WriteLine($"Rectangle created: {width} x {height}");
        }

        // Dangerous method - would cause StackOverflowException
        static void CauseStackOverflow()
        {
            CauseStackOverflow();
        }

        // Dangerous method - would cause OutOfMemoryException
        static void CauseOutOfMemory()
        {
            int[] hugeArray = new int[int.MaxValue];
        }
    }
}

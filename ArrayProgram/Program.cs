class Program
{
    static bool Palindrome(int[] array)
    {
        int left = 0;
        int right = array.Length - 1;

        while (left < right)
        {
            if (array[left] != array[right])
            {
                return false;
            }
            left++;
            right--;
        }
        return true;
    }

    static bool IsSorted(List<int> list)
    {
        if (list.Count <= 1)
        {
            return true;
        }
        for (int i = 0; i < list.Count - 1; i++)
        {
            if (list[i] > list[i + 1])
            {
                return false;
            }
        }
        return true;
    }

    static List<int>? Merge(List<int> list_a, List<int> list_b)
    {
        if (!IsSorted(list_a) || !IsSorted(list_b))
        {
            return null;
        }
        List<int> mergedList = [];
        int i = 0,
            j = 0;

        while (i < list_a.Count && j < list_b.Count)
        {
            if (list_a[i] < list_b[j])
            {
                mergedList.Add(list_a[i]);
                i++;
            }
            else
            {
                mergedList.Add(list_b[j]);
                j++;
            }
        }

        while (i < list_a.Count)
        {
            mergedList.Add(list_a[i]);
            i++;
        }

        while (j < list_b.Count)
        {
            mergedList.Add(list_b[j]);
            j++;
        }

        return mergedList;
    }

    static int[] ArrayConversion(int[,] array)
    {
        List<int> oddValues = [];
        if (array == null || array.Length == 0)
        {
            return [.. oddValues];
        }
        for (int j = 0; j < array.GetLength(1); j++)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, j] % 2 != 0)
                {
                    oddValues.Add(array[i, j]);
                }
            }
        }

        return oddValues.ToArray();
    }

    static void PrintArray2D(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write($"{array[i, j], 3} ");
            }
            Console.WriteLine();
        }
    }

    static string GetValidString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Please enter a valid name (cannot be empty).");
                continue;
            }

            bool isValidName = true;
            foreach (char c in input)
            {
                if (!char.IsLetter(c) && c != ' ' && c != '-' && c != '\'')
                {
                    isValidName = false;
                    break;
                }
            }

            if (isValidName)
            {
                return input.Trim();
            }
            else
            {
                Console.WriteLine(
                    "Please enter a valid name (letters, spaces, hyphens, and apostrophes only)."
                );
            }
        }
    }

    static double GetValidDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            try
            {
                string input = Console.ReadLine() ?? "";
                return double.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine(
                    "Invalid format! Please enter a valid number (e.g., 5, 3.14, -2.5)."
                );
            }
            catch (OverflowException)
            {
                Console.WriteLine("Number too large! Please enter a smaller number.");
            }
        }
    }

    static void Main(string[] args)
    {
        try
        {
            // -- Task 1 ---
            Console.WriteLine("Task 1 - Array of integers");
            int[] myArray = new int[10];
            for (int i = 0; i < myArray.Length; i++)
            {
                myArray[i] = i;
            }
            int[] studentArray = [87, 68, 94, 100, 83, 78, 85, 91, 76, 87];
            int total = 0;
            for (int i = 0; i < studentArray.Length; i++)
            {
                total += studentArray[i];
            }
            Console.WriteLine($"The total mark for the student is: {total}");
            Console.WriteLine($"This consists of {studentArray.Length} marks.");
            Console.WriteLine($"The average mark is: {total / studentArray.Length}");

            // -- Task 4 ---
            Console.WriteLine("\nTask 4 - Array of student names");
            string[] studentNames = new string[6];
            for (int i = 0; i < studentNames.Length; i++)
            {
                studentNames[i] = GetValidString($"Enter name for student {i + 1}: ");
            }
            Console.WriteLine("The names of the students are:");
            for (int i = 0; i < studentNames.Length; i++)
            {
                Console.WriteLine($"Student {i + 1}: {studentNames[i]}");
            }

            // -- Task 5 ---
            Console.WriteLine("\nTask 5 - Array of doubles");
            double[] newArray = new double[10];
            int currentSize = 0;
            double currentLargest,
                currentSmallest;

            Console.WriteLine("Enter 10 numbers:");

            for (int i = 0; i < newArray.Length; i++)
            {
                double input = GetValidDouble($"Enter number {i + 1}: ");
                newArray[currentSize++] = input;
            }

            currentLargest = newArray[0];
            currentSmallest = newArray[0];

            Console.WriteLine("\nArray contents:");
            for (int i = 0; i < currentSize; i++)
            {
                Console.WriteLine($"Element {i}: {newArray[i]}");

                if (newArray[i] > currentLargest)
                {
                    currentLargest = newArray[i];
                }

                if (newArray[i] < currentSmallest)
                {
                    currentSmallest = newArray[i];
                }
            }

            Console.WriteLine($"\nThe largest number is: {currentLargest}");
            Console.WriteLine($"The smallest number is: {currentSmallest}");

            Console.WriteLine("\nComplete array:");
            Console.Write("[");
            for (int i = 0; i < currentSize; i++)
            {
                Console.Write(newArray[i]);
                if (i < currentSize - 1)
                    Console.Write(", ");
            }
            Console.WriteLine("]");

            // -- Task 6 ---
            Console.WriteLine("\nTask 6 - 2D array of integers");
            int[,] mySecondArray = new int[3, 4]
            {
                { 1, 2, 3, 4 },
                { 1, 1, 1, 1 },
                { 2, 2, 2, 2 },
            };
            for (int i = 0; i < mySecondArray.GetLength(0); i++)
            {
                for (int j = 0; j < mySecondArray.GetLength(1); j++)
                {
                    Console.Write($"{mySecondArray[i, j]} \t");
                }
                Console.WriteLine();
            }

            // -- Task 7 ---
            Console.WriteLine("\nTask 7 - List of student names");
            List<string> myStudentList = [];
            Random random = new();
            int randomNumber = random.Next(1, 12);
            Console.WriteLine($"You need to add {randomNumber} students to your class list.");

            for (int i = 0; i < randomNumber; i++)
            {
                string studentName = GetValidString($"Enter the name of student {i + 1}: ");
                myStudentList.Add(studentName);
            }

            Console.WriteLine("The names of the students in your class are:");
            foreach (string name in myStudentList)
            {
                Console.WriteLine(name);
            }

            // -- Task 8 ---
            Console.WriteLine("\nTask 8 - Palindrome check for an array");
            int[] test_2 = [3, 2, 1];
            bool isPalindrome = Palindrome(test_2);
            if (isPalindrome)
            {
                Console.WriteLine("The array is a palindrome.");
            }
            else
            {
                Console.WriteLine("The array is not a palindrome.");
            }

            // -- Task 9 ---
            Console.WriteLine("\nTask 9 - Merge sorted lists");
            List<int> list1 = [1, 2, 2, 5];
            List<int> list2 = [1, 3, 4, 5, 7];
            Console.WriteLine("Test 1 - Both sorted:");
            Console.WriteLine($"List A: [{string.Join(", ", list1)}]");
            Console.WriteLine($"List B: [{string.Join(", ", list2)}]");
            var result1 = Merge(list1, list2);
            if (result1 != null)
                Console.WriteLine($"Merged: [{string.Join(", ", result1)}]");
            else
                Console.WriteLine("Merged: null");

            List<int> list3 = [1, 2, 2, 5];
            List<int> list4 = [];
            Console.WriteLine("\nTest 2 - One empty list:");
            Console.WriteLine($"List A: [{string.Join(", ", list3)}]");
            Console.WriteLine($"List B: [{string.Join(", ", list4)}]");
            var result2 = Merge(list3, list4);
            if (result2 != null)
                Console.WriteLine($"Merged: [{string.Join(", ", result2)}]");
            else
                Console.WriteLine("Merged: null");

            List<int> list5 = [5, 2, 2, 1];
            List<int> list6 = [1, 3, 4, 5, 7];
            Console.WriteLine("\nTest 3 - One unsorted:");
            Console.WriteLine($"List A: [{string.Join(", ", list5)}]");
            Console.WriteLine($"List B: [{string.Join(", ", list6)}]");
            var result3 = Merge(list5, list6);
            if (result3 != null)
                Console.WriteLine($"Merged: [{string.Join(", ", result3)}]");
            else
                Console.WriteLine("Merged: null");

            // -- Task 10 ---
            Console.WriteLine("\nTask 10 - Array conversion (extract odd values)");
            int[,] exampleArray = new int[4, 6]
            {
                { 0, 2, 4, 0, 9, 5 },
                { 7, 1, 3, 3, 2, 1 },
                { 1, 3, 9, 8, 5, 6 },
                { 4, 6, 7, 9, 1, 0 },
            };
            Console.WriteLine("Test 1 - Example array:");
            Console.WriteLine("Original 2D array:");
            PrintArray2D(exampleArray);
            Console.WriteLine(
                $"Extracted odd values: [{string.Join(", ", ArrayConversion(exampleArray))}]"
            );
            Console.WriteLine("Expected: [7, 1, 1, 3, 3, 9, 7, 3, 9, 9, 5, 1, 5, 1]");

            int[,] testArray = new int[3, 4]
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 },
            };
            Console.WriteLine("\nTest 2 - Simple array:");
            Console.WriteLine("Original 2D array:");
            PrintArray2D(testArray);
            Console.WriteLine(
                $"Extracted odd values: [{string.Join(", ", ArrayConversion(testArray))}]"
            );
            Console.WriteLine("Expected: [1, 5, 9, 3, 7, 11]");

            int[,] evenArray = new int[2, 3]
            {
                { 2, 4, 6 },
                { 8, 10, 12 },
            };
            Console.WriteLine("\nTest 3 - All even numbers:");
            Console.WriteLine("Original 2D array:");
            PrintArray2D(evenArray);
            Console.WriteLine(
                $"Extracted odd values: [{string.Join(", ", ArrayConversion(evenArray))}]"
            );
            Console.WriteLine("Expected: [] (empty array)");
        }
        catch (OutOfMemoryException)
        {
            Console.WriteLine("Error: Not enough memory to run the program.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nProgram execution completed. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

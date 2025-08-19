using System.Globalization;

namespace BuggySoft
{
    class RevisedCode
    {
        // Static collections to store tasks for each category
        private static readonly List<string> tasksPersonal = [];
        private static readonly List<string> tasksWork = [];
        private static readonly List<string> tasksFamily = [];

        private static readonly CultureInfo australianCulture = new("en-AU");

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = australianCulture;
            Thread.CurrentThread.CurrentUICulture = australianCulture;

            while (true)
            {
                try
                {
                    DisplayTaskTable();
                    ProcessUserInput();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Displays the task table with all categories and their tasks
        /// </summary>
        private static void DisplayTaskTable()
        {
            Console.Clear();

            int maxTasks = Math.Max(
                Math.Max(tasksPersonal.Count, tasksWork.Count),
                tasksFamily.Count
            );

            // Table header
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(new string(' ', 12) + "CATEGORIES");
            Console.WriteLine(new string(' ', 10) + new string('-', 94));
            Console.WriteLine(
                "{0,10}|{1,30}|{2,30}|{3,30}|",
                "item #",
                "Personal",
                "Work",
                "Family"
            );
            Console.WriteLine(new string(' ', 10) + new string('-', 94));

            // Display table rows
            for (int i = 0; i < maxTasks; i++)
            {
                DisplayTableRow(i, tasksPersonal, tasksWork, tasksFamily);
            }

            // Display current date in Australian format
            Console.WriteLine(new string(' ', 10) + new string('-', 94));
            Console.WriteLine(
                $"Current Date: {DateTime.Now.ToString("dd/MM/yyyy", australianCulture)}"
            );
            Console.ResetColor();
        }

        /// <summary>
        /// Displays a single row of the task table
        /// </summary>
        /// <param name="rowIndex">The row index to display</param>
        /// <param name="personalTasks">List of personal tasks</param>
        /// <param name="workTasks">List of work tasks</param>
        /// <param name="familyTasks">List of family tasks</param>
        private static void DisplayTableRow(
            int rowIndex,
            List<string> personalTasks,
            List<string> workTasks,
            List<string> familyTasks
        )
        {
            Console.Write("{0,10}|", rowIndex);
            Console.Write("{0,30}|", GetTaskAtIndex(personalTasks, rowIndex));
            Console.Write("{0,30}|", GetTaskAtIndex(workTasks, rowIndex));
            Console.Write("{0,30}|", GetTaskAtIndex(familyTasks, rowIndex));
            Console.WriteLine();
        }

        /// <summary>
        /// Gets the task at a specific index, or "N/A" if index is out of bounds
        /// </summary>
        /// <param name="taskList">The task list to check</param>
        /// <param name="index">The index to retrieve</param>
        /// <returns>Task name or "N/A"</returns>
        private static string GetTaskAtIndex(List<string> taskList, int index)
        {
            return index < taskList.Count ? taskList[index] : "N/A";
        }

        /// <summary>
        /// Processes user input for adding new tasks with enhanced validation
        /// </summary>
        private static void ProcessUserInput()
        {
            string category = GetCategoryFromUser();
            if (string.IsNullOrEmpty(category))
            {
                DisplayError("Invalid category selection.");
                return;
            }

            string taskDescription = GetTaskDescriptionFromUser();
            if (string.IsNullOrEmpty(taskDescription))
            {
                DisplayError("Task description cannot be empty.");
                return;
            }
            AddTaskToCategory(category, taskDescription);
        }

        /// <summary>
        /// Gets the category selection from the user with validation
        /// </summary>
        /// <returns>Normalized category name or empty string if invalid</returns>
        private static string GetCategoryFromUser()
        {
            const int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Console.WriteLine(
                    "\nWhich category do you want to place a new task? Type 'Personal', 'Work', or 'Family'"
                );
                Console.Write(">> ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    attempts++;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        $"Please enter a category. Attempts remaining: {maxAttempts - attempts}"
                    );
                    Console.ResetColor();
                    continue;
                }

                string normalizedInput = input.ToLower();
                string result = normalizedInput switch
                {
                    "personal" => "personal",
                    "work" => "work",
                    "family" => "family",
                    _ => string.Empty,
                };

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }

                attempts++;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(
                    $"Invalid category '{input}'. Please enter 'Personal', 'Work', or 'Family'. Attempts remaining: {maxAttempts - attempts}"
                );
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Maximum attempts exceeded. Returning to main menu.");
            Console.ResetColor();
            return string.Empty;
        }

        /// <summary>
        /// Gets the task description from the user with enhanced validation
        /// </summary>
        /// <returns>Task description trimmed to maximum 30 characters</returns>
        private static string GetTaskDescriptionFromUser()
        {
            const int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Console.WriteLine("Describe your task below (max. 30 symbols, min. 3 symbols).");
                Console.Write(">> ");
                string? task = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(task))
                {
                    attempts++;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        $"Task description cannot be empty. Attempts remaining: {maxAttempts - attempts}"
                    );
                    Console.ResetColor();
                    continue;
                }

                if (task.Length < 3)
                {
                    attempts++;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        $"Task description must be at least 3 characters. Attempts remaining: {maxAttempts - attempts}"
                    );
                    Console.ResetColor();
                    continue;
                }

                // Check for invalid characters
                if (ContainsInvalidCharacters(task))
                {
                    attempts++;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        $"Task description contains invalid characters. Please use only letters, numbers, and basic punctuation. Attempts remaining: {maxAttempts - attempts}"
                    );
                    Console.ResetColor();
                    continue;
                }

                // Trim to maximum 30 characters and notify user if trimmed
                if (task.Length > 30)
                {
                    string originalTask = task;
                    task = task.Substring(0, 30);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        $"Task description was trimmed from '{originalTask}' to '{task}'"
                    );
                    Console.ResetColor();
                }

                return task;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Maximum attempts exceeded. Returning to main menu.");
            Console.ResetColor();
            return string.Empty;
        }

        /// <summary>
        /// Checks if the input contains invalid characters
        /// </summary>
        /// <param name="input">The input string to validate</param>
        /// <returns>True if contains invalid characters, false otherwise</returns>
        private static bool ContainsInvalidCharacters(string input)
        {
            // Allow letters, numbers, spaces, and basic punctuation
            foreach (char c in input)
            {
                if (
                    !char.IsLetterOrDigit(c)
                    && !char.IsWhiteSpace(c)
                    && !char.IsPunctuation(c)
                    && c != '-'
                    && c != '_'
                )
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a task to the specified category with success confirmation
        /// </summary>
        /// <param name="category">The category to add the task to</param>
        /// <param name="task">The task description</param>
        private static void AddTaskToCategory(string category, string task)
        {
            try
            {
                switch (category)
                {
                    case "personal":
                        tasksPersonal.Add(task);
                        break;
                    case "work":
                        tasksWork.Add(task);
                        break;
                    case "family":
                        tasksFamily.Add(task);
                        break;
                    default:
                        throw new ArgumentException("Invalid category specified");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    $"✓ Task '{task}' added to {char.ToUpper(category[0]) + category[1..]} category successfully!"
                );
                Console.WriteLine(
                    $"Added on: {DateTime.Now.ToString("dd/MM/yyyy HH:mm", australianCulture)}"
                );
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                DisplayError($"Failed to add task: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Displays an error message in a consistent format
        /// </summary>
        /// <param name="message">The error message to display</param>
        private static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ Error: {message}");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

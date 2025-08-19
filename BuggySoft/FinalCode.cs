using System;
using System.Globalization;

namespace BuggySoft
{
    #region Task Class
    /// <summary>
    /// Represents a single task with importance level and due date
    /// </summary>
    public class Task(string description, DateTime dueDate, bool isImportant = false)
    {
        #region Properties
        public string Description { get; set; } = description;
        public DateTime DueDate { get; set; } = dueDate;
        public bool IsImportant { get; set; } = isImportant;

        // Australian culture for consistent date formatting
        private static readonly CultureInfo AustralianCulture = new("en-AU");
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets a formatted string representation of the task for display in Australian format
        /// </summary>
        public string GetDisplayString()
        {
            string taskInfo =
                $"{Description} ({DueDate.ToString("dd/MM/yyyy", AustralianCulture)})";
            return taskInfo.Length > 28 ? taskInfo[..28] : taskInfo;
        }

        /// <summary>
        /// Gets the full task details for detailed display
        /// </summary>
        public string GetFullDetails()
        {
            return $"{Description} - Due: {DueDate.ToString("dd/MM/yyyy", AustralianCulture)}"
                + (IsImportant ? " [IMPORTANT]" : "");
        }
        #endregion
    }
    #endregion

    #region Category Class
    /// <summary>
    /// Represents a category that contains tasks using composition
    /// The category "has" tasks - when a category is deleted, its tasks are also deleted
    /// </summary>
    public class Category(string name)
    {
        #region Fields and Properties
        public string Name { get; private set; } =
            name ?? throw new ArgumentNullException(nameof(name));
        private readonly List<Task> tasks = [];

        /// <summary>
        /// Gets a read-only view of the tasks
        /// </summary>
        public IReadOnlyList<Task> Tasks => tasks.AsReadOnly();
        #endregion

        #region Task Management Methods
        /// <summary>
        /// Adds a new task to the category
        /// </summary>
        public void AddTask(Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            tasks.Add(task);
        }

        /// <summary>
        /// Removes a task at the specified index
        /// </summary>
        public bool RemoveTaskAt(int index)
        {
            if (index < 0 || index >= tasks.Count)
                return false;
            tasks.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Moves a task to a new priority position within the category
        /// </summary>
        public bool MoveTask(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= tasks.Count || toIndex < 0 || toIndex >= tasks.Count)
                return false;

            var task = tasks[fromIndex];
            tasks.RemoveAt(fromIndex);
            tasks.Insert(toIndex, task);
            return true;
        }

        /// <summary>
        /// Gets a task at the specified index
        /// </summary>
        public Task? GetTaskAt(int index)
        {
            return index >= 0 && index < tasks.Count ? tasks[index] : null;
        }

        /// <summary>
        /// Removes and returns a task at the specified index (for moving between categories)
        /// </summary>
        public Task? ExtractTaskAt(int index)
        {
            if (index < 0 || index >= tasks.Count)
                return null;
            var task = tasks[index];
            tasks.RemoveAt(index);
            return task;
        }

        /// <summary>
        /// Clears all tasks from the category
        /// </summary>
        public void ClearTasks()
        {
            tasks.Clear();
        }
        #endregion
    }
    #endregion

    #region TaskPlanner Class
    /// <summary>
    /// Main task planner class that manages categories and user interaction
    /// </summary>
    public class TaskPlanner
    {
        #region Fields and Constructor
        private readonly Dictionary<string, Category> categories;
        private static readonly CultureInfo AustralianCulture = new CultureInfo("en-AU");

        public TaskPlanner()
        {
            Thread.CurrentThread.CurrentCulture = AustralianCulture;
            Thread.CurrentThread.CurrentUICulture = AustralianCulture;

            categories = new Dictionary<string, Category>(StringComparer.OrdinalIgnoreCase)
            {
                { "Personal", new Category("Personal") },
                { "Work", new Category("Work") },
                { "Family", new Category("Family") },
            };
        }
        #endregion

        #region Main Program Loop
        /// <summary>
        /// Main program loop
        /// </summary>
        public void Run()
        {
            while (true)
            {
                try
                {
                    DisplayTaskTable();
                    DisplayMenu();
                    ProcessMenuChoice();
                }
                catch (Exception ex)
                {
                    DisplayError($"An error occurred: {ex.Message}");
                }
            }
        }
        #endregion

        #region Display Methods
        /// <summary>
        /// Displays the task table with all categories and their tasks
        /// </summary>
        private void DisplayTaskTable()
        {
            Console.Clear();

            var categoryList = categories.Values.ToList();
            int maxTasks = categoryList.Count > 0 ? categoryList.Max(c => c.Tasks.Count) : 0;

            // Display table header
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(new string(' ', 15) + "TASK PLANNER ");
            Console.WriteLine(new string('-', 100));
            Console.WriteLine(
                $"Current Date: {DateTime.Now.ToString("dddd, dd MMMM yyyy", AustralianCulture)}"
            );
            Console.WriteLine(new string('-', 100));

            // Dynamic header based on existing categories
            Console.Write("{0,8}|", "Item #");
            foreach (var category in categoryList)
            {
                Console.Write("{0,30}|", category.Name);
            }
            Console.WriteLine();
            Console.WriteLine(new string('-', 100));

            // Display table rows
            for (int i = 0; i < maxTasks; i++)
            {
                Console.Write("{0,8}|", i);
                foreach (var category in categoryList)
                {
                    var task = category.GetTaskAt(i);
                    if (task != null)
                    {
                        if (task.IsImportant)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write("{0,30}|", task.GetDisplayString());
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("{0,30}|", "N/A");
                    }
                }
                Console.WriteLine();
            }

            Console.ResetColor();
            Console.WriteLine(new string('-', 100));
        }

        /// <summary>
        /// Displays the main menu options
        /// </summary>
        private static void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=== TASK PLANNER MENU ===");
            Console.WriteLine("1. Add new task");
            Console.WriteLine("2. Delete task");
            Console.WriteLine("3. Move task priority");
            Console.WriteLine("4. Move task to another category");
            Console.WriteLine("5. Add new category");
            Console.WriteLine("6. Delete category");
            Console.WriteLine("7. Exit");
            Console.ResetColor();
            Console.Write("Enter your choice (1-7): ");
        }

        /// <summary>
        /// Displays all tasks in a category
        /// </summary>
        private static void DisplayCategoryTasks(Category category)
        {
            Console.WriteLine($"\nTasks in {category.Name}:");
            for (int i = 0; i < category.Tasks.Count; i++)
            {
                var task = category.Tasks[i];
                Console.ForegroundColor = task.IsImportant ? ConsoleColor.Red : ConsoleColor.White;
                Console.WriteLine($"{i}. {task.GetFullDetails()}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Displays an error message
        /// </summary>
        private static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ Error: {message}");
            Console.ResetColor();
            PauseForUser();
        }

        /// <summary>
        /// Displays a success message
        /// </summary>
        private static void DisplaySuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ {message}");
            Console.ResetColor();
        }
        #endregion

        #region Menu Processing
        /// <summary>
        /// Processes the user's menu selection
        /// </summary>
        private void ProcessMenuChoice()
        {
            string? choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    AddNewTask();
                    break;
                case "2":
                    DeleteTask();
                    break;
                case "3":
                    MoveTaskPriority();
                    break;
                case "4":
                    MoveTaskToCategory();
                    break;
                case "5":
                    AddNewCategory();
                    break;
                case "6":
                    DeleteCategory();
                    break;
                case "7":
                    Console.WriteLine("Thank you for using Task Planner!");
                    Environment.Exit(0);
                    break;
                default:
                    DisplayError("Invalid choice. Please enter a number between 1 and 7.");
                    break;
            }
        }
        #endregion

        #region Task Operations
        /// <summary>
        /// Adds a new task to a category with enhanced validation
        /// </summary>
        private void AddNewTask()
        {
            var category = SelectCategory("Which category do you want to add the task to?");
            if (category == null)
                return;

            string description = GetValidatedTaskDescription();
            if (string.IsNullOrEmpty(description))
                return;

            DateTime dueDate = GetDueDateFromUser();
            bool isImportant = GetImportanceFromUser();

            var task = new Task(description, dueDate, isImportant);
            category.AddTask(task);

            DisplaySuccess($"Task '{description}' added successfully to {category.Name}!");
            Console.WriteLine(
                $"Added on: {DateTime.Now.ToString("dd/MM/yyyy HH:mm", AustralianCulture)}"
            );
            PauseForUser();
        }

        /// <summary>
        /// Gets a validated task description from user input
        /// </summary>
        private static string GetValidatedTaskDescription()
        {
            const int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Console.Write("Enter task description (3-30 characters): ");
                string? description = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(description))
                {
                    attempts++;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        $"Description cannot be empty. Attempts remaining: {maxAttempts - attempts}"
                    );
                    Console.ResetColor();
                    continue;
                }

                if (description.Length < 3)
                {
                    attempts++;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        $"Description must be at least 3 characters. Attempts remaining: {maxAttempts - attempts}"
                    );
                    Console.ResetColor();
                    continue;
                }

                if (description.Length > 30)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        $"Description trimmed from {description.Length} to 30 characters."
                    );
                    Console.ResetColor();
                    description = description.Substring(0, 30);
                }

                return description;
            }

            DisplayError("Maximum attempts exceeded for task description.");
            return string.Empty;
        }

        /// <summary>
        /// Deletes a task from a category
        /// </summary>
        private void DeleteTask()
        {
            var category = SelectCategory("Which category contains the task to delete?");
            if (category == null)
                return;

            if (category.Tasks.Count == 0)
            {
                DisplayError("This category has no tasks to delete.");
                return;
            }

            DisplayCategoryTasks(category);
            Console.Write($"Enter task index to delete (0-{category.Tasks.Count - 1}): ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (category.RemoveTaskAt(index))
                {
                    DisplaySuccess("Task deleted successfully!");
                }
                else
                {
                    DisplayError("Invalid task index.");
                }
            }
            else
            {
                DisplayError("Please enter a valid number.");
            }
            PauseForUser();
        }

        /// <summary>
        /// Moves a task to a different priority position within the same category
        /// </summary>
        private void MoveTaskPriority()
        {
            var category = SelectCategory("Which category contains the task to move?");
            if (category == null)
                return;

            if (category.Tasks.Count <= 1)
            {
                DisplayError("Category must have at least 2 tasks to move priorities.");
                return;
            }

            DisplayCategoryTasks(category);
            Console.Write($"Enter task index to move (0-{category.Tasks.Count - 1}): ");

            if (int.TryParse(Console.ReadLine(), out int fromIndex))
            {
                Console.Write($"Enter new position (0-{category.Tasks.Count - 1}): ");
                if (int.TryParse(Console.ReadLine(), out int toIndex))
                {
                    if (category.MoveTask(fromIndex, toIndex))
                    {
                        DisplaySuccess("Task moved successfully!");
                    }
                    else
                    {
                        DisplayError("Invalid task index or position.");
                    }
                }
                else
                {
                    DisplayError("Please enter a valid position number.");
                }
            }
            else
            {
                DisplayError("Please enter a valid task index.");
            }
            PauseForUser();
        }

        /// <summary>
        /// Moves a task from one category to another
        /// </summary>
        private void MoveTaskToCategory()
        {
            var sourceCategory = SelectCategory("Which category contains the task to move?");
            if (sourceCategory == null)
                return;

            if (sourceCategory.Tasks.Count == 0)
            {
                DisplayError("This category has no tasks to move.");
                return;
            }

            DisplayCategoryTasks(sourceCategory);
            Console.Write($"Enter task index to move (0-{sourceCategory.Tasks.Count - 1}): ");

            if (!int.TryParse(Console.ReadLine(), out int taskIndex))
            {
                DisplayError("Please enter a valid task index.");
                PauseForUser();
                return;
            }

            var targetCategory = SelectCategory("Which category do you want to move the task to?");
            if (targetCategory == null)
                return;

            var task = sourceCategory.ExtractTaskAt(taskIndex);
            if (task != null)
            {
                targetCategory.AddTask(task);
                DisplaySuccess(
                    $"Task moved successfully from {sourceCategory.Name} to {targetCategory.Name}!"
                );
            }
            else
            {
                DisplayError("Invalid task index.");
            }
            PauseForUser();
        }
        #endregion

        #region Category Operations
        /// <summary>
        /// Adds a new category with validation
        /// </summary>
        private void AddNewCategory()
        {
            Console.Write("Enter new category name (3-15 characters): ");
            string? categoryName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(categoryName))
            {
                DisplayError("Category name cannot be empty.");
                PauseForUser();
                return;
            }

            if (categoryName.Length < 3)
            {
                DisplayError("Category name must be at least 3 characters.");
                PauseForUser();
                return;
            }

            if (categoryName.Length > 15)
            {
                DisplayError("Category name cannot exceed 15 characters.");
                PauseForUser();
                return;
            }

            if (categories.ContainsKey(categoryName))
            {
                DisplayError("Category already exists.");
                PauseForUser();
                return;
            }

            categories[categoryName] = new Category(categoryName);
            DisplaySuccess($"Category '{categoryName}' added successfully!");
            PauseForUser();
        }

        /// <summary>
        /// Deletes a category and all its tasks
        /// </summary>
        private void DeleteCategory()
        {
            if (categories.Count <= 1)
            {
                DisplayError("Cannot delete the last category.");
                PauseForUser();
                return;
            }

            var category = SelectCategory("Which category do you want to delete?");
            if (category == null)
                return;

            Console.Write(
                $"Are you sure you want to delete '{category.Name}' and all its {category.Tasks.Count} tasks? (y/N): "
            );
            string? confirmation = Console.ReadLine()?.Trim().ToLower();

            if (confirmation == "y" || confirmation == "yes")
            {
                categories.Remove(category.Name);
                DisplaySuccess(
                    $"Category '{category.Name}' and all its tasks deleted successfully!"
                );
            }
            else
            {
                Console.WriteLine("Category deletion cancelled.");
            }
            PauseForUser();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Helper method to select a category from available categories
        /// </summary>
        private Category? SelectCategory(string prompt)
        {
            Console.WriteLine($"\n{prompt}");
            var categoryList = categories.Keys.ToList();

            for (int i = 0; i < categoryList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categoryList[i]}");
            }

            Console.Write($"Enter choice (1-{categoryList.Count}): ");

            if (
                int.TryParse(Console.ReadLine(), out int choice)
                && choice >= 1
                && choice <= categoryList.Count
            )
            {
                return categories[categoryList[choice - 1]];
            }

            DisplayError("Invalid category selection.");
            PauseForUser();
            return null;
        }

        /// <summary>
        /// Gets due date from user input with Australian format validation
        /// </summary>
        private static DateTime GetDueDateFromUser()
        {
            const int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Console.Write("Enter due date (DD/MM/YYYY) or press Enter for today: ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    return DateTime.Today;

                if (
                    DateTime.TryParseExact(
                        input,
                        "dd/MM/yyyy",
                        AustralianCulture,
                        DateTimeStyles.None,
                        out DateTime dueDate
                    )
                )
                {
                    if (dueDate < DateTime.Today)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(
                            "Warning: Due date is in the past. Continue anyway? (y/N): "
                        );
                        Console.ResetColor();
                        string? confirm = Console.ReadLine()?.Trim().ToLower();
                        if (confirm != "y" && confirm != "yes")
                        {
                            attempts++;
                            continue;
                        }
                    }
                    return dueDate;
                }

                string[] australianFormats =
                {
                    "d/M/yyyy",
                    "dd/M/yyyy",
                    "d/MM/yyyy",
                    "d/M/yy",
                    "dd/MM/yy",
                };
                foreach (string format in australianFormats)
                {
                    if (
                        DateTime.TryParseExact(
                            input,
                            format,
                            AustralianCulture,
                            DateTimeStyles.None,
                            out dueDate
                        )
                    )
                    {
                        if (dueDate < DateTime.Today)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(
                                "Warning: Due date is in the past. Continue anyway? (y/N): "
                            );
                            Console.ResetColor();
                            string? confirm = Console.ReadLine()?.Trim().ToLower();
                            if (confirm != "y" && confirm != "yes")
                            {
                                attempts++;
                                continue;
                            }
                        }
                        return dueDate;
                    }
                }

                attempts++;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(
                    $"Invalid date format. Please use DD/MM/YYYY format (e.g., {DateTime.Today.AddDays(7).ToString("dd/MM/yyyy", AustralianCulture)}). Attempts remaining: {maxAttempts - attempts}"
                );
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Maximum attempts exceeded. Using today's date.");
            Console.ResetColor();
            return DateTime.Today;
        }

        /// <summary>
        /// Gets task importance from user input with validation
        /// </summary>
        private static bool GetImportanceFromUser()
        {
            const int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Console.Write("Is this task important? (y/N): ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(input) || input == "n" || input == "no")
                    return false;

                if (input == "y" || input == "yes")
                    return true;

                attempts++;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(
                    $"Please enter 'y' for yes or 'n' for no. Attempts remaining: {maxAttempts - attempts}"
                );
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Maximum attempts exceeded. Setting task as not important.");
            Console.ResetColor();
            return false;
        }

        /// <summary>
        /// Pauses execution until user presses a key
        /// </summary>
        private static void PauseForUser()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion
    }
    #endregion

    #region Main Program Entry Point
    /// <summary>
    /// Main program entry point
    /// </summary>
    class FinalCode
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("🇦🇺 Welcome to  Task Planner! 🇦🇺");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            var taskPlanner = new TaskPlanner();
            taskPlanner.Run();
        }
    }
    #endregion
}

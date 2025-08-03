

// namespace BuggySoft
// {
//     class RevisedCode
//     {
//         // Static collections to store tasks for each category
//         private static List<string> tasksPersonal = new List<string>();
//         private static List<string> tasksWork = new List<string>();
//         private static List<string> tasksFamily = new List<string>();

//         static void Main(string[] args)
//         {
//             while (true)
//             {
//                 try
//                 {
//                     DisplayTaskTable();
//                     ProcessUserInput();
//                 }
//                 catch (Exception ex)
//                 {
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine($"An error occurred: {ex.Message}");
//                     Console.ResetColor();
//                     Console.WriteLine("Press any key to continue...");
//                     Console.ReadKey();
//                 }
//             }
//         }

//         /// <summary>
//         /// Displays the task table with all categories and their tasks
//         /// </summary>
//         private static void DisplayTaskTable()
//         {
//             Console.Clear();

//             // Calculate the maximum number of tasks across all categories
//             int maxTasks = Math.Max(
//                 Math.Max(tasksPersonal.Count, tasksWork.Count),
//                 tasksFamily.Count
//             );

//             // Display table header
//             Console.ForegroundColor = ConsoleColor.Blue;
//             Console.WriteLine(new string(' ', 12) + "CATEGORIES");
//             Console.WriteLine(new string(' ', 10) + new string('-', 94));
//             Console.WriteLine(
//                 "{0,10}|{1,30}|{2,30}|{3,30}|",
//                 "item #",
//                 "Personal",
//                 "Work",
//                 "Family"
//             );
//             Console.WriteLine(new string(' ', 10) + new string('-', 94));

//             // Display table rows
//             for (int i = 0; i < maxTasks; i++)
//             {
//                 DisplayTableRow(i, tasksPersonal, tasksWork, tasksFamily);
//             }

//             Console.ResetColor();
//         }

//         /// <summary>
//         /// Displays a single row of the task table
//         /// </summary>
//         /// <param name="rowIndex">The row index to display</param>
//         /// <param name="personalTasks">List of personal tasks</param>
//         /// <param name="workTasks">List of work tasks</param>
//         /// <param name="familyTasks">List of family tasks</param>
//         private static void DisplayTableRow(
//             int rowIndex,
//             List<string> personalTasks,
//             List<string> workTasks,
//             List<string> familyTasks
//         )
//         {
//             Console.Write("{0,10}|", rowIndex);
//             Console.Write("{0,30}|", GetTaskAtIndex(personalTasks, rowIndex));
//             Console.Write("{0,30}|", GetTaskAtIndex(workTasks, rowIndex));
//             Console.Write("{0,30}|", GetTaskAtIndex(familyTasks, rowIndex));
//             Console.WriteLine();
//         }

//         /// <summary>
//         /// Gets the task at a specific index, or "N/A" if index is out of bounds
//         /// </summary>
//         /// <param name="taskList">The task list to check</param>
//         /// <param name="index">The index to retrieve</param>
//         /// <returns>Task name or "N/A"</returns>
//         private static string GetTaskAtIndex(List<string> taskList, int index)
//         {
//             return index < taskList.Count ? taskList[index] : "N/A";
//         }

//         /// <summary>
//         /// Processes user input for adding new tasks
//         /// </summary>
//         private static void ProcessUserInput()
//         {
//             // Get category selection
//             string category = GetCategoryFromUser();
//             if (string.IsNullOrEmpty(category))
//             {
//                 Console.WriteLine("Invalid category selection. Press any key to continue...");
//                 Console.ReadKey();
//                 return;
//             }

//             // Get task description
//             string taskDescription = GetTaskDescriptionFromUser();
//             if (string.IsNullOrEmpty(taskDescription))
//             {
//                 Console.WriteLine("Task description cannot be empty. Press any key to continue...");
//                 Console.ReadKey();
//                 return;
//             }

//             // Add task to the appropriate category
//             AddTaskToCategory(category, taskDescription);
//         }

//         /// <summary>
//         /// Gets the category selection from the user
//         /// </summary>
//         /// <returns>Normalized category name or empty string if invalid</returns>
//         private static string GetCategoryFromUser()
//         {
//             Console.WriteLine(
//                 "\nWhich category do you want to place a new task? Type 'Personal', 'Work', or 'Family'"
//             );
//             Console.Write(">> ");
//             string? input = Console.ReadLine()?.Trim().ToLower();

//             return input switch
//             {
//                 "personal" => "personal",
//                 "work" => "work",
//                 "family" => "family",
//                 _ => string.Empty,
//             };
//         }

//         /// <summary>
//         /// Gets the task description from the user with length validation
//         /// </summary>
//         /// <returns>Task description trimmed to maximum 30 characters</returns>
//         private static string GetTaskDescriptionFromUser()
//         {
//             Console.WriteLine("Describe your task below (max. 30 symbols).");
//             Console.Write(">> ");
//             string? task = Console.ReadLine()?.Trim();

//             if (string.IsNullOrEmpty(task))
//                 return string.Empty;

//             // Trim to maximum 30 characters
//             return task.Length > 30 ? task.Substring(0, 30) : task;
//         }

//         /// <summary>
//         /// Adds a task to the specified category
//         /// </summary>
//         /// <param name="category">The category to add the task to</param>
//         /// <param name="task">The task description</param>
//         private static void AddTaskToCategory(string category, string task)
//         {
//             switch (category)
//             {
//                 case "personal":
//                     tasksPersonal.Add(task);
//                     break;
//                 case "work":
//                     tasksWork.Add(task);
//                     break;
//                 case "family":
//                     tasksFamily.Add(task);
//                     break;
//                 default:
//                     throw new ArgumentException("Invalid category specified");
//             }

//             Console.WriteLine($"Task '{task}' added to {category} category successfully!");
//             Console.WriteLine("Press any key to continue...");
//             Console.ReadKey();
//         }
//     }
// }

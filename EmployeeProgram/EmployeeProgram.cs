
namespace EmployeeManagement
{
    public class EmployeeProgram
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Employee Management System ===\n");


            TestEmployee("Darth Vader", 15000);
            TestEmployee("Ben Solo", 25000);
            TestEmployee("Luke Skywalker", 50000);
            TestEmployee("Han Solo", 120000);
            TestEmployee("Chewbaka", 200000);

            Console.WriteLine("\n=== Testing Salary Raise Functionality ===\n");


            Employee employee = new("John Doe", 115000);
            Console.WriteLine($"Employee: {employee.GetName()}");
            Console.WriteLine($"Initial Salary: {employee.GetSalary()}");
            Console.WriteLine($"Initial Tax: {employee.Tax():C}");
            Console.WriteLine($"Net Income: {employee.getSalaryAmount() - employee.Tax():C}");

            employee.raiseSalary(10);
            Console.WriteLine($"\nAfter 10% raise:");
            Console.WriteLine($"New Salary: {employee.GetSalary()}");
            Console.WriteLine($"New Tax: {employee.Tax():C}");
            Console.WriteLine($"Net Income: {employee.getSalaryAmount() - employee.Tax():C}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void TestEmployee(string name, double salary)
        {
            Employee employee = new(name, salary);
            Console.WriteLine($"Employee: {employee.GetName()}");
            Console.WriteLine($"Salary: {employee.GetSalary()}");
            Console.WriteLine($"Tax: {employee.Tax():C}");
            Console.WriteLine($"Net Income: {employee.getSalaryAmount() - employee.Tax():C}\n");
        }
    }
}
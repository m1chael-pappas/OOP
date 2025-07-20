using AccountProgram;

class TestAccount
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Banking System - Account Class Testing ===\n");

        // Test 1: Create accounts
        Account account1 = new("John Smith", 1000.50m);
        Account account2 = new("Sarah Johnson", 500.00m);
        Console.WriteLine();

        // Test 2: Display initial states
        account1.Print();
        account2.Print();

        // Test 3: Name property
        Console.WriteLine($"Account 1 holder: {account1.Name}");
        Console.WriteLine($"Account 2 holder: {account2.Name}");
        Console.WriteLine();

        // Test 4: Deposits
        account1.Deposit(250.75m);
        account1.Print();
        account2.Deposit(100.00m);
        account2.Print();

        // Test 5: Withdrawals
        account1.Withdraw(150.25m);
        account1.Print();
        account2.Withdraw(50.00m);
        account2.Print();

        // Test 6: Edge cases
        account1.Withdraw(2000.00m);
        account2.Deposit(-50.00m);
        account2.Withdraw(-25.00m);
        Console.WriteLine();

        // Test 7: Final states
        account1.Print();
        account2.Print();

        Console.WriteLine("=== Testing Complete ===");
        Console.ReadKey();
    }
}

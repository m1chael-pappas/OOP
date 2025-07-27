using System;

namespace BankingSystem
{
    public enum MenuOption
    {
        Withdraw = 0,
        Deposit = 1,
        Transfer = 2,
        Print = 3,
        Quit = 4,
    }

    class BankSystem
    {
        static MenuOption ReadUserOption()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(
                    @"
┌─────────────────────────────────────────────┐
│               BANKING MENU                  │
├─────────────────────────────────────────────┤
│                                             │
│  💸  1. Withdraw Money                      │
│  💰  2. Deposit Money                       │
│  🔄  3. Transfer Money                      │
│  📊  4. Print Account Details               │
│  🚪  5. Exit System                         │
│                                             │
└─────────────────────────────────────────────┘
"
                );
                Console.ResetColor();
                Console.Write("\n💡 Please select an option (1-5): ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());

                    if (choice >= 1 && choice <= 5)
                    {
                        MenuOption selectedOption = (MenuOption)(choice - 1);
                        Console.WriteLine($"You selected: {selectedOption}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        return selectedOption;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "❌ Invalid choice! Please enter a number between 1 and 5."
                        );
                        Console.ResetColor();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Invalid input! Please enter a number between 1 and 5.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Number too large! Please enter a number between 1 and 5.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (true);
        }

        static void DoDeposit(Account account)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("💰 === DEPOSIT === 💰");
            Console.ResetColor();
            Console.Write("Enter amount to deposit: $");

            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());

                // Create and execute deposit transaction
                DepositTransaction transaction = new(account, amount);
                transaction.Execute();
                transaction.Print();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ Successfully deposited ${amount:F2}");
                Console.WriteLine($"💰 New balance: ${account.Balance:F2}");
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid amount! Please enter a valid number.");
                Console.ResetColor();
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Amount too large! Please enter a smaller amount.");
                Console.ResetColor();
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Transaction failed: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DoWithdraw(Account account)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("💸 === WITHDRAW === 💸");
            Console.ResetColor();
            Console.WriteLine($"Current balance: ${account.Balance:F2}");
            Console.Write("Enter amount to withdraw: $");

            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());

                // Create and execute withdraw transaction
                WithdrawTransaction transaction = new WithdrawTransaction(account, amount);
                transaction.Execute();
                transaction.Print();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ Successfully withdrew ${amount:F2}");
                Console.WriteLine($"💰 New balance: ${account.Balance:F2}");
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid amount! Please enter a valid number.");
                Console.ResetColor();
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Amount too large! Please enter a smaller amount.");
                Console.ResetColor();
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Transaction failed: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DoTransfer(Account[] accounts)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("🔄 === TRANSFER === 🔄");
            Console.ResetColor();

            // Display available accounts
            Console.WriteLine("Available accounts:");
            for (int i = 0; i < accounts.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[i].Name} - ${accounts[i].Balance:F2}");
            }

            try
            {
                // Select source account
                Console.Write("\nSelect source account (number): ");
                int fromIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                if (fromIndex < 0 || fromIndex >= accounts.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Invalid account selection!");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                // Select destination account
                Console.Write("Select destination account (number): ");
                int toIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                if (toIndex < 0 || toIndex >= accounts.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Invalid account selection!");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                if (fromIndex == toIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Cannot transfer to the same account!");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Account fromAccount = accounts[fromIndex];
                Account toAccount = accounts[toIndex];

                Console.WriteLine($"\nFrom: {fromAccount.Name} (${fromAccount.Balance:F2})");
                Console.WriteLine($"To: {toAccount.Name} (${toAccount.Balance:F2})");
                Console.Write("Enter amount to transfer: $");

                decimal amount = Convert.ToDecimal(Console.ReadLine());

                // Create and execute transfer transaction
                TransferTransaction transaction = new(fromAccount, toAccount, amount);
                transaction.Execute();
                transaction.Print();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ Successfully transferred ${amount:F2}");
                Console.WriteLine($"💰 {fromAccount.Name} new balance: ${fromAccount.Balance:F2}");
                Console.WriteLine($"💰 {toAccount.Name} new balance: ${toAccount.Balance:F2}");
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid input! Please enter valid numbers.");
                Console.ResetColor();
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Number too large! Please enter a smaller amount.");
                Console.ResetColor();
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Transaction failed: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DoPrint(Account[] accounts)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📊 === ACCOUNT DETAILS === 📊");
            Console.ResetColor();

            foreach (Account account in accounts)
            {
                Console.WriteLine(
                    $@"
╔═══════════════════════════════════════════════╗
║                ACCOUNT SUMMARY                ║
╠═══════════════════════════════════════════════╣
║                                               ║
║  👤 Account Holder: {account.Name, -23}   ║
║  💰 Current Balance: ${account.Balance, -21:F2}   ║
║  📅 Last Updated: {DateTime.Now.ToString("MM/dd/yyyy"), -17}           ║
║                                               ║
╚═══════════════════════════════════════════════╝
"
                );
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DisplayWelcomeBanner()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(
                @"
╔══════════════════════════════════════════════════════════╗
║                                                          ║
║             ██████╗  █████╗ ███╗   ██╗██╗  ██╗           ║
║             ██╔══██╗██╔══██╗████╗  ██║██║ ██╔╝           ║
║             ██████╔╝███████║██╔██╗ ██║█████╔╝            ║
║             ██╔══██╗██╔══██║██║╚██╗██║██╔═██╗            ║
║             ██████╔╝██║  ██║██║ ╚████║██║  ██╗           ║
║             ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝           ║
║                                                          ║
║               🏦 SECURE BANKING SYSTEM 🏦                ║
╚══════════════════════════════════════════════════════════╝
"
            );
            Console.ResetColor();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            DisplayWelcomeBanner();

            // Create multiple accounts for testing transfer functionality
            Account[] accounts =
            [
                new("John Smith", 1000.00m),
                new("Jane Doe", 500.00m),
                new("Bob Johnson", 2000.00m),
            ];

            MenuOption choice;
            do
            {
                choice = ReadUserOption();

                switch (choice)
                {
                    case MenuOption.Withdraw:
                        // For simplicity, always use the first account for withdraw/deposit
                        // You could extend this to let user select which account
                        DoWithdraw(accounts[0]);
                        break;

                    case MenuOption.Deposit:
                        DoDeposit(accounts[0]);
                        break;

                    case MenuOption.Transfer:
                        DoTransfer(accounts);
                        break;

                    case MenuOption.Print:
                        DoPrint(accounts);
                        break;

                    case MenuOption.Quit:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("🏦 Thank you for using the Banking System. Goodbye! 🏦");
                        Console.ResetColor();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Unexpected error occurred.");
                        Console.ResetColor();
                        break;
                }
            } while (choice != MenuOption.Quit);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

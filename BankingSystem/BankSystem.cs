using System;

namespace BankingSystem
{
    public enum MenuOption
    {
        Withdraw = 0,
        Deposit = 1,
        Transfer = 2,
        Print = 3,
        AddAccount = 4,
        Rollback = 5,
        Quit = 6,
    }

    class BankSystem
    {
        // Store the last transaction for rollback functionality
        private static object? lastTransaction = null;

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
│  👤  5. Add New Account                     │
│  ↩️   6. Rollback Last Transaction          │
│  🚪  7. Exit System                         │
│                                             │
└─────────────────────────────────────────────┘
"
                );
                Console.ResetColor();
                Console.Write("\n💡 Please select an option (1-7): ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());

                    if (choice >= 1 && choice <= 7)
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
                            "❌ Invalid choice! Please enter a number between 1 and 7."
                        );
                        Console.ResetColor();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Invalid input! Please enter a number between 1 and 7.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Number too large! Please enter a number between 1 and 7.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (true);
        }

        private static Account? FindAccount(Bank bank)
        {
            Console.Write("Enter the account name: ");
            string? accountName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Account name cannot be empty!");
                Console.ResetColor();
                return null;
            }

            Account? account = bank.GetAccount(accountName);

            if (account == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Account '{accountName}' not found!");
                Console.ResetColor();
            }

            return account;
        }

        static void DoAddAccount(Bank bank)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("👤 === ADD NEW ACCOUNT === 👤");
            Console.ResetColor();

            try
            {
                Console.Write("Enter the account holder's name: ");
                string? name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Account name cannot be empty!");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                if (bank.GetAccount(name) != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"❌ Account with name '{name}' already exists!");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter the starting balance: $");
                decimal balance = Convert.ToDecimal(Console.ReadLine());

                if (balance < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Starting balance cannot be negative!");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Account newAccount = new(name, balance);
                bank.AddAccount(newAccount);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    $"✅ Successfully created account for {name} with balance ${balance:F2}"
                );
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid balance! Please enter a valid number.");
                Console.ResetColor();
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Balance too large! Please enter a smaller amount.");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DoDeposit(Bank bank)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("💰 === DEPOSIT === 💰");
            Console.ResetColor();

            Account? account = FindAccount(bank);
            if (account == null)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter amount to deposit: $");

            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                DepositTransaction transaction = new(account, amount);
                Bank.ExecuteTransaction(transaction);
                lastTransaction = transaction;
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

        static void DoRollback()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("↩️ === ROLLBACK LAST TRANSACTION === ↩️");
            Console.ResetColor();

            if (lastTransaction == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️ No transaction available to rollback.");
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            try
            {
                if (lastTransaction is WithdrawTransaction withdrawTx)
                {
                    Console.WriteLine("Rolling back withdraw transaction...");
                    withdrawTx.Rollback();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅ Withdraw transaction successfully rolled back!");
                    Console.ResetColor();
                    withdrawTx.Print();
                }
                else if (lastTransaction is DepositTransaction depositTx)
                {
                    Console.WriteLine("Rolling back deposit transaction...");
                    depositTx.Rollback();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅ Deposit transaction successfully rolled back!");
                    Console.ResetColor();
                    depositTx.Print();
                }
                else if (lastTransaction is TransferTransaction transferTx)
                {
                    Console.WriteLine("Rolling back transfer transaction...");
                    transferTx.Rollback();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅ Transfer transaction successfully rolled back!");
                    Console.ResetColor();
                    transferTx.Print();
                }

                // Clear the last transaction after successful rollback
                lastTransaction = null;
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Rollback failed: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DoWithdraw(Bank bank)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("💸 === WITHDRAW === 💸");
            Console.ResetColor();

            Account? account = FindAccount(bank);
            if (account == null)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Current balance: ${account.Balance:F2}");
            Console.Write("Enter amount to withdraw: $");

            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());

                WithdrawTransaction transaction = new(account, amount);
                Bank.ExecuteTransaction(transaction);
                lastTransaction = transaction;
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

        static void DoTransfer(Bank bank)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("🔄 === TRANSFER === 🔄");
            Console.ResetColor();

            Console.WriteLine("Select source account:");
            Account? fromAccount = FindAccount(bank);
            if (fromAccount == null)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select destination account:");
            Account? toAccount = FindAccount(bank);
            if (toAccount == null)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            if (fromAccount.Name.Equals(toAccount.Name, StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Cannot transfer to the same account!");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nFrom: {fromAccount.Name} (${fromAccount.Balance:F2})");
            Console.WriteLine($"To: {toAccount.Name} (${toAccount.Balance:F2})");
            Console.Write("Enter amount to transfer: $");

            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());

                TransferTransaction transaction = new(fromAccount, toAccount, amount);
                Bank.ExecuteTransaction(transaction);
                lastTransaction = transaction;
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

        static void DoPrint(Bank bank)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📊 === ACCOUNT DETAILS === 📊");
            Console.ResetColor();

            var accounts = bank.GetAllAccounts();

            if (accounts.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️ No accounts found in the bank.");
                Console.ResetColor();
            }
            else
            {
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
            Bank bank = new();

            // Testing data
            bank.AddAccount(new Account("John Smith", 1000.00m));
            bank.AddAccount(new Account("Jane Doe", 500.00m));
            bank.AddAccount(new Account("Bob Johnson", 2000.00m));

            MenuOption choice;
            do
            {
                choice = ReadUserOption();

                switch (choice)
                {
                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;

                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;

                    case MenuOption.Transfer:
                        DoTransfer(bank);
                        break;

                    case MenuOption.Print:
                        DoPrint(bank);
                        break;

                    case MenuOption.AddAccount:
                        DoAddAccount(bank);
                        break;

                    case MenuOption.Rollback:
                        DoRollback();
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

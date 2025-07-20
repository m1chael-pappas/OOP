namespace BankingSystem
{
    public enum MenuOption
    {
        Withdraw = 0,
        Deposit = 1,
        Print = 2,
        Quit = 3,
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
│  📊  3. Print Account Details               │
│  🚪  4. Exit System                         │
│                                             │
└─────────────────────────────────────────────┘
"
                );
                Console.ResetColor();
                Console.Write("\n💡 Please select an option (1-4): ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());

                    if (choice >= 1 && choice <= 4)
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
                            "❌ Invalid choice! Please enter a number between 1 and 4."
                        );
                        Console.ResetColor();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Invalid input! Please enter a number between 1 and 4.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Number too large! Please enter a number between 1 and 4.");
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

                bool success = account.Deposit(amount);
                if (success)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✅ Successfully deposited ${amount:F2}");
                    Console.WriteLine($"💰 New balance: ${account.Balance:F2}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Deposit failed! Amount must be greater than zero.");
                    Console.ResetColor();
                }
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

                bool success = account.Withdraw(amount);
                if (success)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✅ Successfully withdrew ${amount:F2}");
                    Console.WriteLine($"💰 New balance: ${account.Balance:F2}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        "❌ Withdrawal failed! Check amount is positive and you have sufficient funds."
                    );
                    Console.ResetColor();
                }
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

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DoPrint(Account account)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
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
            Console.ResetColor();
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

            Account userAccount = new("John Smith", 1000.00m);

            MenuOption choice;
            do
            {
                choice = ReadUserOption();

                switch (choice)
                {
                    case MenuOption.Withdraw:
                        DoWithdraw(userAccount);
                        break;

                    case MenuOption.Deposit:
                        DoDeposit(userAccount);
                        break;

                    case MenuOption.Print:
                        DoPrint(userAccount);
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

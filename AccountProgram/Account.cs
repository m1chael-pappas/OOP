namespace AccountProgram
{
    public class Account
    {
        // Fields
        private readonly string _name;
        private decimal _balance;

        // Constructor
        public Account(string name, decimal balance)
        {
            _name = name;
            _balance = balance;
            Console.WriteLine($"Account created for {_name} with initial balance: ${_balance:F2}");
        }

        // Property
        public string Name
        {
            get { return _name; }
        }

        // Methods
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
                Console.WriteLine($"${amount:F2} deposited successfully.");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount > 0)
            {
                if (amount <= _balance)
                {
                    _balance -= amount;
                    Console.WriteLine($"${amount:F2} withdrawn successfully.");
                }
                else
                {
                    Console.WriteLine("Insufficient funds for withdrawal.");
                }
            }
            else
            {
                Console.WriteLine("Withdrawal amount must be positive.");
            }
        }

        public void Print()
        {
            Console.WriteLine($"Account Name: {_name}");
            Console.WriteLine($"Current Balance: ${_balance:F2}");
            Console.WriteLine("------------------------");
        }
    }
}

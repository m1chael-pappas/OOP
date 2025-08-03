namespace BankingSystem
{
    public class Account(string name, decimal balance)
    {
        private decimal _balance = balance;
        private readonly string _name = name;

        public string Name => _name;
        public decimal Balance => _balance;

        public bool Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
                return true;
            }
            return false;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= _balance)
            {
                _balance -= amount;
                return true;
            }
            return false;
        }

        // public void Print()
        // {
        //     Console.WriteLine($"Account Name: {_name}");
        //     Console.WriteLine($"Current Balance: ${_balance:F2}");
        //     Console.WriteLine("------------------------");
        // }
    }
}

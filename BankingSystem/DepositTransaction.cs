namespace BankingSystem
{
    public class DepositTransaction(Account account, decimal amount) : Transaction(amount)
    {
        private readonly Account _account = account;

        public override void Print()
        {
            Console.WriteLine($"Deposit Transaction Details:");
            Console.WriteLine($"Account: {_account.Name}");
            Console.WriteLine($"Amount: ${_amount:F2}");
            Console.WriteLine($"Executed: {_executed}");
            Console.WriteLine($"Success: {_success}");
            Console.WriteLine($"Reversed: {_reversed}");
            Console.WriteLine($"Date/Time: {_dateStamp:yyyy-MM-dd HH:mm:ss}");

            if (_success)
            {
                Console.WriteLine(
                    $"Successfully deposited ${_amount:F2} to {_account.Name}'s account"
                );
            }
            Console.WriteLine("------------------------");
        }

        public override void Execute()
        {
            base.Execute();

            if (_amount <= 0)
            {
                throw new InvalidOperationException("Deposit amount must be greater than zero.");
            }

            bool result = _account.Deposit(_amount);
            if (result)
            {
                _success = true;
            }
        }

        public override void Rollback()
        {
            base.Rollback();

            if (_account.Balance < _amount)
            {
                throw new InvalidOperationException("Insufficient funds to reverse the deposit.");
            }

            bool result = _account.Withdraw(_amount);
            if (result)
            {
                _reversed = true;
            }
        }
    }
}

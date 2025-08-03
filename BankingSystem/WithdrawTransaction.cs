namespace BankingSystem
{
    public class WithdrawTransaction(Account account, decimal amount)
    {
        // Fields
        private readonly Account _account = account;
        private readonly decimal _amount = amount;
        private bool _executed = false;
        private bool _success = false;
        private bool _reversed = false;

        // Properties
        public bool Executed => _executed;
        public bool Success => _success;
        public bool Reversed => _reversed;

        public void Print()
        {
            Console.WriteLine($"Withdraw Transaction Details:");
            Console.WriteLine($"Account: {_account.Name}");
            Console.WriteLine($"Amount: ${_amount:F2}");
            Console.WriteLine($"Executed: {_executed}");
            Console.WriteLine($"Success: {_success}");
            Console.WriteLine($"Reversed: {_reversed}");

            if (_success)
            {
                Console.WriteLine(
                    $"Successfully withdrew ${_amount:F2} from {_account.Name}'s account"
                );
            }
            Console.WriteLine("------------------------");
        }

        public void Execute()
        {
            if (_executed)
            {
                throw new InvalidOperationException("Transaction has already been attempted.");
            }

            _executed = true;

            if (_account.Balance < _amount)
            {
                throw new InvalidOperationException("Insufficient funds in the account.");
            }

            bool result = _account.Withdraw(_amount);
            if (result)
            {
                _success = true;
            }
        }

        public void Rollback()
        {
            if (!_executed || !_success)
            {
                throw new InvalidOperationException(
                    "Cannot rollback: transaction was not successfully executed."
                );
            }

            if (_reversed)
            {
                throw new InvalidOperationException("Transaction has already been reversed.");
            }

            bool result = _account.Deposit(_amount);
            if (result)
            {
                _reversed = true;
            }
        }
    }
}

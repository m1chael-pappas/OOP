namespace BankingSystem
{
    public class TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
    {
        private readonly Account _fromAccount = fromAccount;
        private readonly Account _toAccount = toAccount;
        private readonly decimal _amount = amount;
        private readonly DepositTransaction _deposit = new(toAccount, amount);
        private readonly WithdrawTransaction _withdraw = new(fromAccount, amount);
        private bool _executed = false;
        private bool _reversed = false;

        public bool Executed => _executed;

        public bool Success => _deposit.Success && _withdraw.Success;

        public bool Reversed => _reversed;

        public void Print()
        {
            Console.WriteLine($"Transfer Transaction Details:");
            Console.WriteLine($"From Account: {_fromAccount.Name}");
            Console.WriteLine($"To Account: {_toAccount.Name}");
            Console.WriteLine($"Amount: ${_amount:F2}");
            Console.WriteLine($"Executed: {_executed}");
            Console.WriteLine($"Success: {Success}");
            Console.WriteLine($"Reversed: {_reversed}");

            if (Success)
            {
                Console.WriteLine(
                    $"Successfully transferred ${_amount:F2} from {_fromAccount.Name}'s account to {_toAccount.Name}'s account"
                );
            }

            Console.WriteLine("\nWithdraw Transaction Details:");
            _withdraw.Print();

            Console.WriteLine("Deposit Transaction Details:");
            _deposit.Print();
        }

        public void Execute()
        {
            if (_executed)
            {
                throw new InvalidOperationException(
                    "Transfer transaction has already been attempted."
                );
            }

            _executed = true;

            if (_fromAccount.Balance < _amount)
            {
                throw new InvalidOperationException("Insufficient funds in the source account.");
            }

            try
            {
                _withdraw.Execute();

                _deposit.Execute();
            }
            catch (InvalidOperationException)
            {
                if (_withdraw.Success && !_deposit.Success)
                {
                    _withdraw.Rollback();
                }
                throw;
            }
        }

        public void Rollback()
        {
            if (!_executed || !Success)
            {
                throw new InvalidOperationException(
                    "Cannot rollback: transfer transaction was not successfully executed."
                );
            }

            if (_reversed)
            {
                throw new InvalidOperationException(
                    "Transfer transaction has already been reversed."
                );
            }

            if (_toAccount.Balance < _amount)
            {
                throw new InvalidOperationException(
                    "Insufficient funds in destination account to complete rollback."
                );
            }

            try
            {
                _deposit.Rollback();

                _withdraw.Rollback();

                _reversed = true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }
    }
}

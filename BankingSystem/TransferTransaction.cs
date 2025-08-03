using System;

namespace BankingSystem
{
    public class TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
        : Transaction(amount)
    {
        private readonly Account _fromAccount = fromAccount;
        private readonly Account _toAccount = toAccount;
        private readonly DepositTransaction _deposit = new(toAccount, amount);
        private readonly WithdrawTransaction _withdraw = new(fromAccount, amount);

        public override void Print()
        {
            Console.WriteLine($"Transfer Transaction Details:");
            Console.WriteLine($"From Account: {_fromAccount.Name}");
            Console.WriteLine($"To Account: {_toAccount.Name}");
            Console.WriteLine($"Amount: ${_amount:F2}");
            Console.WriteLine($"Executed: {_executed}");
            Console.WriteLine($"Success: {_success}");
            Console.WriteLine($"Reversed: {_reversed}");
            Console.WriteLine($"Date/Time: {_dateStamp:yyyy-MM-dd HH:mm:ss}");

            if (_success)
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

        public override void Execute()
        {
            base.Execute();

            if (_fromAccount.Balance < _amount)
            {
                throw new InvalidOperationException("Insufficient funds in the source account.");
            }

            try
            {
                _withdraw.Execute();

                _deposit.Execute();

                if (_withdraw.Success && _deposit.Success)
                {
                    _success = true;
                }
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

        public override void Rollback()
        {
            base.Rollback();

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

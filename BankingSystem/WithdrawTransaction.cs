using System;

namespace BankingSystem
{
    public class WithdrawTransaction(Account account, decimal amount) : Transaction(amount)
    {
        private readonly Account _account = account;

        public override void Print()
        {
            Console.WriteLine($"Withdraw Transaction Details:");
            Console.WriteLine($"Account: {_account.Name}");
            Console.WriteLine($"Amount: ${_amount:F2}");
            Console.WriteLine($"Executed: {_executed}");
            Console.WriteLine($"Success: {_success}");
            Console.WriteLine($"Reversed: {_reversed}");
            Console.WriteLine($"Date/Time: {_dateStamp:yyyy-MM-dd HH:mm:ss}");

            if (_success)
            {
                Console.WriteLine(
                    $"Successfully withdrew ${_amount:F2} from {_account.Name}'s account"
                );
            }
            Console.WriteLine("------------------------");
        }

        public override void Execute()
        {
            base.Execute();

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

        public override void Rollback()
        {
            base.Rollback();

            bool result = _account.Deposit(_amount);
            if (result)
            {
                _reversed = true;
            }
        }
    }
}

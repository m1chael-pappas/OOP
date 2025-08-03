namespace BankingSystem
{
    public class Bank
    {
        private readonly List<Account> _accounts;
        private readonly List<Transaction> _transactions;

        public Bank()
        {
            _accounts = [];
            _transactions = [];
        }

        public void AddAccount(Account account)
        {
            if (account != null)
            {
                _accounts.Add(account);
            }
        }

        public Account? GetAccount(string name)
        {
            foreach (Account account in _accounts)
            {
                if (account.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return account;
                }
            }
            return null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            if (transaction != null)
            {
                _transactions.Add(transaction);

                transaction.Execute();
            }
        }

        public static void RollbackTransaction(Transaction transaction)
        {
            transaction?.Rollback();
        }

        public void PrintTransactionHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("📋 === TRANSACTION HISTORY === 📋");
            Console.ResetColor();

            if (_transactions.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️ No transactions found.");
                Console.ResetColor();
                return;
            }

            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine($"\n--- Transaction #{i + 1} ---");
                _transactions[i].Print();
            }
        }

        public Transaction? GetTransaction(int index)
        {
            if (index >= 0 && index < _transactions.Count)
            {
                return _transactions[index];
            }
            return null;
        }

        public int TransactionCount => _transactions.Count;

        public List<Account> GetAllAccounts()
        {
            return [.. _accounts];
        }
    }
}

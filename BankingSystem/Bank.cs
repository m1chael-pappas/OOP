namespace BankingSystem
{
    public class Bank
    {
        private readonly List<Account> _accounts;

        public Bank()
        {
            _accounts = [];
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

        public static void ExecuteTransaction(DepositTransaction transaction)
        {
            transaction.Execute();
        }

        public static void ExecuteTransaction(WithdrawTransaction transaction)
        {
            transaction.Execute();
        }

        public static void ExecuteTransaction(TransferTransaction transaction)
        {
            transaction.Execute();
        }

        public List<Account> GetAllAccounts()
        {
            return [.. _accounts];
        }
    }
}

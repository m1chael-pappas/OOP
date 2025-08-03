namespace BankingSystem
{
    public abstract class Transaction(decimal amount)
    {
        protected decimal _amount = amount;
        protected bool _success = false;
        protected bool _executed = false;
        protected bool _reversed = false;
        protected DateTime _dateStamp = DateTime.Now;

        public bool Success => _success;
        public bool Executed => _executed;
        public bool Reversed => _reversed;
        public DateTime DateStamp => _dateStamp;

        public virtual void Print()
        {
            Console.WriteLine($"Transaction Details:");
            Console.WriteLine($"Amount: ${_amount:F2}");
            Console.WriteLine($"Executed: {_executed}");
            Console.WriteLine($"Success: {_success}");
            Console.WriteLine($"Reversed: {_reversed}");
            Console.WriteLine($"Date/Time: {_dateStamp:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("------------------------");
        }

        public virtual void Execute()
        {
            if (_executed)
            {
                throw new InvalidOperationException("Transaction has already been attempted.");
            }

            _executed = true;
            _dateStamp = DateTime.Now;
        }

        public virtual void Rollback()
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

            _dateStamp = DateTime.Now;
        }
    }
}

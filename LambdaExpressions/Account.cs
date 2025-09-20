namespace LambdaExpressions
{
    /// <summary>
    /// Minimal Account model for testing MyStack&lt;T&gt; as per SIT232 8.1C.
    /// Implements IComparable&lt;Account&gt; so Min/Max can use Comparer&lt;T&gt;.Default.
    /// </summary>
    public class Account : IComparable<Account>
    {
        public string Name { get; set; } = "";
        public decimal Balance { get; set; }

        public int CompareTo(Account? other)
        {
            if (other is null)
                return 1;
            return Balance.CompareTo(other.Balance);
        }

        public override string ToString() => $"{Name} (Balance: {Balance:C})";
    }
}

namespace LambdaExpressions
{
    /// <summary>
    /// Lightweight test helpers to keep Main() readable.
    /// </summary>
    internal static class Test
    {
        public static void Section(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('═', Math.Max(8, title.Length + 6)));
            Console.WriteLine($"== {title} ==");
            Console.WriteLine(new string('─', Math.Max(8, title.Length + 6)));
        }

        public static void ExpectThrows<TEx>(string label, Action action)
            where TEx : Exception
        {
            try
            {
                action();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    $"[FAIL] {label}: expected {typeof(TEx).Name}, but no exception was thrown."
                );
                Console.ResetColor();
            }
            catch (TEx)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PASS] {label}: threw {typeof(TEx).Name} as expected.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    $"[FAIL] {label}: threw {ex.GetType().Name}, expected {typeof(TEx).Name}."
                );
                Console.ResetColor();
            }
        }

        public static void AssertTrue(string label, bool condition)
        {
            if (condition)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PASS] {label}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FAIL] {label}");
            }
            Console.ResetColor();
        }

        public static void Dump<T>(string label, IEnumerable<T> items)
        {
            Console.WriteLine($"{label}:");
            int i = 0;
            foreach (var it in items)
            {
                Console.WriteLine($"  [{i++}] {it}");
            }
            if (i == 0)
                Console.WriteLine("  (empty)");
        }
    }

    /// <summary>
    /// Entry point Tester class .
    /// </summary>
    public class Tester
    {
        static void Main()
        {
            Console.WriteLine("SIT232 8.1C – MyStack<T> test harness");
            Console.WriteLine("------------------------------------------------------");

            // =====================================================
            // A) Build a stack of Accounts (top is the last pushed)
            // =====================================================
            Test.Section("Setup: populate MyStack<Account>");
            var stack = new MyStack<Account>(10);

            // Bottom -> Top order (push in this order)
            var acc1 = new Account { Name = "Everyday", Balance = 500m }; // bottom
            var acc2 = new Account { Name = "CreditCard", Balance = -150m };
            var acc3 = new Account { Name = "Savings", Balance = 1200m };
            var acc4 = new Account { Name = "Deposit", Balance = 100m };
            var acc5 = new Account { Name = "Loan", Balance = -2000m }; // top

            stack.Push(acc1);
            stack.Push(acc2);
            stack.Push(acc3);
            stack.Push(acc4);
            stack.Push(acc5);

            Console.WriteLine($"Pushed 5 accounts. Count = {stack.Count} (expected 5).");

            // =====================================================
            // B) Find: first from TOP that matches a predicate
            // =====================================================
            Test.Section("Find(Func<T,bool>)");

            var firstPositive = stack.Find(a => a.Balance > 0);
            Console.WriteLine($"Find(Balance > 0) -> {firstPositive}");
            Test.AssertTrue(
                "Find should return the TOP-most positive (Deposit $100, not Savings $1200)",
                ReferenceEquals(firstPositive, acc4)
            );

            var firstBigPositive = stack.Find(a => a.Balance >= 1000m);
            Console.WriteLine($"Find(Balance >= 1000) -> {firstBigPositive}");
            Test.AssertTrue(
                "Find big positive should be Savings $1200",
                ReferenceEquals(firstBigPositive, acc3)
            );

            var noneOver2k = stack.Find(a => a.Balance > 2000m);
            Console.WriteLine(
                $"Find(Balance > 2000) -> {(noneOver2k == null ? "null" : noneOver2k.ToString())}"
            );
            Test.AssertTrue(
                "When no match, Find returns default(T) which is null for reference T",
                noneOver2k == default(Account)
            );

            Test.ExpectThrows<ArgumentNullException>("Find(null) throws", () => stack.Find(null!));

            // =====================================================
            // C) FindAll: collect matches (top-to-bottom search order OK)
            // =====================================================
            Test.Section("FindAll(Func<T,bool>)");

            var positives = stack.FindAll(a => a.Balance > 0);
            Console.WriteLine(
                $"FindAll(Balance > 0) -> {(positives == null ? "null" : string.Join(" | ", (IEnumerable<Account>)(positives ?? [])))}"
            );
            Test.AssertTrue(
                "FindAll positives should return 3 items",
                positives != null && positives.Length == 3
            );

            var creditOrDeposit = stack.FindAll(a =>
                a.Name.Contains("Credit", StringComparison.OrdinalIgnoreCase)
                || a.Name.Contains("Deposit", StringComparison.OrdinalIgnoreCase)
            );
            Console.WriteLine(
                "FindAll(Name contains Credit or Deposit) -> "
                    + (
                        creditOrDeposit == null
                            ? "null"
                            : string.Join(" | ", (IEnumerable<Account>)(creditOrDeposit ?? []))
                    )
            );
            Test.AssertTrue(
                "FindAll should find CreditCard and Deposit",
                creditOrDeposit != null
                    && Array.Exists(creditOrDeposit, a => ReferenceEquals(a, acc2))
                    && Array.Exists(creditOrDeposit, a => ReferenceEquals(a, acc4))
            );
            Test.ExpectThrows<ArgumentNullException>(
                "FindAll(null) throws",
                () => stack.FindAll((Func<Account, bool>)null!)
            );

            // =====================================================
            // D) RemoveAll: remove all negatives, report count removed
            // =====================================================
            Test.Section("RemoveAll(Func<T,bool>)");

            int removedNeg = stack.RemoveAll(a => a.Balance < 0);
            Console.WriteLine($"RemoveAll(Balance < 0) -> removed {removedNeg} (expected 2)");
            Test.AssertTrue(
                "Count should now be 3 (only positive balances remain)",
                stack.Count == 3
            );

            // Check contents (order should be preserved among survivors)
            var survivors = stack.FindAll(_ => true) ?? Array.Empty<Account>();
            Test.Dump("Survivors after removing negatives", survivors);
            Test.AssertTrue(
                "Survivors should include Everyday, Savings, Deposit (CreditCard & Loan removed)",
                Array.Exists(survivors, a => ReferenceEquals(a, acc1))
                    && Array.Exists(survivors, a => ReferenceEquals(a, acc3))
                    && Array.Exists(survivors, a => ReferenceEquals(a, acc4))
                    && survivors.Length == 3
            );

            Test.ExpectThrows<ArgumentNullException>(
                "RemoveAll(null) throws",
                () => stack.RemoveAll((Func<Account, bool>)null!)
            );

            // =====================================================
            // E) Max / Min on the current stack (positives only)
            // =====================================================
            Test.Section("Max() and Min() on non-empty stack");

            var curMax = stack.Max();
            var curMin = stack.Min();
            Console.WriteLine($"Max() -> {curMax}");
            Console.WriteLine($"Min() -> {curMin}");

            Test.AssertTrue("Max should be Savings $1200", ReferenceEquals(curMax, acc3));
            Test.AssertTrue("Min should be Deposit $100", ReferenceEquals(curMin, acc4));

            // =====================================================
            // F) Edge cases: empty stack Min/Max default(T), Find default(T)
            // =====================================================
            Test.Section("Edge cases on EMPTY stack");

            var empty = new MyStack<int>(3);
            Console.WriteLine($"Empty Count = {empty.Count}");

            var maxEmpty = empty.Max();
            var minEmpty = empty.Min();
            var findEmpty = empty.Find(x => true);

            Console.WriteLine($"Empty.Max() -> {maxEmpty} (expected 0 default(int))");
            Console.WriteLine($"Empty.Min() -> {minEmpty} (expected 0 default(int))");
            Console.WriteLine($"Empty.Find(any) -> {findEmpty} (expected 0 default(int))");

            Test.AssertTrue("Empty Max default(int)==0", maxEmpty == default(int));
            Test.AssertTrue("Empty Min default(int)==0", minEmpty == default(int));
            Test.AssertTrue("Empty Find default(int)==0", findEmpty == default(int));

            // =====================================================
            // G) Capacity and Pop exceptions
            // =====================================================
            Test.Section("Push capacity & Pop empty exceptions");

            var small = new MyStack<string>(2);
            small.Push("A");
            small.Push("B");
            Test.ExpectThrows<InvalidOperationException>(
                "Push beyond capacity throws",
                () => small.Push("C")
            );

            var popped1 = small.Pop(); // "B"
            var popped2 = small.Pop(); // "A"
            Console.WriteLine($"Pop -> {popped1}, then {popped2}");
            Test.ExpectThrows<InvalidOperationException>("Pop empty throws", () => small.Pop());

            // =====================================================
            // H) Bonus: integer stack quick sanity for RemoveAll/FindAll
            // =====================================================
            Test.Section("Bonus: MyStack<int> quick sanity");

            var ints = new MyStack<int>(8);
            foreach (var n in new[] { 2, -5, 7, 7, -1, 0 })
                ints.Push(n);

            var allSevens = ints.FindAll(n => n == 7);
            Console.WriteLine(
                "FindAll(==7) -> " + (allSevens == null ? "null" : string.Join(", ", allSevens))
            );
            Test.AssertTrue("Two 7s expected", allSevens != null && allSevens.Length == 2);

            int removedNegs = ints.RemoveAll(n => n < 0);
            Console.WriteLine($"RemoveAll(<0) removed {removedNegs} (expected 2)");
            Test.AssertTrue("Count now should be 4", ints.Count == 4);

            Console.WriteLine($"Max -> {ints.Max()}, Min -> {ints.Min()}");
            Test.AssertTrue("Max should be 7", ints.Max() == 7);
            Test.AssertTrue("Min should be 0", ints.Min() == 0);

            Console.WriteLine();
            Console.WriteLine(
                "All tests executed. Review [PASS]/[FAIL] lines above and adjust if your Account class differs."
            );
        }
    }
}

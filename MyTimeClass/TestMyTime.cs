namespace MyTimeClass
{
    /// <summary>
    /// Test driver class for MyTime - demonstrates all public methods and edge cases
    /// </summary>
    class TestMyTime
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== MyTime Class Test Driver ===");
            Console.WriteLine("Testing all public methods and edge cases\n");

            // Test 1: Default Constructor
            Console.WriteLine("1. Testing Default Constructor:");
            MyTime time1 = new();
            Console.WriteLine($"   Default time: {time1}");
            Console.WriteLine($"   Expected: 00:00:00\n");

            // Test 2: Parameterized Constructor
            Console.WriteLine("2. Testing Parameterized Constructor:");
            MyTime time2 = new(14, 30, 45);
            Console.WriteLine($"   Time with (14, 30, 45): {time2}");
            Console.WriteLine($"   Expected: 14:30:45\n");

            // Test 3: Accessor Methods
            Console.WriteLine("3. Testing Accessor Methods:");
            Console.WriteLine($"   GetHour(): {time2.GetHour()}");
            Console.WriteLine($"   GetMinute(): {time2.GetMinute()}");
            Console.WriteLine($"   GetSecond(): {time2.GetSecond()}");
            Console.WriteLine($"   Expected: 14, 30, 45\n");

            // Test 4: Mutator Methods (Valid Input)
            Console.WriteLine("4. Testing Mutator Methods (Valid Input):");
            time1.SetHour(12);
            time1.SetMinute(25);
            time1.SetSecond(30);
            Console.WriteLine($"   After setting (12, 25, 30): {time1}");
            Console.WriteLine($"   Expected: 12:25:30\n");

            // Test 5: SetTime Method
            Console.WriteLine("5. Testing SetTime Method:");
            time1.SetTime(9, 15, 0);
            Console.WriteLine($"   After SetTime(9, 15, 0): {time1}");
            Console.WriteLine($"   Expected: 09:15:00\n");

            // Test 6: NextSecond Method
            Console.WriteLine("6. Testing NextSecond Method:");
            MyTime time3 = new(10, 30, 58);
            Console.WriteLine($"   Initial time: {time3}");
            time3.NextSecond();
            Console.WriteLine($"   After NextSecond(): {time3}");
            time3.NextSecond();
            Console.WriteLine($"   After NextSecond() again: {time3}");
            Console.WriteLine($"   Expected progression: 10:30:58 -> 10:30:59 -> 10:31:00\n");

            // Test 7: NextMinute Method
            Console.WriteLine("7. Testing NextMinute Method:");
            MyTime time4 = new(15, 59, 30);
            Console.WriteLine($"   Initial time: {time4}");
            time4.NextMinute();
            Console.WriteLine($"   After NextMinute(): {time4}");
            Console.WriteLine($"   Expected: 15:59:30 -> 16:00:30\n");

            // Test 8: NextHour Method
            Console.WriteLine("8. Testing NextHour Method:");
            MyTime time5 = new(23, 45, 20);
            Console.WriteLine($"   Initial time: {time5}");
            time5.NextHour();
            Console.WriteLine($"   After NextHour(): {time5}");
            Console.WriteLine($"   Expected: 23:45:20 -> 00:45:20 (24-hour rollover)\n");

            // Test 9: Edge Case - 23:59:59 NextSecond
            Console.WriteLine("9. Testing Critical Edge Case - 23:59:59 NextSecond:");
            MyTime time6 = new(23, 59, 59);
            Console.WriteLine($"   Initial time: {time6}");
            time6.NextSecond();
            Console.WriteLine($"   After NextSecond(): {time6}");
            Console.WriteLine($"   Expected: 23:59:59 -> 00:00:00 (complete rollover)\n");

            // Test 10: PreviousSecond Method
            Console.WriteLine("10. Testing PreviousSecond Method:");
            MyTime time7 = new(10, 30, 1);
            Console.WriteLine($"    Initial time: {time7}");
            time7.PreviousSecond();
            Console.WriteLine($"    After PreviousSecond(): {time7}");
            time7.PreviousSecond();
            Console.WriteLine($"    After PreviousSecond() again: {time7}");
            Console.WriteLine($"    Expected: 10:30:01 -> 10:30:00 -> 10:29:59\n");

            // Test 11: PreviousMinute Method
            Console.WriteLine("11. Testing PreviousMinute Method:");
            MyTime time8 = new(8, 0, 30);
            Console.WriteLine($"    Initial time: {time8}");
            time8.PreviousMinute();
            Console.WriteLine($"    After PreviousMinute(): {time8}");
            Console.WriteLine($"    Expected: 08:00:30 -> 07:59:30\n");

            // Test 12: PreviousHour Method
            Console.WriteLine("12. Testing PreviousHour Method:");
            MyTime time9 = new(0, 15, 45);
            Console.WriteLine($"    Initial time: {time9}");
            time9.PreviousHour();
            Console.WriteLine($"    After PreviousHour(): {time9}");
            Console.WriteLine($"    Expected: 00:15:45 -> 23:15:45 (24-hour rollover)\n");

            // Test 13: Edge Case - 00:00:00 PreviousSecond
            Console.WriteLine("13. Testing Critical Edge Case - 00:00:00 PreviousSecond:");
            MyTime time10 = new(0, 0, 0);
            Console.WriteLine($"    Initial time: {time10}");
            time10.PreviousSecond();
            Console.WriteLine($"    After PreviousSecond(): {time10}");
            Console.WriteLine($"    Expected: 00:00:00 -> 23:59:59 (complete rollover)\n");

            // Test 14: Input Validation Testing
            Console.WriteLine("14. Testing Input Validation (Exception Handling):");
            TestInvalidInputs();

            // Test 15: Method Chaining
            Console.WriteLine("15. Testing Method Chaining:");
            MyTime time11 = new(12, 0, 0);
            Console.WriteLine($"    Initial time: {time11}");
            time11.NextHour().NextMinute().NextSecond();
            Console.WriteLine($"    After chaining NextHour().NextMinute().NextSecond(): {time11}");
            Console.WriteLine($"    Expected: 12:00:00 -> 13:01:01\n");

            Console.WriteLine("=== All Tests Completed Successfully! ===");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Tests input validation by attempting to create/set invalid times
        /// </summary>
        static void TestInvalidInputs()
        {
            try
            {
                Console.WriteLine("    Testing invalid hour (25):");
                MyTime invalidTime = new(25, 30, 45);
                Console.WriteLine("    ERROR: Should have thrown exception!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"    ✓ Correctly caught exception: {ex.Message}");
            }

            try
            {
                Console.WriteLine("    Testing invalid minute (75):");
                MyTime time = new();
                time.SetMinute(75);
                Console.WriteLine("    ERROR: Should have thrown exception!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"    ✓ Correctly caught exception: {ex.Message}");
            }

            try
            {
                Console.WriteLine("    Testing invalid second (-5):");
                MyTime time = new();
                time.SetSecond(-5);
                Console.WriteLine("    ERROR: Should have thrown exception!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"    ✓ Correctly caught exception: {ex.Message}");
            }

            try
            {
                Console.WriteLine("    Testing boundary values (23, 59, 59):");
                MyTime validTime = new(23, 59, 59);
                Console.WriteLine($"    ✓ Valid boundary time created: {validTime}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    ERROR: Boundary values should be valid: {ex.Message}");
            }

            Console.WriteLine();
        }
    }
}

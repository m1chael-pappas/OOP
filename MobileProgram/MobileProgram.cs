namespace MobileProgram
{
    public class Program
    {
        public static void Main()
        {
            Mobile mobile = new Mobile("Monthly", "Samsung Galaxy s6", "0467644122");
            Console.WriteLine("--- Mobile Account Details ---");
            Console.WriteLine($"Account Type: {mobile.getAccType()}");
            Console.WriteLine($"Device: {mobile.getDevice()}");
            Console.WriteLine($"Number: {mobile.getNumber()}");
            Console.WriteLine($"Balance: {mobile.getBalance()}");
            Console.ReadLine();

            Console.WriteLine("\n--- Updating Mobile Details ---");
            mobile.setBalance(100.50);
            mobile.setAccType("PAYG");
            mobile.setDevice("iPhone 12");
            mobile.setNumber("0412345678");
            Console.WriteLine("\nUpdated Mobile Details:");
            Console.WriteLine($"Account Type: {mobile.getAccType()}");
            Console.WriteLine($"Device: {mobile.getDevice()}");
            Console.WriteLine($"Number: {mobile.getNumber()}");
            Console.WriteLine($"Balance: {mobile.getBalance()}");
            Console.ReadLine();

            Console.WriteLine("\n--- Testing Mobile Methods ---");
            mobile.addCredit(10.00);
            mobile.makeCall(5);
            mobile.sendText(2);
            Console.ReadLine();

            Console.WriteLine("\n--- Creating Second Mobile Account ---");
            Mobile mobile2 = new Mobile("PAYG", "Google Pixel 6", "0423456789");
            Console.WriteLine("--- Second Mobile Account Details ---");
            Console.WriteLine($"Account Type: {mobile2.getAccType()}");
            Console.WriteLine($"Device: {mobile2.getDevice()}");
            Console.WriteLine($"Number: {mobile2.getNumber()}");
            Console.WriteLine($"Balance: {mobile2.getBalance()}");
            Console.ReadLine();


            Console.WriteLine("\n--- Testing Second Mobile Account ---");
            mobile2.addCredit(25.00);
            mobile2.makeCall(8);
            mobile2.sendText(3);
            Console.ReadLine();
        }
    }
}
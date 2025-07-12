class Mobile
{
    private string accType, device, number;
    private double balance;
    private const double CALL_COST = 0.245;
    private const double TEXT_COST = 0.078;

    public string getAccType()
    {
        return accType;
    }

    public string getDevice()
    {
        return device;
    }

    public string getNumber()
    {
        return number;
    }

    public string getBalance()
    {
        return balance.ToString("$#,##0.00");
        // return balance.ToString("C2");
    }

    //Mutator methods
    public void setBalance(double balance)
    {
        this.balance = balance;
    }
    public void setAccType(string accType)
    {
        this.accType = accType;
    }
    public void setDevice(string device)
    {
        this.device = device;
    }
    public void setNumber(string number)
    {
        this.number = number;
    }

    //Methods 
    public void addCredit(double amount)
    {
        balance += amount;
        Console.WriteLine($"Added {amount:C} to balance. New balance: {getBalance()}");
    }

    public void makeCall(int minutes)
    {
        double cost = minutes * CALL_COST;
        balance -= cost;
        Console.WriteLine($"Made a call for {minutes} minutes. Cost: {cost:C}, New balance: {getBalance()}");
    }
    public void sendText(int numTexts)
    {
        double cost = numTexts * TEXT_COST;
        balance -= cost;
        Console.WriteLine($"Sent {numTexts} texts. Cost: {cost:C}, New balance: {getBalance()}");
    }

    public Mobile(string accType, string device, string number)
    {
        this.accType = accType;
        this.device = device;
        this.number = number;
        balance = 0.0;
    }
}
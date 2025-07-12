public class Employee(string employeeName, double currentSalary)
{
    private readonly string name = employeeName;
    private double salary = currentSalary;

    public string GetName()
    {
        return name;
    }
    public string GetSalary()
    {
        return salary.ToString("$#,##0.00");
    }

    public double getSalaryAmount()
    {
        return salary;
    }

    public void raiseSalary(double percentage)
    {
        salary *= 1 + percentage / 100;
    }

    public double Tax()
    {
        double tax;

        if (salary <= 18200)
        {
            // 0% tax
            tax = 0;
        }
        else if (salary <= 37000)
        {
            // 19% tax
            tax = (salary - 18200) * 0.19;
        }
        else if (salary <= 90000)
        {
            // 32.5% tax
            tax = 3572 + (salary - 37000) * 0.325;
        }
        else if (salary <= 180000)
        {
            // 37% tax
            tax = 20797 + (salary - 90000) * 0.37;
        }
        else
        {
            // 45% tax
            tax = 54096 + (salary - 180000) * 0.45;
        }

        return tax;
    }

}

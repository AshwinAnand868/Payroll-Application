using System;

namespace MVCClientApp.BusinessLogic
{
    public class BusinessLogic
    {
        // Calculates the final taxed salary of an employee per month and also other details, then returns the Result as a string
        public static string CalculateSalaryPerMonth(int nDependents, string gender, double iTax, double ei, double cpp, double additions, double grosssalary)
        {
            double Deductions = 0;
            double FinalGross = 0;
            string Result = "";
            double Tax = iTax + ei + cpp;

            if (gender.ToLower() == "female")
            {
                Tax = Tax - 0.02;
            }

            switch (nDependents)
            {
                case 2:
                    Deductions = (grosssalary * Tax) / 12;
                    FinalGross = ((grosssalary - Deductions) + additions) / 12;
                    break;
                case 3:
                    Tax = Tax - 0.03;
                    Deductions = (grosssalary * Tax) / 12;
                    FinalGross = ((grosssalary - Deductions) + additions) / 12;
                    break;
                case 4:
                    Tax = Tax - 0.04;
                    Deductions = (grosssalary * Tax) / 12;
                    FinalGross = ((grosssalary - Deductions) + additions) / 12;
                    break;
            }

            Deductions = Math.Round(Deductions, 2);
            FinalGross = Math.Round(FinalGross, 2);

            Result = "Final Salary  " + Convert.ToString(FinalGross) + "  Total Deductions " + Convert.ToString(Deductions);
            return Result;
        }
    }
}

/* Salary Model */
namespace MVCClientApp.Models
{
    public class Salary
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public double Additions { get; set; }
        public double TotalDeductions { get; set; }
        public double FinalSalary { get; set; }
        public string Details { get; set; }
    }
}
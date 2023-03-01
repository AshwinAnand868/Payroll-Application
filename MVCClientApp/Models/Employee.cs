using System.ComponentModel.DataAnnotations;

namespace MVCClientApp.Models
{
    // Employee Model
    public class Employee
    {
        public int emp_id { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [DataType(DataType.Text)]
        public string emp_name { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [DataType(DataType.EmailAddress)]
        public string emp_email { get; set; }
        public string emp_password { get; set; }
        public string emp_gender { get; set; }
        public int? noOfDependants { get; set; }
        public decimal? Additions { get; set; }
        public decimal? ITex { get; set; }
        public decimal? CPP { get; set; }
        public decimal? EI { get; set; }
        public decimal? FinalSalary { get; set; }
    }
}
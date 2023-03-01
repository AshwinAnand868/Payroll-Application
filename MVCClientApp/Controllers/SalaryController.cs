using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using MVCClientApp.Models;
using System.Threading.Tasks;
using MVCClientApp.BusinessLogic;
namespace MVCClientApp.Controllers
{
    public class SalaryController : Controller
    {
        
        Salary salaryDetails = new Salary();

        // GET Salary/CalcPay
        // Returns the list of employees to the view
        public ActionResult CalcPay()
        {
            IEnumerable<Employee> employees = null;
            HttpResponseMessage response;
            Task taskToDo = Task.Run(async () =>
            {
                response = await GlobalVariables.WebApi.GetAsync("Employee");

                if (response.IsSuccessStatusCode)
                {
                    employees = await response.Content.ReadAsAsync<IEnumerable<Employee>>();
                }
                    
            });
            taskToDo.Wait();
            return View(employees);

        }

        // Calculates the final taxed salary along with deductions and other details, then redirects to the action FinalSalary to display the calculated data  
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage resp = GlobalVariables.WebApi.GetAsync("Employee/" + id).Result;
            Employee employee = resp.Content.ReadAsAsync<Employee>().Result;

            if (employee == null)
            {
                return HttpNotFound();
            }



            string details = BusinessLogic.BusinessLogic.CalculateSalaryPerMonth(Convert.ToInt16(employee.noOfDependants), employee.emp_gender.ToString(), Convert.ToDouble(employee.ITex), Convert.ToDouble(employee.EI), Convert.ToDouble(employee.CPP), Convert.ToDouble(employee.Additions), Convert.ToDouble(employee.FinalSalary));

            TempData["empId"] = employee.emp_id;
            TempData["empName"] = employee.emp_name;
            TempData["addition"] = employee.Additions;
            TempData["details"] = details;

            return RedirectToAction("FinalSalary");
        }

        // Displays the employee final salary along with other details
        public ActionResult FinalSalary()
        {
            salaryDetails.EmpId = Convert.ToInt32(TempData["empId"]);
            salaryDetails.EmpName = Convert.ToString(TempData["empName"]);
            salaryDetails.Additions = Convert.ToDouble(TempData["addition"]);
            salaryDetails.Details = Convert.ToString(TempData["details"]);

            return View(salaryDetails);
        }
    }
}
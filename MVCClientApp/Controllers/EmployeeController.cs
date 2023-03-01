using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using MVCClientApp.Models;

namespace MVCClientApp.Controllers
{
    // This controller works directly with the API to get/persist data from/to the database.
    public class EmployeeController : Controller
    {
        // Renders a view with the list of employees fetched from the database
        public ActionResult Index()
        {
            
            IEnumerable<Employee> employees = null;
            HttpResponseMessage response;
            Task taskToDo = Task.Run(async() => {
                response = await GlobalVariables.WebApi.GetAsync("Employee");
                    
                if(response.IsSuccessStatusCode)
                {
                    employees = await response.Content.ReadAsAsync<IEnumerable<Employee>>();
                }
                
            }); 
            taskToDo.Wait();
            return View(employees);
        }

        // Renders a view with a specific employee data, otherwise displays an empty view for the new employee information to be persisted to the database
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0) {
                return View(new Employee());
            }
            else {
                HttpResponseMessage resp = GlobalVariables.WebApi.GetAsync("Employee/" + id.ToString()).Result;
                Employee employee = resp.Content.ReadAsAsync<Employee>().Result;
                return View(employee);
            }

        }



        // Persist a given/passed employee instance into the database. 
        [HttpPost]
        public ActionResult AddOrEdit(Employee employee)
        {
            if (employee != null)
            {
                if(employee.emp_id == 0)
                {
                    Task taskToDo = Task.Run(async () =>
                    {
                        await GlobalVariables.WebApi.PostAsJsonAsync("Employee", employee);
                    });

                    taskToDo.Wait();
                    TempData["SuccessMessage"] = "Record is Saved";
                }
                else
                {
                    Task taskToDo = Task.Run(async () =>
                    {
                        await GlobalVariables.WebApi.PutAsJsonAsync("Employee/" + employee.emp_id, employee);
                    });

                    taskToDo.Wait();
                    TempData["SuccessMessage"] = "Updated Successfully";
                }
            }
            else
            {
                TempData["FailureMessage"] = "Expected type Employee, null being passed";
            }

            return RedirectToAction("Index");
        }

        // Delete the employee from the database whose ID matches with the "id" passed in the parameter.
        public ActionResult Delete(int id)
        {

            HttpResponseMessage response = null;
            Task taskToDo = Task.Run(async () =>
            {
                response = await GlobalVariables.WebApi.DeleteAsync("Employee/" + id);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Deleted Successfully";
                }
                
            });
            
            taskToDo.Wait();
            return RedirectToAction("Index");
        }
    }
}
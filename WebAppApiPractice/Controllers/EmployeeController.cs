using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebAppApiPractice.Models;

namespace WebAppApiPractice.Controllers
{
    public class EmployeeController : ApiController
    {
        private TSQL2012Entities db = new TSQL2012Entities();

        // GET api/Employee
        // Returns all employees from the database belonging to the current Model
        public IQueryable<tblEmployee> GetEmployees()
        {
            return db.tblEmployees;
        }


        // GET api/Employee/2
        // Returns a single employee whose id matches with the parameter passed "id", otherwise returns the status/result as NOT FOUND
        public IHttpActionResult GetEmployee(int id)
        {
            tblEmployee employee = db.tblEmployees.Find(id);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        

        // POST api/Employee
        // Saves the employee passed in the parameter to the database and returns the http status code 201 Created
        public IHttpActionResult PostEmployee(tblEmployee employee)
        {
            db.tblEmployees.Add(employee);
            db.SaveChanges();
           return CreatedAtRoute("DefaultApi", new { id = employee.emp_id }, employee);
        }

        // PUT api/Employee/2
        // Updates the passed employee instance and persist changes in the database
        // Returns the http status code 204 No content - which means that request has been processed successfully and the response is intentionally blank
        public IHttpActionResult PutEmployee(int id, tblEmployee employee)
        {

            // Modifies the state of the passed employee instance
            db.Entry(employee).State = EntityState.Modified; 
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsEmployeeExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/Employee/2
        // Deletes the employee data from the database whose id matches with the "id" passed in the parameter. Persist changes after deletion.
        // Return the http status code 200 as well as the deleted employee instance
        public IHttpActionResult DeleteEmployee(int id)
        {
            tblEmployee emp = db.tblEmployees.Find(id);
            if(emp == null)
            {
                return NotFound();
            }

            db.tblEmployees.Remove(emp);
            db.SaveChanges();

            return Ok(emp);
        }

        // Disposes the resources so that memory can be reclaimed/reused for other future resources.
        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        // Checks whether the employee with the passed "id" exists in the database model
        // Returns True if it exists, otherwise False
        private bool IsEmployeeExist(int id)
        {
            return db.tblEmployees.Count(e => e.emp_id == id) > 0;
        }
    }
}

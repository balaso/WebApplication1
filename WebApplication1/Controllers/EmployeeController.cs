using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebApplication1.Constants;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        int employeeId = 6;
        IList<Employee> employees = new List<Employee>()
        {
            new Employee()
                {
                    EmployeeId = 1, EmployeeName = "Mukesh Kumar", Address = "New Delhi", Department = "IT"
                },
                new Employee()
                {
                    EmployeeId = 2, EmployeeName = "Banky Chamber", Address = "London", Department = "HR"
                },
                new Employee()
                {
                    EmployeeId = 3, EmployeeName = "Rahul Rathor", Address = "Laxmi Nagar", Department = "IT"
                },
                new Employee()
                {
                    EmployeeId = 4, EmployeeName = "YaduVeer Singh", Address = "Goa", Department = "Sales"
                },
                new Employee()
                {
                    EmployeeId = 5, EmployeeName = "Manish Sharma", Address = "New Delhi", Department = "HR"
                },
        };
        [HttpGet]
        public IList<Employee> GetAllEmployees()
        {
            //Return list of all employees  
            return employees;
        }
       [HttpGet]
        [Route("GetInfo")]
        public Employee GetEmployeeInfo(int id)
        {

            //Return a single employee detail  
            var employee = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return employee;
        }
        [HttpGet]
        [Route("{id}")]
        public Employee GetEmployeeDetails(int id)
        {
            //Return a single employee detail  
            var employee = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return employee;
        }


        [HttpPost]
        //public IList<Employee> addEmployee(Employee employee)
            public Employee addEmployee(Employee employee)
        {
            employee.EmployeeId = employeeId;
            int id = 4;
            employeeId++;
           this.employees.Add(employee);

            Console.WriteLine(" Employee: {0}" , employee);
            string url = RequestConstants.BaseUrl + $"employee/{id}";
           return (Employee)ApiHelper.MakeRequest(url, id, typeof(Employee));
           // return (IList<Employee>)(Employee)MakeRequest("https://localhost:44373/api/employee", employee, "GET", "application/json", typeof(IList<Employee>));
            //   return employee;
        }
        [HttpPut]
        public Employee updateEmployee(Employee e)
        {
            return e;
        }
        
    }
}

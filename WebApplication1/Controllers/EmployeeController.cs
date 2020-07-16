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
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
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
        public IList<Employee> GetAllEmployees()
        {
            //Return list of all employees  
            return employees;
        }

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
           // employees.Add(employee);

            Console.WriteLine(" Employee " + employee);
           return (Employee)MakeRequest("https://localhost:44373/api/employee/2", id, "GET", "application/json", typeof(Employee));
           // return (IList<Employee>)(Employee)MakeRequest("https://localhost:44373/api/employee", employee, "GET", "application/json", typeof(IList<Employee>));
            //   return employee;
        }
        [HttpPut]
        public Employee updateEmployee(Employee e)
        {
            return e;
        }
        public static object MakeRequest(string requestUrl, object JSONRequest, string JSONmethod, string JSONContentType, Type JSONResponseType)
        {

            try
            {
                HttpWebRequest request = WebRequest.Create( requestUrl) as HttpWebRequest;
                //WebRequest WR = WebRequest.Create(requestUrl);   
              //
                request.Method = JSONmethod;
                if (!String.IsNullOrEmpty(JSONContentType))
                {
                    request.ContentType = JSONContentType;
                }

                /*** add request body for POST and PUT method ***/
                if("POST".Equals(JSONmethod, StringComparison.OrdinalIgnoreCase) || "PUT".Equals(JSONmethod, StringComparison.OrdinalIgnoreCase))
                {
                    string sb = JsonConvert.SerializeObject(JSONRequest);
                    Byte[] bt = Encoding.UTF8.GetBytes(sb);
                    Stream st = request.GetRequestStream();
                    st.Write(bt, 0, bt.Length);
                    st.Close();
                }
                
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).", response.StatusCode,
                    response.StatusDescription));

                    // DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                    // object objResponse = JsonConvert.DeserializeObject();
                    Stream stream1 = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream1);
                    //string resp = response.Content.ReadAsStringAsync().Result; 
                    string strsb = sr.ReadToEnd();
                    object objResponse = JsonConvert.DeserializeObject(strsb, JSONResponseType); 
                    //JsonConvert.DeserializeObject<JSONResponseType>(strsb);

                    return objResponse;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}

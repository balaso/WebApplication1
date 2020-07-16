using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Employee
    {
        [JsonProperty(PropertyName = "employee_id")]
        public int EmployeeId
        {
            get;
            set;
        }
        [JsonProperty(PropertyName = "employee_name")]
        public string EmployeeName
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string Department
        {
            get;
            set;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using System.Text.Json.Serialization;

namespace scdProiect
{
    internal class EmployeeService
    {
        static HttpClient client = new HttpClient();

        public void createConnection()
        {
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = null;
            HttpResponseMessage response = client.GetAsync("api/employee").Result;
            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                employees = JsonSerializer.Deserialize<List<Employee>>(resultString);
                return employees;
            }
            return null;
        }

        public bool UpdateEmployee(int id, string name, string username, string email)
        {
            var data = new
            {
                id,
                name,
                username,
                email
            };

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            string apiUrl = $"api/employee/{id}";

            HttpResponseMessage response = client.PutAsync(apiUrl, jsonContent).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public bool AddEmployee(string name, string username, string email, string password)
        {
            var data = new
            {
                name,
                username,
                email,
                password
            };
            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("api/employee", jsonContent).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        public bool DeleteEmployee(int id)
        {
            string apiUrl = $"api/employee/{id}";

            HttpResponseMessage response = client.DeleteAsync(apiUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        public List<Employee> GetAllEmployeesByDepartment(int departmentId)
        {
            string apiUrl = $"api/employee/getAllEmployeesByDepartment/{departmentId}";

            HttpResponseMessage response = client.GetAsync(apiUrl).Result;

            List<Employee> employees = null;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                employees = JsonSerializer.Deserialize<List<Employee>>(resultString);
                return employees;
            }
            return null;
        }

        public bool AssignEmployeeToDepartment(int employeeId, int departmentId)
        {
            var data = new
            {
               departmentID=departmentId
            };

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            string apiUrl = $"api/employee/{employeeId}/assignToDepartment";

            HttpResponseMessage response = client.PutAsync(apiUrl, jsonContent).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}

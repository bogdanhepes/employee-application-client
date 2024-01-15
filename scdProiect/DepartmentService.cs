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
    internal class DepartmentService
    {
        static HttpClient client = new HttpClient();

        public void createConnection()
        {
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = null;
            HttpResponseMessage response = client.GetAsync("api/department").Result;
            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                departments = JsonSerializer.Deserialize<List<Department>>(resultString);
                return departments;
            }
            return null;
        }

        public bool UpdateDepartment(int departmentId, string description)
        {
            
            var data = new
            {
                departmentId,
               description,
            };

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            string apiUrl = $"api/department/{departmentId}";

            HttpResponseMessage response = client.PutAsync(apiUrl, jsonContent).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        public bool AddDepartment(string description, int managerId)
        {
            var data = new
            {
                description,
                managerId,
            };

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("api/department", jsonContent).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool DeleteDepartment(int departmentId)
        {
            string apiUrl = $"api/department/{departmentId}";

            HttpResponseMessage response = client.DeleteAsync(apiUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}

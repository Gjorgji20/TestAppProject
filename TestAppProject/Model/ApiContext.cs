using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestAppProject.Model
{
    public class ApiContext
    {
        public ApiContext()
        { }
        public async Task<List<Client>> GetAllClinets()
        {
            List<Client> clients = null;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7209/api/Client");

                if (response.IsSuccessStatusCode)
                {
                    string resultData = await response.Content.ReadAsStringAsync();

                    clients = JsonConvert.DeserializeObject<List<Client>>(resultData);

                }
                
            }
            return clients;
        }
        public async Task<int> InsetClients(Client c)
        {
            int returnvalue = -1;
            using (var client = new System.Net.Http.HttpClient())
            {

                string parameters = JsonConvert.SerializeObject(c);
                StringContent content = new StringContent(parameters, Encoding.UTF8, "application/json");
                var response = await client.PutAsync("https://localhost:7209/api/Client", content);
                                
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    returnvalue = JsonConvert.DeserializeObject<int>(jsonString);

                }
               
            }
            return returnvalue;
        }


    }
}

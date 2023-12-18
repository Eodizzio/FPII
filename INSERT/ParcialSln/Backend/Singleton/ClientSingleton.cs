using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks.Sources;

namespace Backend.Singleton
{
    public class ClientSingleton
    {
        private static ClientSingleton instance;
        private HttpClient client;

        public ClientSingleton()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7021");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static ClientSingleton GetInstance()
        {
            if(instance == null)
                instance = new ClientSingleton();
            return instance;
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await client.GetAsync(url);
            var result = string.Empty;
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<int> PostAsync(string url, string data)
        {
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url, content);
            int response = 0;
            if (result.IsSuccessStatusCode)
                response = await result.Content.ReadFromJsonAsync<int>();
            return response;
        }
    }
}

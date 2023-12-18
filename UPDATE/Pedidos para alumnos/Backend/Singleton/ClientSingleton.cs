using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Backend.Singleton
{
    public class ClientSingleton
    {
        private static ClientSingleton instance;
        private HttpClient client;

        public ClientSingleton()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7126");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static ClientSingleton GetInstance()
        {
            if (instance == null)
                instance = new ClientSingleton();
            return instance;
        }

        public async Task<string> GetAsync(string url)
        {
            var result = await client.GetAsync(url);
            var response = string.Empty;
            if (result.IsSuccessStatusCode)
                response = await result.Content.ReadAsStringAsync();
            return response;
        }

        public async Task<string> PutAsync(string url)
        {
            var result = await client.PutAsync(url, null);
            var response = string.Empty;
            if (result.IsSuccessStatusCode)
                response = "Ok";
            return response;
        }
    }
}

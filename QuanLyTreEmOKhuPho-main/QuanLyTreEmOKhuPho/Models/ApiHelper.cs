using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuanLyTreEmOKhuPho.Services
{
    public class ApiHelper
    {
        private readonly HttpClient _client;

        // Constructor nhận HttpClient từ bên ngoài
        public ApiHelper(HttpClient client)
        {
            _client = client;
        }

        // Hàm generic lấy dữ liệu từ API
        public async Task<List<T>> GetDataAsync<T>(string endpoint)
        {
            List<T> result = new List<T>();
            HttpResponseMessage response = await _client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<T>>(data);
            }

            return result;
        }
    }
}

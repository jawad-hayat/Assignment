using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class Utility
    {
        public static async Task<List<dynamic>> GetData(string url)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throw an exception if HTTP request was unsuccessful
            var responseContent = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var data = JsonSerializer.Deserialize<List<dynamic>>(responseContent, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
            return data;
        }
    }
}

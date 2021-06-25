using Firebase.Database;
using FoodOrderApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.Services
{
    class CategoryDataService
    {
        HttpClient client;

        string URL = "https://foodorderappapi.azurewebsites.net";
        public CategoryDataService()
        {
            
            client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };

            var authHeader = new AuthenticationHeaderValue("Bearer", (string)Application.Current.Properties["token"]);
            client.DefaultRequestHeaders.Authorization = authHeader;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var json = await client.GetStringAsync("api/Categories");
            var categories = JsonConvert.DeserializeObject<List<Category>>(json);

            return categories;
        }
    }
}

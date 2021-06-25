using Firebase.Database;
using FoodOrderApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.Services
{
    class FoodItemService
    {
        HttpClient client;

        string URL = "https://foodorderappapi.azurewebsites.net";
        public FoodItemService()
        {

            client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };

            var authHeader = new AuthenticationHeaderValue("Bearer", (string)Application.Current.Properties["token"]);
            client.DefaultRequestHeaders.Authorization = authHeader;
        }


        public async Task<List<FoodItem>> GetFoodItemsAsync()
        {
            var json = await client.GetStringAsync("api/FoodItems");
            var products = JsonConvert.DeserializeObject<List<FoodItem>>(json);

            return products;
        }

        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByCategoryAsync(int categoryID)
        {
            var foodItemsByCategory = new ObservableCollection<FoodItem>();
            var json = await client.GetStringAsync($"api/FoodItems/{categoryID}");
            var products = JsonConvert.DeserializeObject<List<FoodItem>>(json);
            foreach (var product in products)
            {
                foodItemsByCategory.Add(product);
            }
            return foodItemsByCategory;
        }

        public async Task<ObservableCollection<FoodItem>> GetLatestFoodItemsAsync()
        {
            var latestFoodItems = new ObservableCollection<FoodItem>();
            var products = (await GetFoodItemsAsync()).OrderByDescending(f => f.ProductID).Take(3);
            foreach (var product in products)
            {
                latestFoodItems.Add(product);
            }
            return latestFoodItems;
        }

        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByQueryAsync(string searchText)
        {
            var foodItemsByQuery = new ObservableCollection<FoodItem>();
            var products = (await GetFoodItemsAsync()).Where(p => p.Name.Contains(searchText)).ToList();
            foreach (var product in products)
            {
                foodItemsByQuery.Add(product);
            }
            return foodItemsByQuery;
        }
    }
}

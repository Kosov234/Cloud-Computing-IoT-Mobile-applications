using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.Services
{
    class OrderService
    {

        HttpClient client;

        string URL = "https://foodorderappapi.azurewebsites.net";

        CancellationTokenSource cts;
        public OrderService()
        {

            client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };

            var authHeader = new AuthenticationHeaderValue("Bearer", (string)Application.Current.Properties["token"]);
            client.DefaultRequestHeaders.Authorization = authHeader;
        }

        public async Task<string> PlaceOrderAsync()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            var data = cn.Table<CartItem>().ToList();

            var orderId = Guid.NewGuid().ToString();
            var uname = Preferences.Get("Username", "Guest");

            decimal totalCost = 0;

            foreach (var item in data)
            {
                OrderDetails od = new OrderDetails()
                {
                    OrderId = orderId,
                    OrderDetailId = Guid.NewGuid().ToString(),
                    ProductID = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                totalCost += item.Price * item.Quantity;
                var orderDetailsJson = JsonConvert.SerializeObject(od);
                var orderDetailsContent = new StringContent(orderDetailsJson, Encoding.UTF8, "application/json");
                await client.PostAsync("api/OrderDetails", orderDetailsContent);
            }

            var json = JsonConvert.SerializeObject(new Order()
            {
                OrderId = orderId,
                Username = uname,
                TotalCost = totalCost,
                Location = await GetLocationAsync()
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync("api/Orders", content);
            return orderId;
        }

        private async Task<string> GetLocationAsync()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);

            if (location != null)
            {
                string result = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
                return result;
            }

            return "No Location";
        }
    }
}
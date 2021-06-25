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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.Services
{
    class OrderHistoryService
    {
        HttpClient client;
        List<OrdersHistory> UserOrders;

        string URL = "https://foodorderappapi.azurewebsites.net";
        public OrderHistoryService()
        {

            client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };

            var authHeader = new AuthenticationHeaderValue("Bearer", (string)Application.Current.Properties["token"]);
            client.DefaultRequestHeaders.Authorization = authHeader;

            UserOrders = new List<OrdersHistory>();
        }

        public async Task<List<OrdersHistory>> GetOrderDetailsAsync()
        {
            var uname = Preferences.Get("Username", "Guest");

            var ordersJson = await client.GetStringAsync("api/Orders");
            var orders = JsonConvert.DeserializeObject<List<Order>>(ordersJson);

            orders = orders
                .Where(o => o.Username.Equals(uname))
                .Select(o => new Order
                {
                    OrderId = o.OrderId,
                    ReceiptId = o.ReceiptId,
                    TotalCost = o.TotalCost,
                    Username = o.Username
                }).ToList();

            foreach (var order in orders)
            {
                OrdersHistory uoh = new OrdersHistory();
                uoh.OrderId = order.OrderId;
               // uoh.ReceiptId = order.ReceiptId;
                uoh.TotalCost = order.TotalCost;

                var orderDetailsJson = await client.GetStringAsync($"api/OrderDetails/{order.OrderId}");
                var orderDetails = JsonConvert.DeserializeObject<IEnumerable<OrderDetails>>(orderDetailsJson);


                uoh.AddRange(orderDetails);

                UserOrders.Add(uoh);
            }

            return UserOrders;
        }
    }
}

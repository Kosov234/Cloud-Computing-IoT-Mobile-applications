using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.Helpers
{
    class AddFoodItemData
    {
        HttpClient client;

        string URL = "https://foodorderappapi.azurewebsites.net";

        public List<FoodItem> FoodItems { get; set; }


        public AddFoodItemData()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };

            var authHeader = new AuthenticationHeaderValue("bearer", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImE0NjcwMmI3LTk0NWItNGEwNS1hMDMyLTZiYjBmM2NmYjQ2YyIsInN1YiI6Imtva29zQGdtYWlsLmNvbSIsImVtYWlsIjoia29rb3NAZ21haWwuY29tIiwianRpIjoiZTdmMzRiMDItZGFkMi00Y2UwLTliYTEtN2I5MTg5ZTM5OWMxIiwibmJmIjoxNjI0Mzg3MzA5LCJleHAiOjE2MjQ0MDg5MDksImlhdCI6MTYyNDM4NzMwOX0.yiUnLPDRzfqCkmGH4o1nJsnRnWjHL8gnzj8RlGSSbIFfYBs6SgaYPhE-uA7Zc2tH7X_dJ7Vtj8_f338r3vjqyA");

            client.DefaultRequestHeaders.Authorization = authHeader;

            FoodItems = new List<FoodItem>()
            {
                new FoodItem
                {
                    ProductID = 1,
                    CategoryID = 1,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza 1",
                    Description = "Burger - Pizza - Breakfast",
                    Rating = "4.8",
                    RatingDetail = " (121 ratings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 2,
                    CategoryID = 1,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza Hub 2",
                    Description = "Burger - Pizza - Breakfast",
                    Rating = "4.8",
                    RatingDetail = " (121 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 3,
                    CategoryID = 1,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza Hub 3",
                    Description = "Burger - Pizza - Breakfast",
                    Rating = "4.8",
                    RatingDetail = " (121 ratings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 4,
                    CategoryID = 1,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza Hub 4",
                    Description = "Burger - Pizza - Breakfast",
                    Rating = "4.8",
                    RatingDetail = " (121 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 5,
                    CategoryID = 2,
                    ImageUrl = "MainPizza",
                    Name = "Pizza",
                    Description = "Pizza - Breakfast",
                    Rating = "4.4",
                    RatingDetail = " (120 ratings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 6,
                    CategoryID = 2,
                    ImageUrl = "MainPizza",
                    Name = "Pizza Hub 2",
                    Description = "Pizza Hub 2 - Breakfast",
                    Rating = "4.8",
                    RatingDetail = " (156 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 7,
                    CategoryID = 3,
                    ImageUrl = "MainDessert",
                    Name = "Ice Creams",
                    Description = "IceCream - Breakfast",
                    Rating = "4.4",
                    RatingDetail = " (120 ratings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 8,
                    CategoryID = 3,
                    ImageUrl = "MainDessert",
                    Name = "Cakes",
                    Description = "Cool Cakes - Breakfast",
                    Rating = "4.8",
                    RatingDetail = " (156 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 45
                }
            };
        }

        public async Task AddFoodItemAsync()
        {
            try
            {
                foreach (var item in FoodItems)
                {
                    var json = JsonConvert.SerializeObject(new FoodItem()
                    {
                        CategoryID = item.CategoryID,
                        ProductID = item.ProductID,
                        Description = item.Description,
                        HomeSelected = item.HomeSelected,
                        ImageUrl = item.ImageUrl,
                        Name = item.Name,
                        Price = item.Price,
                        Rating = item.Rating,
                        RatingDetail = item.RatingDetail
                    });

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("api/Categories", content);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}

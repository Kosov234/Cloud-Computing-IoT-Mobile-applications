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
    class AddCategoryData
    {
        public List<Category> Categories { get; set; }

        HttpClient client;

        string URL = "https://foodorderappapi.azurewebsites.net";
        public AddCategoryData()
        {
            client = new HttpClient { 
                BaseAddress = new Uri(URL)
            };

            var authHeader = new AuthenticationHeaderValue("bearer", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImE0NjcwMmI3LTk0NWItNGEwNS1hMDMyLTZiYjBmM2NmYjQ2YyIsInN1YiI6Imtva29zQGdtYWlsLmNvbSIsImVtYWlsIjoia29rb3NAZ21haWwuY29tIiwianRpIjoiZTdmMzRiMDItZGFkMi00Y2UwLTliYTEtN2I5MTg5ZTM5OWMxIiwibmJmIjoxNjI0Mzg3MzA5LCJleHAiOjE2MjQ0MDg5MDksImlhdCI6MTYyNDM4NzMwOX0.yiUnLPDRzfqCkmGH4o1nJsnRnWjHL8gnzj8RlGSSbIFfYBs6SgaYPhE-uA7Zc2tH7X_dJ7Vtj8_f338r3vjqyA");

            client.DefaultRequestHeaders.Authorization = authHeader;


            Categories = new List<Category>()
            {
                new Category(){
                    //CategoryID = 1,
                    CategoryName = "Burger",
                    CategoryPoster = "MainBurger",
                    ImageUrl = "Burger.png"
                },
                new Category(){
                    //CategoryID = 2,
                    CategoryName = "Pizza",
                    CategoryPoster = "MainPizza",
                    ImageUrl = "Pizza.png"
                },
                new Category(){
                    //CategoryID = 3,
                    CategoryName = "Desserts",
                    CategoryPoster = "MainDessert.png",
                    ImageUrl = "Dessert.png"
                },
                new Category(){
                    //CategoryID = 4,
                    CategoryName = "Veg Burger",
                    CategoryPoster = "MainBurger",
                    ImageUrl = "Burger.png"
                },
                new Category(){
                    //CategoryID = 5,
                    CategoryName = "Veg Pizza",
                    CategoryPoster = "MainPizza.png",
                    ImageUrl = "Pizza.png"
                },
                new Category(){
                    //CategoryID = 6,
                    CategoryName = "Cakes",
                    CategoryPoster = "MainDessert.png",
                    ImageUrl = "Dessert.png"
                }

            };
            
        }

        public async Task AddCategoriesAsync()
        {
            try
            {
                foreach(var category in Categories)
                {

                    var json = JsonConvert.SerializeObject(new Category()
                    {
                        CategoryName = category.CategoryName,
                        CategoryPoster = category.CategoryPoster,
                        ImageUrl = category.ImageUrl
                    });
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("api/Categories", content);
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}

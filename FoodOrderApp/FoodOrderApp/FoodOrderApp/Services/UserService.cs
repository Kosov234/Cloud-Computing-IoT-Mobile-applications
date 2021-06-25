using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.Services
{
    class UserService
    {

        HttpClient client;

        string URL = "https://foodorderappapi.azurewebsites.net";
        public UserService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var user = new User
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/AuthManagement/Register", content);

            if (response.IsSuccessStatusCode)
            {
                var registrationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(json);
                Application.Current.Properties["token"] = registrationResponse.Token;
                await Application.Current.SavePropertiesAsync();
                return true;
            }
                return false;
        }


        public async Task<bool> LoginUser(string email, string password)
        {
            var user = new User
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/AuthManagement/Login", content);

            if(response.IsSuccessStatusCode)
            {
                var loginResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await response.Content.ReadAsStringAsync());
                Application.Current.Properties["token"] = loginResponse.Token;
                return true;
            }

            return false;
        }
    }
}
